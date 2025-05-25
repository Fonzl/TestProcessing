using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserDto
{
    public class UpdateTeacherDto : ShortUserDto
    {
        public List<int>? Disciplines { get; set; }
    }
}
