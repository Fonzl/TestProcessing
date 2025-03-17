
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
        public List<UserDto> GetUsers();
        void Update(UpdateUserDto dto);
        void Delete(long id);
        void Insert(CreateUserDto dto);
        public List<ShortUserDto> GetStudentIdGroup(long idGroup);// Выыод студентов группы
        public List<StudentAttemptResultDto> GetStudentAttempt(long idGroup,long idTest);
    }
}
