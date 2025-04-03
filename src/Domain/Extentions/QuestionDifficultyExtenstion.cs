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
                    return QuestionDifficultyMatrixes.Basic;
             
                case QuestionDifficulty.Simple:
                    return QuestionDifficultyMatrixes.Simple;

                case QuestionDifficulty.Manageable:
                    return QuestionDifficultyMatrixes.Manageable;

                case QuestionDifficulty.Average:
                    return QuestionDifficultyMatrixes.Average;

                case QuestionDifficulty.Challenging:
                    return QuestionDifficultyMatrixes.Challenging;

                case QuestionDifficulty.Tough:
                    return QuestionDifficultyMatrixes.Tough; 

                case QuestionDifficulty.Extreme:
                    return QuestionDifficultyMatrixes.Extreme;

                default:
                    return QuestionDifficultyMatrixes.Average;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="difficulty"></param>
        /// <returns>float </returns>
        public static QuestionDifficulty GetDifficultyCategory(this short mattrix)
        {
            if (mattrix >= QuestionDifficultyMatrixes.Basic && mattrix < QuestionDifficultyMatrixes.Simple)
                return QuestionDifficulty.Basic;

            if (mattrix >= QuestionDifficultyMatrixes.Simple && mattrix < QuestionDifficultyMatrixes.Manageable)
                return QuestionDifficulty.Simple;

            if (mattrix >= QuestionDifficultyMatrixes.Manageable && mattrix < QuestionDifficultyMatrixes.Average)
                return QuestionDifficulty.Manageable;

            if (mattrix >= QuestionDifficultyMatrixes.Average && mattrix < QuestionDifficultyMatrixes.Challenging)
                return QuestionDifficulty.Average;

            if (mattrix >= QuestionDifficultyMatrixes.Challenging && mattrix < QuestionDifficultyMatrixes.Tough)
                return QuestionDifficulty.Challenging;

            if (mattrix >= QuestionDifficultyMatrixes.Tough && mattrix < QuestionDifficultyMatrixes.Extreme)
                return QuestionDifficulty.Tough;

                return QuestionDifficulty.Extreme;
        }
    }
}
