using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using DTO.RoleDto;

namespace DTO.UserDto
{
   public class ReturnLoginUser
    {
        public  string jwt {  get; set; }
        public long IdUser { get; set; }
       
        public string UserName { get; set; }
        public RoleDto.RoleDto RoleDto { get; set; }
        

    }
}
