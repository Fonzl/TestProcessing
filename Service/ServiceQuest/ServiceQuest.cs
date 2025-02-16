using DTO.QuestDto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Repository.RepositoryQuest;
using System.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceQuest
{
    public class ServiceQuest(IRepositoryQuest repo, IWebHostEnvironment appEnvironment) : IServiceQuest
    {
        public void CreateQuest(CreateQuestDto createQuest)
        {
            repo.Insert(createQuest);
        }

        public void DeleteQuest(int id)
        {
            repo.Delete(id);
        }

        public List<QuestDto> GetAllQuests()
        {
            return repo.GetAll();
        }

        public List<DetailsQuestDto> GetListQuests(int id)
        {
            return repo.GetListQuests(id);
        }

        public DetailsQuestDto GetQuest(int id)
        {
          return  repo.GetQuest(id);
        }

        public string QuestImg(Microsoft.AspNetCore.Http.IFormFile uploadedFile)
        {
            
            

                // путь к папке Files
                string path = "\\FileTest\\QuestImg\\" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                {
                     uploadedFile.CopyToAsync(fileStream);
                }
              string  FotoPath  =   path ;
               return FotoPath;
            
            
        }

        public void UpdateQuest(UpdateQuestDto updateQuest)
        {
            repo.Update(updateQuest);
        }
    }
}
