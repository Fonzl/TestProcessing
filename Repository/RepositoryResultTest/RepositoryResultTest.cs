using Database;
using DTO.QuestDto;
using DTO.ResultTestDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
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
                .Include(x => x.Responses)
                .First(x => x.Id == id);
            return (new ResultTestDto
            {
                Id = result.Id,
                Result = result.Responses.Result,
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
                 .Include(x => x.Responses)
                 .ToList();
            var listResults = new List<ResultTestDto>();
            foreach (var result in results)
            {
                listResults.Add(new ResultTestDto
                {
                    Id = result.Id,


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
                                            .Include(x => x.Responses)
                                            .Where(x => x.User.Id == dto.StudentId)
                                            .ToList();
            var results = studentResultDiscipline.Where(x => x.Test.Discipline.Id == dto.DisciplineId).ToList();
            decimal sum = 0;
            foreach (var result in results)
            {
                sum += result.Responses.Result;
            }
            decimal staticstic = sum / results.Count;
            return staticstic;

        }

        public ReturnResultAndRespone InsertStudent(AddResultTestStudentDto dto)// тут  расчёт result
        {
            //var resulTest = new ResultTest
            //{

            //};

            List<UserRespone> responses = dto.UserRespones.ToList();
            List<long> answer = new List<long>();
            List<string> d = new List<string>();
            //foreach (var response in responses) {
            //    foreach (var r in response.UserRespones)
            //    {
            //        d.Add(r);
            //    }

            //    answer.Add((long)Convert.ToDouble(response.UserRespones.ToList()));//Вытаскиваем id для вопросов.Пока делаем ток так ,но потом это будет одним из способов в зависимости от категории вопросаю 
            //}
            responses.ForEach(x => {
                x.UserRespones.ForEach
                (x => { d.Add(x); });
                });
                d.ForEach(x => {
                    answer.Add((long)Convert.ToDouble(x));//Вытаскиваем id для вопросов.Пока делаем ток так ,но потом это будет одним из способов в зависимости от категории вопросаю 
                });
                var answers = context.Answers.Where(x => answer.Contains(x.Id)).ToList();
                var answerTrue = answers.Where(x => x.IsCorrectAnswer == true).ToList();// все правельные ответы 
                var resulTest = (Convert.ToDecimal(answerTrue.Count) / Convert.ToDecimal(answer.Count)) * 100;// результат в процентах 
                var userResponses = new UserResponses
                {
                    ListUserResponses = JsonSerializer.Serialize(responses),
                    Result = resulTest,

                };
                var result = new ResultTest
                {

                    Responses = userResponses,
                    Test = context.Tests.First(x => x.Id == dto.TestId),
                    User = context.Users.First(x => x.Id == dto.StudentId)
                };

                context.Results.Add(result);
                context.SaveChanges();
            var listVerifiedRespouns = new List<VerifiedUserRespones>();
           foreach (var r in responses )
            {
                List<long> idAnswer = new List<long>();
                bool correctdefault = false;
                foreach(var v in r.UserRespones)
                {
                    idAnswer.Add((long)Convert.ToDouble(v));
                }
                
                if (answerTrue.Any(x => idAnswer.Any(y => y == x.Id)))
                {
                    correctdefault = true;
                }
                listVerifiedRespouns.Add(new VerifiedUserRespones
                {
                    UserRespones = answers.Where(x => idAnswer.Contains(x.Id)).Select(x => x.AnswerText).ToList(),
                    QuestDto = new QuestDto
                    {
                        Id = r.QuestId,
                        Info = context.Quests.FirstOrDefault(x => x.Id == r.QuestId).Info,
                        Name = context.Quests.FirstOrDefault(x => x.Id == r.QuestId).Name
                    },
                    IsCorrectAnswer = correctdefault,
                });
            }
            return new ReturnResultAndRespone
            {
                Result = resulTest,
                Respones = listVerifiedRespouns
            };
           }

        public List<ResultTestDto> ResultStudentId(long studentId)
            {
                var results = context.Results
                   .Include(x => x.User)
                   .Include(x => x.Test)
                   .Include(x => x.Responses)
                  .Where(x => x.User.Id == studentId).ToList();
                var studerResults = results.Where(x => x.User.Id == studentId).ToList();
                var listResults = new List<ResultTestDto>();
                foreach (var result in results)
                {
                    listResults.Add(new ResultTestDto
                    {
                        Id = result.Id,
                        Result = result.Responses.Result,

                        Test = new DTO.TestDto.DetailsTestDto
                        {
                            Id = result.Test.Id,
                            Name = result.Test.Name,
                        },


                    });
                }
                return listResults;
            }

        public ReturnResultAndRespone returnResultDetails(long idResulTest)
        {
            var respons = context.UserResponses.FirstOrDefault(x => x.Id == idResulTest);
            List<UserRespone> responses = JsonSerializer.Deserialize<List<UserRespone>>(respons.ListUserResponses);
            List<long> answer = new List<long>();
            List<string> d = new List<string>();
            responses.ForEach(x => {
                x.UserRespones.ForEach
                (x => { d.Add(x); });
            });
            d.ForEach(x => {
                answer.Add((long)Convert.ToDouble(x));//Вытаскиваем id для вопросов.Пока делаем ток так ,но потом это будет одним из способов в зависимости от категории вопросаю 
            });
            var answers = context.Answers.Where(x => answer.Contains(x.Id)).ToList();
            var answerTrue = answers.Where(x => x.IsCorrectAnswer == true).ToList();// все правельные ответы 
            var listVerifiedRespouns = new List<VerifiedUserRespones>();
            foreach (var r in responses)
            {
                List<long> idAnswer = new List<long>();
                bool correctdefault = false;
                foreach (var v in r.UserRespones)
                {
                    idAnswer.Add((long)Convert.ToDouble(v));
                }

                if (answerTrue.Any(x => idAnswer.Any(y => y == x.Id)))
                {
                    correctdefault = true;
                }
                listVerifiedRespouns.Add(new VerifiedUserRespones
                {
                     UserRespones = answers.Where(x => idAnswer.Contains(x.Id)).Select(x => x.AnswerText).ToList(),
                    QuestDto = new QuestDto
                    {
                        Id = r.QuestId,
                        Info = context.Quests.FirstOrDefault(x => x.Id == r.QuestId).Info,
                        Name = context.Quests.FirstOrDefault(x => x.Id == r.QuestId).Name
                    },
                    IsCorrectAnswer = correctdefault,
                });
            }
            return new ReturnResultAndRespone
            {
                Result = respons.Result,
                Respones = listVerifiedRespouns
            };
        

    }

    public void Update(UpdateResultTestDto dto)
            {
                var result = context.Results.First(x => x.Id == dto.Id);

                result.Id = dto.Id;

                result.Test = context.Tests.First(x => x.Id == dto.TestId);
                result.User = context.Users.First(x => x.Id == dto.UserId);
                context.Results.Update(result);
                context.SaveChanges();
            }
        }
    }

