using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserDto
{
    public class CreateTeacherDto
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        public List<int>? Disciplines { get; set; }
    }
}
