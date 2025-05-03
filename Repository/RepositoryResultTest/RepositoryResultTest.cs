using System.Data;
using System.Text.Json;
using Database;
using DTO.AnswerDto;
using DTO.QuestDto;
using DTO.ResultTestDto;
using Microsoft.EntityFrameworkCore;

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
                    IsChek = result.Test.IsCheck,
                    NameTest = result.Test.Name,
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
        public DTO.GeneralDto.IsBoolDto TestBool(long IdResponse)
        {
            var res = context.UserResponses
                .Include(x => x.ResultTest.Test)
                .FirstOrDefault(x => x.Id == IdResponse);
            return new DTO.GeneralDto.IsBoolDto { IsTrue = res.ResultTest.Test.IsCheck };
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
            if (studentResultDiscipline == null)
            {
                return 0;
            }
            var results = studentResultDiscipline.Where(x => x.Test.Discipline.Id == dto.DisciplineId).ToList();
            if (results.Count == 0)
            {
                return 0;
            }
            decimal sum = 0;
            foreach (var result in results)
            {
                if (result.Responses.OrderByDescending(x => x.Result).First().Result == null)
                {
                    sum += 0;
                }
                else
                { sum += (decimal)result.Responses.OrderByDescending(x => x.Result).First().Result; }
            }
            decimal staticstic = sum / results.Count;
            return staticstic;

        }
        public List<ResultOfAttemptsDTO> ResultStudentId(long studentId, long idDiscipline)
        {
            var results = context.UserResponses
               .Include(x => x.ResultTest.User)
               .Include(x => x.ResultTest.Test.Discipline)
               .Include(x => x.ResultTest.Responses)
              .Where(x => x.ResultTest.User.Id == studentId && x.ResultTest.Test.Discipline.Id == idDiscipline).ToList();
            var listResults = new List<ResultOfAttemptsDTO>();
            foreach (var result in results)
            {
                if (result.IsFinish == false)
                {
                    continue;
                }
                listResults.Add(new ResultOfAttemptsDTO
                {

                    IdUserRespones = result.Id,
                    Attempts = result.ResultTest.Responses.FindIndex(y => y.Id == result.Id),
                    EvaluationName = result.EvaluationName,
                    Result = (decimal)result.Result,
                    IsChek = result.ResultTest.Test.IsCheck,
                    NameTest = result.ResultTest.Test.Name,
                    DateFinish = result.FinishdateTime.ToLocalTime(),


                });

            }
            return listResults.OrderByDescending(x => x.DateFinish).ToList();
        }
        public ResultOfAttemptsDTO InsertStudent(AddResultTestStudentDto dto)// тут  расчёт result
        {
            if (context.UserResponses.First(x => x.Id == dto.idResult).IsFinish)
            {
                throw new Exception("Тест уже завершён");
            }
            if (context.Tests.Include(x => x.Quests)
                .FirstOrDefault(x => x.Id == dto.TestId).Quests.Count == dto.UserResponesTest.Count)
            {

                List<UserRespon> responses = new List<UserRespon>();
                if (dto.UserResponesTest.ToList() == null)
                {
                    responses = JsonSerializer.Deserialize<List<UserRespon>>(
                       context.UserResponses.First(x => x.Id == dto.idResult).ListUserResponses
                   );
                }
                else
                {
                    responses = dto.UserResponesTest.ToList();
                }
                var listVerifiedRespons = new List<VerifiedUserResponesDto>();
                var ListQuest = context.Quests
                    .Where(x => responses.Select(y => y.QuestId)
                    .ToList().Contains(x.Id)).Include(x => x.Answers)
                    .Include(y => y.CategoryTasks).ToList();
                foreach (var Quest in ListQuest)
                {
                    switch (Quest.CategoryTasks.Id)
                    {
                        case 1:// Обработка с моножественным выбором
                            bool correctdefault = false;


                            List<AnswerVerfiedDto> answerVerfiedUser = new List<AnswerVerfiedDto>();
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
                                answerVerfiedUser.Add(new AnswerVerfiedDto()
                                {
                                    Id = x.Id,
                                    AnswerText = x.AnswerText,
                                    IsCorrectAnswer = x.IsCorrectAnswer,
                                    IsResponeUser = isResponeUser,
                                    PathImg = x.PathToImage

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
                                    Name = Quest.Name,
                                    PathImg = Quest.PathToImage,


                                },
                                IsCorrectQuest = correctdefault,
                                CategoryTasksDto = new DTO.CategoryTasksDto.CategoryTasksDto
                                {
                                    Id = Quest.CategoryTasks.Id,
                                    Name = Quest.CategoryTasks.Name,
                                }
                            });
                            break;
                        case 2:// Обработка с одним ответом
                            bool correctdefault2 = false;


                            List<AnswerVerfiedDto> answerVerfiedUser2 = new List<AnswerVerfiedDto>();
                            Quest.Answers.ForEach(x =>
                            {
                                bool isResponeUser;

                                if (responses.First(x => x.QuestId == Quest.Id).UserRespones == null)// если на вопрос не был дан ответ то он автоматически помечаеться не правельным
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
                                    IsResponeUser = isResponeUser,
                                    PathImg = x.PathToImage


                                });
                            });
                            if (answerVerfiedUser2.Where(y => y.IsResponeUser == true).All(x => Quest.Answers.Where(x => x.IsCorrectAnswer == true).Select(y => y.Id).Contains(x.Id)))// проверка на правельный ответ
                            {
                                correctdefault2 = true;
                            }
                            listVerifiedRespons.Add(new VerifiedUserResponesDto
                            {
                                UserRespones = answerVerfiedUser2.Cast<AnswerVerfiedDto>().ToList(),
                                QuestDto = new QuestDto
                                {
                                    Id = Quest.Id,
                                    Info = Quest.Info,
                                    Name = Quest.Name,
                                    PathImg = Quest.PathToImage,
                                },
                                IsCorrectQuest = correctdefault2,
                                CategoryTasksDto = new DTO.CategoryTasksDto.CategoryTasksDto
                                {
                                    Id = Quest.CategoryTasks.Id,
                                    Name = Quest.CategoryTasks.Name,
                                }
                            });
                            break;
                        case 3:// обработка с письменым ответом
                            bool correctdefault3 = false;
                            var answerVerfiedUser3 = new AnswerVerfiedDto();
                            var answerQuestDto = Quest.Answers.FirstOrDefault(x => x.Quest.Id == Quest.Id);
                            if (responses.First(x => x.QuestId == Quest.Id).UserRespones == null)
                            {
                                answerVerfiedUser3 = new AnswerVerfiedDto()
                                {
                                    Id = answerQuestDto.Id,
                                    AnswerText = null,
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
                        case 4://обработка с последовательным выбором
                            bool correctdefault4 = false;
                            List<AnswerVerfiedDto> answerVerfiedUser4 = new List<AnswerVerfiedDto>();
                            Quest.Answers.OrderBy(x => x.Id).ToList().ForEach(x =>
                            {
                                bool isResponeUser;


                                if (responses.First(x => x.QuestId == Quest.Id).UserRespones == null)// если на вопрос не был дан ответ то он автоматически помечаеться не правельным
                                {
                                    isResponeUser = false;
                                }
                                else
                                {
                                    var indexAnswer = Quest.Answers.OrderBy(y => y.Id).ToList().FindIndex(y => y.Id == x.Id);
                                    isResponeUser = responses.First(y => y.QuestId == Quest.Id).UserRespones[indexAnswer] == x.AnswerText;
                                }

                                answerVerfiedUser4.Add(new AnswerVerfiedDto()
                                {
                                    Id = x.Id,
                                    AnswerText = x.AnswerText,
                                    IsCorrectAnswer = x.IsCorrectAnswer,
                                    IsResponeUser = isResponeUser,
                                    PathImg = x.PathToImage


                                });
                            });
                            if (Quest.Answers.Where(x => x.IsCorrectAnswer == true).All(x => answerVerfiedUser4.Where(y => y.IsResponeUser == true).Select(y => y.Id).Contains(x.Id)))// проверка на правельный ответ
                            {
                                correctdefault4 = true;
                            }
                            listVerifiedRespons.Add(new VerifiedUserResponesDto
                            {
                                UserRespones = answerVerfiedUser4.Cast<AnswerVerfiedDto>().ToList(),
                                QuestDto = new QuestDto
                                {
                                    Id = Quest.Id,
                                    Info = Quest.Info,
                                    Name = Quest.Name,
                                    PathImg = Quest.PathToImage,
                                },
                                IsCorrectQuest = correctdefault4,
                                CategoryTasksDto = new DTO.CategoryTasksDto.CategoryTasksDto
                                {
                                    Id = Quest.CategoryTasks.Id,
                                    Name = Quest.CategoryTasks.Name,
                                }
                            });
                  
                    break;


                }


            }
            var resulTest = (Convert.ToDecimal(listVerifiedRespons
                .Where(x => x.IsCorrectQuest == true).ToList().Count) / Convert.ToDecimal(listVerifiedRespons.Count)) * 100;// результат в процентах 
            string evaluationName = "";
            var listTestEvaluationName = new List<DTO.TestDto.EvaluationDto>();
            var test = context.Tests.FirstOrDefault(x => x.Id == dto.TestId);
            if (test.Evaluations != null)
            {
                listTestEvaluationName = JsonSerializer.Deserialize<List<DTO.TestDto.EvaluationDto>>(test.Evaluations);
                evaluationName = listTestEvaluationName.Where(x => x.Percent <= resulTest).ToList().Max(x => x.EvaluationName);
            }
            var userResponses = context.UserResponses.First(x => x.Id == dto.idResult);
            userResponses.ListUserResponses = JsonSerializer.Serialize(listVerifiedRespons);//Сохраняем ответы ввиде строки
            userResponses.Result = resulTest;
            userResponses.EvaluationName = evaluationName;
            userResponses.IsFinish = true;
            userResponses.FinishdateTime = DateTime.Now.ToUniversalTime();
            context.Update(userResponses);
            context.SaveChanges();
            var f = context.Results
                .Include(x => x.Responses)
                .Include(x => x.Test)
                .Include(x => x.User)
                .FirstOrDefault(y => y.Test.Id == dto.TestId && y.User.Id == dto.StudentId).Responses.Count;
            return new ResultOfAttemptsDTO
            {
                IdUserRespones = dto.idResult,
                Result = resulTest,
                EvaluationName = evaluationName,
                Attempts = test.NumberOfAttempts - context.Results
                .Include(x => x.Test)
                .Include(x => x.User)
                .Include(x => x.Responses)
                .FirstOrDefault(y => y.Test.Id == dto.TestId && y.User.Id == dto.StudentId).Responses.Count,
                IsChek = context.Tests.First(x => x.Id == dto.TestId).IsCheck,
                NameTest = context.Tests.First(x => x.Id == dto.TestId).Name,
                DateFinish = userResponses.FinishdateTime.ToLocalTime(),
            };
        }
            else
            {
                return null;
            }
        }


        public List<VerifiedUserResponesDtoShort> returnResultDetailsFalse(long idResulTest)
        {
            var respons = context.UserResponses
                .Include(x => x.ResultTest.Test)
                .FirstOrDefault(x => x.Id == idResulTest);
            List<VerifiedUserResponesDto> listUserRespons = JsonSerializer.Deserialize<List<VerifiedUserResponesDto>>(respons.ListUserResponses);
            List<long> answer = new List<long>();
            List<string> d = new List<string>();
            List<VerifiedUserResponesDtoShort> verifiedUserResponesDtoShorts = listUserRespons.Cast<VerifiedUserResponesDtoShort>().ToList();

            return verifiedUserResponesDtoShorts;


        }

        public List<VerifiedUserResponesDto> returnResultDetailsTrue(long idResulTest)
        {
            var respons = context.UserResponses
                .Include(x => x.ResultTest.Test)
                .FirstOrDefault(x => x.Id == idResulTest);
            List<VerifiedUserResponesDto> listUserRespons = JsonSerializer.Deserialize<List<VerifiedUserResponesDto>>(respons.ListUserResponses);
            List<long> answer = new List<long>();
            List<string> d = new List<string>();

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

        public ReturnAttemptDto? CheckingStudentResult(long testId, long studentId)
        {
            var result = context.Results
                .Include(x => x.User)
                .Include(x => x.Test)
                .Include(x => x.Responses)
                .FirstOrDefault(x => x.Test.Id == testId && x.User.Id == studentId);

            if (result == null)
            {
                return null;
            }
            var attempt = result.Responses.FirstOrDefault(x => x.IsFinish == false);
            if (attempt == null)
            {
                return null;
            }
            if (null == result.Test.TimeInMinutes)
            {

                return new ReturnAttemptDto
                {
                    idResult = attempt.Id,
                    TestId = testId,
                    UserResponesTest = JsonSerializer.Deserialize<List<UserRespon>>(attempt.ListUserResponses),
                    Minutes = null

                };
            }
            if ((long)attempt.StartdateTime.AddMinutes((double)result.Test.TimeInMinutes).Subtract(DateTime.Now.ToUniversalTime()).Seconds <= 0)
            {


                return new ReturnAttemptDto
                {
                    idResult = attempt.Id,
                    TestId = testId,
                    UserResponesTest = JsonSerializer.Deserialize<List<UserRespon>>(attempt.ListUserResponses),
                    Minutes = (long)attempt.StartdateTime.AddMinutes((double)result.Test.TimeInMinutes).Subtract(DateTime.Now.ToUniversalTime()).TotalMinutes,
                    Second = (long)attempt.StartdateTime.AddMinutes((double)result.Test.TimeInMinutes).Subtract(DateTime.Now.ToUniversalTime()).TotalSeconds

                };
            }
            else
            {

                return new ReturnAttemptDto
                {
                    idResult = attempt.Id,
                    TestId = testId,
                    UserResponesTest = JsonSerializer.Deserialize<List<UserRespon>>(attempt.ListUserResponses),
                    Minutes = (long)attempt.StartdateTime.AddMinutes((double)result.Test.TimeInMinutes).Subtract(DateTime.Now.ToUniversalTime()).Minutes,
                    Second = (long)attempt.StartdateTime.AddMinutes((double)result.Test.TimeInMinutes).Subtract(DateTime.Now.ToUniversalTime()).Seconds
                };
            }

        }

        public long CreatResultAndAttempt(long testId, long studentId)
        {

            if (context.Results.Include(x => x.Test)
                    .Include(x => x.User)
                    .FirstOrDefault(x => x.User.Id == studentId && x.Test.Id == testId) == null)
            {
                var listResponses = new List<UserRespon>();
                context.Tests.Include(x => x.Quests).First(x => x.Id == testId).Quests.ForEach(x =>
                {
                    listResponses.Add(new UserRespon
                    {
                        QuestId = x.Id,
                        UserRespones = null
                    });
                }
                );

                var attempt = new UserResponses
                {
                    StartdateTime = DateTime.Now.ToUniversalTime(),
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
                var listResponses = new List<UserRespon>();
                context.Tests.Include(x => x.Quests).First(x => x.Id == testId).Quests.ForEach(x =>
                {
                    listResponses.Add(new UserRespon
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
                    StartdateTime = DateTime.Now.ToUniversalTime(),
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
            if (context.UserResponses.First(x => x.Id == dto.idResult).IsFinish)
            {
                throw new Exception("Тест уже завершён");
            }
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

