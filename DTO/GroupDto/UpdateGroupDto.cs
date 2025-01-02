using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DTO.GroupDto
{
    
    public class UpdateGroupDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDateOfTraining { get; set; }
        public DateTime EndOfTraining { get; set; }
        [Range(1, 4)]
        public short Cours { get; set; }
        public List<long>? Users { get; set; }
        public List<long> Disciplines { get; set; }
    }
}
