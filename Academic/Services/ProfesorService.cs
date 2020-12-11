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
        public IEnumerable<Materie> GetListMaterii(int IdProfesor);
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

        public IEnumerable<Materie> GetListMaterii(int IdProfesor)
        {
            var orarmaterii = _context.Orarmaterie.Where(o => (o.IdProfesor == IdProfesor)).ToList();
            var list_materii = new HashSet<Materie>();
            foreach (var o in orarmaterii)
            {
                var materie = _context.Materie.Find(o.IdMaterie);
                list_materii.Add(materie);
            }

            return list_materii;
        }

        /*public IEnumerable<DateStudentPtProfesor> GetStudentiInscrisi(int idMaterie, int idProfesor)
        {
            var orarmaterie = _context.Orarmaterie.Where(o => (o.IdMaterie == idMaterie && o.IdProfesor == idProfesor)).ToList();
            var listIdFormatieSpecializare = new HashSet<Tuple<Int32, Int32>>();
            //var list_studenti = new List<Student>();
            foreach (var ora in orarmaterie)
            {
                var IdFormatieSpecializare = new Tuple<Int32, Int32>(ora.IdFormatie, ora.IdSpecializare);
                listIdFormatieSpecializare.Add(IdFormatieSpecializare);
            }

            foreach (var IdFormatieSpecializare in listIdFormatieSpecializare)
            {
                var list_studenti = _context.Student.SingleOrDefault(s =>
                    (s.IdFormatie == IdFormatieSpecializare.Item1 && s.IdSpecializare == IdFormatieSpecializare.Item2));

            }
        }*/
    }
}