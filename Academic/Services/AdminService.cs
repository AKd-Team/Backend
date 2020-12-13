using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Academic.Entities;
using Academic.Helpers;
using Academic.Models;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;

namespace Academic.Services
{
    public interface IAdminService
         {
             Users Create(Users user, string password);
             public Admin CreateAdmin(Admin admin, string password);
             public Profesor CreateProfesor(Profesor profesor, string password);
             public Student CreateStudent(Student student, string password,string semigrupa,string specializare);
             public IEnumerable<Departament> GetDepartaments(int id);
             public IEnumerable<AdminFormSpec> GetFormSpec(int id);
             public IEnumerable<Regulament> GetRegulament(int idFacultate);
             public void ChangeRegula(UpdateRegula regula);
             public void CreateRegula(Regulament regula);
             public void DeleteRegula(int idRegula);

             IEnumerable<Users> GetAll();
         }
    public class AdminService : IAdminService
    {
        private academicContext _context;
        private readonly AppSettings _appSettings;
        public AdminService(IOptions<AppSettings> appSettings, academicContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public Student CreateStudent(Student student, string password,string semigrupa,string specializare)
        {
            var t = _context.Specializare.Where(x=>x.Cod=="MIR" ).ToList();
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_context.Users.Any(x => x.Username == student.Username))
                throw new AppException("Username \"" + student.Username + "\" is already taken");
            if (_context.Specializare.Where(x=>x.Nume == specializare).Count()==0)
                throw new AppException("Erori specializare");
            if (_context.Formatie.Where(x => x.SemiGrupa == semigrupa).Count()==0)
                throw new AppException("Erori semigrupa");
            
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            student.PHash = passwordHash;
            student.PSalt = passwordSalt;
            
            var r = _context.Formatie.Where(x => x.SemiGrupa == semigrupa).ToList().First().IdFormatie;
            var p =_context.Specializare.Where(x => x.Nume == specializare).ToList().First().IdSpecializare;
            student.IdFormatie = r;
            student.IdSpecializare = p;
            
            _context.Student.Add(student);
            _context.SaveChanges();
            return student;
        }
        public Profesor CreateProfesor(Profesor profesor, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_context.Users.Any(x => x.Username == profesor.Username))
                throw new AppException("Username \"" + profesor.Username + "\" is already taken");
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            profesor.PHash = passwordHash;
            profesor.PSalt = passwordSalt;
            
            _context.Profesor.Add(profesor);
            _context.SaveChanges();
            return profesor;
        }
        public Admin CreateAdmin(Admin admin, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_context.Users.Any(x => x.Username == admin.Username))
                throw new AppException("Username \"" + admin.Username + "\" is already taken");
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            admin.PHash = passwordHash;
            admin.PSalt = passwordSalt;
            
