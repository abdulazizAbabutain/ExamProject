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
                    case QuestionType.MultipleChoise:
                        return EnumValues.MultipleChoise;
                    case QuestionType.TrueAndFalse:
                        return EnumValues.TrueAndFalse;
                    default:
                        throw new NotImplementedException();
                }
            }
        }
    }
}
