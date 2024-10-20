using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserDto
{
    public class CreateUserDto
    {
        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string LastName { get; set; }

        public long? Group { get; set; }

        public List<long> Disciplines { get; set; }
        public short Role { get; set; }
    }
}
