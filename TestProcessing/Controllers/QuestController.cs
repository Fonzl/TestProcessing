using DTO.AnswerDto;
using DTO.QuestDto;
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
        [Route("all")]

        public IActionResult GetAllQuests()
        {

            return Json(serviceQuest.GetAllQuests());
        }



        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult GetQuest(int id)
        {
            return Json(serviceQuest.GetQuest(id));
        }

        // POST api/<ValuesController>
        [HttpPost]
        [Route("addAnswer")]
        public IActionResult AddAnswer(List<CreateAnswerDto> dto)
        {
            serviceAnswer.AnswerListCreate(dto);
            
            return Ok("Done");
        }
        [HttpPost]
        [Route("add")]
        public IActionResult AddQuest(CreateQuestDto dto)
        {
            serviceQuest.CreateQuest(dto);

            return Ok("Done");
        }

        // PUT api/<ValuesController>/5
        [HttpPatch]
        [Route("update")]
        public IActionResult UpdateQuest(UpdateQuestDto dto)
        {
            serviceQuest.UpdateQuest(dto);
            return Ok("Done");
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteQuest(int id)
        {
            serviceQuest.DeleteQuest(id);
            return Ok("Done");
        }
    }
}
