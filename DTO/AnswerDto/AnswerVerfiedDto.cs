using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.AnswerDto
{
   public class AnswerVerfiedDto
    {
        public long Id { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrectAnswer { get; set; }
        public bool IsResponeUser { get; set; } = false;

    }
}
