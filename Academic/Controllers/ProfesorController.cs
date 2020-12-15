using Academic.Entities;
using Academic.Helpers;
using Academic.Services;
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
    }
}