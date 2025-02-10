using Application.Commons.Extentions;
using Domain.Entities.Audit;
using Domain.Entities.History;
using Domain.Enums;

namespace Domain.Entities.Examiner
{
    public class Question : EntityAudit
    {
        public Guid Id { get; private set; }
        public string QuestionText { get; private set; }
        public List<string> Variants { get; private set; }
        public QuestionType QuestionType { get; private set; }
        public int Mark { get; private set; }
        public bool RequireManulReview { get; private set; }
        /// <summary>
        /// an index start from 0 to 100, that will detrmain the overall difficulty of the queastion. 
        /// </summary>
        public short DifficultyIndex { get; private set; } 
        public List<string>? Tags { get; private set; }
        public MultipleChoiseQuestion? MultipleChoiseQuestion { get; private set; }
        public TrueFalseQuestion? TrueFalseQuestion { get; private set; }
        public ShortAnswerQuestion? ShortAnswerQuestion { get; private set; }
        public List<QuestionHistory>? Histories { get; private set; }

        private Question(string questionText, QuestionType questionType, int mark, bool requireManulReview,
            List<string>? tags, QuestionDifficulty difficulty)
        {
            Id = Guid.NewGuid();
            QuestionText = questionText;
            Mark = mark;
            QuestionType = questionType;
            RequireManulReview = requireManulReview;
            Tags = tags;
            DifficultyIndex = difficulty.GetMattrix();
        }

        private Question()
        {

        }


        public static Question CreateMultipleChoiseQuestion(string questionText, QuestionType questionType, int mark, bool requireManulReview,
            List<string>? tags, QuestionDifficulty difficulty,
            MultipleChoiseQuestion multipleChoiseQuestion)
        {
            var question = new Question(questionText, questionType, mark, requireManulReview, tags, difficulty);
            
            question.MultipleChoiseQuestion = multipleChoiseQuestion;

            question.Created();
            question.History(Enums.EntityHistoryType.Added);

            return question;
        }

        public static Question CreateTrueAndFalse(string questionText, QuestionType questionType, int mark, bool requireManulReview,
           List<string>? tags, QuestionDifficulty difficulty,
           TrueFalseQuestion trueFalseQuestion)
        {
            var question = new Question(questionText, questionType, mark, requireManulReview, tags, difficulty);

            question.Created();
            question.History(Enums.EntityHistoryType.Added);

            return question;
        }

        public void History(Enums.EntityHistoryType type)
        {
            Histories ??= new List<QuestionHistory>();
            Histories.Add(QuestionHistory.History(this, VerstionNumber, type));
        }
    }
}
