using Application.Commons.Extentions;
using Application.Commons.Models.Pageination;
using Application.Commons.SharedModelResult;
using Domain.Entities.EntityLookup;
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
        var query = PredicateBuilder.New<Question>(true);

        Guid? tagId = null;
        Guid? categoryId = null;
        List<Tag> tags = null;
        List<Category> categories = null;

        if (!string.IsNullOrWhiteSpace(request.Tags))
            tagId = _repositoryManager.TagRepository.GetTagReference(request.Tags);
        if (request.Category.IsNotNullOrEmpty())
            categoryId = _repositoryManager.CategoryRepository.GetCollection().Find(e => e.Id == categoryId).Select(e => e.Id).FirstOrDefault();

        if (tagId.HasValue)
            query = query.And(q => q.Tags.Contains(tagId.Value));

        if (request.QuestionType.HasValue)
            query = query.And(q => q.QuestionType == request.QuestionType);

        if (request.RequireManualReview.HasValue)
            query = query.And(q => q.RequireManualReview == request.RequireManualReview.Value);

        if (categoryId.HasValue)
            query = query.And(q => q.Category == categoryId);

        if (request.Search.IsNotNullOrEmpty())
            query = query.And(q => q.QuestionText.Contains(request.Search));

        var questions = _repositoryManager.QuestionRepository.GetAll(query, request.PageNumber, request.PageSize).ToList();
        
        var questionTags = questions.Where(e => e.Tags.IsNotNull()).SelectMany(e => e.Tags).ToList();
        var questionCategories = questions.Where(e => e.Category.IsNotNull()).Select(e => e.Category).ToList();

        if(questionTags.IsNotNull() && questionTags.Any())
            tags = _repositoryManager.TagRepository.GetCollection().Find(t => questionTags.Contains(t.Id)).ToList();
        
        if(questionCategories.IsNotNull() && questionCategories.Any())
            categories = _repositoryManager.CategoryRepository.GetCollection().Find(e => questionCategories.Contains(e.Id)).ToList();

        var data = questions.Select(q => new GetAllQuestionsQueryResult
        {
            Id = q.Id,
            Mark = q.Mark,
            QuestionText = q.QuestionText,
            QuestionType = new QuestionTypeLookup { Id = q.QuestionType },
            RequireManualReview = q.RequireManualReview,
            Difficulty = new QuestionDifficultyLookup { Id = q.DifficultyIndex.GetDifficultyCategory() },
            Tags = q.Tags.IsNotNull() ? tags.Where(e => q.Tags.Contains(e.Id)).Select(e => new TagResult
            {
                Name = e.Name,
                ColorCode = e.ColorHexCode,
            }) : null,
            Category = categories.Where(cat => cat.Id == q.Category).Select(e => e.Name).FirstOrDefault(),
        }).ToList();

        var totalCount = _repositoryManager.QuestionRepository.Count();

        return new PageResponse<GetAllQuestionsQueryResult>(data, request.PageNumber, request.PageSize, totalCount);
    }
}
