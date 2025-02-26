using DTO.AnswerDto;
using DTO.AnswerDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Service.ServiceAnswer;


namespace TestProcessing.Controllers
{
    [Route("Answer")]
    [ApiController]
    
    public class AnswerController(IServiceAnswer serviceAnswer, IConfiguration configuration, IWebHostEnvironment appEnvironment) : Controller
    {
        
        
        [HttpGet]
        [Authorize]
        [Route("Answer/{id}")]

        public IActionResult GetQuestAnswers(long id)
        {
            try
            {
                return Json(serviceAnswer.GetQuestAnswers(id));
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }


       
        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult GetAnswer(int id)
        {
            try
            {
                return Json(serviceAnswer.GetAnswer(id));
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }

        // POST api/<ValuesController>
        [HttpPost]
        [Route("addAnswer")]
        public IActionResult AddAnswer(List<CreateAnswerDto> dto)
        {
            try
            {
                serviceAnswer.AnswerListCreate(dto);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
        [HttpPost]
        [Authorize(Roles = "teacher,admin")]
        [Route("add")]
        public IActionResult AddAnswer([FromForm] CreateAnswerDto dto, Microsoft.AspNetCore.Http.IFormFileCollection? uploadedFile, IWebHostEnvironment env)
        {
            dto.PathToImg = new List<string>();
            try
            {
                List<string> list = new List<string>();
                var StringSettings = configuration.GetSection("ConnectionStrings");//
                foreach (var file in uploadedFile)
                {
                    var myUniqueFileName = $@"{Guid.NewGuid()}";//генерируем имя
                                                                // путь к папке Files
                    string path = appEnvironment.ContentRootPath + StringSettings["FilePatchShortAnswer"] + myUniqueFileName + file.FileName;// myUniqueFileName+"."+uploadedFile.ContentType;
                                                                                                                                            // сохраняем файл в папку Files 
                    using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    var s = StringSettings["ServerConnectkingString"];
                    dto.PathToImg.Add(s + "/Answer/postAnswerImg/" + myUniqueFileName + file.FileName);
                }


                serviceAnswer.CreateAnswer(dto);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }

        // PUT api/<ValuesController>/5
        [Authorize(Roles = "teacher,admin")]
        [HttpPatch]
        [Route("update")]
        public IActionResult UpdateAnswer(UpdateAnswerDto dto)
        {
            try
            {
                serviceAnswer.UpdateAnswer(dto);
                return StatusCode(200, "The content has been changed");
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }

        // DELETE api/<ValuesController>/5
        [Authorize(Roles = "teacher,admin")]
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteAnswer(int id)
        {
            try
            {
                serviceAnswer.DeleteAnswer(id);
                return StatusCode(200, "Deletion was successful");
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
    
        [HttpGet]
        [Route("postAnswerImg/{nameImg}")]

        public IActionResult RetunPhoto(string nameImg)//по id test
        {
            var tokenSettings = configuration.GetSection("ConnectionStrings");
            try
            {
                var appPath = appEnvironment.ContentRootPath;
                string path = appPath + tokenSettings["FilePatchShortAnswer"] + "\\" + nameImg;
                byte[] mas = System.IO.File.ReadAllBytes(path);
                string type;
                new FileExtensionContentTypeProvider().TryGetContentType(path, out type);
                return File(mas, type);

            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
    }
}

