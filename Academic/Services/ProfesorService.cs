using Academic.Entities;
using Academic.Helpers;
using Microsoft.Extensions.Options;

namespace Academic.Services
{
    public interface IProfesorService
    {
        Users GetById(int id);
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
    }
}