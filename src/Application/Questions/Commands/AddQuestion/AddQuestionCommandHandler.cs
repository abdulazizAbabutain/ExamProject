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

namespace Application.Questions.Commands.AddQuestion;

internal class AddQuestionCommandHandler(IServiceManager serviceManager) : IRequestHandler<AddQuestionCommand, GetQuestionsByIdQueryResult>
{
    private readonly IServiceManager _serviceManager = serviceManager;

    public async Task<GetQuestionsByIdQueryResult> Handle(AddQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = BuildQuestion(request);
        _serviceManager.QuestionService.InsertQuestion(question);

        return new GetQuestionsByIdQueryResult
        {
            Id = question.Id,
            Mark = question.Mark,
            QuestionText = question.QuestionText,
            Variants = question.Variants,
            QuestionType = new QuestionTypeLookup
            {
                Id = question.QuestionType
            },
            Difficulty = new QuestionDifficultyLookup
            {
                Id = question.DifficultyIndex.GetDifficultyCategory()
            },
            Sources =  null,
            RequireManualReview = question.RequireManualReview,
            Tags = null,
            MultipleChoiceOptions = question.MultipleChoiseQuestion is not null ? question.MultipleChoiseQuestion.Options.Select(e => new MultipleChoiseQuestionResult
            {
                Id = e.Id,
                IsCorrect = e.IsCorrect,
                OptionText = e.OptionText,
                FeedBack = e.FeedBack,
                Weight = e.Weight
            }) : null,
            TrueAndFalse = question.TrueFalseQuestion is not null ? new TrueFalseQuestionQueryResult
            {
                Id = question.TrueFalseQuestion.Id,
                AnswerFeedBack = question.TrueFalseQuestion.AnswerFeedBack,
                IsCorrect = question.TrueFalseQuestion.IsCorrect,
                WrongFeedBack = question.TrueFalseQuestion.WrongAnswerFeedBack,
            } : null
        };
    }


    private Question BuildMultipleChoiseQuestion(AddQuestionCommand request)
    {
        var question = CreateQuestion(request);

        var options = new List<MultipleChoiseQuestionOption>(); 
        foreach(var item in request.Options!)
        {
            options.Add(MultipleChoiseQuestion.CreateOption(item.OptionText,item.IsCorrect,item.Weight,item.FeedBack));
        }
        question.CreateMultipleChoiceQuestion(new MultipleChoiseQuestion(options));
        
        return question;
    }

    private Question BuildTrueAndAnswer(AddQuestionCommand request)
    {
        var question = CreateQuestion(request);
        var model = request.TrueAndAnswer;
        var trueAndFalse = new TrueFalseQuestion(model!.IsCorrect, model.WrongFeedBack, model.AnswerFeedBack);
        question.CreateTrueAndFalse(trueAndFalse);
        return question;

    }


    private Question CreateQuestion(AddQuestionCommand request)
    {
        return new Question(request.QuestionText, request.Variants,request.QuestionType, request.Mark, request.RequireManulReview,request.Difficulty,
            request.Language,request.Tags,request.Sources,request.Category);
    }

    private Question BuildQuestion(AddQuestionCommand request)
    {
        switch (request.QuestionType)
        {
            case QuestionType.MultipleChoise:
                return BuildMultipleChoiseQuestion(request);
            case QuestionType.TrueAndFalse:
                return BuildTrueAndAnswer(request);

        }
        return null;
    }
}
