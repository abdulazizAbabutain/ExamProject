using Domain.Enums;

namespace Domain.Entities
{
    public class Question
    {
        public Guid Id { get; set; }
        public required string QuestionText { get; set; }
        public required QuestionType QuestionType { get; set; }
        public required int Mark { get; set; }
        public required bool RequireManulReview { get; set; }
        public MultipleChoiseQuestion? MultipleChoiseQuestion { get; set; }
        public TrueFalseQuestion? TrueFalseQuestion { get; set; }
        public ShortAnswerQuestion? ShortAnswerQuestion { get; set; }
    }
}
