using Domain.Entities.Audit;
using Domain.Entities.History;
using Domain.Entities.Translation;
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
        public List<QuestionTranslation> Translations  { get; set; }


        public static Question CreateMultipleChoiseQuestion(string questionText, QuestionType questionType, int mark, bool requireManulReview, 
            MultipleChoiseQuestion multipleChoiseQuestion)
        {
            var question = new Question
            {
                Id = Guid.NewGuid(),
                QuestionText = questionText,
                Mark = mark,
                QuestionType = questionType,
                RequireManulReview = requireManulReview,
                MultipleChoiseQuestion = multipleChoiseQuestion
            };
            question.Created();
            question.History(Enums.EntityHistoryType.Added);
            
            return question;    
        }

        public static Question CreateTrueAndFalse(string questionText, QuestionType questionType, int mark, bool requireManulReview,
           TrueFalseQuestion trueFalseQuestion)
        {
            var question = new Question
            {
                Id = Guid.NewGuid(),
                QuestionText = questionText,
                Mark = mark,
                QuestionType = questionType,
                RequireManulReview = requireManulReview,
                TrueFalseQuestion = trueFalseQuestion
            };
            question.Created();
            question.History(Enums.EntityHistoryType.Added);

            return question;
        }

        public void History(Enums.EntityHistoryType type)
        {
            Histories ??= new List<QuestionHistory>();
            Histories.Add(QuestionHistory.History(this,VerstionNumber, type));
        }
    }
}
