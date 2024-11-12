using Database;
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
        public StudentUserDto Login(string name)
        {
            var user = context.Users
                .Include(x => x.Role)
                .Include(x => x.Group)
                .Include(x => x.Disciplines)
                .First(x => x.FullName == name);
            return (new StudentUserDto
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
            return (new StudentUserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Group = new DTO.GroupDto.DetailsGroupDto
                {
                    Id = user.Group.Id,
                    Name = user.Group.Name,
                    StartDateOfTraining = user.Group.StartDateOfTraining,
                    EndOfTraining = user.Group.EndOfTraining

                },
                Role = new DTO.RoleDto.RoleDto
                {
                    Id=user.Role.Id,
                    Name = user.Role.Name,
                }
     
                
            });

        }

        public List<UserDto> GetUsers()
        {
            var listUser = context.Users.ToList();
            var users = new List<UserDto>();
            foreach (var user in listUser)
            {
                users.Add(new UserDto
                {
                    Id = user.Id,
                   FullName= user.FullName,
                });
            }
            return users;
        }

        public void Insert(CreateUserDto dto)
        {
            var user = new User
            {
               FullName = dto.FullName,
               Password = Convert.ToHexString(
                    MD5.Create().ComputeHash(System.Text.Encoding.ASCII.GetBytes(dto.Password))),
                Disciplines = context.Disciplines.Where(x => dto.Disciplines.Contains(x.Id)).ToList(),
                Group = context.Groups.FirstOrDefault(x => x.Id == dto.Group),
                Role = context.Roles.First(x => x.Id == dto.Role)

            };
            context.Users.Add(user);
            context.SaveChanges();
        }

        public void Update(UpdateUserDto dto)
        {
            var user = context.Users.First(x => x.Id == dto.Id);
            user.FullName = dto.FullName;
            user.Disciplines = context.Disciplines.Where(x => dto.Disciplines.Contains(x.Id)).ToList();
            user.Group = context.Groups.FirstOrDefault(x => x.Id == dto.Group);
            user.Role = context.Roles.First(x => x.Id == dto.Role); 
            context.Users.Update(user);
            context.SaveChanges();
        }
      

    }
}
