using Application.Commons.Managers;
using MediatR;

namespace Application.Questions.Queries.GetAllQuestions;

public class GetAllQuestionsQueryHandler(IServiceManager serviceManager) : IRequestHandler<GetAllQuestionsQuery, IEnumerable<GetAllQuestionsQueryResult>>
{
    private readonly IServiceManager _serviceManager = serviceManager;

    public async Task<IEnumerable<GetAllQuestionsQueryResult>> Handle(GetAllQuestionsQuery request, CancellationToken cancellationToken)
    {
        var result = _serviceManager.QuestionService.GetQuestions();

        return result.Select(e => new GetAllQuestionsQueryResult 
        {
            Id = e.Id,
            Mark = e.Mark,
            QuestionText = e.QuestionText,
            QuestionType = e.QuestionType,
            RequireManulReview = e.RequireManulReview   
        }).ToList();
    }
}
