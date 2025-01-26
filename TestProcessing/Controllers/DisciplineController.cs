    using DTO.DisciplineDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.ServiceDiscipline;

namespace TestProcessing.Controllers
{
    [Route("Discipline")]
    [ApiController]
    public class DisciplineController(IServiceDiscipline service) : Controller
    {
        // GET: api/<ValuesController>
        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("all")]

        public IActionResult GetAllDisciplines()
        {
            try
            {
                return Json(service.GetAllDiscipline());
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }



        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult GetDiscipline(int id)
        {
            try
            {
                return Json(service.GetDiscipline(id));
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
        [HttpGet("teacher")]
        [Authorize(Roles = "teacher")]//обычный выбор дисциплин для учителя
        public IActionResult GetDisciplineTeacher()
        {
            try
            {
                var id = User.FindFirst("id")?.Value;
                return Json(service.TeacherGetDiscipline(Convert.ToInt16(id)));
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
        [HttpGet("student")]
        [Authorize(Roles = "student")]//обычный выбор дисциплин для студента
        public IActionResult GetDisciplineStudent()
        {
            try
            {
                var id = User.FindFirst("id")?.Value;
                return Json(service.StudentGetDiscipline(Convert.ToInt16(id)));
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
        [HttpGet("studentProfilDiscipline/{id}")]//Список дисциплин в профиле ученика для статистики
        [Authorize(Roles = "teacher")]
        public IActionResult GetDisciplineStudentProfil(long id)
        {
            try
            {

                return Json(service.StudentGetDiscipline(id));
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
        // POST api/<ValuesController>
        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("add")]
        public IActionResult AddDiscipline(CreateDisciplineDto dto)
        {
            try
            {
                service.CreateDiscipline(dto);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }

        // PUT api/<ValuesController>/5
        [HttpPatch]
        [Authorize(Roles = "admin")]
        [Route("update")]
        public IActionResult UpdateDiscipline(UpdateDisciplineDto dto)
        {
            try
            {
                service.UpdateDiscipline(dto);
                return StatusCode(200, "The content has been changed");
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteDiscipline(int id)
        {
            try
            {
                service.DeleteDiscipline(id);
                return StatusCode(200, "Deletion was successful");
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }

    }
}
