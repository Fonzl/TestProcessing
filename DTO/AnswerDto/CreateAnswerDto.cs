using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.AnswerDto
{
    public class CreateAnswerDto: CreateAnswerDtoShort
    {
     
        public List<string> PathToImg { get;set; }

    } 
}
