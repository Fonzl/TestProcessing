namespace Database
{
    public class UserResponses
    {

        public long Id { get; set; }
        public decimal? Result { get; set; }
        public string? EvaluationName { get; set; }
        public string ListUserResponses { get; set; }
        public DateTime StartdateTime { get; set; }
        public DateTime FinishdateTime { get; set; }
        public bool IsFinish { get; set; }
        public ResultTest ResultTest { get; set; }
    }
}
