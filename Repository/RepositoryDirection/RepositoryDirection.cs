﻿using Database;
using DTO.DirectionDto;
using DTO.DisciplineDto;
using Microsoft.EntityFrameworkCore;
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
        public void CreatShedule(CreatShedule dto)
        {
            
            var shedule = new Schedule
            {
                Cours = dto.CoursId,
                DirectionId = dto.DirectionId,
                Direction = context.Directions.First(x => x.Id == dto.DirectionId),
            };
            context.Schedules.Add(shedule);
            context.SaveChanges();
        }

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
            var schedule = context.Schedules
                .Include(x => x.Disciplines)
                .FirstOrDefault( x => x.Cours == coursId && x.DirectionId == directionId);
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
        public List<ReturnScheduleDirectionShortDto> GetDirectionsSheduleShort()
        {
            var schedules = context.Schedules
                .Include(x => x.Disciplines)
                .Include(x => x.Direction);
            var listScheduleDirectionDto = new List<ReturnScheduleDirectionShortDto>();
            foreach (var schedule in schedules)
            {
                listScheduleDirectionDto.Add(new ReturnScheduleDirectionShortDto
                {
                    CoursId = schedule.Cours,
                    Direction = schedule.Direction.Name,
                    DirectionId = schedule.DirectionId
                });
            } 
             return listScheduleDirectionDto;
           
           
            
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
            var schedule = context.Schedules
                .Include(x => x.Disciplines)
                .Where(x => x.DirectionId == dto.DirectionId && x.Cours == dto.CoursId).FirstOrDefault();
            if (schedule == null) return;
            var listDiscipline = context.Disciplines.Where(x => dto.DisciplineId.Contains(x.Id)).ToList();
            schedule.Disciplines = listDiscipline;
            context.Schedules.Update(schedule);
            context.SaveChanges();

        }
    }
}
