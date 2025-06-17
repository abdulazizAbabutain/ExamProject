using Domain.Enums;

namespace Domain.Entities.Examiner
{
    public class MultipleChoiseQuestion
    {
        public List<MultipleChoiceQuestionOption> Options { get; set; }
        public MultipleChoiseQuestionType Type { get; set; }


        public MultipleChoiseQuestion(List<MultipleChoiceQuestionOption> options)
        {
            Type = options.Where(e => e.IsCorrect).Count() > 1 ? MultipleChoiseQuestionType.MuktipleChoise : MultipleChoiseQuestionType.SingleChoise;
            Options = options;
        }

        public static MultipleChoiceQuestionOption CreateOption(string optionText, bool isCorrect,float weight, string? feedback)
        {
            return new MultipleChoiceQuestionOption(optionText, isCorrect, weight, feedback);
        }
    }
}
