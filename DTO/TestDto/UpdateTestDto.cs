using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.QuestDto;

namespace DTO.TestDto
{
    public class UpdateTestDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string InfoTest { get; set; }
        public int DisciplineId { get; set; }
        public long? Time { get; set; }
        public List<EvaluationDto>? EvaluationDtos { get; set; }
        public bool IsCheck { get; set; }
        public long? NumberOfAttempts { get; set; }
    }
}
