using DTO.CategoryTasksDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.ServiceCategoryTasks;

namespace CategoryTasksProcessing.Controllers
{
    [Route("CategoryTasks")]
    [ApiController]
    public class CategoryTasksController(IServiceCategoryTasks service) : Controller
    {
        [Authorize]
        [HttpGet]
        [Route("all")]

        public async Task<IActionResult> GetAllCategoryTaskss()
        {
            try
            {
                return Json(service.GetCategoryTasks());
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }



        // GET api/<ValuesController>/5
        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryTasks(int id)
        {
            try
            {
                return Json(service.GetCategory(id));
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
        public async Task<IActionResult> AddCategoryTasks(CreateCategoryTasksDto dto)
        {
            try
            {
                service.CreateCategory(dto);
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
        public async Task<IActionResult> UpdateCategoryTasks(UpdateCategoryTasksDto dto)
        {
            try
            {
                service.UpdateCategory(dto);
                return StatusCode(200, "The content has been changed");
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }

        // DELETE api/<ValuesController>/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryTasks(int id)
        {
            try
            {
                service.DeleteCategory(id);
                return StatusCode(200, "Deletion was successful");
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
    }
}
