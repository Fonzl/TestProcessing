using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{

    public class Group
    {
        public long Id { get; set; }
        public string Name { get; set; }
        
        public DateTime StartDateOfTraining { get; set; }
        public DateTime EndOfTraining { get; set; }
        public List<User>? Users { get; set; }
        [Range(1, 4)]
        public  int Cours { get; set; }
        public int DirectionId { get; set; }
        [ForeignKey("DirectionId")]
        public Direction Direction { get; set; }
        [ForeignKey("Cours,DirectionId")]
        public Schedule Schedule { get; set; }
    }

}
