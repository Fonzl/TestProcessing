
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
        public List<UserDto.StudentUserDto> Users{ get; set; }
        public int Cours { get; set; }
        public string Direction { get; set; }
       
    }
}
