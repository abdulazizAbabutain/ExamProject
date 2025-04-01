using Domain.Enums;

namespace Application.Questions.Commands.AddQuestion.CommandModels
{
    public class QuestionSourceCommand
    {
        public SourceType Type { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required string URL { get; set; }
        public string? ISBN { get; set; }
    }
}
