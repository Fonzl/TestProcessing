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
        List<ShortUserDto> GetUsers(short idRole);
        void UpdateUser(UpdateUserDto user);
        void DeleteUser(int id);
        void CreateStudent(CreateStudentDto user);
        void CreateTeacher(CreateTeacherDto user);
        public List<ShortUserDto> GetStudentIdGroup(long idGroup);
        public List<StudentAttemptResultDto> GetStudentAttempt(long idGroup, long idTest);
        void PasswordСhange(PasswordСhangeDto passwordСhangeDto);
    }
}
