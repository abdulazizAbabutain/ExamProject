using Domain.Enums;

namespace Domain.Entities
{
    public class MultipleChoiseQuestion
    {
        public Guid Id { get; set; }
        public List<MultipleChoiseQuestionOption> Options { get; set; }
        public MultipleChoiseQuestionType Type { get; set; }
    }
}
