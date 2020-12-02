using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Academic.Entities;
using Academic.Helpers;
using Academic.Models;

namespace Academic.Services
{
    public interface IUsersService
    {
        LoginResponse Login(string username, string password);
        LoginStudent LoginStudent(string username, string token);
        LoginAdmin LoginAdmin(string username, string token);
        LoginProfesor LoginProfesor(string username, string token);
        IEnumerable<Users> GetAll();
        Users GetById(int id);
        Users Create(Users user, string password);
        public Admin CreateAdmin(Admin admin, string password);
        void Delete(int id);
        //void Update(Users user, string password = null);
    }
    public class UserService : IUsersService
    {

        private academicContext _context;
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, academicContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        //Serviciul de login
        public LoginResponse Login(string Username, string Password)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == Username);

            // return null if user not found
            if (user == null) return null;

            if (!VerifyPasswordHash(Password, user.PHash, user.PSalt))
                return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new LoginResponse(user,token);

        }

        public LoginProfesor LoginProfesor(string Username, string token)
        {
            var profesor = _context.Profesor.SingleOrDefault(x => x.Username == Username);
            if (profesor == null)
                return null;
            return new LoginProfesor(profesor, token);

        }
        public LoginAdmin LoginAdmin(string Username, string token)
        {
            var admin = _context.Admin.SingleOrDefault(x => x.Username == Username);
            if (admin == null)
                return null;
            return new LoginAdmin(admin, token);

        }
        public LoginStudent LoginStudent(string Username, string token)
        {
            var student = _context.Student.SingleOrDefault(x => x.Username == Username);
            if (student == null)
                return null;
            return new LoginStudent(student, token);
        }
        /*
        public void Update(Users userParam, string password = null)
        {
            var user = _context.Users.Find(userParam.Id);

            if (user == null)
                throw new AppException("User not found");

            // update username if it has changed
            if (!string.IsNullOrWhiteSpace(userParam.UserName) && userParam.UserName != user.UserName)
            {
                // throw error if the new username is already taken
                if (_context.Users.Any(x => x.UserName == userParam.UserName))
                    throw new AppException("Username " + userParam.UserName + " is already taken");

                user.UserName = userParam.UserName;
            }

            // update user properties if provided
            if (!string.IsNullOrWhiteSpace(userParam.FirstName))
                user.FirstName = userParam.FirstName;

            if (!string.IsNullOrWhiteSpace(userParam.LastName))
                user.LastName = userParam.LastName;

            // update password if provided
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.Users.Update(user);
            _context.SaveChanges();
        }
        */
        //serviciul de delete
        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
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
        //serviciul ce returneazza toti userii
        public IEnumerable<Users> GetAll()
        {
            return _context.Users;
        }
        //serviciul ce returneaza un user dupa id
        public Users GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public string GetType(int id)
        {
            return _context.Users.Find(id).TipUtilizator;
        }
        //serviciul ce genereaza token-ul
        private string generateJwtToken(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.IdUser.ToString()),
                    //new Claim("tip",user.TipUtilizator)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        //serviciul folosit pt verificarea tokenului
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("empty password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        //verifica hash-ul parolei
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (storedHash.Length != 64) throw new ArgumentException("hash exceeds limit");
            if (storedSalt.Length != 128) throw new ArgumentException("salt exceeds limit");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }
    }
}
//ar fi recomandat sa creem adminService, pt a putea face request-uri de creere cont si sa putem verifica cum trebuie login-ul!