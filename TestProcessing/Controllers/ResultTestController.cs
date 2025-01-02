using DTO.ResultTestDto;
using Microsoft.AspNetCore.Authorization;
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

        
        [Authorize(Roles = "student")]
        [HttpPost]
        [Route("student")]
        public IActionResult AddResultTest(CreateResultTestDto dto)
        {
            var id = User.FindFirst("id")?.Value;
            AddResultTestStudentDto studentDto = new AddResultTestStudentDto() { AnsweId = dto.AnsweId,StudentId = Convert.ToInt16(id),TestId =dto.TestId};
            service.CreateResultTest(studentDto);
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
