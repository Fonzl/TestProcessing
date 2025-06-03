
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
        public ShortUserDto? GetLoginUser(string login);
        public UserDto? Login(string login, string password);
        public StudentUserDto GetUser(long id);
        public TeacherUserDto GetTeacher(long id);
        public List<ShortUserDto> GetUsers(short idRole);
        void UpdateStudent(UpdateStudentDto dto);
        void UpdateTeacher(UpdateTeacherDto dto);
        void Delete(long id);
        string InsertStudent(CreateStudentDto dto,string login);
        string InsertTeacher(CreateTeacherDto dto,string login);
        public List<ShortUserDto> GetStudentIdGroup(long idGroup);// Выыод студентов группы
        public List<StudentAttemptResultDto> GetStudentAttempt(long idGroup,long idTest);
        void PasswordСhange(PasswordСhangeDto passwordСhangeDto);
    }
}
