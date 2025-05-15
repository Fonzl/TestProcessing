using System.ComponentModel.DataAnnotations.Schema;

namespace Database
{
    public class Schedule
    {


        public int Cours { get; set; }
        public int DirectionId { get; set; }
        [ForeignKey("DirectionId")]
        public Direction Direction { get; set; }
        public List<Discipline>? Disciplines { get; set; }

    }
}
