﻿using Database;
using DTO.DisciplineDto;
using DTO.UserDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryUser
{
    public class RepositoryUser(ApplicationContext context) : IRepositoryUser
    {
        public UserDto? Login(string login, string password) //Находит юзера и возращает 
        {


            var user = context.Users
                .Include(x => x.Role)
                .FirstOrDefault(x => x.Login == login && x.Password == Convert.ToHexString(
                    MD5.Create().ComputeHash(System.Text.Encoding.ASCII.GetBytes(password))));
            if (user == null)
            {
                return null;
            }
            else
            {
                return (new UserDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    
                    
                    Role = new DTO.RoleDto.RoleDto
                    {
                        Id = context.Roles.First(x => x.Id == user.RoleId).Id,
                        Name = context.Roles.First(x => x.Id == user.RoleId).Name,
                    }
                });
            }


        }

        public void Delete(long id)
        {
            var user = context.Users.First(x => x.Id == id);
            context.Users.Remove(user);
            context.SaveChanges();
        }

        public StudentUserDto GetUser(long id)
        {
            var user = context.Users
                .Include(x => x.Role)
                .Include(x => x.Group)
                .Include(x => x.Disciplines)
                .First(x => x.Id == id);
            if (user.Role.Id == 3)
            {
                return (new StudentUserDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    login = user.Login,
                    Group = new DTO.GroupDto.DetailsGroupDto
                    {
                        Id = user.Group.Id,
                        Name = user.Group.Name,
                        StartDateOfTraining = user.Group.StartDateOfTraining,
                        EndOfTraining = user.Group.EndOfTraining,
                        
                        
                        

                    },
                    Role = new DTO.RoleDto.RoleDto
                    {
                        Id = user.Role.Id,
                        Name = user.Role.Name,
                    }


                });
            }
            else
            {
                throw new Exception("Нет доступа к данным этого пользователя");
            }
            

        }
        public TeacherUserDto? GetTeacher(long id)
        {
            var user = context.Users
                .Include(x => x.Role)
                .Include(x => x.Group)
                .Include(x => x.Disciplines)
                .First(x => x.Id == id);
           
            if (user.Role.Id == 2)
            {
                var listDiscipline = new List<DisciplineDto>();
                user.Disciplines.ForEach(x =>
                {
                    listDiscipline.Add(new DisciplineDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                    });
                });
                return (new TeacherUserDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Disciplines = listDiscipline,
                    Role = new DTO.RoleDto.RoleDto
                    {
                        Id = user.Role.Id,
                        Name = user.Role.Name,
                    },
                    login = user.Login,



                });
            }
            else
            {
                throw new Exception("Нет доступа к данным этого пользователя");
            }


        }

        public List<ShortUserDto> GetUsers(short idRole)
        {
            if (idRole == 2 || idRole == 3)
            {
                var listUser = context.Users
                    .Include(x => x.Role)
                    .Where(x => x.Role.Id == idRole)
                    .ToList();
                var users = new List<ShortUserDto>();
                foreach (var user in listUser)
                {
                    users.Add(new ShortUserDto
                    {
                        Id = user.Id,
                        FullName = user.FullName,
                    });
                }
                return users;
            }
            else 
            {
                throw new Exception("Информация к таким пользователям недоступна");
            }
        }


        public string InsertStudent(CreateStudentDto dto,string login)
        {
            
            //do
            //{
            //    var rand = new Random();
            //    //login = Transliterate(string.Join("", dto.FullName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)))
            //    //    + rand.Next(00, 99);
            //}
            //while (context.Users.FirstOrDefault(x => x.Login == login) != null);
            var user = new User
            {
                FullName = dto.FullName,
                Password = Convert.ToHexString(
                    MD5.Create().ComputeHash(System.Text.Encoding.ASCII.GetBytes(dto.Password))),
                Group = context.Groups.FirstOrDefault(x => x.Id == dto.Group),
                Role = context.Roles.First(x => x.Id == 3),
                Login = login,
                

            };
            context.Users.Add(user);
            context.SaveChanges();
            return login;
        }
        public string InsertTeacher(CreateTeacherDto dto, string login)
        {
            //string login;
            //do
            //{
            //    var rand = new Random();
            //    login = Transliterate(string.Join("", dto.FullName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)))
            //        + rand.Next(00, 99);
            //}
            //while (context.Users.FirstOrDefault(x => x.Login == login) != null);
            var user = new User
            {
                FullName = dto.FullName,
                Password = Convert.ToHexString(
                    MD5.Create().ComputeHash(System.Text.Encoding.ASCII.GetBytes(dto.Password))),
                Disciplines =  context.Disciplines.Where(x => dto.Disciplines.Contains(x.Id)).ToList(),
                Role = context.Roles.First(x => x.Id == 2),
                Login = login,

            };
            context.Users.Add(user);
            context.SaveChanges();
            return login;
        }
        public void UpdateStudent(UpdateStudentDto dto)
        {
            var user = context.Users.Include(x => x.Role).First(x => x.Id == dto.Id);
            if (user.Role.Id != 3)
            {
                throw new Exception("Неправельные введённые данные");
            }
            user.FullName = dto.FullName;
            
            user.Group = context.Groups.FirstOrDefault(x => x.Id == dto.Group);
           
            context.Users.Update(user);
            context.SaveChanges();
        }

        public List<ShortUserDto> GetStudentIdGroup(long idGroup)
        {
            var listStudent = context.Users
                 .Include(x => x.Group)
                 .Where(x => x.Group.Id == idGroup).ToList();
            var listUserDto = new List<ShortUserDto>();
            listStudent.ForEach(x =>
            {
                listUserDto.Add(new UserDto
                {
                    Id = x.Id,
                    FullName = x.FullName,



                });

            });
            return listUserDto;
        }

        public List<StudentAttemptResultDto> GetStudentAttempt(long idGroup, long idTest)
        {
            var listStudent = context.Users
                .Include(x => x.Group)
                .Where(x => x.Group.Id == idGroup).ToList();//Вытащили все студентов группы
            var resultsStudents = context.Results
                .Include(x => x.Responses)
                .Include(x => x.User.Group)
                .Include(x => x.Test)
                .Where(x => x.Test.Id == idTest && listStudent.Select(y => y.Id).ToList().Contains(x.User.Id)).ToList();//Находит все результаты студентов по листу студентов и тесту
            var listStudentAttempt = new List<StudentAttemptResultDto>();
            resultsStudents.ForEach(x =>
            {
                listStudentAttempt.Add(new StudentAttemptResultDto
                {
                    Id = x.User.Id,
                    FullName = x.User.FullName,
                    IdAttempt = x.Responses.OrderByDescending(x => x.Result).First().Id,
                    Result = x.Responses.OrderByDescending(x => x.Result).FirstOrDefault().Result
                });
            });
            return listStudentAttempt;

        }

        public void PasswordСhange(PasswordСhangeDto passwordСhangeDto)//Смена пароля пользователю 
        {
            var user = context.Users.FirstOrDefault(x => x.Id == passwordСhangeDto.IdUser);
            user.Password = Convert.ToHexString(
                    MD5.Create().ComputeHash(System.Text.Encoding.ASCII.GetBytes(passwordСhangeDto.Password)));
            context.Users.Update(user);
            context.SaveChanges();
        }

       

        public void UpdateTeacher(UpdateTeacherDto dto)
            {
            var user = context.Users
                .Include(x => x.Disciplines)
                .Include(x => x.Role).First(x => x.Id == dto.Id);
            if (user.Role.Id != 2)
            {
                throw new Exception("Неправельные введённые данные");
            }
            
            user.FullName = dto.FullName;
            user.Disciplines = context.Disciplines.Where(x => dto.Disciplines.Contains(x.Id)).ToList();
          
            context.Users.Update(user);
            context.SaveChanges();
        }

        public ShortUserDto? GetLoginUser(string login)
        {
            var user = context.Users.FirstOrDefault(x => x.Login == login);
            if (user == null)
            {
                return null;
            }
            return new ShortUserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                
            };
        }
    }
}
