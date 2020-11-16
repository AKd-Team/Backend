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

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest model)
        {
            // map model to entity
            var user = _mapper.Map<Users>(model);

            try
            {
                // create user
                _adminService.Create(user, model.Password);
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