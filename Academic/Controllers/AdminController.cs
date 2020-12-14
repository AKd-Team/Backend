using System;
using Academic.Entities;
using Academic.Helpers;
using Academic.Models;
using Academic.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

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
                _adminService.CreateStudent(student, model.Password,model.Semigrupa,model.Specializare);
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

        [HttpGet("departamente/{idFacultate}")]
        public IActionResult GetDepartamente(int idFacultate)
        {
            var dep = _adminService.GetDepartaments(idFacultate);
            return Ok(dep);
        }
        
        /*
         * Desc: Functie care preia o lista cu toate specializarile si grupele si semigrupele acestora
         */
        [HttpGet("formSpec/{idFacultate}")]
        public IActionResult GetSpecForm(int idFacultate)
        {
            var specForm = _adminService.GetFormSpec(idFacultate);
            return Ok(specForm);
        }

        /*
         * Desc: Returneaza o lista de reguli(din tabela Regulament) pe baza id-ului de facultate al unui admin
         * In: idFacultate - un int care reprezinta IdFacultate al unui admin
         * Out: reguli - lista de elemente de tip Regulament
         * Err: Nu exista caz de eroare
         */
        [HttpGet("listaReguli/{idFacultate}")]
        public IActionResult GetRegulament(int idFacultate)
        {
            var reguli = _adminService.GetRegulament(idFacultate);
            return Ok(reguli);
        }
        
        /*
         * Desc: Schimba detaliile unei reguli(din tabela regula) din baza de date
         * In: Un model de tip UpdateRegula care contine IdRegula, Titlu si Continut
         * Out: Un mesaj de succes sau un mesaj de eroare
         * Err: Pentru cazul in care IdRegula nu exista deja in tabela Regula
         *      Pentru cazul in care exista deja o alta regula cu acel titlu sau cu acel text
         */
        [HttpPut("updateRegula")]
        public IActionResult ChangeRegula(UpdateRegula model)
        {
            try
            {
                _adminService.ChangeRegula(model);
                return Ok(new {message = "Regula a fost modificata cu succes"});
            }
            catch (AppException ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }
        
        /*
         * Desc: Adauga o regula noua in baza de date
         * In: Un model de tip AddRegula care contine titlu si continut
         * Out: Un mesaj de succes sau un mesaj de eroare
         * Err: In cazul in care o regula cu acelasi titlu sau acelasi text exista deja
         */
        [HttpPost("addRegula")]
        public IActionResult CreateRegula(AddRegula model)
        {
            var regula = _mapper.Map<Regulament>(model);
            try
            {
                _adminService.CreateRegula(regula);
                return Ok(new {message = "Regula a fost creata cu succes"});
            }
            catch (AppException ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }

        /*
         * Desc: Partea de controller pentru stergerea unei reguli din tabela regulament
         * In: idRegula - un int ce reprezinta id-ul regulii care trebuie stearsa
         * Out: Mesaj de succes Ok("Regula a fost stearsa cu succes") sau un mesaj de insucces
         * Err: Daca regula cu acest id nu exista
         */
        [HttpDelete("deleteRegula/{idRegula}")]
        public IActionResult DeleteRegula(int idRegula) 
        {
            try
            {
                _adminService.DeleteRegula(idRegula);
                return Ok(new {message = "Regula a fost stearsa cu succes"});
            }
            catch (AppException ex)
            {
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