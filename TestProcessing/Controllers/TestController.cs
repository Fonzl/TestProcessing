﻿
using DTO.TestDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.ServiceTest;

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
        [Authorize(Roles = "teacher,admin")]
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
        [Authorize(Roles = "student")]
        [HttpGet("{testId}/student")]
        public IActionResult GetTestStusent(long testId)
        {
            try
            {
                var UserId = User.FindFirst("id")?.Value;
                return Json(service.GetTestStudentDto(testId, Convert.ToInt64(UserId)));
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
                var UserId = User.FindFirst("id")?.Value;
                return Json(service.GetTestsListDiscipline(dis, Convert.ToInt64(UserId)));
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }

        // POST api/<ValuesController>
        [HttpPost]
        [Authorize(Roles = "teacher,admin")]
        [Route("add")]
        public IActionResult AddTest(CreateTestDto dto)
        {
            try
            {

                return Json(service.CreateTest(dto));
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
