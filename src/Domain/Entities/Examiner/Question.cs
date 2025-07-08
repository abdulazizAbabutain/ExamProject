using Application.Commons.Extentions;
using Ardalis.GuardClauses;
using Domain.Auditing;
using Domain.Enums;
using Domain.Extentions;

namespace Domain.Entities.Examiner;

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
    /// Determine if the 
    /// </summary>
    public bool HasAttachments { get; private set; }
    /// <summary>
    /// File path for an image for handling images
    /// </summary>
    public List<string> ImagesFilePaths { get; private set; }
    /// <summary>
    /// Details specific to multiple choice questions (if applicable).
    /// </summary>
    public MultipleChoiseQuestion? MultipleChoiceQuestion { get; private set; }
    /// <summary>
    /// Details specific to true/false questions (if applicable).
    /// </summary>
    public TrueFalseQuestion? TrueFalseQuestion { get; private set; }
    /// <summary>
    /// Details specific to short answer questions (if applicable).
    /// </summary>
    public ShortAnswerQuestion? ShortAnswerQuestion { get; private set; }
    /// <summary>
    /// Details specific to long answer questions (if applicable).
    /// </summary>
    public LongAnswerQuestion? LongAnswerQuestion { get; private set; }
    /// <summary>
    /// Details specific to reordering questions (if applicable).
    /// </summary>
    public List<ReorderingQuestion>? ReorderingQuestions { get; private set; }
    #region lookups
    /// <summary>
    /// The language in which the question is written.
    /// </summary
    public Guid? Language { get; private set; }
    /// <summary>
    /// Optional list of tags used to categorize the question.
    /// </summary
    public List<Guid>? Tags { get; private set; }
    /// <summary>
    /// Optional list of sources (e.g., books, websites) from which the question originates.
    /// </summary>
    public List<Guid>? Sources { get; private set; }
    /// <summary>
    /// The category under which this question falls, if any.
    /// </summary>
    public Guid? Category { get; private set; }
    #endregion
    #endregion


    /// <summary>
    /// Initializes a new instance of the <see cref="Question"/> class with the specified text, type, mark, review requirement, difficulty, and optional metadata.
    /// </summary>
    /// <param name="questionText">The main text of the question. Must not be null or empty.</param>
    /// <param name="variants">Optional alternative phrasings of the question.</param>
    /// <param name="questionType">The type of the question (e.g., multiple choice, true/false).</param>
    /// <param name="mark">The number of points assigned to the question.</param>
    /// <param name="requireManualReview">Indicates whether the question requires manual grading.</param>
    /// <param name="difficulty">The difficulty level of the question. Must not be null.</param>
    /// <param name="language">Optional language identifier for the question.</param>
    /// <param name="tags">Optional list of tag identifiers for categorization.</param>
    /// <param name="sources">Optional list of source identifiers for reference.</param>
    /// <param name="category">Optional category identifier for the question.</param>
    public Question(string questionText, List<string>? variants, QuestionType questionType, int mark, bool requireManualReview,QuestionDifficulty difficulty, 
        Guid? language = null, List<Guid>? tags = null, List<Guid>? sources = null, Guid? category = null)
    {
        QuestionText = Guard.Against.NullOrEmpty(questionText);
        Variants = variants;
        Mark = mark;
        Language = language;
        QuestionType = questionType;
        RequireManualReview = requireManualReview;
        Tags = tags;
        Sources = sources;
        Category = category;
        DifficultyIndex = Guard.Against.Null(difficulty).GetMatrix();
    }

    /// <summary>
/// Initializes a new instance of the <see cref="Question"/> class for ORM or serialization purposes.
/// </summary>
private Question() { }


    public void CreateMultipleChoiceQuestion(MultipleChoiseQuestion multipleChoiseQuestion)
    {
        MultipleChoiceQuestion = multipleChoiseQuestion;
        Created();
    }

    public void CreateTrueAndFalse(TrueFalseQuestion trueFalseQuestion)
    {
        TrueFalseQuestion = trueFalseQuestion;
        Created();
    }

    public void CreateShortAnswer(string correctAnswer, List<string>? possibleAnswers, string? wrongAnswerFeedBack, string? correctAnswerFeedBack)
    {
        var shortAnswer = new ShortAnswerQuestion(correctAnswer, possibleAnswers, wrongAnswerFeedBack, correctAnswerFeedBack);
        ShortAnswerQuestion = shortAnswer;
        Created();
    }

    public void CreateLongAnswer(int? maximinWords, int? minimalWords, string answer, string? feedback)
    {
        var longAnswer = new LongAnswerQuestion(maximinWords,minimalWords,answer, feedback);
        LongAnswerQuestion = longAnswer;
        Created();
    }

    public void CreateReordering(IEnumerable<ReorderingQuestion> reorderingQuestions)
    {
        ReorderingQuestions ??= new List<ReorderingQuestion>();
        ReorderingQuestions.AddRange(reorderingQuestions);
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

        if(DifficultyIndex != difficultyIndex)
        {
            DifficultyIndex = difficultyIndex;
        }
    }

    public void RemoveTag(Guid tagId)
    {
        Tags.Remove(tagId);

        if (Tags.Count == 0)
            Tags = null;
    }
    
}