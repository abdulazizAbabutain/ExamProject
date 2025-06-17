using Ardalis.GuardClauses;

namespace Domain.Entities.Examiner
{
    public class MultipleChoiceQuestionOption
    {
        public MultipleChoiceQuestionOption(string optionText, bool isCorrect, float weight, string? feedback = null)
        {
            Guard.Against.Null(weight);
            Guard.Against.Negative(weight);

            Id = Guid.NewGuid();
            OptionText = Guard.Against.NullOrEmpty(optionText);
            IsCorrect = Guard.Against.Null(isCorrect);
            Weight = weight;
            FeedBack = feedback;
        }
        #region propity 
        public Guid Id { get; private set; }
        public string OptionText { get; private set; }
        public bool IsCorrect { get; private set; }
        public float Weight { get; private set; }
        public string? FeedBack { get; private set; }
        #endregion
    }
}
