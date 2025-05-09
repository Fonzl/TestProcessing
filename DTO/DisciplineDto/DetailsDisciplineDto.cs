using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DisciplineDto
{
    public class DetailsDisciplineDto : DisciplineDto
    {
  

        public List<UserDto.ShortUserDto> Users { get; set; }
       
    }
}
