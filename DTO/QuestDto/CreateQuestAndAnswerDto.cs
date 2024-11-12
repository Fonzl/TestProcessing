using DTO.AnswerDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.QuestDto
{
    public class CreateQuestAndAnswerDto
    {
        public CreateQuestDto questDto;
        public List<CreateAnswerDto> answerList;
    }
}
