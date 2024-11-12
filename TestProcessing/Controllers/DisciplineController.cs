using DTO.DisciplineDto;
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
        [Route("all")]

        public IActionResult GetAllDisciplines()
        {

            return Json(service.GetAllDiscipline());
        }



        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult GetDiscipline(int id)
        {
            return Json(service.GetDiscipline(id));
        }

        // POST api/<ValuesController>
        [HttpPost]
        [Route("add")]
        public IActionResult AddDiscipline(CreateDisciplineDto dto)
        {
            service.CreateDiscipline(dto);
            return Ok("Done");
        }

        // PUT api/<ValuesController>/5
        [HttpPatch]
        [Route("update")]
        public IActionResult UpdateDiscipline(UpdateDisciplineDto dto)
        {
            service.UpdateDiscipline(dto);
            return Ok("Done");
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteDiscipline(int id)
        {
            service.DeleteDiscipline(id);
            return Ok("Done");
        }
    }
}
