using Domain.Enums;

namespace Domain.Entities.Examiner
{
    public class MultipleChoiseQuestion
    {
        public Guid Id { get; set; }
        public List<MultipleChoiseQuestionOption> Options { get; set; }
        public MultipleChoiseQuestionType Type { get; set; }

        public static MultipleChoiseQuestionOption CreateOption(string optionText, bool isCorrect,float weight, string? feedback)
        {
            return new MultipleChoiseQuestionOption
            {
                Id = Guid.NewGuid(),
                IsCorrect = isCorrect,
                OptionText = optionText,
                Weight = weight,
                FeedBack = feedback
            };
        }

        public static MultipleChoiseQuestion CreateMultipleChoiseQuestion(List<MultipleChoiseQuestionOption> options)
        {
            return new MultipleChoiseQuestion()
            {
                Type = options.Where(e => e.IsCorrect).Count() > 1 ? MultipleChoiseQuestionType.MuktipleChoise : MultipleChoiseQuestionType.SingleChoise,
                Id = Guid.NewGuid(),
                Options = options
            };
        }
    }
}
