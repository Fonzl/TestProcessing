using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Service.ServiceUser;
using System.Security.Claims;
using DTO.UserDto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestProcessing.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController(IServiceUser service) : Controller
    {
        // GET: api/<ValuesController>
        
        [HttpGet]
        [Authorize]
        [Route("all")] 
        public IActionResult GetAllUsers()
        {

            return Json(service.GetUsers());
        }
        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            return Json(service.GetUser(id));
        }
        // POST api/<ValuesController>
        [HttpPost]
        [Route("add")]
        public IActionResult AddUser(CreateUserDto dto)
        {
            service.CreateUser(dto);
            return Ok("Done");
        }

        // PUT api/<ValuesController>/5
        [HttpPatch]
        [Route("update")]
        public IActionResult UpdateUser(UpdateUserDto dto)
        {
            service.UpdateUser(dto);
            return Ok("Done");
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            service.DeleteUser(id);
            return Ok("Done");
        }
        [HttpGet]
        [Route("login/{name}")]
        public IActionResult Login(string name)
        {
            return Json(service.Login(name));
        }
    }
}
