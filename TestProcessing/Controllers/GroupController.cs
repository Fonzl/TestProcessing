using DTO.GroupDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.ServiceGroup;

namespace TestProcessing.Controllers
{
    [Route("Group")]
    [ApiController]
    public class GroupController(IServiceGroup service) : Controller
    {
        [HttpGet]
        [Route("all")]

        public IActionResult GetAllGroups()
        {

            return Json(service.GetAllGroups());
        }



        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult GetGroup(int id)
        {
            return Json(service.GetGroup(id));
        }

        // POST api/<ValuesController>
        [HttpPost]
        [Route("add")]
        public IActionResult AddGroup(CreateGroupDto dto)
        {
            service.CreateGroup(dto);
            return Ok("Done");
        }

        // PUT api/<ValuesController>/5
        [HttpPatch]
        [Route("update")]
        public IActionResult UpdateGroup(UpdateGroupDto dto)
        {
            service.UpdateGroup(dto);
            return Ok("Done");
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteGroup(int id)
        {
            service.DeleteGroup(id);
            return Ok("Done");
        }
    }
}
