using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DirectionDto
{
    public class ReturnScheduleDirection
    {
        public int CoursId { get; set; }
        public int DirectionId { get; set; }
        public string Direction { get; set; }
        public List<DTO.DisciplineDto.DisciplineDto> Discipline { get; set; }
    }
}
