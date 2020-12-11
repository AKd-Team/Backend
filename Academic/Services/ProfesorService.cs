using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Academic.Entities;
using Academic.Helpers;
using Microsoft.Extensions.Options;

namespace Academic.Services
{
    public interface IProfesorService
    {
        Users GetById(int id);
        public IEnumerable<Orarmaterie> GetListMaterii(int IdProfesor);
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

        public IEnumerable<Orarmaterie> GetListMaterii(int IdProfesor)
        {
            //var listMaterii = _context.Orarmaterie.SingleOrDefault(o=>o.IdProfesor==IdProfesor);
            return _context.Orarmaterie.Where(o => (o.IdProfesor == IdProfesor)).ToList();
        }
    }
}