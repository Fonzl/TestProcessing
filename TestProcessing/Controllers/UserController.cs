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
        public async Task<IActionResult> GetAllUsers(short idRole)
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
        public async Task<IActionResult> GetListStusentIdGroup(long idGroup)
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
        public  async Task<IActionResult> GetListStusentAttemptIdGroup(long idGroup, long idTest)//Отдаёт список студентов и их выший бал на тестирование
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
        public async Task<IActionResult> GetUser(int id)
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
        public async Task<IActionResult> GetTeacher(int id)
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
        public async Task<IActionResult> AddStudent(CreateStudentDto dto)
        {
            try
            {
                if( dto.checkingPassword == dto.Password)
                {
             
                    return Json(new DTO.GeneralDto.Login() { login = service.CreateStudent(dto) } );
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
        public async Task<IActionResult> AddTeacher(CreateTeacherDto dto)
        {
            try
            {
                if (dto.checkingPassword == dto.Password)
                {
                    
                    return   Json(new DTO.GeneralDto.Login() { login = service.CreateTeacher(dto) });
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
        public async Task<IActionResult> UpdateStudent(UpdateStudentDto dto)
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
        public async Task<IActionResult> UpdateTeacher(UpdateTeacherDto dto)
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
        public async Task<IActionResult> DeleteUser(int id)
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
        public async Task<IActionResult> Login(LoginUserDto dto)
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
        public async Task<IActionResult> PasswordСhange(PasswordСhangeDto dto)
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
