using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserDto
{
    public class UserDto
    {
        public long Id { get; set; }
      
        public string FirstName { get; set; }
        
        public string Surname { get; set; }
       
        public string LastName { get; set; }
       
        public GroupDto.DetailsGroupDto? Group { get; set; }

        public List<DisciplineDto.DetailsDisciplineDto> Disciplines { get; set; }
        public RoleDto.RoleDto Role { get; set; }
    }
}
