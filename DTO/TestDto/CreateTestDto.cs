using DTO.QuestDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.TestDto
{
    public class CreateTestDto
    {
        public string Name { get; set; }
        public string InfoTest { get; set; }
        public List<CreateQuestAndAnswerDto>? Quests { get; set; }
        public int DisciplineId { get; set; }
        public long? Time { get; set; }
        public List<EvaluationDto>? EvaluationDtos { get; set; }
        public bool IsCheck { get; set; }
    }
}
