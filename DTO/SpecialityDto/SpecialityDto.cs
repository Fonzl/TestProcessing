using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SpecialityDto
{
    public class SpecialityDto
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public List<TestDto.TestDto> Tests { get; set; }
    }
}
