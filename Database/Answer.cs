namespace Database
{
    public class Answer
    {
        public long Id { get; set; }
        public string? AnswerText { get; set; }
        public Quest Quest { get; set; }
        public bool IsCorrectAnswer { get; set; }
        public List<string>? PathToImage { get; set; }

    }
}
