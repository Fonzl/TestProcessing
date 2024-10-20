using Database;
using DTO.CourseDto;
using DTO.TestDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                .Include(x => x.Specialties)
                .SingleOrDefault(x => x.Id == id);
            if (test == null) return null;
            return (new DetailsTestDto
            {
                Id = test.Id,
                InfoTest = test.InfoTest,
                Name = test.Name,
                Course = new CourseDto
                {
                    Id = test.Course.Id
                },
                Discipline = new DTO.DisciplineDto.DetailsDisciplineDto
                {
                    Id = test.Discipline.Id,
                    Name= test.Discipline.Name
                    
                },
                Specialties = test.Specialties.Select(x => new DTO.SpecialityDto.SpecialityDto
                {
                    Id= x.Id,
                    Name= x.Name,

                }).ToList(),
                Quests = test.Quests.Select(x => new DTO.QuestDto.QuestDto { 
                    Id=x.Id,
                    Name = x.Name } ).ToList()
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
                Course = context.Courses.First(x => x.Id == dto.CourseId),
                Specialties = context.Specialities.Where(x => dto.Specialties.Contains(x.Id)).ToList(),
                Quests = context.Quests.Where(x => dto.Quests.Contains(x.Id)).ToList(),
                Discipline = context.Disciplines.First(x => x.Id == dto.DisciplineId)
            };
            context.Tests.Add(test);
            context.SaveChanges();
        }

        public void Update(UpdateTestDto dto)
        {
            var test = context.Tests.First(x => x.Id == dto.Idi);

        }

     
    }
}
