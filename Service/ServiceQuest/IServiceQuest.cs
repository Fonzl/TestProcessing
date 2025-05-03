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
        DetailsQuestDto GetQuest(long id);
        public ChekAnswerQuest ChekAnswerQuest(long questId);
        List<QuestDto> GetAllQuests();
        List<DetailsQuestDto> GetListQuests(long id);
        void DeleteQuest(long id);
        List<string>? UpdateQuest(UpdateQuestDto updateQuest);
        long CreateQuest(CreateQuestDto createQuest);
      
    }
}
