using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.AnswerDto
{
    public class AnswerDto
    {
        public long Id { get; set; }
        public string AnswerText { get; set; }
        public long QuestId {  get; set; }
        public QuestDto.DetailsQuestDto Quest { get; set; }
        public bool IsCorrectAnswer { get; set; }

    }
}
