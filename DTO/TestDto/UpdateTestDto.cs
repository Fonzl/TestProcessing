using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.TestDto
{
    public class UpdateTestDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string InfoTest { get; set; }
        public CourseDto.CourseDto Course { get; set; }
        public List<SpecialityDto.SpecialityDto> Specialties { get; set; }
        public List<QuestDto.QuesrDto> Quests { get; set; }
        public DisciplineDto.DisciplineDto Discipline { get; set; }
    }
}
