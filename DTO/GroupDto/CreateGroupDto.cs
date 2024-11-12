using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.GroupDto
{
    public class CreateGroupDto
    {
        public string Name { get; set; }
        public DateTime StartDateOfTraining { get; set; }
        public DateTime EndOfTraining { get; set; }
        public List<long> Users { get; set; }
        public short SpecialityId { get; set; }
        public short CourseId { get; set; }
    }
}
