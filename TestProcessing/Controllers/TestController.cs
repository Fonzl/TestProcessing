
using DTO.TestDto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.IdentityModel.JsonWebTokens;
using Service;
using Service.ServiceTest;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace TestProcessing.Controllers
{
    [Route("Test")]
    [ApiController]
    public class TestController(IServiceTest service) : Controller
    {
        // GET: api/<ValuesController>
        [HttpGet]
        [Route("all")]
        [Authorize(Roles = "admin")]

        public IActionResult GetAllTests(HttpContext context)
        {

            return Json(service.GetTests());
        }



        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult GetTest(int id)
        {
            return Json(service.GetTest(id));
        }
        [HttpGet("student")]
        [Authorize(Roles = "student")]
        public IActionResult GetTestStudent()
        {
            var id = User.FindFirst("id")?.Value;
           return Json(service.GetTestsListStudent(Convert.ToInt16(id)));
        }
        [HttpGet("teacher/{dis}")]
        [Authorize(Roles = "teacher")]
        public IActionResult GetTestTeacher(long dis)
        {
            
            return Json(service.GetTestsListTeacherDiscipline(dis));
        }
        // POST api/<ValuesController>
        [HttpPost]
        [Route("add")]
        public IActionResult AddTest(CreateTestDto dto)
        {
            service.CreateTest(dto);
            return Ok("Done");
        }

        // PUT api/<ValuesController>/5
        [HttpPatch]
        [Route("update")]
        public IActionResult UpdateTest(UpdateTestDto dto)
        {
            service.UpdateTest(dto);
            return Ok("Done");
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTest(int id)
        {
            service.DeleteTest(id);
            return Ok("Done");
        }
    }
}
