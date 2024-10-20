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
        public short CourseId { get; set; }
        public List<long> Specialties { get; set; }
        public List<long> Quests { get; set; }
        public short? DisciplineId { get; set; }
    }
}
