using Database;
using DTO.GeneralDto;
using DTO.ResultTestDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.ServiceResultTest;
using Service.ServiceUser;

namespace TestProcessing.Controllers
{
    [Route("ResultTest")]
    [ApiController]
    public class ResultTestController(IServiceResultTest service,IServiceUser serviceUser) : Controller
    {

        [Authorize(Roles = "student")]
        [HttpGet]
        [Route("student/{idDiscipline}")]
        public async Task<IActionResult> GetResultTest(long idDiscipline)//результаты тестов студента все
        {
            try
            {
                var id = User.FindFirst("id")?.Value;
                var results = service.ResultStudentId(Convert.ToInt16(id), idDiscipline);
                if (results.Count == 0)
                {
                    return Json(new IsBoolDto { IsTrue = false });
                }
                else
                {
                    return Json(results);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }


        [Authorize(Roles = "student")]
        [HttpPost]
        [Route("studentPassedTest")]
        public async Task<IActionResult> AddResultTest(CreateResultTestDto dto)//Расчёт результата теста
        {
            try
            {
                var id = User.FindFirst("id")?.Value;
                AddResultTestStudentDto studentDto = new AddResultTestStudentDto() 
                {
                    StudentId = Convert.ToInt16(id), 
                    TestId = dto.TestId,
                    UserResponesTest = dto.UserResponesTest,
                    idResult = dto.idResult 
                };

                return Json(service.CreateResultTest(studentDto));
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
        [Authorize(Roles = "teacher,admin")]//рузуьтаты тестов по  студента для учителя;id - id  студента
        [HttpGet]
        [Route("teacherStudentId/{idStudent}/Discipline/{idDiscipline}")]
        public async Task<IActionResult> GetResultTestTeacher(long idStudent, long idDiscipline)
        {
            try
            {
                var results = service.ResultStudentId(Convert.ToInt16(idStudent), idDiscipline);
                if (results.Count == 0)
                {
                    return Json(new IsBoolDto { IsTrue = false });
                }
                else
                {
                    return Json(results);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
        [Authorize(Roles = "teacher,admin")]//статистика по предмету  студента для учителя
        [HttpGet]
        [Route("teacherResultGroup/{idGroup}/Test/{idTest}")]
        public async Task<IActionResult> GetTeacherResultGroup(long idGroup,long idTest )
        {
            try
            {
                return Json(service.userAttemptShorts(idGroup, idTest));
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
        [Authorize(Roles = "teacher,admin")]//статистика по предмету  студента для учителя
        [HttpGet]
        [Route("teacherStatisticResultId/{studentId}/discipline/{disciplineId}")]
        public async Task<IActionResult> GetTeacherStatisticResultTest(int disciplineId,long studentId)
        {
            try
            {
                ResultStatisticsDto dto = new ResultStatisticsDto
                {
                    DisciplineId = disciplineId,
                    StudentId = studentId
                };
                return Json(new StatisticResult { result = service.GetStatisticsDiscipline(dto) });
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
        [Authorize(Roles = "student")]//статистика по предмету  студента для студента
        [HttpGet]
        [Route("studentStatisticResultId/{disciploneId}")]
        public async Task<IActionResult> GetStudentStatisticResultTest(int disciploneId)
        {
            try
            {
                var id = User.FindFirst("id")?.Value;
                return Json(new StatisticResult { result = service.GetStatisticsDiscipline(new ResultStatisticsDto { DisciplineId = disciploneId, StudentId = Convert.ToInt16(id) }) });
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
        //[Authorize(Roles = "admin")]
        //[HttpPatch]
        //[Route("update")]
        //public async Task<IActionResult> UpdateResultTest(UpdateResultTestDto dto)
        //{
        //    try
        //    {

        //        service.UpdateResultTest(dto);
        //        return StatusCode(200, "The content has been changed");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(520, ex.Message);
        //    }
        //}

        // DELETE api/<ValuesController>/5
        //[HttpDelete("delete/{id}")]
        //[Authorize(Roles = "admin")]
        //public async Task<IActionResult> DeleteResultTest(int id)
        //{
        //    try
        //    {
        //        service.DeleteResultTest(id);
        //        return StatusCode(200, "Deletion was successful");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(520, ex.Message);
        //    }

        //}

        [Authorize]
        [HttpGet]
        [Route("resultDetails/{id}")]
        public async Task<IActionResult> GetResultTestDetails(long id)
        {
            try
            {
                if (serviceUser.GetUser(Convert.ToInt16(User.FindFirst("id")?.Value)).Role.Id == 3)
                {
                    if (service.TestBool(id).IsTrue == true)
                    { var responesDto = service.ReturnResultDetailsTrue(id);


                        return Json(new ReturnResultDetails
                        {
                            numberOfCorrect = responesDto.Where(x => x.IsCorrectQuest == true).Count(),
                            numberOfIncorrect = responesDto.Where(x => x.IsCorrectQuest == false).Count(),
                            verifiedUserRespones = responesDto


                        });
                    }
                    else
                    {   
                        var responesDtoShorts = service.ReturnResultDetailsFalse(id);
                        
                        return Json(new ReturnResultDetailsShort
                        {
                            numberOfCorrect = responesDtoShorts.Where(x => x.IsCorrectQuest == true).Count(),
                            numberOfIncorrect = responesDtoShorts.Where(x => x.IsCorrectQuest == false).Count(),
                            verifiedUserRespones = responesDtoShorts
                        });

                    }

                }
                else
                {
                    var responesDto = service.ReturnResultDetailsTrue(id);


                    return Json(new ReturnResultDetails
                    {
                        numberOfCorrect = responesDto.Where(x => x.IsCorrectQuest == true).Count(),
                        numberOfIncorrect = responesDto.Where(x => x.IsCorrectQuest == false).Count(),
                        verifiedUserRespones = responesDto


                    });
                }
            }

            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
        [Authorize(Roles = "student")]
        [HttpPost]
        [Route("createAttempt/{idTest}")]//Запрос на создание попытки
        public async Task<IActionResult> CreateAttempt(long idTest)
        {
            try
            {
                
                
                    var studentId = User.FindFirst("id")?.Value;
                    return Json(new IdDto
                    {
                        Id = service.CreatResultAndAttempt(idTest, Convert.ToInt16(studentId))
                    });
                
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
        [Authorize(Roles = "student")]
        [HttpGet]
        [Route("checkingAttempt/{idTest}")]//проверка на действующие попытки
        public async Task<IActionResult> CheckingAttempt(long idTest)
        {
            try
            {

               
                var studentId = User.FindFirst("id")?.Value;// Вытаскиваем Id пользователя из jwt
                var result = service.CheckingStudentResult(idTest, Convert.ToInt16(studentId));
                if (result == null)
                {
                    return Json(new IsBoolDto()//Отправиться если попыток нет
                    {
                        IsTrue = true,
                    });
                }
                if (result.Second == 0 && result.Minutes == 0)// Время закончилось.Завершаем тест.
                {
                    return Json(service.CreateResultTest(new AddResultTestStudentDto
                    {
                        StudentId = Convert.ToInt16(studentId),
                        TestId = result.TestId,
                        UserResponesTest = result.UserResponesTest,
                        idResult = result.idResult,

                    }));
                }
                else
                {
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
        [Authorize(Roles = "student")]
        [HttpPatch]
        [Route("Attempt/update")]
        public async Task<IActionResult> UpdateAttemptTest(CreateResultTestDto dto)
        {
            try
            {
                var id = User.FindFirst("id")?.Value;
                var updateDto = new AddResultTestStudentDto()
                {
                    idResult = dto.idResult,
                    TestId = dto.TestId,
                    UserResponesTest = dto.UserResponesTest,
                    StudentId = Convert.ToInt16(id)
                };
                
                service.UpdateRespones(updateDto);

                return StatusCode(200, "The content has been changed");
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
    }
}

