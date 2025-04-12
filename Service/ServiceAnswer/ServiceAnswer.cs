using DTO.AnswerDto;
using Repository.RepositoryAnswer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceAnswer
{
    public class ServiceAnswer(IRepositoryAnswer repo ) : IServiceAnswer
    {
        public long CreateAnswer(CreateAnswerDto createAnswerDto)
        {
          return  repo.Insert(createAnswerDto);
        }

        public void DeleteAnswer(long id)
        {
            repo.Delete(id);
        }

        public List<StudentAnsewerDto> GetQuestAnswers(long id)
        {
            return repo.Answers(id);
        }

        public AnswerDto GetAnswer(long id)
        {
           return repo.Answer(id);
        }

        public List<string>? UpdateAnswer(UpdateAnswerDto updateAnswerDto)
        {
           return repo.Update(updateAnswerDto);
        }

        public void AnswerListCreate(List<CreateAnswerDto> answerList)
        {
            repo.InsertList(answerList);
        }
    }
}
