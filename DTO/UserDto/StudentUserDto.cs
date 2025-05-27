using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserDto
{
    public class StudentUserDto : UserDto
    {

        
        public GroupDto.GroupDto? Group { get; set; }
       
        public string login { get; set; }

    }
}
