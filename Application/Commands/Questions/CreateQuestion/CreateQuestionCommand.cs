using Domain.Enums;

namespace Application.Commands.Questions.CreateQuestion
{
    public class CreateQuestionCommand
    {
        public required string Text { get; set; }
        public required QuestionType Type { get; set; }
        public int Mark { get; set; }
        public List<CreateQuestionOptionCommand>? Options { get; set; }
        
    }




    public class CreateQuestionOptionCommand
    {
        public required string OptionText { get; set; }
        public required bool IsCorrect { get; set; }
        public required float Whight { get; set; }
    }
}
