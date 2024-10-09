using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResultTestDto
{
    public class ResultTestDto
    {
        public long Id { get; set; }
        public UserDto.UserDto User { get; set; }
        public TestDto.TestDto Test { get; set; }
        public decimal Result { get; set; }
    }
}
