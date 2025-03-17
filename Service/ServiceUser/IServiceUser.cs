using DTO.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceUser
{
    public interface IServiceUser
    {   
        ReturnLoginUser? Login(string username, string password);
        StudentUserDto GetUser( long id);
        List<UserDto> GetUsers();
        void UpdateUser(UpdateUserDto user);
        void DeleteUser(int id);
        void CreateUser(CreateUserDto user);
        public List<ShortUserDto> GetStudentIdGroup(long idGroup);
        public List<StudentAttemptResultDto> GetStudentAttempt(long idGroup, long idTest);
    }
}
