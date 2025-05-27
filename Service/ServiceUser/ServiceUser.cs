using DTO.UserDto;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.IdentityModel.Tokens;
using Repository.RepositoryUser;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Service.ServiceUser
{
    public class ServiceUser(IRepositoryUser repo) : IServiceUser
    {
        
        public void CreateStudent(CreateStudentDto user)
        {
            

            
           repo.InsertStudent(user);
        }

        public void CreateTeacher(CreateTeacherDto user)
        {
            repo.InsertTeacher(user);
        }

        public void DeleteUser(int id)
        {
            repo.Delete(id);
        }

        public List<StudentAttemptResultDto> GetStudentAttempt(long idGroup, long idTest)
        {
           return repo.GetStudentAttempt(idGroup, idTest);
        }

        public List<ShortUserDto> GetStudentIdGroup(long idGroup)
        {
            return repo.GetStudentIdGroup(idGroup);
        }

        public TeacherUserDto GetTeacher(long id)
        { var teacher = repo.GetTeacher(id);
            if (teacher == null)
            {
                throw new Exception("Пользователь не найден");
            }    
            return teacher;
        }

        public StudentUserDto GetUser(long id)
        {
            return  repo.GetUser(id);
        }

        public List<ShortUserDto> GetUsers(short idRole)
        {
           return repo.GetUsers( idRole);
        }

        public ReturnLoginUser Login(string username,string password) // Находим пользователя по данным и делаем jwt token
        {

           var dto = repo.Login(username,password);
            if(dto == null)
            {
                return null;
            }
            var claims = new List<Claim> {
                new Claim("id", dto.Id.ToString()),
                new Claim(ClaimTypes.Role, dto.Role.Name) };
            var jwt = new JwtSecurityToken(
               issuer: AuthOptions.ISSUER,
               audience: AuthOptions.AUDIENCE,
               claims: claims,
               expires: DateTime.Now.Add(TimeSpan.FromDays(1)), // время действия 2 минуты
               signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            //claims: claims,
            //expires: DateTime.Now.ToUniversalTime().Add(TimeSpan.FromDays(1)), 
            //        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["KEY"])), SecurityAlgorithms.HmacSha256));
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            
           return new ReturnLoginUser { jwt = token ,IdUser = dto.Id,UserName = dto.FullName, RoleDto = dto.Role};
        }

        public void PasswordСhange(PasswordСhangeDto passwordСhangeDto)
        {
            repo.PasswordСhange(passwordСhangeDto);
        }

        public void UpdateStudent(UpdateStudentDto user)
        {
            repo.UpdateStudent(user);
        }

        public void UpdateTeacher(UpdateTeacherDto user)
        {
            repo.UpdateTeacher(user);
        }
       
    }
}
