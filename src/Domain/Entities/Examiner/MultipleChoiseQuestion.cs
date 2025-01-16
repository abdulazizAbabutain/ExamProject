using Domain.Enums;

namespace Domain.Entities.Examiner
{
    public class MultipleChoiseQuestion
    {
        public Guid Id { get; set; }
        public required List<MultipleChoiseQuestionOption> Options { get; set; }
        public required MultipleChoiseQuestionType Type { get; set; }
    }
}
