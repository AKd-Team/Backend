using Microsoft.AspNetCore.Mvc;
using Academic.Models;
using Academic.Services;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Academic.Entities;
using AutoMapper;
using Academic.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Academic.Services;

namespace Academic.Controllers
{

        [ApiController]
        [Route("[controller]")]
        public class UsersController : ControllerBase
        {
            private UserService.IUsersService _usersService;
            private IMapper _mapper;
            private readonly AppSettings _appSettings;

            public UsersController(
                UserService.IUsersService userService,
                IOptions<AppSettings> appSettings,
                IMapper mapper)
            {
                _usersService = userService;
                _appSettings = appSettings.Value;
                _mapper = mapper;
            }

            [HttpPost("authenticate")]
            public IActionResult Autentificate(LoginRequest model)
            {
                var response = _usersService.Login(model.Username, model.Password);

                if (response == null)
                    return BadRequest(new {message = "User or Password is incorrect"});
                return Ok(response);
            }

            /*
            [HttpPost("register")]
            public IActionResult Register(RegisterRequest model)
            {
                // map model to entity
                var user = _mapper.Map<User>(model);
    
                try
                {
                    // create user
                    _userService.Create(user,model.Password);
                    return Ok();
                }
                catch (AppException ex)
                {
                    // return error message if there was an exception
                    return BadRequest(new { message = ex.Message });
                }
            }
            */
            [HttpGet("{id}")]
            public IActionResult GetById(int id)
            {
                var user = _usersService.GetById(id);
                var model = _mapper.Map<Users>(user);
                return Ok(model);
            }

            /*
            [HttpPut("{id}")]
            public IActionResult Update(int id, [FromBody] UpdateRequest model)
            {
                // map model to entity and set id
                var user = _mapper.Map<User>(model);
                user.Id = id;
    
                try
                {
                    // update user 
                    _userService.Update(user, model.Password);
                    return Ok();
                }
                catch (AppException ex)
                {
                    // return error message if there was an exception
                    return BadRequest(new { message = ex.Message });
                }
            }
            */
            [Authorize]
            [HttpGet]
            public IActionResult GetAll()
            {
                var users = _usersService.GetAll();
                return Ok(users);
            }
        }
}