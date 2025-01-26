using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.GroupDto
{
    public class CreateGroupDto
    {
        public string Name { get; set; }
        public DateTime StartDateOfTraining { get; set; }
        public DateTime EndOfTraining { get; set; }
        public List<long>? Users { get; set; }
        [Range(1, 4)]
        public short Cours { get; set; }
        public int Direction { get; set; }
    }
   // public enum Cours { One = 1, Two = 2, Three = 3, Four = 4 };
}
