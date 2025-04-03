using Application.Commons.Extentions;
using Domain.Entities.Audit;
using Domain.Enums;
using Domain.Extentions;

namespace Domain.Entities.Examiner
{
    public class Question : EntityAudit
    {
        #region propities 
        /// <summary>
        /// Unique identifier for the question.
        /// </summary>
        public Guid Id { get; private set; }
        /// <summary>
        /// The actual text of the question displayed to the user.
        /// </summary>
        public string QuestionText { get; private set; }
        /// <summary>
        /// Different phrase of questions
        /// </summary>
        public List<string>? Variants { get; private set; }
        /// <summary>
        /// The type of the question, e.g., MultipleChoice, TrueFalse, ShortAnswer.
        /// </summary>
        public QuestionType QuestionType { get; private set; }
        /// <summary>
        /// The number of points (marks) assigned to this question.
        /// </summary>
        public int Mark { get; private set; }
        /// <summary>
        /// Indicates whether the question needs manual grading (e.g., short answer).
        /// </summary>
        public bool RequireManualReview { get; private set; }
        /// <summary>
        /// An index (0–100) representing how difficult the question is perceived to be.
        /// </summary>
        public short DifficultyIndex { get; private set; }
        /// <summary>
        /// Details specific to multiple choice questions (if applicable).
        /// </summary>
        public MultipleChoiseQuestion? MultipleChoiseQuestion { get; private set; }
        /// <summary>
        /// Details specific to true/false questions (if applicable).
        /// </summary>
        public TrueFalseQuestion? TrueFalseQuestion { get; private set; }
        /// <summary>
        /// Details specific to short answer questions (if applicable).
        /// </summary>
        public ShortAnswerQuestion? ShortAnswerQuestion { get; private set; }
        #region lookups
        /// <summary>
        /// The language in which the question is written.
        /// </summary
        public Guid Language { get; set; }
        /// <summary>
        /// Optional list of tags used to categorize the question.
        /// </summary
        public List<Guid>? Tags { get; private set; }
        /// <summary>
        /// Optional list of sources (e.g., books, websites) from which the question originates.
        /// </summary>
        public List<Guid>? Sources { get; set; }
        /// <summary>
        /// The category under which this question falls, if any.
        /// </summary>
        public Guid? Category { get; set; }
        #endregion
        #endregion


        public Question(string questionText, List<string>? variants, Guid language, QuestionType questionType, int mark, bool requireManualReview,
            List<Guid>? tags,List<Guid>? sources ,QuestionDifficulty difficulty)
        {
            QuestionText = questionText;
            Variants = variants;
            Mark = mark;
            Language = language;
            QuestionType = questionType;
            RequireManualReview = requireManualReview;
            Tags = tags;
            Sources = sources;
            DifficultyIndex = difficulty.GetMattrix();
        }

        private Question() { }


        public void CreateMultipleChoiceQuestion(MultipleChoiseQuestion multipleChoiseQuestion)
        {
            MultipleChoiseQuestion = multipleChoiseQuestion;
            Created();
        }

        public void CreateTrueAndFalse(TrueFalseQuestion trueFalseQuestion)
        {
            TrueFalseQuestion = trueFalseQuestion;
            Created();
        }

        public void UpdateBasicQueastion(string questionText, List<string>? variants, int mark, bool requireManualReview, List<string> tags, short difficultyIndex)
        {
            Updated();
            if (!QuestionText.Equals(questionText))
            {
                QuestionText = questionText;
            }
            
            if(Variants.IsNotNull() && !Variants.Equals(variants) && (Variants.IsNotNull() && variants.IsNull()))
            {
                Variants = variants;
            }
            
            if(Mark != mark)
            {
                Mark = mark;
            }

            if(RequireManualReview != requireManualReview)
            {
                RequireManualReview = requireManualReview;

            }

            if(!Tags.Equals(tags))
            {
                //TODO: Fix
                Tags = null;

            }

            if(DifficultyIndex != difficultyIndex)
            {
                DifficultyIndex = difficultyIndex;
            }
        }
    }
}
