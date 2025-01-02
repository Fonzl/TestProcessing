using DTO.CategoryTasksDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.ServiceCategoryTasks;

namespace CategoryTasksProcessing.Controllers
{
    [Route("CategoryTasks")]
    [ApiController]
    public class CategoryTasksController(IServiceCategoryTasks service) : Controller
    {
        [HttpGet]
        [Route("all")]

        public IActionResult GetAllCategoryTaskss()
        {

            return Json(service.GetCategoryTasks());
        }



        // GET api/<ValuesController>/5
        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public IActionResult GetCategoryTasks(int id)
        {
            return Json(service.GetCategory(id));
        }

        // POST api/<ValuesController>
        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("add")]
        public IActionResult AddCategoryTasks(CreateCategoryTasksDto dto)
        {
            service.CreateCategory(dto);
            return Ok("Done");
        }

        // PUT api/<ValuesController>/5
        [HttpPatch]
        [Authorize(Roles = "admin")]
        [Route("update")]
        public IActionResult UpdateCategoryTasks(UpdateCategoryTasksDto dto)
        {
            service.UpdateCategory(dto);
            return Ok("Done");
        }

        // DELETE api/<ValuesController>/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteCategoryTasks(int id)
        {
            service.DeleteCategory(id);
            return Ok("Done");
        }
    }
}
