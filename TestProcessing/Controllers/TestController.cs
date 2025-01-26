
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

        public IActionResult GetAllTests()
        {
            try
            {
                return Json(service.GetTests());
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }


        //Тесты юзеров по дисциплине 
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetTest(int id)
        {
            try
            {
                return Json(service.GetTest(id));
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
        //[HttpGet("student")]
        //[Authorize(Roles = "student")]
        //public IActionResult GetTestStudent()
        //{
        //    try
        //    {
        //        var id = User.FindFirst("id")?.Value;
        //        return Json(service.GetTestsListStudent(Convert.ToInt16(id)));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(520, ex.Message);
        //    }
        //}

        [HttpGet("GetListTestDiscipline/{dis}")]
        [Authorize(Roles = "teacher,student")]
        public IActionResult GetListTestDiscipline(long dis)
        {
         try
            {
                return Json(service.GetTestsListDiscipline(dis));
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
        // POST api/<ValuesController>
        [HttpPost]
        [Route("add")]
        public IActionResult AddTest(CreateTestDto dto)
        {
            try
            {
                service.CreateTest(dto);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }

        // PUT api/<ValuesController>/5
        [HttpPatch]
        [Route("update")]
        public IActionResult UpdateTest(UpdateTestDto dto)
        {
            try
            {
                service.UpdateTest(dto);
                return StatusCode(200, "The content has been changed");
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
            
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTest(int id)
        {
        
            try
            {
                service.DeleteTest(id);
                return StatusCode(200, "Deletion was successful");
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
    }
}
