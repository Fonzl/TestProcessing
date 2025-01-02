using DTO.GroupDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.ServiceGroup;

namespace GroupProcessing.Controllers
{
    [Route("Group")]
    [ApiController]
    public class GroupController(IServiceGroup service) : Controller
    {

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("all")]

        public IActionResult GetAllGroups()
        {

            return Json(service.GetAllGroups());
        }



        // GET api/<ValuesController>/5
        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]

        public IActionResult GetGroup(int id)
        {
            return Json(service.GetGroup(id));
        }

        // POST api/<ValuesController>
        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("add")]
        public IActionResult AddGroup(CreateGroupDto dto)
        {
            service.CreateGroup(dto);
            return Ok("Done");
        }

        // PUT api/<ValuesController>/5
        [HttpPatch]
        [Authorize(Roles = "admin")]
        [Route("update")]
        public IActionResult UpdateGroup(UpdateGroupDto dto)
        {
            service.UpdateGroup(dto);
            return Ok("Done");
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteGroup(int id)
        {
            service.DeleteGroup(id);
            return Ok("Done");
        }
        [HttpGet]
        [Route("student")]
        [Authorize(Roles = "student")]
        public IActionResult GetGroupStudent()
        {
            var id = User.FindFirst("id")?.Value;
            return Json(service.GetGroupUser(Convert.ToInt16(id)));
        }
    }
}
