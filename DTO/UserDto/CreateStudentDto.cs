using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserDto
{
    public class CreateStudentDto
    {
        public string FullName { get; set; }
        public string Password { get; set; }

        public long? Group { get; set; }

    }
}
