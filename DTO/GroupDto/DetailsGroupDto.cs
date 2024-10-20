using DTO.CourseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.GroupDto
{
    public class DetailsGroupDto : GroupDto
    {
        
        public DateTime StartDateOfTraining { get; set; }
        public DateTime EndOfTraining { get; set; }
        public List<UserDto.UserDto> Users{ get; set; }
        public SpecialityDto.SpecialityDto Speciality { get; set; }
        public CourseDto.CourseDto Course { get; set; }
    }
}
