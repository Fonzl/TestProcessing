using Database;
using DTO.AnswerDto;
using DTO.QuestDto;
using DTO.ResultTestDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Data;
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
            var ListAttempts = new List<ResultOfAttemptsDTO>();
            result.Responses.ForEach(x =>
            {
                ListAttempts.Add(new ResultOfAttemptsDTO
                {
                    IdUserRespones = x.Id,
                    Attempts = result.Responses.FindIndex(y => y.Id == x.Id),
                    EvaluationName = x.EvaluationName,
                    Result = (decimal)x.Result,
                });
                
            });
            return (new ResultTestDto
            {
                Id = result.Id,
                Result = ListAttempts,


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
            { if (result.Responses.OrderByDescending(x => x.Result).First().Result == null)
                {
                    sum += 0;
                }
             else
                { sum += (decimal)result.Responses.OrderByDescending(x => x.Result).First().Result; }
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
                var ListAttempts = new List<ResultOfAttemptsDTO>();
                result.Responses.ForEach(x =>
                {
                    ListAttempts.Add(new ResultOfAttemptsDTO
                    {
                        IdUserRespones = x.Id,
                        Attempts = result.Responses.FindIndex(y => y.Id == x.Id),
                        EvaluationName = x.EvaluationName,
                        Result = (decimal)x.Result
                    });

                });
                listResults.Add(new ResultTestDto
                {
                    Id = result.Id,
                    Result = ListAttempts,

                    Test = new DTO.TestDto.DetailsTestDto
                    {
                        Id = result.Test.Id,
                        Name = result.Test.Name,
                    },


                });
            }
            return listResults;
        }
        public ResultOfAttemptsDTO InsertStudent(AddResultTestStudentDto dto)// тут  расчёт result
        {
            if (context.Tests.Include(x => x.Quests)
                .FirstOrDefault(x => x.Id == dto.TestId).Quests.Count == dto.UserResponesTest.Count) {
                //var resulTest = new ResultTest
                //{

                //};
                List<UserRespons> responses = new List<UserRespons>();
                if (dto.UserResponesTest.ToList() == null)
                {
                     responses = JsonSerializer.Deserialize<List<UserRespons>>(
                        context.UserResponses.First(x => x.Id == dto.idResult).ListUserResponses
                    );
                }
                else
                {
                     responses = dto.UserResponesTest.ToList();
                }

                //foreach (var response in responses) {
                //    foreach (var r in response.UserRespones)
                //    {
                //        d.Add(r);
                //    }

                //    answer.Add((long)Convert.ToDouble(response.UserRespones.ToList()));//Вытаскиваем id для вопросов.Пока делаем ток так ,но потом это будет одним из способов в зависимости от категории вопросаю 
                //}




                var listVerifiedRespons = new List<VerifiedUserResponesDto>();
                var ListQuest = context.Quests.Where(x => responses.Select(y => y.QuestId).ToList().Contains(x.Id)).Include(x => x.Answers).Include(y => y.CategoryTasks).ToList();
                foreach (var Quest in ListQuest)
                {
                    switch (Quest.CategoryTasks.Id)
                    {
                        case 1:
                            bool correctdefault = false;


                            List<AnswerVerfiedDto> answerVerfiedUser = new List<AnswerVerfiedDto>();
                            Quest.Answers.ForEach(x =>
                            {
                                bool isResponeUser;
                            
                                if (responses.First(x => x.QuestId == Quest.Id).UserRespones == null )
                                {
                                    isResponeUser = false;
                                }
                                else
                                {
                                    isResponeUser = responses.First(x => x.QuestId == Quest.Id).UserRespones.Select(y => (long)Convert.ToDouble(y))
                                    .ToList().Contains(x.Id);
                                }
                                answerVerfiedUser.Add(new AnswerVerfiedDto()
                                {
                                    Id = x.Id,
                                    AnswerText = x.AnswerText,
                                    IsCorrectAnswer = x.IsCorrectAnswer,
                                    IsResponeUser = isResponeUser

                                });
                            });
                            if (Quest.Answers.Where(x => x.IsCorrectAnswer == true).All(x => answerVerfiedUser.Where(y => y.IsResponeUser == true).Select(y => y.Id).Contains(x.Id)))
                            {
                                correctdefault = true;
                            }
                            listVerifiedRespons.Add(new VerifiedUserResponesDto
                            {
                                UserRespones = answerVerfiedUser.Cast<AnswerVerfiedDto>().ToList(),
                                QuestDto = new QuestDto
                                {
                                    Id = Quest.Id,
                                    Info = Quest.Info,
                                    Name = Quest.Name
                                },
                                IsCorrectQuest = correctdefault,
                            });
                            break;
                        case 2:
                            bool correctdefault2 = false;


                            List<AnswerVerfiedDto> answerVerfiedUser2 = new List<AnswerVerfiedDto>();
                            Quest.Answers.ForEach(x =>
                            {
                                bool isResponeUser;

                                if (responses.First(x => x.QuestId == Quest.Id).UserRespones == null)
                                {
                                    isResponeUser = false;
                                }
                                else
                                {
                                    isResponeUser = responses.First(x => x.QuestId == Quest.Id).UserRespones.Select(y => (long)Convert.ToDouble(y))
                                    .ToList().Contains(x.Id);
                                }
                                answerVerfiedUser2.Add(new AnswerVerfiedDto()
                                {
                                    Id = x.Id,
                                    AnswerText = x.AnswerText,
                                    IsCorrectAnswer = x.IsCorrectAnswer,
                                    IsResponeUser = isResponeUser

                                });
                            });
                            if (Quest.Answers.Where(x => x.IsCorrectAnswer == true).All(x => answerVerfiedUser2.Where(y => y.IsResponeUser == true).Select(y => y.Id).Contains(x.Id)))
                            {
                                correctdefault = true;
                            }
                            listVerifiedRespons.Add(new VerifiedUserResponesDto
                            {
                                UserRespones = answerVerfiedUser2.Cast<AnswerVerfiedDto>().ToList(),
                                QuestDto = new QuestDto
                                {
                                    Id = Quest.Id,
                                    Info = Quest.Info,
                                    Name = Quest.Name
                                },
                                IsCorrectQuest = correctdefault2,
                            });
                            break;
                        case 3:
                            bool correctdefault3 = false;
                            var answerVerfiedUser3 = new AnswerVerfiedDto();
                            var answerQuestDto = Quest.Answers.FirstOrDefault(x => x.Quest.Id == Quest.Id);
                            if (responses.First(x => x.QuestId == Quest.Id).UserRespones.First() == null)
                            {
                                answerVerfiedUser3 = new AnswerVerfiedDto()
                                {
                                    Id = answerQuestDto.Id,
                                    AnswerText = responses.FirstOrDefault(x => x.QuestId == Quest.Id).UserRespones.FirstOrDefault(),
                                    IsCorrectAnswer = answerQuestDto.IsCorrectAnswer,
                                    IsResponeUser = false,
                                };
                            }

                            else
                            {
                                answerVerfiedUser3 = new AnswerVerfiedDto()
                                {
                                    Id = answerQuestDto.Id,
                                    AnswerText = responses.FirstOrDefault(x => x.QuestId == Quest.Id).UserRespones.First().ToString(),
                                    IsCorrectAnswer = answerQuestDto.IsCorrectAnswer,
                                    IsResponeUser = answerQuestDto.AnswerText.ToLower().Split(";").Any(x => responses.First(x => x.QuestId == Quest.Id).UserRespones
                                    .Select(s => s.ToLower()).ToArray().Contains(x)),
                                };
                            }
                            listVerifiedRespons.Add(new VerifiedUserResponesDto
                            {
                                UserRespones = new List<AnswerVerfiedDto>() { answerVerfiedUser3 },
                                QuestDto = new QuestDto
                                {
                                    Id = Quest.Id,
                                    Info = Quest.Info,
                                    Name = Quest.Name,
                                    PathImg = Quest.PathToImage

                                },
                                IsCorrectQuest = answerVerfiedUser3.IsResponeUser,
                                CategoryTasksDto = new DTO.CategoryTasksDto.CategoryTasksDto
                                {
                                    Id = Quest.CategoryTasks.Id,
                                    Name = Quest.CategoryTasks.Name,
                                }
                            });
                            break;

                    }

                    
                }
                var resulTest = (Convert.ToDecimal(listVerifiedRespons.Where(x => x.IsCorrectQuest == true).ToList().Count) / Convert.ToDecimal(listVerifiedRespons.Count)) * 100;// результат в процентах 
                string evaluationName = "";
                var listTestEvaluationName = new List<DTO.TestDto.EvaluationDto>();
                var test = context.Tests.FirstOrDefault(x => x.Id == dto.TestId);
                if (test.Evaluations != null)
                {
                    listTestEvaluationName = JsonSerializer.Deserialize<List<DTO.TestDto.EvaluationDto>>(test.Evaluations);
                    evaluationName = listTestEvaluationName.Where(x => x.Percent <= resulTest).ToList().Max(x => x.EvaluationName);
                }
                var userResponses = context.UserResponses.First(x => x.Id == dto.idResult);


                userResponses.ListUserResponses = JsonSerializer.Serialize(listVerifiedRespons);
                userResponses.Result = resulTest;
                userResponses.EvaluationName = evaluationName;
                userResponses.IsFinish = true;

                    

                
                //if (context.Results.FirstOrDefault(x => x.User.Id == dto.StudentId && x.Test.Id == dto.TestId) == null)
                //{

                //    var result = new ResultTest
                //    {

                //        Responses = new List<UserResponses>() { userResponses },
                //        Test = context.Tests.First(x => x.Id == dto.TestId),
                //        User = context.Users.First(x => x.Id == dto.StudentId),

                //    };

                //    context.Results.Add(result);
                //    context.SaveChanges();
                //}
                //else
                {
                    context.Update(userResponses);
                    context.SaveChanges();
                    //var result = context.Results.Include(x => x.Responses).FirstOrDefault(x => x.User.Id == dto.StudentId && x.Test.Id == dto.TestId);
                    //context.Results.Update(result);
                    //context.SaveChanges();

                }


                return new ResultOfAttemptsDTO
                {
                    IdUserRespones = dto.idResult,
                    Result = resulTest,
                    EvaluationName = evaluationName,
                    Attempts = context.Results.Include(x => x.Responses).FirstOrDefault(x => x.Test.Id == dto.TestId && x.User.Id == dto.StudentId).Responses.Count(),
                };
            }
            else
            {
                return null;
            }
           }

       

        public List<VerifiedUserResponesDto> returnResultDetails(long idResulTest)
        {
            var respons = context.UserResponses
                .Include(x => x.ResultTest.Test)
                .FirstOrDefault(x => x.Id == idResulTest);
            List<VerifiedUserResponesDto> listUserRespons = JsonSerializer.Deserialize<List<VerifiedUserResponesDto>>(respons.ListUserResponses);
            List<long> answer = new List<long>();
            List<string> d = new List<string>();
            //listUserRespons.ForEach(x => {
            //    x.UserRespones.ForEach
            //    (x => { d.Add(x); });
            //});
            //d.ForEach(x => {
            //    answer.Add((long)Convert.ToDouble(x));//Вытаскиваем id ответов пользователя.Пока делаем ток так ,но потом это будет одним из способов в зависимости от категории вопросаю 
            //});
            //var answersUser = context.Answers.Where(x => answer.Contains(x.Id)).ToList();
           
            //var listVerifiedRespouns = new List<VerifiedUserResponesDto>();
            //var ListQuest = context.Quests.Where(x => x.Tests.Contains(respons.ResultTest.Test)).Include(x => x.Answers).Include(x => x.CategoryTasks).ToList();
            //foreach (var Quest in ListQuest)
            //{
            //    switch (Quest.CategoryTasks.Id)
            //    {
            //        case 1:
            //            bool correctdefault = false;


            //            List<AnswerVerfiedDto> answerVerfiedUser = new List<AnswerVerfiedDto>();
            //            Quest.Answers.ForEach(x =>
            //            {
            //                answerVerfiedUser.Add(new AnswerVerfiedDto()
            //                {
            //                    Id = x.Id,
            //                    AnswerText = x.AnswerText,
            //                    IsCorrectAnswer = x.IsCorrectAnswer,
            //                    IsResponeUser = answersUser.Contains(x)

            //                });
            //            });

            //            if (Quest.Answers.Where(x => x.IsCorrectAnswer == true).All(x => answerVerfiedUser.Where(y => y.IsResponeUser == true).Select(y => y.Id).Contains(x.Id)))
            //            {
            //                correctdefault = true;
            //            }
            //            listVerifiedRespouns.Add(new VerifiedUserResponesDto
            //            {
            //                UserRespones = answerVerfiedUser.Cast<AnswerVerfiedDto>().ToList(),
            //                QuestDto = new QuestDto
            //                {
            //                    Id = Quest.Id,
            //                    Info = Quest.Info,
            //                    Name = Quest.Name
            //                },
            //                IsCorrectQuest = correctdefault,
            //                CategoryTasksDto = new DTO.CategoryTasksDto.CategoryTasksDto
            //                {
            //                    Id = Quest.CategoryTasks.Id,
            //                    Name = Quest.CategoryTasks.Name,
            //                }           
            //            });
            //            break;
            //        case 2:
            //            bool correctdefault2 = false;


            //            List<AnswerVerfiedDto> answerVerfiedUser2 = new List<AnswerVerfiedDto>();
            //            Quest.Answers.ForEach(x =>
            //            {
            //                answerVerfiedUser2.Add(new AnswerVerfiedDto()
            //                {
            //                    Id = x.Id,
            //                    AnswerText = x.AnswerText,
            //                    IsCorrectAnswer = x.IsCorrectAnswer,
            //                    IsResponeUser = answersUser.Contains(x)

            //                });
            //            });

            //            if (Quest.Answers.Where(x => x.IsCorrectAnswer == true).All(x => answerVerfiedUser2.Where(y => y.IsResponeUser == true).Select(y => y.Id).Contains(x.Id)))
            //            {
            //                correctdefault = true;
            //            }
            //            listVerifiedRespouns.Add(new VerifiedUserResponesDto
            //            {
            //                UserRespones = answerVerfiedUser2.Cast<AnswerVerfiedDto>().ToList(),
            //                QuestDto = new QuestDto
            //                {
            //                    Id = Quest.Id,
            //                    Info = Quest.Info,
            //                    Name = Quest.Name
            //                },
            //                IsCorrectQuest = correctdefault2,
            //                CategoryTasksDto = new DTO.CategoryTasksDto.CategoryTasksDto
            //                {
            //                    Id = Quest.CategoryTasks.Id,
            //                    Name = Quest.CategoryTasks.Name,
            //                }
            //            });
            //            break;
            //        case 3:
            //            bool correctdefault3 = false;
            //            var answerVerfiedUser3 = new AnswerVerfiedDto();
            //            var answerQuestDto = Quest.Answers.FirstOrDefault(x => x.Quest.Id == Quest.Id);
            //            if (listUserRespons.First(x => x.QuestId == Quest.Id).UserRespones.First() == null)
            //            {
            //                answerVerfiedUser3 = new AnswerVerfiedDto()
            //                {
            //                    Id = answerQuestDto.Id,
            //                    AnswerText = listUserRespons.FirstOrDefault( x => x.QuestId == Quest.Id).UserRespones.FirstOrDefault(),
            //                    IsCorrectAnswer = answerQuestDto.IsCorrectAnswer,
            //                    IsResponeUser = false,
            //                };
            //            }

            //            else
            //            {
            //                answerVerfiedUser3 = new AnswerVerfiedDto()
            //                {
            //                    Id = answerQuestDto.Id,
            //                    AnswerText = listUserRespons.FirstOrDefault(x => x.QuestId == Quest.Id).UserRespones.FirstOrDefault() ,
            //                    IsCorrectAnswer = answerQuestDto.IsCorrectAnswer,
            //                    IsResponeUser = answerQuestDto.AnswerText.ToLower().Split(";").Any(x => listUserRespons.FirstOrDefault(x => x.QuestId == Quest.Id).UserRespones
            //                    .Select(s => s.ToLower()).ToArray().Contains(x)),
            //                };
            //            }
            //            listVerifiedRespouns.Add(new VerifiedUserResponesDto
            //            {
            //                UserRespones = new List<AnswerVerfiedDto>() { answerVerfiedUser3 },
            //                QuestDto = new QuestDto
            //                {
            //                    Id = Quest.Id,
            //                    Info = Quest.Info,
            //                    Name = Quest.Name
            //                },
            //                IsCorrectQuest = answerVerfiedUser3.IsResponeUser,
            //                CategoryTasksDto = new DTO.CategoryTasksDto.CategoryTasksDto
            //                {
            //                    Id = Quest.CategoryTasks.Id,
            //                    Name = Quest.CategoryTasks.Name,
            //                }
            //            });
            //            break;

            //    }

           // }
            return listUserRespons;
        

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

        public ReturnAttemptDto?  CheckingStudentResult(long testId, long studentId)
        {
            var result =context.Results
                .Include(x => x.User)
                .Include(x => x.Test)
                .Include(x => x.Responses)
                .FirstOrDefault(x => x.Test.Id == testId && x.User.Id == studentId);
           
            if (result == null)
            {
                return null;
            }
            var attempt = result.Responses.FirstOrDefault(x => x.IsFinish == false);
            if (attempt == null )
            {
                return null;
            }
           if (null == result.Test.TimeInMinutes)
            {

                return new ReturnAttemptDto
                { idResult = attempt.Id,
                    TestId = testId,
                    UserResponesTest = JsonSerializer.Deserialize<List<UserRespons>>(attempt.ListUserResponses),
                    Minutes = null

                };
            }
           if ((long)attempt.StartdateTime.AddMinutes((double)result.Test.TimeInMinutes).Subtract(DateTime.UtcNow).Seconds <= 0 )
            {

           
                return new ReturnAttemptDto
                {
                    idResult = attempt.Id,
                    TestId = testId,
                    UserResponesTest = JsonSerializer.Deserialize<List<UserRespons>>(attempt.ListUserResponses),
                    Minutes = (long)attempt.StartdateTime.AddMinutes((double)result.Test.TimeInMinutes).Subtract(DateTime.UtcNow).TotalMinutes,
                    Second = (long)attempt.StartdateTime.AddMinutes((double)result.Test.TimeInMinutes).Subtract(DateTime.UtcNow).TotalSeconds

                };
            }
            else{
                
                return new ReturnAttemptDto
                {
                    idResult = attempt.Id,
                    TestId = testId,
                    UserResponesTest = JsonSerializer.Deserialize<List<UserRespons>>(attempt.ListUserResponses),
                    Minutes = (long)attempt.StartdateTime.AddMinutes((double)result.Test.TimeInMinutes).Subtract(DateTime.UtcNow).Minutes,
                     Second = (long)attempt.StartdateTime.AddMinutes((double)result.Test.TimeInMinutes).Subtract(DateTime.UtcNow).Seconds
                };
            }
            
        }

        public long CreatResultAndAttempt(long testId, long studentId)
        {

        if(context.Results.Include(x => x.Test )
                .Include(x => x.User)
                .FirstOrDefault(x => x.User.Id == studentId && x.Test.Id == testId) == null)
            {
                var listResponses = new List<UserRespons>();
                context.Tests.Include(x => x.Quests).First(x => x.Id == testId).Quests.ForEach(x =>
                {
                    listResponses.Add(new UserRespons
                    {
                        QuestId = x.Id,
                        UserRespones = null
                    });
                }
                );

                var attempt = new UserResponses {
                    StartdateTime = DateTime.UtcNow,
                    IsFinish = false,
                    ListUserResponses = JsonSerializer.Serialize(listResponses)

                };

                context.Results.Add(new ResultTest
                {
                    Test = context.Tests.First(x => x.Id == testId),
                    Responses = new List<UserResponses>() { attempt },
                    User = context.Users.First(x => x.Id == studentId)


                });
                context.UserResponses.Add(attempt);
                context.SaveChanges();
                return attempt.Id;
            }
            else
            {
                var listResponses = new List<UserRespons>();
                context.Tests.Include(x => x.Quests).First(x => x.Id == testId).Quests.ForEach(x =>
                {
                    listResponses.Add(new UserRespons
                    {
                        QuestId = x.Id,
                        UserRespones = null
                    });
                }
                );
                 context.Results.First(x => x.Test.Id == testId && x.User.Id == studentId);
                var attempt = new UserResponses
                {
                    ResultTest = context.Results.First(x => x.Test.Id == testId && x.User.Id == studentId),
                    StartdateTime = DateTime.UtcNow,
                    IsFinish = false,
                    ListUserResponses = JsonSerializer.Serialize(listResponses)
                    
                };
               

                context.UserResponses.Add(attempt);
                context.SaveChanges();
                return attempt.Id;
            }
        
        }

        public void UpdateRespones(AddResultTestStudentDto dto)
        {
           var respones = context.UserResponses.First(x => x.Id == dto.idResult);
            respones.ListUserResponses = JsonSerializer.Serialize(dto.UserResponesTest);
            context.UserResponses.Update(respones);
            context.SaveChanges();
        }

        public bool CheckingForAccess()
        {
            throw new NotImplementedException();
        }
    }
    }
    
