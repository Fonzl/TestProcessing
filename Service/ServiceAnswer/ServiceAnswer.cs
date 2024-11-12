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
        public void CreateAnswer(CreateAnswerDto createAnswerDto)
        {
            repo.Insert(createAnswerDto);
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

        public void UpdateAnswer(UpdateAnswerDto updateAnswerDto)
        {
            repo.Update(updateAnswerDto);
        }

        public void AnswerListCreate(List<CreateAnswerDto> answerList)
        {
            repo.InsertList(answerList);
        }
    }
}
