using Application.Commons.Extentions;
using Application.Commons.Managers;
using Application.Questions.Queries.GetQuestionsById;
using Application.Questions.Queries.GetQuestionsById.ResultModel;
using Domain.Entities.EntityLookup;
using Domain.Entities.Examiner;
using Domain.Enums;
using Domain.Extentions;
using Domain.Lookups;
using MediatR;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Application.Questions.Commands.AddQuestion;

internal class AddQuestionCommandHandler(IServiceManager serviceManager) : IRequestHandler<AddQuestionCommand, AddQuestionCommandResult>
{
    private readonly IServiceManager _serviceManager = serviceManager;

    public async Task<AddQuestionCommandResult> Handle(AddQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = BuildQuestion(request);
        _serviceManager.QuestionService.InsertQuestion(question);


        return new AddQuestionCommandResult
        {
            Id = question.Id
        };
    }

    public FeedbackQueryResult? GetFeedback(string? correctAnswerFeedback, string? WrongAnswerFeedback)
        {
            if (string.IsNullOrWhiteSpace(correctAnswerFeedback) && string.IsNullOrWhiteSpace(WrongAnswerFeedback))
                return null;

            return new FeedbackQueryResult
            {
                CorrectAnswerFeedback = correctAnswerFeedback,
                WrongAnswerFeedback = WrongAnswerFeedback
            };
        }


    private Question BuildMultipleChoiseQuestion(AddQuestionCommand request)
    {
        var question = CreateQuestion(request);

        var options = new List<MultipleChoiceQuestionOption>(); 
        foreach(var item in request.Options!)
        {
            options.Add(MultipleChoiseQuestion.CreateOption(item.OptionText,item.IsCorrect,item.Weight,item.FeedBack));
        }
        question.CreateMultipleChoiceQuestion(new MultipleChoiseQuestion(options));
        
        return question;
    }

    private Question BuildTrueAndFalseAnswer(AddQuestionCommand request)
    {
        var question = CreateQuestion(request);
        var model = request.TrueAndFalseAnswer;

        var feedbackCorrect = model.Feedback.IsNotNull() ? model.Feedback.CorrectAnswerFeedback : null;
        var feedbackWronge = model.Feedback.IsNotNull() ? model.Feedback.WrongAnswerFeedback : null;

        var trueAndFalse = new TrueFalseQuestion(model!.IsCorrect, feedbackCorrect, feedbackWronge);
        question.CreateTrueAndFalse(trueAndFalse);
        return question;

    }
    private Question BuildShortAnswer(AddQuestionCommand request)
    {
        var question = CreateQuestion(request);
        var model = request.ShortAnswer;

        var correctFeedback = model.Feedback.IsNotNull() ? model.Feedback.CorrectAnswerFeedback : null;
        var wrongFeedback = model.Feedback.IsNotNull() ? model.Feedback.WrongAnswerFeedback : null;

        question.CreateShortAnswer(model.CorrectAnswer, model.PossibleAnswers, wrongFeedback, correctFeedback);
        return question;
    }


    private Question BuildLongAnswer(AddQuestionCommand request)
    {
        var question = CreateQuestion(request);
        var model = request.LongAnswer;

        question.CreateLongAnswer(model.MaximinWords,model.MinimanWords,model.Answer,model.GeneralFeedback);
        return question;
    }

    private Question BuildReoderingQuestion(AddQuestionCommand request)
    {
        var question = CreateQuestion(request);
        var model = request.Reordering;

        var reorderingItems = new List<ReorderingQuestion>();
        foreach (var item in model)
        {
            var correctFeedback = item.Feedback.IsNotNull() ? item.Feedback.CorrectAnswerFeedback : null;
            var wrongFeedback = item.Feedback.IsNotNull() ? item.Feedback.WrongAnswerFeedback : null;

            reorderingItems.Add(new ReorderingQuestion(item.Value,item.Order, wrongFeedback, correctFeedback));
        }

        question.CreateReordering(reorderingItems);
        return question;
    }


    private Question CreateQuestion(AddQuestionCommand request)
    {
        return new Question(request.QuestionText, request.Variants,request.QuestionType, request.Mark, request.RequireManualReview,request.Difficulty,
            request.Language,request.Tags,request.Sources,request.Category);
    }

    private Question BuildQuestion(AddQuestionCommand request)
    {
        switch (request.QuestionType)
        {
            case QuestionType.MultipleChoice:
                return BuildMultipleChoiseQuestion(request);
            case QuestionType.TrueAndFalse:
                return BuildTrueAndFalseAnswer(request);
            case QuestionType.ShortAnswer:
                return BuildShortAnswer(request);
            case QuestionType.LongAnswer:
                return BuildLongAnswer(request);
            case QuestionType.Reordering:
                return BuildReoderingQuestion(request);
        }
        return null;
    }
}
