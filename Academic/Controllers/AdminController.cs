using Academic.Entities;
using Academic.Helpers;
using Academic.Models;
using Academic.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Academic.Controllers
{
    [ApiController]
    [Route("admin")]
    [Authorize("admin")]
    public class AdminController : ControllerBase
    {
        private IAdminService _adminService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public AdminController(
            IAdminService adminService,
            IOptions<AppSettings> appSettings,
            IMapper mapper)
        {
            _adminService = adminService;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        [HttpPost("registerAdmin")]
        public IActionResult RegisterAdmin(RegisterAdmin model)
        {
            // map model to entity
            var admin = _mapper.Map<Admin>(model);

            try
            {
                // create user
                _adminService.CreateAdmin(admin, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new {message = ex.Message});
            }
        }
        [HttpPost("registerStudent")]
        public IActionResult RegisterStudent(RegisterStudent model)
        {
            // map model to entity
            var student = _mapper.Map<Student>(model);

            try
            {
                // create user
                _adminService.CreateStudent(student, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new {message = ex.Message});
            }
        }
        [HttpPost("registerProfesor")]
        public IActionResult RegisterProfesor(RegisterProfesor model)
        {
            // map model to entity
            var profesor = _mapper.Map<Profesor>(model);

            try
            {
                // create user
                _adminService.CreateProfesor(profesor, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new {message = ex.Message});
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _adminService.GetAll();
            return Ok(users);
        }
    }
}