            _context.Admin.Add(admin);
            _context.SaveChanges();
            return admin;
        }
        public Users Create(Users user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_context.Users.Any(x => x.Username == user.Username))
                throw new AppException("Username \"" + user.Username + "\" is already taken");
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PHash = passwordHash;
            user.PSalt = passwordSalt;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("empty password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        /*
         * Desc: Returneaza toate departamentele unei facultati
         * In: id-ul unei facultati.
         * Out: Lista de departamente mult dorita.
         * Err: Nu am adaugat caz de eroare, inca!
         */
        public IEnumerable<Departament> GetDepartaments(int id)
        {
            return _context.Departament.Where(d=> d.IdFacultate == id).ToList();
        }

        /*
         * Desc: O varianta ineficienta pt gasirea tuturor specializarilor, grupelor si semigrupelor de la o facultate,
         * dar merge.
         * In: Id-ul unei facultati
         * Out: O lista care contine toate specializarile cu grupele si semigrupele sale
         * Err: Nu am pus caz de eroare, inca!
         */
        public IEnumerable<AdminFormSpec> GetFormSpec(int id)
        {
            var formSpec = new List<AdminFormSpec>();
            var spec = _context.Specializare.Where(s => s.IdFacultate == id).ToList();
            foreach (var s in spec)
            {
                var form = _context.Formatie.Where(f => f.IdSpecializare == s.IdSpecializare).ToList();
                var grupe = new List<string>();
                var semiGrupe = new List<string>();
                foreach (var f in form)
                {
                    if (!grupe.Contains(f.Grupa))
                        grupe.Add(f.Grupa);
                    semiGrupe.Add(f.SemiGrupa);
                }
                formSpec.Add(new AdminFormSpec(s.Nume, grupe, semiGrupe));
            }
            return formSpec;
        }

        /*
         * Desc: Returneaza o lista de reguli(din tabela Regulament) pe baza id-ului de facultate al unui admin
         * In: idFacultate - un int care reprezinta IdFacultate al unui admin
         * Out: reguli - lista de elemente de tip Regulament
         * Err: Nu exista caz de eroare
         */
        public IEnumerable<Regulament> GetRegulament(int idFacultate)
        {
            return _context.Regulament.Where(r => r.IdFacultate == idFacultate || r.IdFacultate == null)
                .OrderByDescending(r => r.IdFacultate).ToList();
        }

        /*
         * Desc: Schimba detaliile unei reguli(din tabela regula) din baza de date
         * In: Un model de tip UpdateRegula care contine IdRegula, Titlu si Continut
         * Out: Un mesaj de succes sau un mesaj de eroare
         * Err: Pentru cazul in care IdRegula nu exista deja in tabela Regula
         *      Pentru cazul in care exista deja o alta regula cu acel titlu sau cu acel text
         */
        public void ChangeRegula(UpdateRegula regula)
        {
            if(_context.Regulament.Any(r => r.Titlu == regula.Titlu && r.Continut == regula.Continut 
                                            && r.IdFacultate == regula.IdFacultate))
                throw new AppException("Deja exista o rgula cu acest titlu si continut la aceasta facultate");
            
            if (_context.Regulament.Count(r => r.IdRegulament == regula.IdRegula) == 0)
                throw new AppException("Nu exista aceasta regula");
            
            var reg = _context.Regulament.First(r => r.IdRegulament == regula.IdRegula);

            reg.Titlu = regula.Titlu;
            reg.Continut = regula.Continut;
            _context.SaveChanges();
        }

        /*
         * Desc: Adauga o regula noua in baza de date
         * In: Un model de tip AddRegula care contine titlu si continut
         * Out: Un mesaj de succes sau un mesaj de eroare
         * Err: In cazul in care o regula cu acelasi titlu sau acelasi text exista deja
         */
        public void CreateRegula(Regulament regula)
        {
            if(_context.Regulament.Any(r => (r.Titlu == regula.Titlu || r.Continut == regula.Continut) 
                                            && r.IdFacultate == regula.IdFacultate))
                throw new AppException("Deja exista o rgula cu acest titlu sau continut la aceasta facultate");
            
            _context.Regulament.Add(regula);
            _context.SaveChanges();
        }

        /*
         * Desc: Partea de service pentru stergerea unei reguli din tabela regulament
         * In: idRegula - un int ce reprezinta id-ul regulii care trebuie stearsa
         * Out:
         * Err: Daca regula cu acest id nu exista se va transmite mesajul de eroare "Nu exista aceasta regula."
         */
        public void DeleteRegula(int idRegula)
        {
            if(_context.Regulament.Count(r => r.IdRegulament == idRegula) == 0)
                throw new AppException("Nu exista aceasta regula.");
            var regulament = _context.Regulament.First(r => r.IdRegulament == idRegula);
            _context.Regulament.Remove(regulament);
            _context.SaveChanges();
        }

        public IEnumerable<Users> GetAll()
        {
            return _context.Users;
        }
    }
}