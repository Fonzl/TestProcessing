using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.GroupDto
{
    public class UpdateGroupDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public short SpecialityId { get; set; }
        public DateTime StartDateOfTraining { get; set; }
        public DateTime EndOfTraining { get; set; }
        public short CourseId { get; set; }
        public List<long> Users { get; set; }
    }
}
