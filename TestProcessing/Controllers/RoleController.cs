using DTO.RoleDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.ServiceRole;

namespace TestProcessing.Controllers
{
    [Route("Role")]
    [ApiController]
    public class RoleController(IServiceRole service) : Controller
    {
        [HttpGet]
        [Route("all")]

        public IActionResult GetAllRole()
        {

            return Json(service.GetAllRoles());
        }



        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult GetRole(short id)
        {
            return Json(service.GetRole(id));
        }

        // POST api/<ValuesController>
        [HttpPost]
        [Route("add")]
        public IActionResult AddRole(CreateRoleDto dto)
        {
            service.CreateRole(dto);
            return Ok("Done");
        }

        // PUT api/<ValuesController>/5
        [HttpPatch]
        [Route("update")]
        public IActionResult UpdateRole(UpdateRoleDto dto)
        {
            service.UpdateRole(dto);
            return Ok("Done");
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRole(short id)
        {
            service.DeleteRole(id);
            return Ok("Done");
        }
    }
}
