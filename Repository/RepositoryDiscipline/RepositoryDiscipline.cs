using Database;
using DTO.DisciplineDto;
using DTO.TestDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryDiscipline
{
    public class RepositoryDiscipline(ApplicationContext context) : IRepositoryDiscipline
    {
        public void Delete(int id)
        {
           var dic = context.Disciplines.First(d => d.Id == id);
            context.Disciplines.Remove(dic);
            context.SaveChanges();
        }

        public List<DisciplineDto> GetAll()
        {
            var list = context.Disciplines.ToList();
            var dcList = new List<DisciplineDto>(); //Лист дисциплин
            foreach (var d in list)
            {
                dcList.Add(new DisciplineDto
                {
                    Id = d.Id,
                    Name = d.Name
                });
            }
            return dcList;
        }

        public DetailsDisciplineDto Get(int Id)
        {
            var dc = context.Disciplines
                .Include(x => x.Users)
                .Include(x => x.Tests)
                .First(x => x.Id == Id);
            return new DetailsDisciplineDto
            {
                Id = dc.Id,
                Name = dc.Name,
                Tests = dc.Tests.Select(x => new DTO.TestDto.DetailsTestDto()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList(),
                Users = dc.Users.Select(x => new DTO.UserDto.StudentUserDto()
                {
                  Id= x.Id,
                FullName = x.FullName,
                }).ToList(),
            };
            
        }

        public void Insert(CreateDisciplineDto dto)
        {
            var dc = new Discipline
            {
                Name = dto.Name,
                Tests = context.Tests.Where(x => dto.Tests.Contains(x.Id)).ToList(),
                Users = context.Users.Where(x => dto.Tests.Contains(x.Id)).ToList(),
            };
            context.Disciplines.Add(dc);
            context.SaveChanges();
        }

        public void Update(UpdateDisciplineDto dto)
        {
           var dc = context.Disciplines.First( x => x.Id == dto.Id );
            dc .Name = dto.Name;
            dc.Tests = context.Tests.Where(x => dto.Tests.Contains(x.Id)).ToList();
            dc.Users = context.Users.Where(x => dto.Tests.Contains(x.Id)).ToList();
        }

        public List<DisciplineDto> TeacherGet(long id)
        {
            var teacher = context.Users
                .Include(x => x.Disciplines)
                .First(x => x.Id == id);
            var list = context.Disciplines.Where(x =>  teacher.Disciplines.Contains(x)).ToList();
            var dcList = new List<DisciplineDto>();
            foreach (var d in list)
            {
                dcList.Add(new DisciplineDto
                {
                    Id = d.Id,
                    Name = d.Name
                });
            }
            return dcList;
        }

        public List<DisciplineDto> StudentGet(long id)
        {
            var student = context.Users
                 .Include(x => x.Group)
                 .First(x => x.Id == id);
           var shedule = context.Schedules.
                Include(x => x.Direction)
                .First(x => x.DirectionId == student.Group.DirectionId && x.Cours == student.Group.Cours);
            var list = context.Disciplines.Include(x => x.Schedules )
                .Where(x =>  x.Schedules.Contains(shedule)).ToList();
            var dcList = new List<DisciplineDto>();
            foreach (var d in list)
            {
                dcList.Add(new DisciplineDto
                {
                    Id = d.Id,
                    Name = d.Name
                });
            }
            return dcList;
        }
    }
}
