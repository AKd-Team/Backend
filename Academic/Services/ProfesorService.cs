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
            var orarmaterii = _context.Orarmaterie.Where(o => (o.IdProfesor == IdProfesor && o.Tip.Equals("Curs"))).ToList();
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

        /*public IEnumerable<DateStudentPtProfesor> GetStudentiInscrisi(int idMaterie, int idProfesor)
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
        }*/
        

        public IEnumerable<DateStudentPtProfesor> GetStudentiInscrisi(int idMaterie, int idProfesor)
        {
            var dataCurenta = DateTime.Now;
            int anUniversitarInceput = dataCurenta.Year;
            if (dataCurenta.Month < 10)
            {
                anUniversitarInceput--;
            }
            string anCalendaristic = anUniversitarInceput.ToString() + "-" + (anUniversitarInceput + 1).ToString();

            var StudentiDetaliati = new List<DateStudentPtProfesor>();
            var detaliuContractList = _context.Detaliucontract.Where(det =>
                det.IdMaterie == idMaterie && det.AnCalendaristic.Equals(anCalendaristic)).ToList();
            foreach (var detaliuContract in detaliuContractList)
            {
                int idStudent = detaliuContract.IdStudent;
                var student = _context.Student.Find(idStudent);
                var formatie = _context.Formatie.SingleOrDefault(f => f.IdFormatie == student.IdFormatie);
                var specializare =
                    _context.Specializare.SingleOrDefault(s => s.IdSpecializare == student.IdSpecializare);
                var faculta = _context.Facultate.SingleOrDefault(fac => fac.IdFacultate == specializare.IdFacultate);
                StudentiDetaliati.Add(new DateStudentPtProfesor(student, formatie.Grupa, formatie.SemiGrupa, specializare.Nume, faculta.Nume));
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
    }
}