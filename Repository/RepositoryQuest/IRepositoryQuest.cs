using DTO.QuestDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryQuest
{
    public interface IRepositoryQuest
    {
        List<QuestDto> GetAll();
        List<DetailsQuestDto> GetListQuests(int testId);
        DetailsQuestDto GetQuest(int id);
        List<string>? Update(UpdateQuestDto dto);
        long Insert(CreateQuestDto dto);
        void Delete(int id);
    }
}
