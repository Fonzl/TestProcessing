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

            return Json(service.GetAllDiscipline());
        }



        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult GetDiscipline(int id)
        {
            return Json(service.GetDiscipline(id));
        }
        [HttpGet("teacher")]
        [Authorize(Roles = "teacher")]//обычный выбор дисциплин для учителя
        public IActionResult GetDisciplineTeacher()
        {
            var id = User.FindFirst("id")?.Value;
            return Json(service.TeacherGetDiscipline(Convert.ToInt16(id)));
        }
        [HttpGet("student")]
        [Authorize(Roles = "student")]//обычный выбор дисциплин для студента
        public IActionResult GetDisciplineStudent()
        {
            var id = User.FindFirst("id")?.Value;
            return Json(service.StudentGetDiscipline(Convert.ToInt16(id)));
        }
        [HttpGet("studentProfilDiscipline/{id}")]//Список дисциплин в профиле ученика для статистики
        [Authorize(Roles = "teacher")]
        public IActionResult GetDisciplineStudentProfil(long id)
        {

            return Json(service.StudentGetDiscipline(id));
        }
        // POST api/<ValuesController>
        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("add")]
        public IActionResult AddDiscipline(CreateDisciplineDto dto)
        {
            service.CreateDiscipline(dto);
            return Ok("Done");
        }

        // PUT api/<ValuesController>/5
        [HttpPatch]
        [Authorize(Roles = "admin")]
        [Route("update")]
        public IActionResult UpdateDiscipline(UpdateDisciplineDto dto)
        {
            service.UpdateDiscipline(dto);
            return Ok("Done");
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteDiscipline(int id)
        {
            service.DeleteDiscipline(id);
            return Ok("Done");
        }

    }
}
