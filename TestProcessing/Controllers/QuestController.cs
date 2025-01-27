using DTO.AnswerDto;
using DTO.QuestDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.ServiceAnswer;
using Service.ServiceQuest;

namespace TestProcessing.Controllers
{
    [Route("Quest")]
    [ApiController]
    public class QuestController(IServiceQuest serviceQuest,IServiceAnswer serviceAnswer) : Controller
    {
        [HttpGet]
        [Authorize(Roles = "teacher,admin")]
        [Route("all")]

        public IActionResult GetAllQuests()
        {
            try
            {
                return Json(serviceQuest.GetAllQuests());
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }


        [Authorize(Roles = "teacher,student")]
        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult GetQuest(int id)
        {
            try
            {
                return Json(serviceQuest.GetQuest(id));
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }

        // POST api/<ValuesController>
        [HttpPost]
        [Route("addAnswer")]
        public IActionResult AddAnswer(List<CreateAnswerDto> dto)
        {
            try
            {
                serviceAnswer.AnswerListCreate(dto);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
        [HttpPost]
        [Authorize(Roles = "teacher,admin")]
        [Route("add")]
        public IActionResult AddQuest(CreateQuestDto dto)
        {
            try
            {
                serviceQuest.CreateQuest(dto);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }

        // PUT api/<ValuesController>/5
        [Authorize(Roles = "teacher,admin")]
        [HttpPatch]
        [Route("update")]
        public IActionResult UpdateQuest(UpdateQuestDto dto)
        {
            try
            {
                serviceQuest.UpdateQuest(dto);
                return StatusCode(200, "The content has been changed");
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }

        // DELETE api/<ValuesController>/5
        [Authorize(Roles = "teacher,admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteQuest(int id)
        {
            try
            {
                serviceQuest.DeleteQuest(id);
                return StatusCode(200, "Deletion was successful");
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
        [HttpGet]
        [Route("getListQuests/{id}")]
        [Authorize(Roles = "teacher,student")]
        public IActionResult GetListQuest(int id)//по id test
        {
            try
            {
                return Json(serviceQuest.GetListQuests(id));
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }

    }
}
