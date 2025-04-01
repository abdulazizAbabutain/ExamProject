using Domain.Enums;
using System.Drawing;

namespace Domain.Entities.Examiner
{
    public class MultipleChoiseQuestion
    {
        public Guid Id { get; set; }
        public List<MultipleChoiseQuestionOption> Options { get; set; }
        public MultipleChoiseQuestionType Type { get; set; }


        public MultipleChoiseQuestion(List<MultipleChoiseQuestionOption> options)
        {
            Id = Guid.NewGuid();
            Type = options.Where(e => e.IsCorrect).Count() > 1 ? MultipleChoiseQuestionType.MuktipleChoise : MultipleChoiseQuestionType.SingleChoise;
            Options = options;
        }

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
    }
}
