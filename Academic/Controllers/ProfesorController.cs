using System;
using Academic.Entities;
using Academic.Helpers;
using Academic.Services;
using Academic.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
namespace Academic.Controllers
{
    [ApiController] 
    [Route("profesor")]
    [Authorize("profesor")]
    public class ProfesorController : ControllerBase
    {
        private IProfesorService _profesorService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public ProfesorController(
            IProfesorService profesorService,
            IOptions<AppSettings> appSettings,
            IMapper mapper)
        {
            _profesorService = profesorService;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _profesorService.GetById(id);
            var model = _mapper.Map<Users>(user);
            return Ok(model);
        }

        [HttpGet("{id}/materii")]
        public IActionResult GetListMaterii(int id)
        {
            var user = _profesorService.GetListMaterii(id);
            return Ok(user);
        }

        [HttpGet("{id}/materii/{idMaterie}")]
        public IActionResult GetStudentiLaMaterie(int id, int idMaterie)
        {
            var studenti = _profesorService.GetStudentiInscrisi(idMaterie, id);
            return Ok(studenti);
        }

        /* Desc: Partea de controller pentru a returna o lista de sali
         * In: -
         * Out: sali - o lista de obiecte de tip sala
         * Err: -
         */
        [HttpGet("sali")]
        public IActionResult GetSali()
        {
            var sali = _profesorService.GetSali();
            return Ok(sali);
        }

        /*
         * Desc: Partea de controller pentru gasirea orarului unei sali
         * In: idSala - un int ce reprezinta id-ul salii pt care cautam orarul
         * Out: orarSali - o lista de obiecte de tip OrarSali
         * Err: -
         */
        [HttpGet("orarSali/{idSala}")]
        public IActionResult GetOrarSali(int idSala)
        {
            var orarSali = _profesorService.GetOrarSali(idSala);
            return Ok(orarSali);
        }
        
        /*
         * Desc: Partea de controller pentru vizualizarea rezultatelor
         * In: idMaterie - int, id-ul unei materii pt care profesorul a fost evaluat
         *     idProfesor - int, id-ul profesorului care o sa vada rezultatele
         * Out: Un obiect de tip RezultateEvaluare
         * Err: -
         */
        [HttpGet("rezultateEvaluare/{idMAterie}/{idProfesor}")]
        public IActionResult GetRezultateEvaluari(int idMaterie, int idProfesor)
        {
            return Ok(_profesorService.GetRezultateEvaluare(idMaterie, idProfesor));
        }
        [HttpPost("ProgExamen")]
        public IActionResult ProgExamen(AddExamen model)
        {
            
            var examen = new Orarmaterie();
            var oraIncT = TimeSpan.Parse(model.OraInceput);
            examen.OraInceput = oraIncT;
            var oraSf = TimeSpan.Parse(model.OraSfarsit);
            examen.OraSfarsit = oraSf;
            examen.IdMaterie = model.IdMaterie;
            examen.IdFormatie = model.IdFormatie;
            examen.IdProfesor = model.IdProfesor;
            examen.IdSala = model.IdSala;
            examen.Data = model.Data;
            examen.ZiuaSaptamanii = "";
            examen.Frecventa = "";
            examen.Tip = "Examen";
            try
            {
                _profesorService.ProgExamen(examen);
                return Ok(new {message = "Examenul a fost adaugat cu succes"});
            }
            catch (AppException ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }

        [HttpPut("AdaugareNote")]
        public IActionResult AdaugareNote(AdaugareNota an)
        {
            try
            {
                _profesorService.AdaugareNote(an);
                return Ok(new {message = "Nota a fost adaugata cu succes"});
            }
            catch (AppException ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }

        [HttpGet("StudFaraNote/{id_materie}")]
        public IActionResult GetStudentFaraNota(int id_materie)
        {
            var StudFaraNota = _profesorService.GetStudentFaraNota(id_materie);
            return Ok(StudFaraNota);
        }

        [HttpGet("getFormatii")]
        public IActionResult getFormatii()
        {
            var listaForm = _profesorService.GetFormatii();
            return Ok(listaForm);
        }
        
        [HttpGet("materii/statistici/{idMaterie}")]
        public IActionResult GetStatisticiMaterie(int idMaterie)
        {
            var statistici = _profesorService.GetStatisticiMaterie(idMaterie);
            return Ok(statistici);
        }

        [HttpGet("orar/{idProfesor}")]
        public IActionResult GetOrar(int idProfesor)
        {
            {
                var orar = _profesorService.GetOrar(idProfesor);
                return Ok(orar);
            }
        }
        
    }
}