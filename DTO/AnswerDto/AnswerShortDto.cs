using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.AnswerDto
{
    public class AnswerShortDto
    {
        public long Id { get; set; }
        public string AnswerText { get; set; }
        public List<string> PathPhoto { get; set; }
    }
}
