using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Academic.Entities;
using Academic.Helpers;
using Academic.Models;
using Microsoft.Extensions.Options;

namespace Academic.Services
{
    public interface IProfesorService
    {
        Users GetById(int id);
        public IEnumerable<MaterieNedetaliata> GetListMaterii(int IdProfesor);
        public IEnumerable<DateStudentPtProfesor> GetStudentiInscrisi(int idMaterie, int idProfesor);
        public IEnumerable<Sala> GetSali();
        public IEnumerable<OrarSali> GetOrarSali(int idSala);
        public RezultatEvaluare GetRezultateEvaluare(int idMaterie, int idProfesor);
        public void ProgExamen(Orarmaterie examen);
        
    }
    public class ProfesorService : IProfesorService
    {
        private academicContext _context;
        private readonly AppSettings _appSettings;

        public ProfesorService(IOptions<AppSettings> appSettings, academicContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public Users GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public IEnumerable<MaterieNedetaliata> GetListMaterii(int IdProfesor)
        {
            var orarmaterii = _context.Orarmaterie.Where(o => (o.IdProfesor == IdProfesor)).ToList();
            var list_materii = new HashSet<MaterieNedetaliata>();
            foreach (var o in orarmaterii)
            {
                var materie = _context.Materie.SingleOrDefault(m=> m.IdMaterie == o.IdMaterie);
                var obj = new MaterieNedetaliata(materie);
                if(!list_materii.Contains(obj))
                        list_materii.Add(new MaterieNedetaliata(materie));
            }

            return list_materii;
        }

        public IEnumerable<DateStudentPtProfesor> GetStudentiInscrisi(int idMaterie, int idProfesor)
        {
            var orarmaterie = _context.Orarmaterie.Where(o => (o.IdMaterie == idMaterie && o.IdProfesor == idProfesor)).ToList();
            var listIdFormatieSpecializare = new HashSet<Tuple<Int32, Int32>>();
            var StudentiDetaliati = new List<DateStudentPtProfesor>();
            foreach (var ora in orarmaterie)
            {
                var IdFormatieSpecializare = new Tuple<Int32, Int32>(ora.IdFormatie, ora.IdSpecializare);
                listIdFormatieSpecializare.Add(IdFormatieSpecializare);
            }

            foreach (var IdFormatieSpecializare in listIdFormatieSpecializare)
            {
                var list_studenti = _context.Student.Where(s =>
                    (s.IdFormatie == IdFormatieSpecializare.Item1 && s.IdSpecializare == IdFormatieSpecializare.Item2)).ToList();
                foreach (var student in list_studenti)
                {
                    var formatie = _context.Formatie.SingleOrDefault(f => f.IdFormatie == student.IdFormatie);
                    var specializare =
                        _context.Specializare.SingleOrDefault(s => s.IdSpecializare == student.IdSpecializare);
                    var faculta = _context.Facultate.SingleOrDefault(fac => fac.IdFacultate == specializare.IdFacultate);
                    StudentiDetaliati.Add(new DateStudentPtProfesor(student, formatie.Grupa, formatie.SemiGrupa, specializare.Nume, faculta.Nume));
                }

            }

            return StudentiDetaliati;
        }

        /* Desc: Partea de service pentru a returna o lista de sali
         * In: -
         * Out: sali - o lista de obiecte de tip sala
         * Err: -
         */
        public IEnumerable<Sala> GetSali()
        {
            return _context.Sala.ToList();
        }

        /*
         * Desc: Partea de service pentru gasirea orarului unei sali
         * In: idSala - un int ce reprezinta id-ul salii pt care cautam orarul
         * Out: orarSali - o lista de obiecte de tip OrarSali
         * Err: -
         */
        public IEnumerable<OrarSali> GetOrarSali(int idSala)
        {
            var orarSali = new List<OrarSali>();
            foreach (var orar in _context.Orarmaterie
                .Where(o => o.IdSala == idSala && o.Tip == "Examen" && o.Data != null).ToList())
            {
                var profesor = _context.Profesor.Find(orar.IdProfesor);
                var numeProfesor = profesor.Nume + " " + profesor.Prenume;
                var formatie = _context.Formatie.Find(orar.IdFormatie, orar.IdSpecializare);
                var specializare = _context.Specializare.Find(formatie.IdSpecializare);
                var oraInceput = orar.OraInceput.ToString();
                oraInceput = oraInceput.Substring(0, oraInceput.Length - 3);
                var oraSfarsit = orar.OraSfarsit.ToString();
                oraSfarsit = oraSfarsit.Substring(0, oraSfarsit.Length - 3);
                var data = orar.Data;

                var dataDetaliata = data.Value.ToString("yyyy-MM-dd");
                
                var conditieTitlu = false;
                
                foreach (var os in orarSali)
                {
                    if (specializare.Cod == os.CodSpecializare && oraInceput == os.OraInceput 
                                                               && oraSfarsit == os.OraSfarsit && dataDetaliata == os.Data)
                    {
                        os.Titlu = formatie.Grupa + " " + os.Titlu;
                        conditieTitlu = true;
                        break;
                    }
                }

                if (conditieTitlu) continue;
                
                var titlu = formatie.Grupa + " " + numeProfesor;
                orarSali.Add(new OrarSali(titlu, oraInceput, oraSfarsit, dataDetaliata, specializare.Cod));
            }
            return orarSali;
        }
        
        /*
         * Desc: Partea de service pt obtinerea rezultatelor unei evaluari
         * In: idMaterie - int
         *     idProfesor - int
         * Out: rezultate - un obiect de tip rezultate evalaure
         * Err: -
         */
        public RezultatEvaluare GetRezultateEvaluare(int idMaterie, int idProfesor)
        {
            var rezultate = new RezultatEvaluare();
            var review = _context.Review
                .Where(r => r.IdProfesor == idProfesor && r.IdMaterie == idMaterie && r.Nota != null)
                .GroupBy(g => g.IdCriteriu, r => r.Nota)
                .Select(g => new
                {
                    Criteriu = g.Key,
                    Media = g.Average()
                })
                .ToList();
            foreach (var r in review)
            {
                rezultate.AddElement(_context.Criteriu.Find(r.Criteriu).Descriere, r.Media);
            }
            return rezultate;
        }
        
 /*
         * Desc:
         * Input:idSala- int, dupa care rezervam sala
         * Output: nimic
         * ERR:
         */
        public void ProgExamen(Orarmaterie examen)
        {
            var idSala = examen.IdSala;
            var orarSala = GetOrarSali(idSala).ToList();
            var sali = GetSali();
            var ok = false;
            foreach (var sala in sali)
            {
                if (sala.IdSala == idSala)
                    ok = true;
            }

            if (ok == false)
            {
                throw new Exception("Nu exista sala cu asemenea id");
            }
            var data = examen.Data;
            var dataDetaliata = data?.ToString("yyyy-MM-dd");
            var condOra = true;
            /*
             *  x=examen.OraInceput
             * y=examen.OraSfarsit
             * a=ora.Inc
             * b=oraSf
             */
            foreach (var orar in orarSala)
            {
                var oraInc = TimeSpan.Parse(orar.OraInceput);
                var oraSf = TimeSpan.Parse(orar.OraSfarsit);
                if (TimeSpan.Compare(examen.OraInceput, oraInc) >= 0 &&
                    TimeSpan.Compare(examen.OraInceput, oraSf) < 0) //daca x apartine [a,b)
                {
                    condOra = false;
                    throw new Exception("Examenul nu poate sa inceapa in timp ce se desfasoara alt examen");
                }
                else if (TimeSpan.Compare(examen.OraSfarsit, oraInc) >= 0 &&
                         TimeSpan.Compare(examen.OraSfarsit, oraSf) <= 0) //daca y apartine [a,b]
                {
                    condOra = false;
                    throw new Exception("Examenul trebuie sa se termine inaintea altui examen rezervat");
                }
                else if (TimeSpan.Compare(examen.OraInceput, oraInc) < 0 &&
                         TimeSpan.Compare(examen.OraSfarsit, oraSf) > 0) //daca [a,b] inclus in [x,y]
                {
                    condOra = false;
                    throw new Exception("Exista un examen in acest inetrval orar");
                }
            }

            if (condOra)
            {
                _context.Orarmaterie.Add(examen);
                _context.SaveChanges();
            }
            
        }

    }
}