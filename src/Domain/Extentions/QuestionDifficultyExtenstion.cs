using Domain.Constants;
using Domain.Enums;

namespace Application.Commons.Extentions;

/// <summary>
/// Provides extension methods for mapping between <see cref="QuestionDifficulty"/> levels and their corresponding matrix values.
/// </summary>
public static class QuestionDifficultyExtension
{
    /// <summary>
    /// Converts a <see cref="QuestionDifficulty"/> enum value into its corresponding matrix score.
    /// </summary>
    /// <param name="difficulty">The difficulty level to convert.</param>
    /// <returns>A <see cref="short"/> value representing the matrix value of the given difficulty level.</returns>
    public static short GetMatrix(this QuestionDifficulty difficulty)
    {
        return DifficultyToMatrixMap.TryGetValue(difficulty, out var matrix)
           ? matrix
           : QuestionDifficultyMatrixes.Average;
    }
    /// <summary>
    /// Determines the <see cref="QuestionDifficulty"/> level that corresponds to a given matrix value.
    /// </summary>
    /// <param name="matrix">The matrix value to evaluate.</param>
    /// <returns>A <see cref="QuestionDifficulty"/> category based on the provided matrix value.</returns>

    public static QuestionDifficulty GetDifficultyCategory(this short matrix)
    {
        if (matrix >= QuestionDifficultyMatrixes.Basic && matrix < QuestionDifficultyMatrixes.Simple)
            return QuestionDifficulty.Basic;

        if (matrix >= QuestionDifficultyMatrixes.Simple && matrix < QuestionDifficultyMatrixes.Manageable)
            return QuestionDifficulty.Simple;

        if (matrix >= QuestionDifficultyMatrixes.Manageable && matrix < QuestionDifficultyMatrixes.Average)
            return QuestionDifficulty.Manageable;

        if (matrix >= QuestionDifficultyMatrixes.Average && matrix < QuestionDifficultyMatrixes.Challenging)
            return QuestionDifficulty.Average;

        if (matrix >= QuestionDifficultyMatrixes.Challenging && matrix < QuestionDifficultyMatrixes.Tough)
            return QuestionDifficulty.Challenging;

        if (matrix >= QuestionDifficultyMatrixes.Tough && matrix < QuestionDifficultyMatrixes.Extreme)
            return QuestionDifficulty.Tough;

        return QuestionDifficulty.Extreme;
    }

    /// <summary>
    /// A cached mapping between <see cref="QuestionDifficulty"/> enum values and their corresponding matrix scores.
    /// </summary>
    /// <remarks>
    /// This dictionary improves performance by avoiding repetitive switch-case logic or calculations when converting
    /// difficulty levels to their associated matrix values. The values are defined in <see cref="QuestionDifficultyMatrixes"/>.
    /// </remarks>
    private static readonly Dictionary<QuestionDifficulty, short> DifficultyToMatrixMap =
        new Dictionary<QuestionDifficulty, short>
        {
            { QuestionDifficulty.Basic, QuestionDifficultyMatrixes.Basic },
            { QuestionDifficulty.Simple, QuestionDifficultyMatrixes.Simple },
            { QuestionDifficulty.Manageable, QuestionDifficultyMatrixes.Manageable },
            { QuestionDifficulty.Average, QuestionDifficultyMatrixes.Average },
            { QuestionDifficulty.Challenging, QuestionDifficultyMatrixes.Challenging },
            { QuestionDifficulty.Tough, QuestionDifficultyMatrixes.Tough },
            { QuestionDifficulty.Extreme, QuestionDifficultyMatrixes.Extreme },
        };
}
