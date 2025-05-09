using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DirectionDto
{
    public class ReturnScheduleDirection : ReturnScheduleDirectionShortDto
    {
        public List<DTO.DisciplineDto.DisciplineDto> Discipline { get; set; }
    }
}
