using Database;
using DTO.ResultTestDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryResultTest
{
    public class RepositoryResultTest(ApplicationContext context) : IRepositoryResultTest
    {
        public void Delete(int id)
        {
            var result = context.Results.First(r => r.Id == id);
            context.Results.Remove(result);
            context.SaveChanges();
        }

        public ResultTestDto GetResult(long id)
        {
            var result = context.Results
                .Include(x => x.User)
                .Include(x => x.Test)
                .First(x => x.Id == id);
            return (new ResultTestDto
            {
                Id = result.Id,
                Result = result.Result,
                Test = new DTO.TestDto.DetailsTestDto
                {
                    Id = result.Test.Id,
                    Name = result.Test.Name,
                },
                User = new DTO.UserDto.StudentUserDto
                {
                    Id = result.User.Id,
                   FullName = result.User.FullName,
                }

            });
        }

        public List<ResultTestDto> GetResults()
        {
           var results = context.Results
                .Include(x => x.User)
                .Include(x => x.Test)
                .ToList();
            var listResults = new List<ResultTestDto>();
            foreach (var result in results)
            {
                listResults.Add(new ResultTestDto
                {
                    Id = result.Id,
                    
                    Result = result.Result,
                    Test = new DTO.TestDto.DetailsTestDto
                    {
                        Id = result.Test.Id,
                        Name = result.Test.Name,
                    },
                    User = new DTO.UserDto.StudentUserDto
                    {
                        Id = result.User.Id,
                        FullName = result.User.FullName,
                    }

                });
            }
            return listResults;
        }

        public decimal GetStatisticsDiscipline(ResultStatisticsDto dto)// тут делаеться расчёт статистики 
        {
            var studentResultDiscipline = context.Results
                                            .Include(x => x.Test.Discipline)
                                            .Include(x => x.User)
                                            .Where(x => x.User.Id == dto.StudentId)
                                            .ToList();
            var results = studentResultDiscipline.Where(x => x.Test.Discipline.Id == dto.DisciplineId).ToList();
            decimal sum = 0;
            foreach (var result in results)
            {
                sum += result.Result;
            }
            decimal staticstic = sum / results.Count;
            return staticstic;
        }

        public void InsertStudent(AddResultTestStudentDto dto)// тут  расчёт result
        {
            var answer = context.Answers.Where(x => dto.AnsweId.Contains(x.Id)).ToList();
            var answerTrue = answer.Where(x => x.IsCorrectAnswer == true).ToList();
            var resulTest = (Convert.ToDecimal(answerTrue.Count)/ Convert.ToDecimal(answer.Count))*100;
            var result = new ResultTest
            {
               
                Result = resulTest,
                Test = context.Tests.First(x => x.Id == dto.TestId),
                User = context.Users.First(x => x.Id == dto.StudentId)
            };
            context.Results.Add(result);
            context.SaveChanges();
        }

        public List<ResultTestDto> ResultStudentId(long studentId)
        {
            var results = context.Results
               .Include(x => x.User)
               .Include(x => x.Test)
              .Where(x => x.User.Id == studentId).ToList();
            var studerResults = results.Where(x => x.User.Id == studentId).ToList();
            var listResults = new List<ResultTestDto>();
            foreach (var result in results)
            {
                listResults.Add(new ResultTestDto
                {
                    Id = result.Id,

                    Result = result.Result,
                    Test = new DTO.TestDto.DetailsTestDto
                    {
                        Id = result.Test.Id,
                        Name = result.Test.Name,
                    },
                   

                });
            }
           return listResults;
        }

        public void Update(UpdateResultTestDto dto)
        {
            var result = context.Results.First(x =>x.Id == dto.Id);

            result.Id = dto.Id;
            result.Result = dto.Result;
            result.Test = context.Tests.First(x => x.Id == dto.TestId);
            result.User = context.Users.First(x => x.Id == dto.UserId);
            context.Results.Update(result);
            context.SaveChanges();
        }
    }
}
