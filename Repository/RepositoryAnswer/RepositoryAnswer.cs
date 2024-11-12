using Database;
using DTO.AnswerDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryAnswer
{
    public class RepositoryAnswer(ApplicationContext context) : IRepositoryAnswer
    {
        public AnswerDto Answer(long id)
        {   var answer = context.Answers.First(a => a.Id == id);
            return new AnswerDto()
            {
                Id = answer.Id,
                AnswerText = answer.AnswerText,
                IsCorrectAnswer = answer.IsCorrectAnswer,
            };
        }

        public List<StudentAnsewerDto> Answers(long id)
        {
            var answersList = context.Answers.Where(x => x.Quest.Id == id).ToList();
            List<StudentAnsewerDto> studentAnsewers = new List<StudentAnsewerDto>();
            foreach( var ansewer in answersList)
            {
                studentAnsewers.Add(new StudentAnsewerDto
                {
                    Id = ansewer.Id,
                    AnswerText = ansewer.AnswerText
                });

            }
            return studentAnsewers;
        }

        public void Delete(long Id)
        {
            var answer = context.Answers.SingleOrDefault(x => x.Id == Id);
            if (answer == null) return;
            context.Answers.Remove(answer);
            context.SaveChanges();
        }

        public void Insert(CreateAnswerDto dto)
        {
            var answer = new Answer
            {
                AnswerText = dto.AnswerText,
                IsCorrectAnswer = dto.IsCorrectAnswer,
                Quest = context.Quests.First(x => x.Id == dto.QuestId)
            };
            context.Answers.Add(answer);
            context.SaveChanges();
        }

        public void InsertList(List<CreateAnswerDto> list)
        {
            foreach (var answer in list)
            {
                var answerAdd = new Answer
                {
                    AnswerText = answer.AnswerText,
                    IsCorrectAnswer = answer.IsCorrectAnswer,
                    Quest = context.Quests.First(x => x.Id == answer.QuestId)
                };
                context.Answers.Add(answerAdd);
                context.SaveChanges();
            }

        }

        public void Update(UpdateAnswerDto dto)
        {
            var answer = context.Answers.SingleOrDefault(x => x.Id == dto.Id);
            if (answer == null) return;
            answer.AnswerText = dto.AnswerText;
            answer.IsCorrectAnswer = dto.IsCorrectAnswer;
            context.Answers.Update(answer);
            context.SaveChanges();
        }
    }
}
