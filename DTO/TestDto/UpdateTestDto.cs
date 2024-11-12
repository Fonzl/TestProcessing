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
        public short CourseId {  get; set; }
        public List<short> Specialties { get; set; }
        public List<long> Quests { get; set; }
        public int DisciplineId { get; set; }
    }
}
