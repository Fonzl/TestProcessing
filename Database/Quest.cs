namespace Database
{
    public class Quest
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public CategoryTasks CategoryTasks { get; set; }
        public List<Test> Tests { get; set; }
        public List<Answer>? Answers { get; set; }
        public List<string>? PathToImage { get; set; }
    }
}
