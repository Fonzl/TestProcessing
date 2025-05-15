using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DirectionDto
{
    public class CreatShedule
    {
        [Range(1,6)]
        public int CoursId { get; set; }
        public int DirectionId { get; set; }
    }
}
