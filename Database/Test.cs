namespace Database
{
    public class Test
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string InfoTest { get; set; }
        public List<Quest>? Quests { get; set; }
        public Discipline Discipline { get; set; }
        public long? TimeInMinutes { get; set; }
        public string? Evaluations { get; set; }
        public bool IsCheck { get; set; }
        public long? NumberOfAttempts { get; set; }
    }
}
