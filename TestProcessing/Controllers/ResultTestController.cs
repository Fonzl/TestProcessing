using DTO.ResultTestDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.ServiceResultTest;

namespace TestProcessing.Controllers
{
    [Route("ResultTest")]
    [ApiController]
    public class ResultTestController(IServiceResultTest service) : Controller
    {
        [HttpGet]
        [Route("all")]

        public IActionResult GetAllResultTests()
        {

            return Json(service.GetAllResultTests());
        }



        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult GetResultTest(int id)
        {
            return Json(service.GetResultTest(id));
        }

        // POST api/<ValuesController>
        [HttpPost]
        [Route("add")]
        public IActionResult AddResultTest(CreateResultTestDto dto)
        {
            service.CreateResultTest(dto);
            return Ok("Done");
        }

        // PUT api/<ValuesController>/5
        [HttpPatch]
        [Route("update")]
        public IActionResult UpdateResultTest(UpdateResultTestDto dto)
        {
            service.UpdateResultTest(dto);
            return Ok("Done");
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteResultTest(int id)
        {
            service.DeleteResultTest(id);
            return Ok("Done");
        }
    }
}
