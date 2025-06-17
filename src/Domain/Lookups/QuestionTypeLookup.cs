using Domain.Constants;
using Domain.Enums;

namespace Domain.Lookups
{
    public class QuestionTypeLookup
    {
      
        public QuestionType Id { get; set; }
        public string Value
        {
            get
            {
                switch (Id)
                {
                    case QuestionType.MultipleChoice:
                        return EnumValues.MultipleChoise;
                    case QuestionType.TrueAndFalse:
                        return EnumValues.TrueAndFalse;
                    case QuestionType.ShortAnswer: 
                        return EnumValues.ShortAnswer;
                    case QuestionType.LongAnswer:
                        return EnumValues.LongAnswer;
                    case QuestionType.Reordering:
                        return EnumValues.Reordering;
                    default:
                        throw new NotImplementedException();
                }
            }
        }
    }
}
