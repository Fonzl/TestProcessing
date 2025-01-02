using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserDto
{
    public class UserDto
    {
        public long Id { get; set; }

        public string FullName { get; set; }
        public RoleDto.RoleDto Role { get; set; }
    }
}
