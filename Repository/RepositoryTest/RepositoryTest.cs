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
                UserAttempt = test.NumberOfAttempts,

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
            List<TestDto> list = new List<TestDto>();
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

        public long Insert(CreateTestDto dto)
        {

                
            var test = new Test
            {
                Name = dto.Name,
                InfoTest = dto.InfoTest,
                
                Discipline = context.Disciplines.First(x => x.Id == dto.DisciplineId),
                TimeInMinutes = dto.Time,
                Evaluations = JsonSerializer.Serialize(dto.EvaluationDtos),
                IsCheck = dto.IsCheck,
                NumberOfAttempts = dto.NumberOfAttempts,

               

            };
            context.Tests.Add(test);
            context.SaveChanges();
            return test.Id;
        }

        public void Update(UpdateTestDto dto)
        {
            var test = context.Tests.First(x => x.Id == dto.Id);
            test.InfoTest = dto.InfoTest;
            test.Name = dto.Name;
            test.Discipline = context.Disciplines.First(x => x.Id == dto.DisciplineId);
           test.Evaluations = JsonSerializer.Serialize(dto.EvaluationDtos);
            test.IsCheck = dto.IsCheck;
            test.NumberOfAttempts = dto.NumberOfAttempts;
            test.TimeInMinutes = dto.Time;
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

        public List<TestDto> GetTestDiscipline(long disciplineId, long IdUsser)//Получаем тесты по всей дисциплине.
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
                    .Include(x => x.Quests)
             .Where(x => x.Discipline.Id == disciplineId).ToList().ForEach(x =>
             {
                 if(x.Quests.ToList().Count == 0 || x.Quests == null)
                 {
                     return;
                 }
                 if (x.NumberOfAttempts == null)
                 {
                     List.Add(x);
                 }
                
                 else
                 {
                     var AttemptTest = result.FirstOrDefault(y =>
                     y.Test.Id == x.Id && y.User.Id == user.Id);
                     if (AttemptTest == null)                        //такой логики не должно быть в системе  но из-за дропов бд она появилась 
                     {
                         List.Add(x);
                         return;
                     }
                       
                     if (x.NumberOfAttempts > AttemptTest.Responses.Count)
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
            else
            {
                var List = context.Tests
                .Where(x => x.Discipline.Id == disciplineId).ToList();
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
        }

        public DetailsTestDto GetTestStudentDto(long testId, long IdUsser)
        {
            var user = context.Users.First(x => x.Id == IdUsser);
            var test = context.Tests
                .Include(x => x.Discipline)
                .SingleOrDefault(x => x.Id == testId);
            var result = context.Results
               .Include(x => x.Responses)
               .Include(x => x.Test)
               .Where(x => x.User.Id == IdUsser).ToList();
            if (test == null) return null;
            if(result.FirstOrDefault(y => y.Test.Id == test.Id && y.User.Id == user.Id) == null)
            {
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
                    UserAttempt = test.NumberOfAttempts 

                });
            }
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
                UserAttempt = test.NumberOfAttempts - result.FirstOrDefault(y => y.Test.Id == test.Id && y.User.Id == user.Id).Responses.Count ,
                    
            });
        }
    }
}
