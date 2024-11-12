using Database;
using DTO.AnswerDto;
using DTO.QuestDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryQuest
{
    public class RepositoryQuest(ApplicationContext context) : IRepositoryQuest
    {
        public void Delete(int id)
        {
            var quest = context.Quests.First(x => x.Id == id);
            context.Quests.Remove(quest);
            context.SaveChanges();
        }

        public List<QuestDto> GetAll()
        {
           var list = context.Quests.ToList();
            var result = new List<QuestDto>();
            foreach (var quest in list)
            {
                result.Add(new QuestDto
                {
                    Id = quest.Id,
                    Name = quest.Name,
                    Info = quest.Info
                });    
            }
            return result;
        }

        public DetailsQuestDto GetQuest(int id)
        {
            
            var quest = context.Quests
                .Include(x => x.Tests)
                .Include(x => x.Answers)
                .Include(x => x.CategoryTasks)
                .First(x => x.Id == id);
            return new DetailsQuestDto
            {
                Id = quest.Id,
                Name = quest.Name,
                Info = quest.Info,
                Answers = quest.Answers.Select(x => new AnswerDto
                {
                    Id = x.Id,
                    AnswerText = x.AnswerText
                }).ToList(),
                CategoryTasks = new DTO.CategoryTasksDto.CategoryTasksDto
                {
                    Id = quest.CategoryTasks.Id,
                    Name = quest.CategoryTasks.Name,
                },
            };
            
        }

        public void Insert(CreateQuestDto dto)
        {
            var quest = new Quest
            {
                
                Name = dto.Name,
                Info = dto.Info,
                Answers = context.Answers.Where(x => dto.Answers.Contains(x.Id)).ToList(),
                CategoryTasks = context.CategoryTasks.First(x => x.Id == dto.CategoryTaskId),
                Tests= context.Tests.Where(x => dto.Tests.Contains(x.Id)).ToList()

            }; 
            context.Quests.Add(quest);
            context.SaveChanges();
        }

        public void Update(UpdateQuestDto dto)
        {
            var quest = context.Quests.First(x => x.Id == dto.Id);


            quest.Name = dto.Name;
            quest.Info = dto.Info;
            quest.Answers = context.Answers.Where(x => dto.Answers.Contains(x.Id)).ToList();
            quest.CategoryTasks = context.CategoryTasks.First(x => x.Id == dto.CategoryTaskId);
            quest.Tests = context.Tests.Where(x => dto.Tests.Contains(x.Id)).ToList();

            
            context.Quests.Update(quest);
            context.SaveChanges();
        }
    }
}
