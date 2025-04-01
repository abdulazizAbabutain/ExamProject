using Application.Commons.Extentions;
using Domain.Entities.Audit;
using Domain.Enums;
using Domain.Extentions;
using LiteDB;

namespace Domain.Entities.Examiner
{
    public class Question : EntityAudit
    {
        public Guid Id { get; private set; }
        public string QuestionText { get; private set; }
        public List<string>? Variants { get; private set; }
        public QuestionType QuestionType { get; private set; }
        public int Mark { get; private set; }
        public bool RequireManulReview { get; private set; }
        public Guid Language { get; set; }
        /// <summary>
        /// an index start from 0 to 100, that will detrmain the overall difficulty of the queastion. 
        /// </summary>
        public short DifficultyIndex { get; private set; } 
        public List<Guid>? Tags { get; private set; }
        public MultipleChoiseQuestion? MultipleChoiseQuestion { get; private set; }
        public TrueFalseQuestion? TrueFalseQuestion { get; private set; }
        public ShortAnswerQuestion? ShortAnswerQuestion { get; private set; }
        public List<Guid>? Sources { get; set; }

        public Question(string questionText, List<string>? variants, Guid language, QuestionType questionType, int mark, bool requireManulReview,
            List<Guid>? tags,List<Guid>? sources ,QuestionDifficulty difficulty)
        {
            QuestionText = questionText;
            Variants = variants;
            Mark = mark;
            Language = language;
            QuestionType = questionType;
            RequireManulReview = requireManulReview;
            Tags = tags;
            Sources = sources;
            DifficultyIndex = difficulty.GetMattrix();
        }

        private Question() { }


        public void CreateMultipleChoiseQuestion(MultipleChoiseQuestion multipleChoiseQuestion)
        {
            MultipleChoiseQuestion = multipleChoiseQuestion;
            Created();
        }

        public void CreateTrueAndFalse(TrueFalseQuestion trueFalseQuestion)
        {
            TrueFalseQuestion = trueFalseQuestion;
            Created();
        }

        public void UpdateBasicQueastion(string questionText, List<string>? variants, int mark, bool requireManulReview, List<string> tags, short difficultyIndex)
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

            if(RequireManulReview != requireManulReview)
            {
                RequireManulReview = requireManulReview;

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
