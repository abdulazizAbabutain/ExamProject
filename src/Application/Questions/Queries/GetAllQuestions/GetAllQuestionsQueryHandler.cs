using Application.Commons.Models.Pageination;
using Domain.Managers;
using LiteDB;
using MediatR;

namespace Application.Questions.Queries.GetAllQuestions;

public class GetAllQuestionsQueryHandler(IRepositoryManager repositoryManager) : IRequestHandler<GetAllQuestionsQuery, PageResponse<GetAllQuestionsQueryResult>>
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;

    public async Task<PageResponse<GetAllQuestionsQueryResult>> Handle(GetAllQuestionsQuery request, CancellationToken cancellationToken)
    {
        
        var data = _repositoryManager.QuestionRepository.Query();

        if (!string.IsNullOrEmpty(request.Tag))
            data = data.Where(e => e.Tags.Contains(request.Tag));

        var result = data.Select(e => new GetAllQuestionsQueryResult 
        {
            Id = e.Id,
            Mark = e.Mark,
            QuestionText = e.QuestionText,
            QuestionType = e.QuestionType,
            RequireManulReview = e.RequireManulReview
        }).Skip((request.PageNumber - 1) * request.PageSize)
        .Take(request.PageNumber)
        .ToList();

        var totalCount = _repositoryManager.QuestionRepository.Count();
        
        return new PageResponse<GetAllQuestionsQueryResult>(result, request.PageNumber, request.PageSize, totalCount);
    }
}
