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
    }
}