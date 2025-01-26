using DTO.QuestDto;
using Repository.RepositoryQuest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceQuest
{
    public class ServiceQuest(IRepositoryQuest repo) : IServiceQuest
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

        public List<QuestDto> GetListQuests(int id)
        {
            return repo.GetListQuests(id);
        }

        public DetailsQuestDto GetQuest(int id)
        {
          return  repo.GetQuest(id);
        }

        public void UpdateQuest(UpdateQuestDto updateQuest)
        {
            repo.Update(updateQuest);
        }
    }
}
