using Application.Commons.Managers;
using Domain.Entities.Examiner;
using Domain.Enums;
using MediatR;

namespace Application.Questions.Commands.AddQuestion;

internal class AddQuestionCommandHandler(IServiceManager serviceManager) : IRequestHandler<AddQuestionCommand, AddQuestionCommandResult>
{
    private readonly IServiceManager _serviceManager = serviceManager;

    public async Task<AddQuestionCommandResult> Handle(AddQuestionCommand request, CancellationToken cancellationToken)
    {
        _serviceManager.QuestionService.InsertQuestion(BuildQuestion(request));
        return new AddQuestionCommandResult();
    }


    private Question BuildMultipleChoiseQuestion(AddQuestionCommand request)
    {
        var options = new List<MultipleChoiseQuestionOption>(); 
        foreach(var item in request.Options)
        {
            options.Add(MultipleChoiseQuestion.CreateOption(item.OptionText,item.IsCorrect,item.Weight,item.FeedBack));
        }
        return Question.CreateMultipleChoiseQuestion(request.QuestionText, request.QuestionType, request.Mark, 
            request.RequireManulReview,request.Tags, request.Difficulty,
            MultipleChoiseQuestion.CreateMultipleChoiseQuestion(options));
    }

    private Question BuildTrueAndAnswer(AddQuestionCommand request)
    {
        var model = request.TrueAndAnswer;
        var TrueAndFalse = new TrueFalseQuestion(model.IsCorrect, model.WrongFeedBack, model.AnswerFeedBack);
        return Question.CreateTrueAndFalse(request.QuestionText, request.QuestionType, request.Mark, request.RequireManulReview,request.Tags,request.Difficulty
            ,TrueAndFalse);
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
