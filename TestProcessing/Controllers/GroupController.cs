using DTO.GroupDto;
using Microsoft.AspNetCore.Authorization;
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

        public async Task<IActionResult> GetAllGroups()
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

        public async Task<IActionResult> GetGroup(int id)
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
        public async Task<IActionResult> GetDisciplineGroup(int id)
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
        public async Task<IActionResult> AddGroup(CreateGroupDto dto)
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
        public async Task<IActionResult> UpdateGroup(UpdateGroupDto dto)
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
        public async Task<IActionResult> DeleteGroup(int id)
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
        public async Task<IActionResult> GetGroupStudent()
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
