
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

        public async Task<IActionResult> GetAllTests()
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
        public async Task<IActionResult> GetTest(int id)
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
        public async Task<IActionResult> GetTestStusent(long testId)
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
        //public async Task<IActionResult> GetTestStudent()
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
        [Authorize(Roles = "teacher,student,admin")]
        public async Task<IActionResult> GetListTestDiscipline(long dis)
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
        public async Task<IActionResult> AddTest(CreateTestDto dto)
        {
            try
            {

                return Json(new DTO.GeneralDto.IdDto { Id = service.CreateTest(dto) });
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }

        // PUT api/<ValuesController>/5
        [HttpPatch]
        [Route("update")]
        public async Task<IActionResult> UpdateTest(UpdateTestDto dto)
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
        public async Task<IActionResult> DeleteTest(int id)
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
