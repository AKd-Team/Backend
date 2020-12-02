using System;
using System.Collections.Generic;
using System.Linq;
using Academic.Entities;
using Academic.Helpers;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;

namespace Academic.Services
{
    public interface IAdminService
         {
             Users Create(Users user, string password);
             public Admin CreateAdmin(Admin admin, string password);
             public Profesor CreateProfesor(Profesor profesor, string password);
             public Student CreateStudent(Student student, string password);

             IEnumerable<Users> GetAll();
         }
    public class AdminService : IAdminService
    {
        private academicContext _context;
        private readonly AppSettings _appSettings;
        public AdminService(IOptions<AppSettings> appSettings, academicContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public Student CreateStudent(Student student, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_context.Users.Any(x => x.Username == student.Username))
                throw new AppException("Username \"" + student.Username + "\" is already taken");
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            student.PHash = passwordHash;
            student.PSalt = passwordSalt;
            
            _context.Student.Add(student);
            _context.SaveChanges();
            return student;
        }
        public Profesor CreateProfesor(Profesor profesor, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_context.Users.Any(x => x.Username == profesor.Username))
                throw new AppException("Username \"" + profesor.Username + "\" is already taken");
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            profesor.PHash = passwordHash;
            profesor.PSalt = passwordSalt;
            
            _context.Profesor.Add(profesor);
            _context.SaveChanges();
            return profesor;
        }
        public Admin CreateAdmin(Admin admin, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_context.Users.Any(x => x.Username == admin.Username))
                throw new AppException("Username \"" + admin.Username + "\" is already taken");
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            admin.PHash = passwordHash;
            admin.PSalt = passwordSalt;
            
            _context.Admin.Add(admin);
            _context.SaveChanges();
            return admin;
        }
        public Users Create(Users user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_context.Users.Any(x => x.Username == user.Username))
                throw new AppException("Username \"" + user.Username + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PHash = passwordHash;
            user.PSalt = passwordSalt;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("empty password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public IEnumerable<Users> GetAll()
        {
            return _context.Users;
        }
    }
}