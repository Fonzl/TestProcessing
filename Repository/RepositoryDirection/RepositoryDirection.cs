using Database;
using DTO.DirectionDto;
using DTO.DisciplineDto;
using Repository.RepositoryDirection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryDirection
{
    public class RepositoryDirection(ApplicationContext context):IRepositoryDirection { 
        


            public void Delete(int id)
            {
                var directions = context.Directions.SingleOrDefault(c => c.Id == id);
                if (directions == null) return;
                context.Directions.Remove(directions);
                context.SaveChanges();

            }

            public List<DirectionDto> GetAll()
            {
                var list = context.Directions.ToList();
                var Direction = new List<DirectionDto>();
                foreach (var task in list)
                {
                    Direction.Add(new DirectionDto
                    {
                        Id = task.Id,
                        Name = task.Name,
                    });
                }
                return Direction;
            }

            public DirectionDto GetDirection(int id)
            {
                var directions = context.Directions.First(c => c.Id == id);
                return new DirectionDto()
                {
                    Id = directions.Id,
                    Name = directions.Name,
                };
            }

        public ReturnScheduleDirection GetDirectionsShedule(int coursId, int directionId)
        {
            var schedule = context.Schedules.FirstOrDefault( x => x.Cours == coursId && x.DirectionId == directionId);
            var listDisciplineDto = new List<DisciplineDto>();
            schedule.Disciplines.ForEach(x =>
            {
                var disciplineDto = new DisciplineDto { Id = x.Id, Name = x.Name };
                listDisciplineDto.Add(disciplineDto);
            });
            return new ReturnScheduleDirection()
            {
                CoursId = coursId,
                DirectionId = directionId,
                Direction = context.Directions.SingleOrDefault(x => x.Id == directionId).Name,
                Discipline = listDisciplineDto
            };
        }

        public void GetShedule(UpdateScheduleDirectionDto dto)
        {
            throw new NotImplementedException();
        }

        public void Insert(CreateDirectionDto dto)
            {
                var directions = new Direction
                {
                    Name = dto.DirectionName,
                };
                context.Directions.Add(directions);
                context.SaveChanges();
            }
            public void Update(UpdateDirectionDto dto)
            {
                var directions = context.Directions.SingleOrDefault(x => x.Id == dto.Id);
                if (directions == null) return;
                directions.Name = dto.Name;
                context.Directions.Update(directions);
                context.SaveChanges();

            }

        public void UpdateShedule(UpdateScheduleDirectionDto dto)
        {
            var shedule = context.Schedules.Where(x => x.DirectionId == dto.DirectionId && x.Cours == dto.CoursId).FirstOrDefault();
            if (shedule == null) return;
            shedule.Disciplines.Clear();
            shedule.Disciplines.AddRange(context.Disciplines.Where(x => dto.DisciplineId.Contains(x.Id)).ToList());
            context.Schedules.Add(shedule);
            context.SaveChanges();

        }
    }
}
