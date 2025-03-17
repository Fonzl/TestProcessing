using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserDto
{
    public class StudentAttemptResultDto : ShortUserDto
    {
        public decimal? Result { get; set; }
        public long? IdAttempt { get; set; }
    }
}
