using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Service.ServiceUser;
using System.Security.Claims;
using DTO.UserDto;
using Database;
using Microsoft.Extensions.Configuration;

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
            try
            {
                return Json(service.GetUsers());
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
        // GET api/<ValuesController>/5
        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        
        public IActionResult GetUser(int id)
        {
            try
            {
                return Json(service.GetUser(id));
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
        public IActionResult AddUser(CreateUserDto dto)
        {
            try
            {
                service.CreateUser(dto);
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
        public IActionResult UpdateUser(UpdateUserDto dto)
        {
            try
            {
               
                service.UpdateUser(dto);
                return StatusCode(200, "The content has been changed");
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }

        }
        [Authorize(Roles = "admin")]
        // DELETE api/<ValuesController>/5
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                service.DeleteUser(id);
                return StatusCode(200, "Deletion was successful");
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }

        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginUserDto dto)
        {
            try
            {
             
                return Json(service.Login(dto.Name, dto.Password));
               
            }
            catch (Exception ex)
            {
                return StatusCode(404,ex.Message);
            }

        }
    }
}
