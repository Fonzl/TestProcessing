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
            try
            {
                return Json(service.GetAllGroups());
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }



        // GET api/<ValuesController>/5
        //[Authorize(Roles = "teacher")]
        [Authorize(Roles = "admin,teacher")]
        [HttpGet("{id}")]

        public IActionResult GetGroup(int id)
        {
            try
            {
                return Json(service.GetGroup(id));
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
        [Authorize(Roles = "teacher")]
        [HttpGet("Discipline/{id}")]//Возрает список групп по дисцилине
        public IActionResult GetDisciplineGroup(int id)
        {
            try
            {
                return Json(service.GetDisciplineGroupList(id));
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }

        // POST api/<ValuesController>
        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("add")]
        public IActionResult AddGroup(CreateGroupDto dto)
        {
            try
            {
                service.CreateGroup(dto);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }

        // PUT api/<ValuesController>/5
        [HttpPatch]
        [Authorize(Roles = "admin")]
        [Route("update")]
        public IActionResult UpdateGroup(UpdateGroupDto dto)
        {
            try
            {
                service.UpdateGroup(dto);
                return StatusCode(200, "The content has been changed");
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteGroup(int id)
        {
            try
            {
                service.DeleteGroup(id);
                return StatusCode(200, "Deletion was successful");
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
        [HttpGet]
        [Route("student")]
        [Authorize(Roles = "student")]
        public IActionResult GetGroupStudent()
        {
            try
            {
                var id = User.FindFirst("id")?.Value;
                return Json(service.GetGroupUser(Convert.ToInt16(id)));
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
    }
}
