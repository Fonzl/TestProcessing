using System.ComponentModel.DataAnnotations.Schema;

namespace Database
{

    public class Group
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public DateTime StartDateOfTraining { get; set; }
        public DateTime EndOfTraining { get; set; }
        public List<User>? Users { get; set; }

        public int Cours { get; set; }
        public int DirectionId { get; set; }
        [ForeignKey("DirectionId")]
        public Direction Direction { get; set; }
        [ForeignKey("Cours,DirectionId")]
        public Schedule Schedule { get; set; }
    }

}
