using Database;
using DTO.AnswerDto;
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
                var answersUser = context.Answers.Where(x => answer.Contains(x.Id)).ToList();
                var answerTrue = answersUser.Where(x => x.IsCorrectAnswer == true).ToList();// все правельные ответы 
                var resulTest = (Convert.ToDecimal(answerTrue.Count) / Convert.ToDecimal(answer.Count)) * 100;// результат в процентах 
                string evaluationName = "";
                var listTestEvaluationName = new List<DTO.TestDto.EvaluationDto>();
                var test = context.Tests.FirstOrDefault(x => x.Id == dto.TestId);
            if (test.Evaluations != null)
                {
                    listTestEvaluationName = JsonSerializer.Deserialize<List<DTO.TestDto.EvaluationDto>>(test.Evaluations);
                    evaluationName = listTestEvaluationName.Where(x => x.Percent <= resulTest).ToList().Max(x => x.EvaluationName);
                }
            var userResponses = new UserResponses
                {
                    ListUserResponses = JsonSerializer.Serialize(responses),
                    Result = resulTest,
                    EvaluationName = evaluationName,

                };
                var result = new ResultTest
                {

                    Responses = userResponses,
                    Test = context.Tests.First(x => x.Id == dto.TestId),
                    User = context.Users.First(x => x.Id == dto.StudentId),
                    
                };

                context.Results.Add(result);
                context.SaveChanges();
            var listVerifiedRespouns = new List<VerifiedUserResponesDto>();
            var ListQuest = context.Quests.Where(x => responses.Select(y => y.QuestId).ToList().Contains(x.Id)).Include(x => x.Answers).ToList();
            foreach (var Quest in ListQuest)
            {
                bool correctdefault = false;


                List<AnswerVerfiedDto> answerVerfiedUser = new List<AnswerVerfiedDto>();
                Quest.Answers.ForEach(x =>
                {
                    answerVerfiedUser.Add(new AnswerVerfiedDto()
                    {
                        Id = x.Id,
                        AnswerText = x.AnswerText,
                        IsCorrectAnswer = x.IsCorrectAnswer,
                        IsResponeUser = answersUser.Contains(x)

                    });
                });

                if (Quest.Answers.Where(x => x.IsCorrectAnswer == true).All(x => answerVerfiedUser.Where(y => y.IsResponeUser == true).Select(y => y.Id).Contains(x.Id)))
                {
                    correctdefault = true;
                }
                listVerifiedRespouns.Add(new VerifiedUserResponesDto
                {
                    UserRespones = answerVerfiedUser,
                    QuestDto = new QuestDto
                    {
                        Id = Quest.Id,
                        Info = Quest.Info,
                        Name = Quest.Name
                    },
                    IsCorrectQuest = correctdefault,
                });
            }
            
            
         
            return new ReturnResultAndRespone
            {
                Result = resulTest,
                EvaluationName = evaluationName,
                ListRespones = listVerifiedRespouns,
                Attempts = context.Results.Where(x => x.Test.Id == dto.TestId && x.User.Id == dto.StudentId).ToList().Count,
            };
          
           }

       

        public List<VerifiedUserResponesDto> returnResultDetails(long idResulTest)
        {
            var respons = context.UserResponses
                .Include(x => x.ResultTest.Test)
                .FirstOrDefault(x => x.Id == idResulTest);
            List<UserRespone> listUserRespons = JsonSerializer.Deserialize<List<UserRespone>>(respons.ListUserResponses);
            List<long> answer = new List<long>();
            List<string> d = new List<string>();
            listUserRespons.ForEach(x => {
                x.UserRespones.ForEach
                (x => { d.Add(x); });
            });
            d.ForEach(x => {
                answer.Add((long)Convert.ToDouble(x));//Вытаскиваем id ответов пользователя.Пока делаем ток так ,но потом это будет одним из способов в зависимости от категории вопросаю 
            });
            var answersUser = context.Answers.Where(x => answer.Contains(x.Id)).ToList();
           
            var listVerifiedRespouns = new List<VerifiedUserResponesDto>();
            var ListQuest = context.Quests.Where(x => x.Tests.Contains(respons.ResultTest.Test)).Include(x => x.Answers).ToList();
            foreach (var Quest in ListQuest)
            {
               bool correctdefault = false;


                List<AnswerVerfiedDto> answerVerfiedUser = new List<AnswerVerfiedDto>();
                Quest.Answers.ForEach(x => 
                {
                    answerVerfiedUser.Add(new AnswerVerfiedDto()
                    {
                        Id = x.Id,
                        AnswerText = x.AnswerText,
                        IsCorrectAnswer = x.IsCorrectAnswer,
                        IsResponeUser = answersUser.Contains(x)

                    });
                });
               
                if (Quest.Answers.Where(x => x.IsCorrectAnswer == true).All( x =>   answerVerfiedUser.Where(y => y.IsResponeUser == true).Select(y => y.Id).Contains(x.Id)))
                {
                     correctdefault = true;
                }
                listVerifiedRespouns.Add(new VerifiedUserResponesDto
                {
                     UserRespones = answerVerfiedUser,
                    QuestDto = new QuestDto
                    {
                        Id = Quest.Id,
                        Info = Quest.Info,
                        Name = Quest.Name
                    },
                    IsCorrectQuest = correctdefault,
                });
            }
            return listVerifiedRespouns;
        

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

