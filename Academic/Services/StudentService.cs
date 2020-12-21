using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using Academic.Entities;
using Academic.Helpers;
using Academic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
        IEnumerable<OrarExamen> GetExamene(int idStudent);
        IEnumerable<OptiuniReview> GetOptiuniReview(int idStudent);
        IEnumerable<ProfesorNedetaliat> GetProfesoriNedetaliati(int idMaterie, string tip);
        bool ExistentaEvaluare(ReviewComplet rc);
        void AdaugareReview(ReviewComplet rc);
        IEnumerable<Criteriu> GetCriterii();
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
                    .Where(o => o.IdMaterie == detaliuContract.IdMaterie && o.Tip != "Examen").ToList())
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

        /*
         * Desc: Partea de service pentru functia care returneaza orarul de examene pt un student dat
         * In: id-ul studentului
         * Out: orarListat - o lista de obiecte de tip OrarExamen, in care atributele obiectelor o sa fie definite astfel
         *     titlu - un string ce contine: numele materiei, numele profesorului, sala de examen si grupa care are examenul
         *     !!!!Pt examene in baza de date se va folosi o regula de exceptie, in care o sa fie notate grupele fara semigrupa
         *     oraInceput - ora de inceput a examenului
         *     oraSfarsit - ora de sfarsit a examenului
         *     data - data in care are loc examenul
         * Err: -
         */
        public IEnumerable<OrarExamen> GetExamene(int idStudent)
        {
            var orarListat = new List<OrarExamen>();
            
            foreach (var detaliuContract in _context.Detaliucontract
                .Where(dc => dc.IdStudent == idStudent).ToList())
            {
                var numeMaterie = _context.Materie.Find(detaliuContract.IdMaterie).Nume;

                foreach (var orar in _context.Orarmaterie
                    .Where(o => o.IdMaterie == detaliuContract.IdMaterie && o.Tip == "Examen").ToList())
                {
                    if (orar.Data == null) continue;
                    var data = orar.Data.Value.ToString("yyyy-MM-dd");
                    var oraInceput = orar.OraInceput.ToString();
                    oraInceput = oraInceput.Substring(0, oraInceput.Length - 3);
                    var oraSfarsit = orar.OraSfarsit.ToString();
                    oraSfarsit = oraSfarsit.Substring(0, oraSfarsit.Length - 3);
                    
                    //formare titlu
                    var numeSala = _context.Sala.Find(orar.IdSala).Nume;
                    var profesor = _context.Profesor.Find(orar.IdProfesor);
                    var numeProfesor = profesor.Nume + " " + profesor.Prenume;
                    var formatie = _context.Formatie.Find(orar.IdFormatie, orar.IdSpecializare);
                    
                    var titlu = numeMaterie + ", " + numeProfesor + ", " + numeSala + ", " + formatie.Grupa;

                    orarListat.Add(new OrarExamen(titlu, oraInceput, oraSfarsit, data));
                }
            }
            return orarListat;
        }
        
        /*
         * Desc: Partea de service pt transmiterea optiunilor de evaluare pt profesor
         * In: idStudent - int; reprezinta id-ul unui student
         * Out: optReview - o lista de obiecte de tip OptiuniReview
         * Err: -
         */
        public IEnumerable<OptiuniReview> GetOptiuniReview(int idStudent)
        {
            var optReview = new List<OptiuniReview>();
            var anMaxim = _context.Detaliucontract.Where(dc => dc.IdStudent == idStudent)
                .Max(dc => dc.AnDeStudiu);
            foreach (var detaliu in _context.Detaliucontract.Where(dc => dc.IdStudent == idStudent 
                                                                         && dc.AnDeStudiu == anMaxim).ToList())
            {
                var idMaterie = detaliu.IdMaterie;
                var numeMaterie = _context.Materie.Find(idMaterie).Nume;
                var anCalendaristic = detaliu.AnCalendaristic;
                var curs = false;
                var seminar = false;
                var laborator = false;
                foreach (var orar in _context.Orarmaterie.Where(o => o.IdMaterie == idMaterie).ToList())
                {
                    var tip = orar.Tip;
                    switch (tip)
                    {
                        case "Curs":
                            curs = true;
                            break;
                        case "Seminar":
                            seminar = true;
                            break;
                        case "Laborator":
                            laborator = true;
                            break;
                    }
                }
                optReview.Add(new OptiuniReview(idMaterie, numeMaterie, anMaxim, anCalendaristic, curs, seminar, laborator));
            }
            return optReview;
        }

        /*
         * Desc: Partea de service care returneaza profesorii de la o materie de un anumite tip
         * In: idMaterie - int
         *     tip - string, tipul activitatii
         * Out: profesori - o lista de obiecte de tip ProfesoriNedetaliati
         * Err: -
         */
        public IEnumerable<ProfesorNedetaliat> GetProfesoriNedetaliati(int idMaterie, string tip)
        {
            var profesori = new List<ProfesorNedetaliat>();
            
            foreach (var orar in _context.Orarmaterie.Where(o => o.IdMaterie == idMaterie 
                                                                 && o.Tip == tip).ToList())
            {
                var profesor = _context.Profesor.Find(orar.IdProfesor);
                var nume = profesor.Grad + " " + profesor.Nume + " " + profesor.Prenume;
                var profesorNedetaliat = new ProfesorNedetaliat(profesor.IdUser,nume);
                if (!profesori.Contains(profesorNedetaliat))
                    profesori.Add(profesorNedetaliat);
            }
            return profesori;
        }
        
        /*
         * Desc: Partea de service pentru existenta unei anumite evaluari 
         * In: rc - un obiect de tip ReviewComplet
         * Out: true sau false
         * Err: -
         */
        public bool ExistentaEvaluare(ReviewComplet rc)
        {
            if (_context.Review.Any(r => r.IdStudent == rc.IdStudent && r.IdProfesor == rc.IdProfesor 
                                                                     && r.IdMaterie == rc.IdMaterie 
                                                                     && r.AnDeStudiu == rc.AnDeStudiu 
                                                                     && r.AnCaledaristic == rc.AnCalendaristic))
                return true;
            return false;
        }

        /*
         * Desc: Partea de service pe a transmite datele de evaluare facute asupra unui profesor. 
         * In: rc - un obiect de tip AdaugareReview
         * Out: -
         * Err: Daca numarul de note nu e egal cu numarul de criterii
         */
        public void AdaugareReview(ReviewComplet rc)
        {
            if (rc.Criterii.Count != rc.Note.Count)
                throw new AppException("Numarul de note difera fata de cel de criterii");
            for (var i = 0; i < rc.Criterii.Count; i++)
            {
                var review = new Review
                {
                    IdProfesor = rc.IdProfesor,
                    IdMaterie = rc.IdMaterie,
                    IdCriteriu = rc.Criterii.ElementAt(i),
                    IdStudent = rc.IdStudent,
                    AnDeStudiu = rc.AnDeStudiu,
                    AnCaledaristic = rc.AnCalendaristic,
                    Nota = rc.Note.ElementAt(i)
                };
                _context.Review.Add(review);
                _context.SaveChanges();
            }
        }
        
        /*
         * Desc: Partea de service pt a transmite lista de criterii pentru evaluarea unui profesor
         * In: -
         * Out: o lista de criterii
         * Err: -
         */
        public IEnumerable<Criteriu> GetCriterii()
        {
            return _context.Criteriu.ToList();
        }
        
    }
}