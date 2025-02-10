using Domain.Constants;
using Domain.Enums;

namespace Application.Commons.Extentions
{
    public static class QuestionDifficultyExtenstion
    {
        /// <summary>
        /// calcalute the startign mattrix for each given difficulty,
        /// </summary>
        /// <param name="difficulty"></param>
        /// <returns>float </returns>
        public static short GetMattrix(this QuestionDifficulty difficulty)
        {
            switch(difficulty)
            {
                case QuestionDifficulty.Basic:
                    return QuestionDifficultyMattrixes.Basic;
             
                case QuestionDifficulty.Simple:
                    return QuestionDifficultyMattrixes.Simple;

                case QuestionDifficulty.Manageable:
                    return QuestionDifficultyMattrixes.Manageable;

                case QuestionDifficulty.Average:
                    return QuestionDifficultyMattrixes.Average;

                case QuestionDifficulty.Challenging:
                    return QuestionDifficultyMattrixes.Challenging;

                case QuestionDifficulty.Tough:
                    return QuestionDifficultyMattrixes.Tough; 

                case QuestionDifficulty.Extreme:
                    return QuestionDifficultyMattrixes.Extreme;

                default:
                    return QuestionDifficultyMattrixes.Average;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="difficulty"></param>
        /// <returns>float </returns>
        public static QuestionDifficulty GetDifficultyCategory(this short mattrix)
        {
            if (mattrix >= QuestionDifficultyMattrixes.Basic && mattrix < QuestionDifficultyMattrixes.Simple)
                return QuestionDifficulty.Basic;

            if (mattrix >= QuestionDifficultyMattrixes.Simple && mattrix < QuestionDifficultyMattrixes.Manageable)
                return QuestionDifficulty.Simple;

            if (mattrix >= QuestionDifficultyMattrixes.Manageable && mattrix < QuestionDifficultyMattrixes.Average)
                return QuestionDifficulty.Manageable;

            if (mattrix >= QuestionDifficultyMattrixes.Average && mattrix < QuestionDifficultyMattrixes.Challenging)
                return QuestionDifficulty.Average;

            if (mattrix >= QuestionDifficultyMattrixes.Challenging && mattrix < QuestionDifficultyMattrixes.Tough)
                return QuestionDifficulty.Challenging;

            if (mattrix >= QuestionDifficultyMattrixes.Tough && mattrix < QuestionDifficultyMattrixes.Extreme)
                return QuestionDifficulty.Tough;

                return QuestionDifficulty.Extreme;
        }
    }
}
