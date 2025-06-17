using Domain.Enums;

namespace Application.Questions.Commands.AddQuestion.CommandModels
{
    public class QuestionSourceCommand
    {
        public SourceType Type { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string URL { get; set; }
    }
}
