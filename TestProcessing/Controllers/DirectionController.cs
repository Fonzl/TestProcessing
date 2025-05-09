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

        public IActionResult GetAllDirectionss()
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
        public IActionResult GetDirections(int id)
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
        //[Authorize(Roles = "admin")]
        //[HttpGet("DirectionsShedule/{id}")]
        //public IActionResult GetDirectionsShedule(int id)
        //{
        //    try
        //    {
        //        return Json(service.GetDirection(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(520, ex.Message);
        //    }
        //}
        // POST api/<ValuesController>
        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("add")]
        public IActionResult AddDirections(CreateDirectionDto dto)
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
        public IActionResult UpdateDirections(UpdateDirectionDto dto)
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
        public IActionResult DeleteDirection(int id)
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
        public IActionResult AddDirections(UpdateScheduleDirectionDto dto)
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
        public IActionResult GetDirectionsShedule(int corusId, int directionId)
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

        public IActionResult GetAllShedules()
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
