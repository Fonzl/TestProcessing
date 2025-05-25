
using DTO.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryUser
{
    public interface IRepositoryUser
        
    {
        public UserDto? Login(string username, string password);
        public StudentUserDto GetUser(long id);
        public TeacherUserDto GetTeacher(long id);
        public List<ShortUserDto> GetUsers(short idRole);
        void UpdateStudent(UpdateStudentDto dto);
        void UpdateTeacher(UpdateTeacherDto dto);
        void Delete(long id);
        void InsertStudent(CreateStudentDto dto);
        void InsertTeacher(CreateTeacherDto dto);
        public List<ShortUserDto> GetStudentIdGroup(long idGroup);// Выыод студентов группы
        public List<StudentAttemptResultDto> GetStudentAttempt(long idGroup,long idTest);
        void PasswordСhange(PasswordСhangeDto passwordСhangeDto);
    }
}
