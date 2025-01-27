using DTO.QuestDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceQuest
{
    public interface IServiceQuest
    {
        DetailsQuestDto GetQuest(int id);
        List<QuestDto> GetAllQuests();
        List<DetailsQuestDto> GetListQuests(int id);
        void DeleteQuest(int id);
        void UpdateQuest(UpdateQuestDto updateQuest);
        void CreateQuest(CreateQuestDto createQuest);
    }
}
