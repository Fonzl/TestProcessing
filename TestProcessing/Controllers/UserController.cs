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
        [Route("allRoleUser/{idRole}")]
        [Authorize(Roles = "admin")]
        public IActionResult GetAllUsers(short idRole)
        {
            try
            {
                return Json(service.GetUsers(idRole));
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
        [Authorize(Roles = "admin,teacher")]
        [HttpGet("student/{id}")]
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
        [Authorize(Roles = "admin")]
        [HttpGet("teacher/{id}")]
        public IActionResult GetTeacher(int id)
        {
            try
            {
                return Json(service.GetTeacher(id));
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
        // POST api/<ValuesController>
        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("addStudent")]
        public IActionResult AddStudent(CreateStudentDto dto)
        {
            try
            {
                if( dto.checkingPassword == dto.Password)
                {
                    service.CreateStudent(dto);
                    return StatusCode(201);
                }
                else
                {
                    throw new Exception("Пароли не совподают");
                }
               
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }

        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("addTeacher")]
        public IActionResult AddTeacher(CreateTeacherDto dto)
        {
            try
            {
                if (dto.checkingPassword == dto.Password)
                {
                    service.CreateTeacher(dto);
                    return StatusCode(201);
                }
                else
                {
                    throw new Exception("Парли не совподают");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }

        }
        // PUT api/<ValuesController>/5
        [HttpPatch]
        [Authorize(Roles = "admin")]
        [Route("updateStudent")]
        public IActionResult UpdateStudent(UpdateStudentDto dto)
        {
            try
            {

                service.UpdateStudent(dto);
                return StatusCode(200, "The content has been changed");
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }

        }
        [HttpPatch]
        [Authorize(Roles = "admin")]
        [Route("updateTeacher")]
        public IActionResult UpdateTeacher(UpdateTeacherDto dto)
        {
            try
            {

                service.UpdateTeacher(dto);
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
                var user = service.Login(dto.login, dto.Password);
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
        [Authorize(Roles = "admin")]
        [HttpPatch]
        [Route("PasswordСhange")]
        public IActionResult PasswordСhange(PasswordСhangeDto dto)
        {
            try
            {
                if(dto.Password == dto.PasswordConfirmation)
                {
                    service.PasswordСhange(dto);
                    return StatusCode(201, "Password update");
                }
                else
                {
                    throw new Exception("Пароли не совпадают");
                }
               

            }
            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }

        }
    }
}
