using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Academic.Entities;
using Academic.Helpers;
using Academic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Academic.Services
{
    public interface IStudentService
    {
        Users GetById(int id);
        Profesori GetByTeacherId(int id);
        IEnumerable<Profesori> GetAllTeachers();
        //Student GetStudentById(int id);
        StudentDetaliat GetStudentById(int id);
        IEnumerable<FacultatiCuDepartamente> GetFacultati();
        IEnumerable<Regulament> GetRegulament(int id);
        IEnumerable<OrarPersonalizat> GetOrar(int idStudent);
    }

    public class StudentService : IStudentService
    {

        private academicContext _context;
        private readonly AppSettings _appSettings;

        public StudentService(IOptions<AppSettings> appSettings, academicContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public Users GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public Profesori GetByTeacherId(int id)
        { 
            var p = _context.Profesor.Find(id);
            var dep = _context.Departament.SingleOrDefault(d => d.IdDepartament == p.IdDepartament);
            var fac = _context.Facultate.SingleOrDefault(f => f.IdFacultate == dep.IdFacultate);
            return new Profesori(p, dep.Nume, fac.Nume);
        }

        public IEnumerable<Profesori> GetAllTeachers()
        {
            if (_context.Profesor != null)
            {
                var list_prof = new List<Profesori>();
                foreach (var p in _context.Profesor.ToList())
                {
                    var dep = _context.Departament.SingleOrDefault(d => d.IdDepartament == p.IdDepartament);
                    var fac = _context.Facultate.SingleOrDefault(f => f.IdFacultate == dep.IdFacultate);
                    list_prof.Add(new Profesori(p,dep.Nume,fac.Nume));
                }

                return list_prof;
            }
            throw new Exception("Nu exista profesori!!!");
        }

        public StudentDetaliat GetStudentById(int id)
        {
            var student = _context.Student.Find(id);
            if (student != null)
            {
                var formatie = _context.Formatie.SingleOrDefault(f => f.IdFormatie == student.IdFormatie);
                var specializare =
                    _context.Specializare.SingleOrDefault(s => s.IdSpecializare == student.IdSpecializare);
                var faculta = _context.Facultate.SingleOrDefault(fac => fac.IdFacultate == specializare.IdFacultate);
                return new StudentDetaliat(student, formatie.Grupa, formatie.SemiGrupa, formatie.AnStudiu, faculta.Nume, specializare.Nivel, specializare.Nume);
            }
            else
            {
                throw new Exception("Nu exista student cu acest id!");
            }
        }
       

        /*
         * Descriere: O functie, nu tocmai eficienta, care returneaza o lista de FacultatiCuDepartamente
         * In: Nu trebuie date de intrare
         * Out: listFac - IEnumerable<FacultatiCuDepartamente>
         * Err: Nu am pus caz de eroare, inca!
         */
        public IEnumerable<FacultatiCuDepartamente> GetFacultati()
        {
            var listFac = new List<FacultatiCuDepartamente>();
            foreach (var f in _context.Facultate.ToList())
            {
                var dep = new List<string>();
                foreach (var d in _context.Departament.ToList())
                {
                    if (d.IdFacultate == f.IdFacultate)
                        dep.Add(d.Nume);
                }
                listFac.Add(new FacultatiCuDepartamente(f.Nume, dep));
            }
            return listFac;
        }

        /*
         * Desc: Aceasta functie cauta toate regulamentele unei facultati pe baza unui id de specializare
         * In: id - int - reprezentand id specializarii studentului
         * Out: O lista cu regulamentele facultatii
         */
        public IEnumerable<Regulament> GetRegulament(int id)
        {
            var spec = _context.Specializare.FirstOrDefault(s => s.IdSpecializare == id);
            if (spec != null) //Verifica existenta specializarii respective
            {
                if (spec.IdFacultate != null)
                {
                    var idFac = spec.IdFacultate.Value;
                    
                    return _context.Regulament.Where(r => (r.IdFacultate == idFac || r.IdFacultate == null))
                        .OrderByDescending(r => r.IdFacultate).ToList();
                }
            }
            //Pt cazul in care nu exista specializare
            return _context.Regulament.Where(r => r.IdFacultate == null);
        }

        /*
         * Desc: Partea de service pentru afisarea orarului
         * In: idStudent - un int care reprezinta id-ul studentului pentru care afisam orarul
         * Out: orar - o lista cu elemente de tip orar personalizat
         * Err:
         */
        public IEnumerable<OrarPersonalizat> GetOrar(int idStudent)
        {
            var orarListat = new List<OrarPersonalizat>();
            
            foreach (var detaliuContract in _context.Detaliucontract
                .Where(dc => dc.IdStudent == idStudent).ToList())
            {
                var numeMaterie = _context.Materie.Find(detaliuContract.IdMaterie).Nume;
                
                foreach (var orar in _context.Orarmaterie
                    .Where(o => o.IdMaterie == detaliuContract.IdMaterie).ToList())
                {
                    var numeSala = _context.Sala.Find(orar.IdSala).Nume;
                    var numeProfesor = _context.Profesor.Find(orar.IdProfesor).Nume;
                    var titlu = numeMaterie + System.Environment.NewLine + orar.Tip;
                    var formatie = _context.Formatie.Find(orar.IdFormatie, orar.IdSpecializare);
                    var grupaSemigrupa = formatie.Grupa + System.Environment.NewLine + formatie.SemiGrupa;
                    var oraInceput = orar.OraInceput.ToString();
                    oraInceput = oraInceput.Substring(0, oraInceput.Length - 3);
                    var oraSfarsit = orar.OraSfarsit.ToString();
                    oraSfarsit = oraSfarsit.Substring(0, oraSfarsit.Length - 3);
                    orarListat.Add(new OrarPersonalizat(titlu, oraInceput, oraSfarsit, 
                        orar.ZiuaSaptamanii, grupaSemigrupa, numeProfesor, numeSala, orar.Frecventa));
                }
            }
            return orarListat;
        }
    }
}