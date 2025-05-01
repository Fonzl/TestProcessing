using Database;
using DTO.AnswerDto;
using DTO.GeneralDto;
using DTO.QuestDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Service.ServiceAnswer;
using Service.ServiceQuest;

namespace TestProcessing.Controllers
{
    [Route("Quest")]
    [ApiController]
    public class QuestController(IServiceQuest serviceQuest, IServiceAnswer serviceAnswer, IConfiguration configuration, IWebHostEnvironment appEnvironment) : Controller
    {
        [HttpGet]
        [Authorize(Roles = "teacher,admin")]
        [Route("all")]

        public IActionResult GetAllQuests()
        {
            try
            {
                return Json(serviceQuest.GetAllQuests());
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }


        [Authorize(Roles = "teacher,student")]
        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult GetQuest(int id)
        {
            try
            {
                return Json(serviceQuest.GetQuest(id));
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
        public IActionResult AddQuest([FromForm] CreateQuestDtoShort dto, Microsoft.AspNetCore.Http.IFormFileCollection? uploadedFile)
        {

            try
            {
                CreateQuestDto createQuestDto = new CreateQuestDto() {
                    CategoryTaskId = dto.CategoryTaskId,
                    Info = dto.Info,
                    Name = dto.Name,
                    Tests = dto.Tests,
                    PathPhotos = new List<string>()
                };
                List<string> list = new List<string>();
                var StringSettings = configuration.GetSection("ConnectionStrings");//
                foreach (var file in uploadedFile)
                {
                    var myUniqueFileName = $@"{Guid.NewGuid()}";//генерируем имя

                    var fileExtension = Path.GetExtension(file.FileName);
                    string path = StringSettings["FilePatchwwwroot"] + StringSettings["FilePatchShortQuest"] + myUniqueFileName + fileExtension;// myUniqueFileName+"."+uploadedFile.ContentType;
                                                                                                                                                // сохраняем файл в папку Files 
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    list.Add(StringSettings["FilePatchShortQuest"] + myUniqueFileName + fileExtension);
                }
                
                list.ForEach(x =>
                {
                    createQuestDto.PathPhotos.Add(x);
                });
                return Json(new IdDto
                {
                    Id = serviceQuest.CreateQuest(createQuestDto)
                });
                
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
        public IActionResult UpdateQuest([FromForm] UpdateQuestDto dto, Microsoft.AspNetCore.Http.IFormFileCollection? uploadedFile)
        {
            try
            {
                dto.PathPhotos = new List<string>();
                List<string> list = new List<string>();
                var StringSettings = configuration.GetSection("ConnectionStrings");//
                foreach (var file in uploadedFile)
                {
                    var myUniqueFileName = $@"{Guid.NewGuid()}";//генерируем имя
                                                                // путь к папке Files
                    var fileExtension = Path.GetExtension(file.FileName);
                    string path = StringSettings["FilePatchwwwroot"] + StringSettings["FilePatchShortQuest"] + myUniqueFileName + fileExtension;// myUniqueFileName+"."+uploadedFile.ContentType;
                                                                                                                                                // сохраняем файл в папку Files 
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    list.Add(StringSettings["FilePatchShortQuest"] + myUniqueFileName + fileExtension);
                }
              

                list.ForEach(x =>
                {
                    dto.PathPhotos.Add(x);
                });
                var listDelete = serviceQuest.UpdateQuest(dto);
                if (listDelete == null)
                {
                    return StatusCode(200, "The content has been changed");
                }
                else
                {
                    foreach (var item in listDelete)
                    {

                        FileInfo fileInfo = new FileInfo(StringSettings["FilePatchwwwroot"] + item);
                        if (fileInfo.Exists)
                        {
                            DateTime newTime = DateTime.Now;

                            fileInfo.CreationTime = newTime;
                            fileInfo.LastWriteTime = newTime;
                            fileInfo.LastAccessTime = newTime;
                            // альтернатива с помощью класса File
                            // File.Delete(path);
                        }


                    }
                    return StatusCode(200, "The content has been changed");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }

        // DELETE api/<ValuesController>/5
        [Authorize(Roles = "teacher,admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteQuest(int id)
        {
            try
            {
                serviceQuest.DeleteQuest(id);
                return StatusCode(200, "Deletion was successful");
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
        [HttpGet]
        [Route("getListQuests/{id}")]
        [Authorize(Roles = "teacher,student")]
        public IActionResult GetListQuest(int id)//по id test
        {
            try
            {
                return Json(serviceQuest.GetListQuests(id));
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex.Message);
            }
        }
        //[HttpGet]
        //[Route("postQuestImg/{nameImg}")]

        //public IActionResult ReturnPhoto(string nameImg)//по id test
        //{
        //    var tokenSettings = configuration.GetSection("ConnectionStrings");
        //    try
        //    {
        //        var appPath = appEnvironment.ContentRootPath;
        //        string path = appPath + tokenSettings["FilePatchShortQuest"] + "\\" + nameImg;
        //        byte[] mas = System.IO.File.ReadAllBytes(path);
        //        string type;
        //        new FileExtensionContentTypeProvider().TryGetContentType(path, out type);
        //        return File(mas, type);

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(520, ex.Message);
        //    }
        //}
        //[HttpDelete]
        //[Route("DeleteQuestImg")]

        //public IActionResult DeletePhoto(string nameImg)//по id test
        //{
        //    var tokenSettings = configuration.GetSection("ConnectionStrings");
        //    try
        //    {
        //        var appPath = appEnvironment.ContentRootPath;
        //        string path = appPath + tokenSettings["FilePatchShortQuest"] + "\\" + nameImg;
        //        FileInfo fileInf = new FileInfo(path);
        //        if (fileInf.Exists)
        //        {
        //            fileInf.Delete();
        //            // альтернатива с помощью класса File
        //            // File.Delete(path);
        //        }

        //        return StatusCode(200, "Deletion was successful");

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(520, ex.Message);
        //    }

        //}

        //public List<string> SaveFile(Microsoft.AspNetCore.Http.IFormFileCollection? uploadedFile)
        //{
        //    List<string> list = new List<string>();
        //    var StringSettings = configuration.GetSection("ConnectionStrings");//
        //    foreach (var file in uploadedFile)
        //    {
        //        var myUniqueFileName = $@"{Guid.NewGuid()}";//генерируем имя
        //                                                    // путь к папке Files
        //        var fileExtension = Path.GetExtension(file.FileName);
        //        string path = appEnvironment.ContentRootPath + StringSettings["FilePatchShortQuest"] + myUniqueFileName + fileExtension;// myUniqueFileName+"."+uploadedFile.ContentType;
        //                                                                                                                                // сохраняем файл в папку Files 
        //        using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
        //        {
        //            file.CopyTo(fileStream);
        //        }
        //        list.Add(StringSettings["FilePatchShortQuest"] + myUniqueFileName + fileExtension);
        //    }
        //    return list;
        //}

    }
}
