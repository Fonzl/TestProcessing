namespace Database
{
    public class Direction
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public List<Group> Groups { get; set; }
        public List<Schedule> Schedule { get; set; }
    }
}
