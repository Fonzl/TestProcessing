using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DisciplineDto
{
    public class DisciplineDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TestDto.TestDto> Tests { get; set; }
        public List<UserDto.UserDto> Users { get; set; }
    }
}
