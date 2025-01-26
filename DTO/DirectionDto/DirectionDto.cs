using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DirectionDto
{
    public class DirectionDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public List<int> GroupsId { get; set; }
        public List<int> SchedulesId { get; set; }
    }
}
