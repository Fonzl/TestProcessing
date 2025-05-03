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
using Microsoft.Extensions.Configuration;

namespace Service.ServiceQuest
{
    public class ServiceQuest(IRepositoryQuest repo) : IServiceQuest
    {
        public ChekAnswerQuest ChekAnswerQuest(long questId)
        {
            return repo.ChekAnswerQuest(questId);
        }

        public long CreateQuest(CreateQuestDto createQuest)
        {
          
               return repo.Insert(createQuest);
            
        }

        public void DeleteQuest(long id)
        {
            repo.Delete(id);
        }

        public List<QuestDto> GetAllQuests()
        {
            return repo.GetAll();
        }

        public List<DetailsQuestDto> GetListQuests(long id)
        {
            return repo.GetListQuests(id);
        }

        public DetailsQuestDto GetQuest(long id)
        {
          return  repo.GetQuest(id);
        }

        
        
        public List<string>? UpdateQuest(UpdateQuestDto updateQuest)
        {
           return repo.Update(updateQuest);
        }
    }
}
