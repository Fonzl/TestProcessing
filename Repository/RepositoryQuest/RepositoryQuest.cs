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
        public ChekAnswerQuest ChekAnswerQuest(long questId)
        {
            var quest = context.Quests
                .Include(x => x.Answers)
                .Include(x => x.CategoryTasks)
                .FirstOrDefault(x => x.Id == questId);
            var listAnswer = new List<AnswerDto>();
            quest.Answers.ForEach(x =>
            {
                listAnswer.Add(new AnswerDto
                {
                    AnswerText = x.AnswerText,
                    IsCorrectAnswer = x.IsCorrectAnswer,
                    Id = x.Id

                });
            });
            return new ChekAnswerQuest
            {

                CategoryTasks = new DTO.CategoryTasksDto.CategoryTasksDto()
                {
                    Id = quest.CategoryTasks.Id,
                    Name = quest.CategoryTasks.Name,
                },
                Answers = listAnswer

            };

        }

        public void Delete(long id)
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

     

        public List<DetailsQuestDto> GetListQuests(long testId)
        {
            var test = context.Tests.First(x => x.Id == testId);
           var list = context.Quests
                .Include(x => x.Answers)
                .Include(x => x.CategoryTasks)
                .Where(x => x.Tests.Contains(test)).ToList();
            
            
            var result = new List<DetailsQuestDto>();
            foreach (var quest in list)
            {
                var listAnswer = new List<AnswerShortDto>();
                if (quest.CategoryTasks.Id == 3)
                {
                   
                    quest.Answers.ForEach(x => listAnswer.Add(new AnswerShortDto
                    {
                        Id = x.Id,
                        AnswerText = null,
                        PathPhoto = x.PathToImage
                    })

                    );
                }
                else
                {
                    
                    quest.Answers.ForEach(x => listAnswer.Add(new AnswerShortDto
                    {
                        Id = x.Id,
                        AnswerText = x.AnswerText,
                        PathPhoto = x.PathToImage
                    })

                    );
                }
                    result.Add(new DetailsQuestDto
                    {
                        Id = quest.Id,
                        Name = quest.Name,
                        Info = quest.Info,
                        Answers = listAnswer,
                        CategoryTasks = new DTO.CategoryTasksDto.CategoryTasksDto
                        {
                            Id = quest.CategoryTasks.Id,
                            Name = quest.CategoryTasks.Name,
                        },
                        PathImg = quest.PathToImage




                    });
                
            }
            return result;
        }

        public DetailsQuestDto GetQuest(long id)
        {
            
            var quest = context.Quests
                .Include(x => x.Tests)
                .Include(x => x.Answers)
                .Include(x => x.CategoryTasks)
                .First(x => x.Id == id);
            if (quest.CategoryTasks.Id == 3)
            {
                return new DetailsQuestDto
                {

                    Id = quest.Id,
                    Name = quest.Name,
                    Info = quest.Info,
                    Answers = quest.Answers.Select(x => new AnswerShortDto
                    {
                        Id = x.Id,
                        AnswerText = null,
                        PathPhoto = x.PathToImage,

                    }).ToList(),
                    CategoryTasks = new DTO.CategoryTasksDto.CategoryTasksDto
                    {
                        Id = quest.CategoryTasks.Id,
                        Name = quest.CategoryTasks.Name,
                    },
                    PathImg = quest.PathToImage
                };
            }
            return new DetailsQuestDto
            {
                
                Id = quest.Id,
                Name = quest.Name,
                Info = quest.Info,
                Answers = quest.Answers.Select(x => new AnswerShortDto
                {
                    Id = x.Id,
                    AnswerText = x.AnswerText,
                    PathPhoto = x.PathToImage,

                }).ToList(),
                CategoryTasks = new DTO.CategoryTasksDto.CategoryTasksDto
                {
                    Id = quest.CategoryTasks.Id,
                    Name = quest.CategoryTasks.Name,
                },
                PathImg = quest.PathToImage
            };
            
        }

        public long Insert(CreateQuestDto dto)
        {
            var listAnswer = new List<Answer>();

            var quest = new Quest
            {


                Name = dto.Name,
                Info = dto.Info,
                Answers = listAnswer,
                CategoryTasks = context.CategoryTasks.First(x => x.Id == dto.CategoryTaskId),
                Tests = context.Tests.Where(x => dto.Tests.Contains(x.Id)).ToList(),
                PathToImage = dto.PathPhotos


            }; 
            context.Quests.Add(quest);
            context.SaveChanges();
            return quest.Id;
        }

        public List<string>? Update(UpdateQuestDto dto)
        {
            var quest = context.Quests.Include(x => x.Tests).First(x => x.Id == dto.Id);
            var listPhotoDelete = quest.PathToImage.Where(x => dto.PathPhotos.Contains(x) == false).ToList();
            quest.Name = dto.Name;
            quest.Info = dto.Info;
            quest.CategoryTasks = context.CategoryTasks.First(x => x.Id == dto.CategoryTaskId);
            quest.Tests = context.Tests.Where(x => dto.Tests.Contains(x.Id)).ToList(); 
            quest.PathToImage = dto.PathPhotos;
            context.Quests.Update(quest);
            context.SaveChanges();
            return listPhotoDelete;
        }
    }
}
