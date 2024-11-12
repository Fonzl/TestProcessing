using DTO.CourseDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.ServiceCourse;

namespace TestProcessing.Controllers
{
    
    [ApiController]
    [Route("Cours")]
    public class CoursController(IServiceCourse service) : Controller
    {
        [HttpGet]
        [Route("all")]

        public IActionResult GetAllCours()
        {

            return Json(service.GetAllCours());
        }



        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult GetCours(short id)
        {
            return Json(service.GetCourse(id));
        }

        // POST api/<ValuesController>
        [HttpPost]
        [Route("add/{id}")]
        public IActionResult AddCourse(short id)
        {
            service.CreateCourse(id);
            return Ok("Done");
        }

        // PUT api/<ValuesController>/5
        [HttpPatch]
        [Route("update")]
        public IActionResult UpdateCourse(short id)
        {
            service.UpdateCourse(id);
            return Ok("Done");
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(short id)
        {
            service.DeleteCourse(id);
            return Ok("Done");
        }

    }
}
