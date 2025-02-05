using Domain.Entities.Audit;
using Domain.Entities.Examiner;
using Domain.Enums;

namespace Domain.Entities.History
{
    public class QuestionHistory 
    {
        public Guid Id { get; set; }
        public required string QuestionText { get; set; }
        public required QuestionType QuestionType { get; set; }
        public required int Mark { get; set; }
        public required bool RequireManulReview { get; set; }
        public Guid QuestionId { get; set; }
        public MultipleChoiseQuestion? MultipleChoiseQuestion { get; set; }
        public TrueFalseQuestion? TrueFalseQuestion { get; set; }


        public DateTime ActionDate { get; set; }
        public EntityHistoryType ActionType { get; set; }
        public int VerstionNumber { get; set; }
       
        public static QuestionHistory History(Question question, int verstion, EntityHistoryType historyType)
        {
            return new QuestionHistory()
            {
                Id = Guid.NewGuid(),
                Mark = question.Mark,
                QuestionText = question.QuestionText,
                QuestionType = question.QuestionType,
                RequireManulReview = question.RequireManulReview,
                MultipleChoiseQuestion = question.MultipleChoiseQuestion,
                TrueFalseQuestion = question.TrueFalseQuestion,
                QuestionId = question.Id,
                ActionDate = DateTime.Now,
                ActionType = historyType,
                VerstionNumber = verstion,
            };
        }


    }
}
