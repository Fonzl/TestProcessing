using DTO.RoleDto;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "admin")]
        [Route("all")]

        public IActionResult GetAllRole()
        {
            try
            {
                return Json(service.GetAllRoles());
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }



        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult GetRole(short id)
        {
            try
            {
                return Json(service.GetRole(id));
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
        public IActionResult AddRole(CreateRoleDto dto)
        {
            try
            {
                service.CreateRole(dto);
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
        public IActionResult UpdateRole(UpdateRoleDto dto)
        {
            try
            {
                service.UpdateRole(dto);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
       
        
        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteRole(short id)
        {
            try
            {
                service.DeleteRole(id);
                return StatusCode(200, "Deletion was successful");
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
    }
}
