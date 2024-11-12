
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
        public StudentUserDto Login(string username);
        public StudentUserDto GetUser(long id);
        public List<UserDto> GetUsers();
        void Update(UpdateUserDto dto);
        void Delete(long id);
        void Insert(CreateUserDto dto);
    }
}
