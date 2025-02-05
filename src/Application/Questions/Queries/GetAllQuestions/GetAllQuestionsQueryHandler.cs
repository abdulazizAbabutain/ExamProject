using Application.Commons.Managers;
using Application.Commons.Models.Pageination;
using MediatR;

namespace Application.Questions.Queries.GetAllQuestions;

public class GetAllQuestionsQueryHandler(IServiceManager serviceManager) : IRequestHandler<GetAllQuestionsQuery, PageResponse<GetAllQuestionsQueryResult>>
{
    private readonly IServiceManager _serviceManager = serviceManager;

    public async Task<PageResponse<GetAllQuestionsQueryResult>> Handle(GetAllQuestionsQuery request, CancellationToken cancellationToken)
    {
        var result = _serviceManager.QuestionService.GetQuestions(request.PageNumber,request.PageSize);
        var count = _serviceManager.QuestionService.Count();
       
        return new PageResponse<GetAllQuestionsQueryResult>(result.Select(e => new GetAllQuestionsQueryResult
        {
            Mark = e.Mark,
            QuestionText = e.QuestionText,
            QuestionType = e.QuestionType,
            RequireManulReview = e.RequireManulReview,
            Id = e.Id
        }).ToList(),request.PageNumber,request.PageSize, count);
    }
}
