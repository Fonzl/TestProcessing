namespace Database
{
    public class ResultTest
    {
        public long Id { get; set; }
        public User User { get; set; }
        public Test Test { get; set; }

        public List<UserResponses> Responses { get; set; }
    }
}
