using DTO.DirectionDto;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.ServiceDirection;

namespace TestProcessing.Controllers
{
    [Route("Direction")]
    [ApiController]
    public class DirectionController(IServiceDirection service) : Controller
    {
        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("all")]

        public async Task<IActionResult> GetAllDirectionss()
        {
            try
            {
                return Json(service.GetDirections());
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }



        // GET api/<ValuesController>/5
        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDirections(int id)
        {
            try
            {
                return Json(service.GetDirection(id));
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("addSchedule")]
        public async Task<IActionResult> AddSchedule(CreatShedule dto)
        {
            try
            {
                service.CreatShedule(dto);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("add")]
        public async Task<IActionResult> AddDirections(CreateDirectionDto dto)
        {
            try
            {
                service.CreateDirection(dto);
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
        public async Task<IActionResult> UpdateDirections(UpdateDirectionDto dto)
        {
            try
            {
                service.UpdateDirection(dto);
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
        public async Task<IActionResult> DeleteDirection(int id)
        {
            try
            {
                service.DeleteDirections(id);
                return StatusCode(200, "Deletion was successful");
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
        [HttpPatch]
        [Authorize(Roles = "admin")]
        [Route("updateShedule")]
        public async Task<IActionResult> AddDirections(UpdateScheduleDirectionDto dto)
        {
            try
            {
                service.UpdateSchedule(dto);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("GetDirectionsShedule/{directionId}/Cours/{corusId}")]
        public async Task<IActionResult> GetDirectionsShedule(int corusId, int directionId)
        {
            try
            {
                return Json(service.GetDirectionsShedule(corusId, directionId));

            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("AllListShedules")]

        public async Task<IActionResult> GetAllShedules()
        {
            try
            {
                return Json(service.GetDirectionsSheduleShort());
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
    }
}
