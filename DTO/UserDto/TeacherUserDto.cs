using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserDto
{
    public class TeacherUserDto:UserDto
    {
        public List<DisciplineDto.DetailsDisciplineDto>? Disciplines { get; set; }
        public RoleDto.RoleDto Role { get; set; }
    }
}
