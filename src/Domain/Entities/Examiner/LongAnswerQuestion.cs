namespace Domain.Entities.Examiner
{
    public class LongAnswerQuestion
    {
        public LongAnswerQuestion(int? maximinWords, int? minimanWords, string answer, string? feedback)
        {
            MaximinWords = maximinWords;
            MinimanWords = minimanWords;
            Answer = answer;
            Feedback = feedback;
        }

        public Guid Id { get; set; }
        public int? MaximinWords { get; set; }
        public int? MinimanWords { get; set; }
        public string Answer { get; set; }
        public string? Feedback { get; set; }
    }
}
