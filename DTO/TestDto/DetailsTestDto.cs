using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.TestDto
{
    public class DetailsTestDto : TestDto
    {
        public string InfoTest { get; set; }    

       
        public DisciplineDto.DetailsDisciplineDto? Discipline { get; set; }
        public long? Time {  get; set; }
        public List<EvaluationDto> EvaluationDtos { get; set; }
        public bool IsCheck { get; set; }

    }
}
