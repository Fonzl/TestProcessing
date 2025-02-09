using Database;
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

                Discipline = new DTO.DisciplineDto.DetailsDisciplineDto
                {
                    Id = test.Discipline.Id,
                    Name = test.Discipline.Name

                },
                Time = test.TimeInMinutes,
                EvaluationDtos = JsonSerializer.Deserialize<List<EvaluationDto>>(test.Evaluations),
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
            var test = new Test
            {
                Name = dto.Name,
                InfoTest = dto.InfoTest,
                Quests = context.Quests.Where(x => dto.Quests.Contains(x.Id)).ToList(),
                Discipline = context.Disciplines.First(x => x.Id == dto.DisciplineId),
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

        public List<TestDto> GetTestDiscipline(long disciplineId)//Получаем тесты по всё дисциплине.
        {
            var testList = context.Tests.Where(x => x.Discipline.Id == disciplineId).ToList();
            
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
    }
}
