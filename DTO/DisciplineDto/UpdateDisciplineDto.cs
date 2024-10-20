using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DisciplineDto
{
    public class UpdateDisciplineDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<long> Tests { get; set; }
        public List<long> Users { get; set; }
    }
}
