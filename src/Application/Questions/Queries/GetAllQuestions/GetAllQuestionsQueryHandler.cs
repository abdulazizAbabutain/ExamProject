using Application.Commons.Extentions;
using Application.Commons.Models.Pageination;
using Domain.Entities.Examiner;
using Domain.Extentions;
using Domain.Lookups;
using Domain.Managers;
using LinqKit;
using LiteDB;
using MediatR;
using System.Linq;

namespace Application.Questions.Queries.GetAllQuestions;

public class GetAllQuestionsQueryHandler(IRepositoryManager repositoryManager) : IRequestHandler<GetAllQuestionsQuery, PageResponse<GetAllQuestionsQueryResult>>
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;

    public async Task<PageResponse<GetAllQuestionsQueryResult>> Handle(GetAllQuestionsQuery request, CancellationToken cancellationToken)
    {
        var query = PredicateBuilder.New<Question>();

        Guid? tagId = null;

        if (!string.IsNullOrWhiteSpace(request.Tags))
            tagId = _repositoryManager.TagRepository.GetTagReference(request.Tags);

        if (tagId.HasValue)
            query = query.Or(q => q.Tags.Contains(tagId.Value));

        if (request.QuestionType.HasValue)
            query = query.Or(q => q.QuestionType == request.QuestionType);

        if (request.RequireManualReview.HasValue)
            query = query.Or(q => q.RequireManualReview == request.RequireManualReview.Value);

        var questions = _repositoryManager.QuestionRepository.GetAll(query, request.PageNumber, request.PageSize).ToList();

        var data = questions.Select(q => new GetAllQuestionsQueryResult
        {
            Id = q.Id,
            Mark = q.Mark,
            QuestionText = q.QuestionText,
            QuestionType = new QuestionTypeLookup { Id = q.QuestionType },
            RequireManulReview = q.RequireManualReview,
            Difficulty = new QuestionDifficultyLookup { Id = q.DifficultyIndex.GetDifficultyCategory() },
            Tags = q.Tags.IsNotNull() ? _repositoryManager.TagRepository.GetCollection().Find(t => q.Tags.Contains(t.Id)).Select(e => e.Name).ToList() : null,
        }).ToList();

        var totalCount = _repositoryManager.QuestionRepository.Count();

        return new PageResponse<GetAllQuestionsQueryResult>(data, request.PageNumber, request.PageSize, totalCount);
    }
}
