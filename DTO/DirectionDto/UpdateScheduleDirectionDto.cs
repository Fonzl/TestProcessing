using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DirectionDto
{
    public class UpdateScheduleDirectionDto
    {
        [Range(1,4)]
        public int CoursId { get; set; }
        public int DirectionId { get; set; }
        public List<int> DisciplineId {  get; set; } 
    }
}
