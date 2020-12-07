using Academic.Entities;
using Academic.Helpers;
using Academic.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Academic.Controllers
{
    [ApiController] 
    [Route("student")]
    [Authorize("student")]
    public class StudentController : ControllerBase
    {
        private IStudentService _studentService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public StudentController(
            IStudentService studentService,
            IOptions<AppSettings> appSettings,
            IMapper mapper)
        {
            _studentService = studentService;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }
        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _studentService.GetById(id);
            var model = _mapper.Map<Users>(user);
            return Ok(model);
        }
        [HttpGet("profesor/{id}")]
        public IActionResult GetByTeacherId(int id)
        {
            var profesor = _studentService.GetByTeacherId(id);
            return Ok(profesor);
        }
        [HttpGet("profesor")]
        public IActionResult GetAllTeachers()
        {
            var profesor = _studentService.GetAllTeachers();
            return Ok(profesor);
        }

        [HttpGet("info/{id}")]
        public IActionResult GetStudentById(int id)
        {
            var student = _studentService.GetStudentById(id);
            var model = _mapper.Map<Student>(student);
            return Ok(model);
        }

        [HttpGet("listaFacultati")]
        public IActionResult GetFacultati()
        {
            var facultati = _studentService.GetFacultati();
            return Ok(facultati);
        }

        [HttpGet("regulamen/{idSpec}")]
        public IActionResult GetRegulament(int idSpec)
        {
            var regulament = _studentService.GetRegulament(idSpec);
            return Ok(regulament);
        }
    }
}