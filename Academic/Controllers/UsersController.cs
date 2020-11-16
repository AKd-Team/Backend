using Microsoft.AspNetCore.Mvc;
using Academic.Models;
using Academic.Services;
using Academic.Entities;
using AutoMapper;
using Academic.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
//in controller se fac requesturile la api
namespace Academic.Controllers
{

        [ApiController]
        [Route("users")]
        public class UsersController : ControllerBase
        {
            private IUsersService _usersService;
            private IMapper _mapper;
            private readonly AppSettings _appSettings;

            public UsersController(
                IUsersService userService,
                IOptions<AppSettings> appSettings,
                IMapper mapper)
            {
                _usersService = userService;
                _appSettings = appSettings.Value;
                _mapper = mapper;
            }
            //request-ul de login
            [HttpPost("login")]
            public IActionResult Login(LoginRequest model)
            {
                var response = _usersService.Login(model.Username, model.Password);

                if (response == null)
                    return BadRequest(new {message = "User or Password is incorrect"});
                return Ok(response);
            }
            [HttpPost("register")]
            public IActionResult Register(RegisterRequest model)
            {
                // map model to entity
                var user = _mapper.Map<Users>(model);
    
                try
                {
                    // create user
                    _usersService.Create(user,model.Password);
                    return Ok();
                }
                catch (AppException ex)
                {
                    // return error message if there was an exception
                    return BadRequest(new { message = ex.Message });
                }
            }
            //request-ul ce returneaa un user dupa id
            [HttpGet("{id}")]
            [Authorize("")]
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
            //requestul ce returneaza toti userii
            [HttpGet]
            [Authorize("")]
            public IActionResult GetAll()
            {
                var users = _usersService.GetAll();
                return Ok(users);
            }
        }
}