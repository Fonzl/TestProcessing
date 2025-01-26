﻿using DTO.ResultTestDto;
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
        [Authorize(Roles = "admin")]
        public IActionResult GetAllResultTests()
        {
            try
            {
                return Json(service.GetAllResultTests());
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
        [Authorize(Roles = "student")]
        [HttpGet]
        [Route("student")]
        public IActionResult GetResultTest()//результаты тестов студента все
        {
            try
            {
                var id = User.FindFirst("id")?.Value;
                return Json(service.ResultStudentId(Convert.ToInt16(id)));
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }


        [Authorize(Roles = "student")]
        [HttpPost]
        [Route("student")]
        public IActionResult AddResultTest(CreateResultTestDto dto)//Расчёт результата теста
        {
            try
            {
                var id = User.FindFirst("id")?.Value;
                AddResultTestStudentDto studentDto = new AddResultTestStudentDto() { StudentId = Convert.ToInt16(id), TestId = dto.TestId, UserRespones = dto.UserRespones };

                return Json(service.CreateResultTest(studentDto));
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
        [Authorize(Roles = "teacher")]//рузуьтаты тестов по  студента для учителя;id - id  студента
        [HttpGet]
        [Route("teacherStudentResultId/{id}")]
        public IActionResult GetResultTest(long id)
        {
            try
            {
                return Json(service.ResultStudentId(id));
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }

        [Authorize(Roles = "teacher")]//статистика по предмету  студента для учителя
        [HttpPost]
        [Route("teacherStatisticResultId")]
        public IActionResult GetTeacherStatisticResultTest(ResultStatisticsDto dto)
        {
            try
            {
                return Json(service.GetStatisticsDiscipline(dto));
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
        [Authorize(Roles = "student")]//статистика по предмету  студента для студента
        [HttpGet]
        [Route("studentStatisticResultId/{disciploneId}")]
        public IActionResult GetStudentStatisticResultTest(int disciploneId)
        {
            try
            {
                var id = User.FindFirst("id")?.Value;
                return Json(service.GetStatisticsDiscipline(new ResultStatisticsDto { DisciplineId = disciploneId, StudentId = Convert.ToInt16(id) }));
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
        [Authorize(Roles = "admin")]
        [HttpPatch]
        [Route("update")]
        public IActionResult UpdateResultTest(UpdateResultTestDto dto)
        {
            try
            {
                service.UpdateResultTest(dto);
                return StatusCode(200, "The content has been changed");
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteResultTest(int id)
        {
            try
            {
                service.DeleteResultTest(id);
                return StatusCode(200, "Deletion was successful");
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }

        }
        [Authorize]
        [HttpPost]
        [Route("resultDetails/{id}")]
        public IActionResult GetResultTestDetails(long id)
        {
            try
            {
                return Json(service.ReturnResultDetails(id));
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
    }
}
