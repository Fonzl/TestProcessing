using Database;
using DTO.QuestDto;
using DTO.TestDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Repository.RepositoryTest
{
    public class RepositoryTest(ApplicationContext context) : IRepositoryTest
    {

        public DetailsTestDto GetTestDto(long id)
        {
            var test = context.Tests
                .Include(x => x.Discipline)
                .Include(x => x.Quests)
                .SingleOrDefault(x => x.Id == id);
            if (test == null) return null;
            
            return (new DetailsTestDto
            {
                Id = test.Id,
                InfoTest = test.InfoTest,
                Name = test.Name,

                Discipline = new DTO.DisciplineDto.DisciplineDto
                {
                    Id = test.Discipline.Id,
                    Name = test.Discipline.Name

                },
                Time = test.TimeInMinutes,
                IsCheck = test.IsCheck,

            });
            

            
        }

        public void Delete(long id)
        {
            var test = context.Tests.First(x => x.Id == id);
            context.Tests.Remove(test);
            context.SaveChanges();

        }

        public List<TestDto> GetTestList()
        {
            var testList = context.Tests;
            List<TestDto > list = new List<TestDto>();
            foreach (var test in testList)
            {
                TestDto dto = new TestDto
                {
                    Id = test.Id,
                    Name = test.Name
                };
                list.Add(dto);
            }
            return list;  
        }

        public void Insert(CreateTestDto dto)
        {
            List<Quest> list = new List<Quest>();
            foreach (var item in dto.Quests)
            {
                List<Answer> listAnswer = new List<Answer>();
                foreach(var answer in item.answerList)
                {
                    listAnswer.Add(new Answer
                    {
                        AnswerText = answer.AnswerText,
                        IsCorrectAnswer = answer.IsCorrectAnswer,

                    });
                }
                list.Add(new Quest
                {

                    Answers = listAnswer,
                    CategoryTasks = context.CategoryTasks.First(x => x.Id == item.questDto.CategoryTaskId),
                    Info = item.questDto.Info,
                    Name = item.questDto.Name,
                    
                });

            }
            var test = new Test
            {
                Name = dto.Name,
                InfoTest = dto.InfoTest,
                Quests = list,
                Discipline  = context.Disciplines.First(x => x.Id == dto.DisciplineId),
                TimeInMinutes = dto.Time,
                Evaluations = JsonSerializer.Serialize(dto.EvaluationDtos),
                IsCheck = dto.IsCheck, 
                
                
            };
            context.Tests.Add(test);
            context.SaveChanges();
        }

        public void Update(UpdateTestDto dto)
        {
            var test = context.Tests.First(x => x.Id == dto.Id);
            test.InfoTest = dto.InfoTest;
            test.Name = dto.Name;
            test.Discipline = context.Disciplines.First(x => x.Id == dto.DisciplineId);
            test.Quests = context.Quests.Where(x => dto.Quests.Contains(x.Id)).ToList();
            context.Tests.Update(test);
            context.SaveChanges();

        }
        //public List<TestDto> GetTestStudent(long id)
        //{
        //    var user = context.Users
        //        .Include(x => x.Group)
        //        .Include(x => x.Group.Disciplines)
        //        .First(x => x.Id == id);
        //    var tes = context
        //        .Tests
        //        .Include(x => x.Discipline);
        //    var test = tes.Where(x => user.Group.Disciplines.Contains(x.Discipline)).ToList();
        //    List<TestDto> list = new List<TestDto>();
        //    foreach (var item in test)
        //    {
        //        TestDto dto = new TestDto
        //        {
        //            Id = item.Id,
        //            Name = item.Name
        //        };
        //        list.Add(dto);
        //    }
        //    return list;
           
        //}

        public List<TestDto> GetTestDiscipline(long disciplineId, long IdUsser)//Получаем тесты по всё дисциплине.
        {
            var user = context.Users.First(x => x.Id == IdUsser);
           var result = context.Results
                .Include(x => x.Responses)
                .Include(x => x.Test)
                .Where(x => x.User.Id == IdUsser).ToList();
       
            if (user.RoleId == 3)
            {

                var List = new List<Test>();

                context.Tests
             .Where(x => x.Discipline.Id == disciplineId).ToList().ForEach(x =>
             {
                 if (x.NumberOfAttempts == null)
                 {
                     List.Add(x);
                 }
                 else
                 {
                     var AttemptTest = result.First(y =>
                     y.Test.Id == x.Id && y.User.Id == user.Id).Responses.Count;
                     if (x.NumberOfAttempts < AttemptTest)
                     {
                         List.Add(x);
                     }
                 }

             }


                    );
                List<TestDto> list = new List<TestDto>();
                foreach (var test in List)
                {
                    TestDto dto = new TestDto
                    {
                        Id = test.Id,
                        Name = test.Name
                    };
                    list.Add(dto);
                }
                return list.OrderBy(x => x.Id).ToList();
            }
            else {
                var List = context.Tests
                .Where(x => x.Discipline.Id == disciplineId).ToList();
                List <TestDto> list = new List<TestDto>();
                foreach (var test in List)
                {
                    TestDto dto = new TestDto
                    {
                        Id = test.Id,
                        Name = test.Name
                    };
                    list.Add(dto);
                }
                return list.OrderBy(x => x.Id).ToList();
            }
        }
    }
}
