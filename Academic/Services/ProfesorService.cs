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
        public void AdaugareNote(AdaugareNota an);
        public IEnumerable<StudentFaraNota> GetStudentFaraNota(int id_materie);
        public IEnumerable<ListaFormatii> GetFormatii();
        public IEnumerable<NotaStudenti> GetStatisticiMaterie(int idMaterie);
        public IEnumerable<OrarPersonalizatProfesor> GetOrar(int idProfesor);
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
 * Input: orarmaterie ora1 si orarmaterie ora2
 * output: true daca sunt egale si false daca sunt false
 * error: fara erori
 */
        public bool eqOrarmater(Orarmaterie ora1, Orarmaterie ora2)
        {
            if (Equals(ora1.Data, ora2.Data) && Equals(ora1.IdFormatie, ora2.IdFormatie) &&
                Equals(ora1.IdMaterie, ora2.IdMaterie) && Equals(ora1.IdProfesor, ora2.IdProfesor) &&
                Equals(ora1.IdSala, ora2.IdSala) && Equals(ora1.OraInceput, ora2.OraInceput) && 
                Equals(ora1.OraSfarsit,ora2.OraSfarsit) && Equals(ora1.IdSpecializare, ora2.IdSpecializare))
            {
                return true;
            }

            return false;
        }
        /*
         * Desc: 
         * Input:Orarmaterie
         * Output: nimic
         * ERR:-daca id-ul salii nu exista
         *     -daca intervalul examenului se intersecteaza cu un alt examen
         *     
         */
        public void ProgExamen(Orarmaterie examen)
        {
            foreach (var ora in _context.Orarmaterie.ToList())
            {
                if (eqOrarmater(examen,ora)==true)
                {
                    throw new Exception("Examenul este deja programat");
                }
            }
            var idSala = examen.IdSala;
            var orarSala = GetOrarSali(idSala).ToList();
            var sali = GetSali();
            var ok = false;
            var numesala = _context.Sala.Find(idSala).Nume;
            int idSpec = _context.Formatie.SingleOrDefault(formatie => formatie.IdFormatie == examen.IdFormatie)
                .IdSpecializare;
            foreach (var sala in sali)
            {
                if (sala.IdSala == idSala)
                {
                    ok = true;
                }
            }
            if (ok == false)
            {
                throw new Exception("Nu exista sala cu asemenea id");
            }
            
            var data = examen.Data;
            var dataDetaliata = data?.ToString("yyyy-MM-dd");
            /*
             *  x=examen.OraInceput
             * y=examen.OraSfarsit
             * a=ora.Inc
             * b=oraSf
             */
            if(String.CompareOrdinal(numesala,"Online")!= 0)
            {
                foreach (var orar in orarSala)
            {
                var oraInc = TimeSpan.Parse(orar.OraInceput);
                var oraSf = TimeSpan.Parse(orar.OraSfarsit);
                if (TimeSpan.Compare(examen.OraInceput, oraInc) >= 0 &&
                    TimeSpan.Compare(examen.OraInceput, oraSf) < 0) //daca x apartine [a,b) si sala nu este 1(cea online)
                {
                    throw new Exception("Examenul nu poate sa inceapa in timp ce se desfasoara alt examen");
                }
                if (TimeSpan.Compare(examen.OraSfarsit, oraInc) >= 0 &&
                         TimeSpan.Compare(examen.OraSfarsit, oraSf) <= 0 ) //daca y apartine [a,b]
                {
                    throw new Exception("Examenul trebuie sa se termine inaintea altui examen rezervat");
                }
                if (TimeSpan.Compare(examen.OraInceput, oraInc) < 0 &&
                         TimeSpan.Compare(examen.OraSfarsit, oraSf) > 0) //daca [a,b] inclus in [x,y]
                {
                    throw new Exception("Exista un examen in acest inetrval orar");
                }
            }}

            examen.IdSpecializare = idSpec;
            _context.Orarmaterie.Add(examen);
            _context.SaveChanges();
        }

        public void AdaugareNote(AdaugareNota an)
        {
            var anDeStudiu = _context.Detaliucontract.Where(dc => dc.IdStudent == an.idStudent 
                                                                  && dc.IdMaterie==an.idMaterie)
                .Max(dc => dc.AnDeStudiu);
            var anCalendaristic = _context.Detaliucontract.FirstOrDefault(dc => dc.IdStudent == an.idStudent
                                                                         && dc.AnDeStudiu == anDeStudiu)?.AnCalendaristic;

            var detaliucontract = _context.Detaliucontract.First(dc => dc.IdStudent == an.idStudent
                                                            && dc.IdMaterie == an.idMaterie 
                                                            && dc.AnDeStudiu==anDeStudiu 
                                                            && dc.AnCalendaristic==anCalendaristic);
            if (an.restanta)
            {
                detaliucontract.NotaRestanta = an.nota;
                if (an.nota >= 5)
                {
                    detaliucontract.DataPromovarii = detaliucontract.DataRestanta;
                    detaliucontract.Promovata = true;
                }
            }
            else
            {
                detaliucontract.Nota = an.nota;
                if (an.nota >= 5)
                {
                    detaliucontract.DataPromovarii = detaliucontract.DataExamen;
                    detaliucontract.Promovata = true;
                }
            }


            _context.SaveChanges();
        }
        
        /*
         * Desc: O functie care primeste id-ul materiei si returneaza o lista cu studentii care nu au nota la materia respectiva
         * Input: Integer care reprezinta id_ul Materiei
         * Output: Un list de StudentFaraNota
         * Error: -
         */
        public IEnumerable<StudentFaraNota> GetStudentFaraNota(int id_materie)
        {

            var stdFaraNota = new List<StudentFaraNota>();
            var listaStudenti = _context.Student.ToList();
            var detaliuContract = _context.Detaliucontract.Where(dc => dc.IdMaterie == id_materie).ToList();
            foreach (var ctr in detaliuContract)
            {
                foreach (var student in listaStudenti)
                {
                    if (student.IdUser == ctr.IdStudent && ctr.Nota == null && ctr.NotaRestanta == null)
                    {
                        var nume = student.Nume;
                        var prenume = student.Prenume;
                        var grupa = _context.Formatie.SingleOrDefault(fr => fr.IdFormatie == student.IdFormatie)
                            ?.Grupa;
                        var specializare = _context.Specializare
                            .SingleOrDefault(spe => spe.IdSpecializare == student.IdSpecializare)?.Nume;
                        var idStud = student.IdUser;

                        stdFaraNota.Add(new StudentFaraNota(nume, prenume, grupa, specializare, idStud));
                    }
                }
            }

            return stdFaraNota;
        }
    
        
        /*
         * un mic get pentru fromatii
         */
        public IEnumerable<ListaFormatii> GetFormatii()
        {
            var listform = new List<ListaFormatii>();
            foreach (var formatie in _context.Formatie.ToList())
            {
                var id = formatie.IdFormatie;
                var grupa = formatie.Grupa;
                listform.Add(new ListaFormatii(id, grupa));
            }

            return listform;
        }
        
        public IEnumerable<NotaStudenti> GetStatisticiMaterie(int idMaterie)
        {
            StatisticiMaterie statistici = new StatisticiMaterie();
            var dataCurenta = DateTime.Now;
            int anUniversitarInceput = dataCurenta.Year;
            if (dataCurenta.Month < 10)
            {
                anUniversitarInceput--;
            }
            string anCalendaristic = anUniversitarInceput.ToString() + "-" + (anUniversitarInceput + 1).ToString();
            
            var detaliuContracte = _context.Detaliucontract.Where(d =>
                d.IdMaterie == idMaterie && d.AnCalendaristic.Equals(anCalendaristic)).ToList();
            foreach (var det in detaliuContracte)
            {
                int nota = 0;
                if (det.NotaRestanta != null)
                    nota = Math.Max((byte)det.Nota, (byte)det.NotaRestanta);
                else if (det.Nota != null)
                    nota = (int)det.Nota;
                if (nota != 0)
                {
                    
                }
                statistici.updateNrStudenti(nota);
            }
            return statistici.StatisticiNote;
            
        }

        public IEnumerable<OrarPersonalizatProfesor> GetOrar(int idProfesor)
        {
            var orarListat = new List<OrarPersonalizatProfesor>();
            foreach (var orar in _context.Orarmaterie
                .Where(o => o.IdProfesor == idProfesor && o.Tip != "Examen").ToList())
            {
                var numeMaterie = _context.Materie.Find(orar.IdMaterie).Nume;
                var oraInceput = orar.OraInceput.ToString();
                oraInceput = oraInceput.Substring(0, oraInceput.Length - 3);
                var oraSfarsit = orar.OraSfarsit.ToString();
                oraSfarsit = oraSfarsit.Substring(0, oraSfarsit.Length - 3);
                var formatie = _context.Formatie.Find(orar.IdFormatie, orar.IdSpecializare);
                var grupaSemigrupa = formatie.Grupa + " " + formatie.SemiGrupa;
                var numeSala = _context.Sala.Find(orar.IdSala).Nume;
                orarListat.Add(new OrarPersonalizatProfesor(numeMaterie, oraInceput, oraSfarsit, 
                    orar.ZiuaSaptamanii, grupaSemigrupa, numeSala, orar.Frecventa));
            }

            return orarListat;
        }
    }
}