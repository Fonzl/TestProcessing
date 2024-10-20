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
        private readonly ApplicationContext _context = context;
        private DbSet<Answer> _answer = context.Set<Answer>();

        public List<StudentAnsewerDto> Answers(long id)
        {
            var answersList = _answer.Where(x => x.Quest.Id == id).ToList();
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
            var answer = _answer.SingleOrDefault(x => x.Id == Id);
            if (answer == null) return;
            _answer.Remove(answer);
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
            _answer.Add(answer);
            context.SaveChanges();
        }

        public void Update(UpdateAnswerDto dto)
        {
            var answer = _answer.SingleOrDefault(x => x.Id == dto.Id);
            if (answer == null) return;
            answer.AnswerText = dto.AnswerText;
            answer.IsCorrectAnswer = dto.IsCorrectAnswer;
            _answer.Update(answer);
            context.SaveChanges();
        }
    }
}
