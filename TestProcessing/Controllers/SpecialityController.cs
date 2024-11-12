using DTO.SpecialityDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.ServiceSpeciality;

namespace TestProcessing.Controllers
{
    [Route("Speciality")]
    [ApiController]
    public class SpecialityController(IServiceSpeciality service) : Controller
    {
        [HttpGet]
        [Route("all")]

        public IActionResult GetAllSpecialities()
        {

            return Json(service.GetAllSpecialties());
        }



        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult GetSpeciality(short id)
        {
            return Json(service.GetSpeciality(id));
        }

        // POST api/<ValuesController>
        [HttpPost]
        [Route("add")]
        public IActionResult AddSpeciality(CreateSpecialityDto dto)
        {
            service.CreateSpeciality(dto);
            return Ok("Done");
        }

        // PUT api/<ValuesController>/5
        [HttpPatch]
        [Route("update")]
        public IActionResult UpdateSpeciality(UpdateSpecialityDto dto)
        {
            service.UpdatSpeciality(dto);
            return Ok("Done");
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteSpeciality(short id)
        {
            service.DeleteSpeciality(id);
            return Ok("Done");
        }
    }
}
