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
        DetailsQuestDto GetQuest(int id);
        void Update(UpdateQuestDto dto);
        void Insert(CreateQuestDto dto);
        void Delete(int id);
    }
}
