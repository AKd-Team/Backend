using Academic.Entities;
using Academic.Helpers;
using Microsoft.Extensions.Options;

namespace Academic.Services
{
    public interface IStudentService
    {
        Users GetById(int id);
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
    }
}