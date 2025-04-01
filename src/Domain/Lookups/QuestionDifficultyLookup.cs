using Domain.Enums;

namespace Domain.Lookups
{
    public class QuestionDifficultyLookup
    {
     
        public QuestionDifficulty Id { get; set; }
        public string Value
        {
            get
            {
                switch (Id)
                {
                    case QuestionDifficulty.Extreme:
                        return nameof(QuestionDifficulty.Extreme);
                    case QuestionDifficulty.Tough:
                        return nameof(QuestionDifficulty.Tough);
                    case QuestionDifficulty.Challenging:
                        return nameof(QuestionDifficulty.Challenging);
                    case QuestionDifficulty.Average:
                        return nameof(QuestionDifficulty.Average);
                    case QuestionDifficulty.Manageable:
                        return nameof(QuestionDifficulty.Manageable);
                    case QuestionDifficulty.Simple:
                        return nameof(QuestionDifficulty.Simple);
                    case QuestionDifficulty.Basic:
                        return nameof(QuestionDifficulty.Basic);
                    default:
                        throw new NotImplementedException();
                }
            }
        }
    }
}
