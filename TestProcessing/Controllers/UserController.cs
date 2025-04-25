using DTO.UserDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.ServiceUser;

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
        [HttpGet]
        [Route("ListStudent/Group/{idGroup}")]
        [Authorize(Roles = "admin,teacher")]
        public IActionResult GetListStusentIdGroup(long idGroup)
        {
            try
            {
                return Json(service.GetStudentIdGroup(idGroup));
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
        [HttpGet]
        [Route("ListStudent/Group/{idGroup}/Test/{idTest}")]
        [Authorize(Roles = "admin,teacher")]
        public IActionResult GetListStusentAttemptIdGroup(long idGroup, long idTest)//Отдаёт список студентов и их выший бал на тестирование
        {
            try
            {
                return Json(service.GetStudentAttempt(idGroup, idTest));
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
                var user = service.Login(dto.Name, dto.Password);
                if (user == null)
                {
                    return StatusCode(404, "Пользователь не найден");
                }


                return Json(user);

            }
            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }

        }
    }
}
