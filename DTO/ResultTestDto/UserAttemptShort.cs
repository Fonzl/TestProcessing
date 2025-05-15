using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResultTestDto
{
    public class UserAttemptShort
    {
        public UserDto.ShortUserDto User { get; set; }
        public decimal? result {  get; set; }
    }
}
