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
        
        public void CreateUser(CreateUserDto user)
        {
           repo.Insert(user);
        }

        public void DeleteUser(int id)
        {
            repo.Delete(id);
        }

        public StudentUserDto GetUser(long id)
        {
            return  repo.GetUser(id);
        }

        public List<UserDto> GetUsers()
        {
           return repo.GetUsers();
        }

        public ReturnLoginUser Login(string username,string password) // Находим пользователя по данным и делаем jwt token
        {

            UserDto dto = repo.Login(username,password);
            var claims = new List<Claim> {
                new Claim("id", dto.Id.ToString()),
                new Claim(ClaimTypes.Role, dto.Role.Name) };
            var jwt = new JwtSecurityToken(
               issuer: AuthOptions.ISSUER,
               audience: AuthOptions.AUDIENCE,
               claims: claims,
               expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)), // время действия 2 минуты
               signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            //claims: claims,
            //expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)), 
            //        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["KEY"])), SecurityAlgorithms.HmacSha256));
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            
           return new ReturnLoginUser { jwt = token ,IdUser = dto.Id,UserName = dto.FullName, RoleDto = dto.Role};
        }

        public void UpdateUser(UpdateUserDto user)
        {
            repo.Update(user);
        }
    }
}
