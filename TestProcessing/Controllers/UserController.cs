using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Service.ServiceUser;
using System.Security.Claims;
using DTO.UserDto;
using Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestProcessing.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController(IServiceUser service) : Controller
    {
        // GET: api/<ValuesController>
        
        [HttpGet]
        [Route("all")]
        [Authorize(Roles = "admin")]
        public IActionResult GetAllUsers()
        {

            return Json(service.GetUsers());
        }
        // GET api/<ValuesController>/5
        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        
        public IActionResult GetUser(int id)
        {
            return Json(service.GetUser(id));
        }
        // POST api/<ValuesController>
        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("add")]
        public IActionResult AddUser(CreateUserDto dto)
        {
            service.CreateUser(dto);
            return Ok("Done");
        }

        // PUT api/<ValuesController>/5
        [HttpPatch]
        [Authorize(Roles = "admin")]
        [Route("update")]
        public IActionResult UpdateUser(UpdateUserDto dto)
        {
            service.UpdateUser(dto);
            return Ok("Done");
        }
        [Authorize(Roles = "admin")]
        // DELETE api/<ValuesController>/5
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUser(int id)
        {
            service.DeleteUser(id);
            return Ok("Done");
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginUserDto dto)
        {
            return Json(service.Login(dto.Name, dto.Password));
        }
    }
}
