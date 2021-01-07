using System;
using Academic.Entities;
using Academic.Helpers;
using Academic.Models;
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
            var model = _mapper.Map<StudentDetaliat>(student);
            return Ok(model);
        }

        [HttpGet("listaFacultati")]
        public IActionResult GetFacultati()
        {
            var facultati = _studentService.GetFacultati();
            return Ok(facultati);
        }

        [HttpGet("regulament/{idSpec}")]
        public IActionResult GetRegulament(int idSpec)
        {
            var regulament = _studentService.GetRegulament(idSpec);
            return Ok(regulament);
        }

        /*
         * Desc: Partea de controller pentru afisarea orarului
         * In: idStudent - un int care reprezinta id-ul studentului pentru care afisam orarul
         * Out: orar - o lista cu elemente de tip orar personalizat
         * Err:
         */
        [HttpGet("orar/{idStudent}")]
        public IActionResult GetOrar(int idStudent)
        {
            var orar = _studentService.GetOrar(idStudent);
            return Ok(orar);
        }

        /*
         * Desc: Partea de controller pentru functia care returneaza orarul de examene pt un student dat
         * In: id-ul studentului
         * Out: orarListat - o lista de obiecte de tip OrarExamen
         * Err: -
         */
        [HttpGet("examene/{idStudent}")]
        public IActionResult GetExamene(int idStudent)
        {
            var examene = _studentService.GetExamene(idStudent);
            return Ok(examene);
        }
        
        /*
         * Desc: Partea de controller pt optiunile de review
         * In: idStudent - int; reprezinta id-ul unui student
         * Out: optReview - o lista de obiecte de tip OptiuniReview
         * Err: -
         */
        [HttpGet("optiuniReview/{idStudent}")]
        public IActionResult GetOptiuniReview(int idStudent)
        {
            return Ok(_studentService.GetOptiuniReview(idStudent));
        }
        
        /*
         * Desc: Partea de controller pt a returna profesorii nedetaliati
         * In: idMaterie - int
         *     tip - string, tipul activitatii
         * Out: profesori - o lista de obiecte de tip ProfesoriNedetaliati
         * Err: -
         */
        [HttpGet("profesori/{idMaterie}/{tip}")]
        public IActionResult GetProfesoriNedetaliati(int idMaterie, string tip)
        {
            return Ok(_studentService.GetProfesoriNedetaliati(idMaterie,tip));
        }
        
        /*
         * Desc: Partea de controller pt verificarea existentei unei evaluari
         * In: rc - un obiect de tip ReviewComplet
         * Out: true sau false
         * Err: -
         */
        [HttpGet("existentaEvaluare")]
        public IActionResult ExistentaEvaluare(ReviewComplet rc)
        {
            return Ok(_studentService.ExistentaEvaluare(rc));
        }
        
        /*
         * Desc: Partea de controller pe a transmite datele de evaluare facute asupra unui profesor. 
         * In: rc - un obiect de tip AdaugareReview
         * Out: Un mesaj de succes sau un mesaj de eroare
         * Err: -
         */
        [HttpPost("adaugareReview")]
        public IActionResult AdaugareReview(ReviewComplet rc)
        {
            try
            {
                _studentService.AdaugareReview(rc);
                return Ok(new {message = "Review-ul a fost adaugat cu succes"});
            }
            catch (AppException ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }

        /*
         * Desc: Partea de controller pt a transmite lista de criterii pentru evaluarea unui profesor
         * In: -
         * Out: o lista de criterii
         * Err: -
         */
        [HttpGet("criterii")]
        public IActionResult GetCriterii()
        {
            return Ok(_studentService.GetCriterii());
        }

        [HttpGet("note/{idStudent}")]
        public IActionResult GetNote(int idStudent)
        {
            return Ok(_studentService.GetNota(idStudent));
        }

        [HttpGet("materii/{idStudent}")]
        public IActionResult GetMaterii(int idStudent)
        {
            return Ok(_studentService.GetMaterii(idStudent));
        }

        [HttpGet("materii/statistici/{idMaterie}")]
        public IActionResult GetStatisticiMaterie(int idMaterie)
        {
            return Ok(_studentService.GetStatisticiMaterie(idMaterie));
        }
            
    }
}