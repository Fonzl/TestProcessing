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
        [Authorize(Roles = "admin")]
        public IActionResult GetAllResultTests()
        {

            return Json(service.GetAllResultTests());
        }
        [Authorize(Roles = "student")]
        [HttpGet]
        [Route("student")]

        // GET api/<ValuesController>/5
        
        public IActionResult GetResultTest()//результаты тестов студента все
        {
            var id = User.FindFirst("id")?.Value;
            return Json(service.ResultStudentId(Convert.ToInt16(id)));
        }

        
        [Authorize(Roles = "student")]
        [HttpPost]
        [Route("student")]
        public IActionResult AddResultTest(CreateResultTestDto dto)//Расчёт результата теста
        {
            var id = User.FindFirst("id")?.Value;
            AddResultTestStudentDto studentDto = new AddResultTestStudentDto() { AnsweId = dto.AnsweId,StudentId = Convert.ToInt16(id),TestId =dto.TestId};
            service.CreateResultTest(studentDto);
            return Ok("Done");
        }
        [Authorize(Roles = "teacher")]//рузуьтаты тестов по  студента для учителя;id - id  студента
        [HttpGet]
        [Route("teacherStudentResultId/{id}")]
        public IActionResult GetResultTest(long id)
        {
            
            return Json(service.ResultStudentId(id));
        }

        [Authorize(Roles = "teacher")]//статистика по предмету  студента для учителя
        [HttpGet]
        [Route("teacherStatisticResultId")]
        public IActionResult GetTeacherStatisticResultTest(ResultStatisticsDto dto)
        {

            return Json(service.GetStatisticsDiscipline(dto));
        }
        [Authorize(Roles = "student")]//статистика по предмету  студента для студента
        [HttpGet]
        [Route("studentStatisticResultId/{disciploneId}")]
        public IActionResult GetStudentStatisticResultTest(int disciploneId)
        {
            var id = User.FindFirst("id")?.Value;
            return Json(service.GetStatisticsDiscipline(new ResultStatisticsDto { DisciplineId = disciploneId,StudentId = Convert.ToInt16(id)}));
        }
        [Authorize(Roles = "admin")]
        [HttpPatch]
        [Route("update")]
        public IActionResult UpdateResultTest(UpdateResultTestDto dto)
        {
            service.UpdateResultTest(dto);
            return Ok("Done");
        }
        [Authorize(Roles = "admin")]
        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteResultTest(int id)
        {
            service.DeleteResultTest(id);
            return Ok("Done");
        }
    }
}
