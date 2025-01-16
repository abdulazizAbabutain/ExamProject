using Domain.Entities.Audit;
using Domain.Entities.History;
using Domain.Enums;

namespace Domain.Entities.Examiner
{
    public class Question : EntityAudit
    {
        public Guid Id { get; set; }
        public required string QuestionText { get; set; }
        public required QuestionType QuestionType { get; set; }
        public required int Mark { get; set; }
        public required bool RequireManulReview { get; set; }
        public MultipleChoiseQuestion? MultipleChoiseQuestion { get; set; }
        public TrueFalseQuestion? TrueFalseQuestion { get; set; }
        public ShortAnswerQuestion? ShortAnswerQuestion { get; set; }
        public List<QuestionHistory>? Histories { get; set; }
        //public List<QuestionTranslation> Translations  { get; set; }
    }
}
