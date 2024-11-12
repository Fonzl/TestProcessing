using DTO.AnswerDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceAnswer
{
    public interface IServiceAnswer
    {
        AnswerDto GetAnswer(long id);
        List<StudentAnsewerDto> GetQuestAnswers(long id);
        void AnswerListCreate(List<CreateAnswerDto> answerList);
        void DeleteAnswer(long id);
        void UpdateAnswer(UpdateAnswerDto updateAnswerDto);
        void CreateAnswer(CreateAnswerDto createAnswerDto);
    }
}
