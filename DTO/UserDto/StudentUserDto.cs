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
        
       
        public GroupDto.DetailsGroupDto? Group { get; set; }
        public RoleDto.RoleDto Role { get; set; }
    }
}
