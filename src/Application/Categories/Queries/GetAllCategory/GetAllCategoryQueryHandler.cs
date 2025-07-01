using Application.Commons.Models.Pageination;
using Domain.Entities.EntityLookup;
using Domain.Extentions;
using Domain.Managers;
using LinqKit;
using MediatR;

namespace Application.Categories.Queries.GetAllCategory
{
    public class GetAllCategoryQueryHandler(IRepositoryManager repositoryManager) : IRequestHandler<GetAllCategoryQuery, PageResponse<GetAllCategoryQueryResult>>
    {
        private readonly IRepositoryManager _repositoryManager = repositoryManager;

        public async Task<PageResponse<GetAllCategoryQueryResult>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {

            var query = PredicateBuilder.New<Category>(true);

            if (request.IsRoot.HasValue)
                query = query.And(q => q.IsRoot == request.IsRoot);

            if (request.Level.HasValue)
                query = query.And(q => q.Level == request.Level);

            if (request.Search.IsNotNullOrEmpty())
                query = query.And(q => q.Name.Contains(request.Search));

            var category = _repositoryManager.CategoryRepository.GetAll(query, request.PageNumber, request.PageSize);

            var result = category.Select(cat => new GetAllCategoryQueryResult
            {
                Id = cat.Id,
                Name = cat.Name,
                Level = cat.Level,
                ParentId = cat.ParentId,
            }).ToList();

            var count = _repositoryManager.CategoryRepository.Count();

            return new PageResponse<GetAllCategoryQueryResult>(result, request.PageNumber, request.PageSize, count);
        }
    }
}
