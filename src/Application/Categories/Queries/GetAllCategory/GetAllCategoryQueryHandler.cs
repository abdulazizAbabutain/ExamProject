using Application.Commons.Models.Pageination;
using Domain.Entities.EntityLookup;
using Domain.Extentions;
using Domain.Managers;
using LinqKit;
using MapsterMapper;
using MediatR;

namespace Application.Categories.Queries.GetAllCategory
{
    public class GetAllCategoryQueryHandler(IRepositoryManager repositoryManager,IMapper mapper) : IRequestHandler<GetAllCategoryQuery, PageResponse<GetAllCategoryQueryResult>>
    {
        private readonly IRepositoryManager _repositoryManager = repositoryManager;
        private readonly IMapper _mapper = mapper;

        public async Task<PageResponse<GetAllCategoryQueryResult>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {

            var query = PredicateBuilder.New<Category>(true);

            if (request.IsRoot.HasValue)
                query = query.And(q => q.IsRoot == request.IsRoot);

            if (request.Level.HasValue)
                query = query.And(q => q.Level == request.Level);

            if (request.Search.IsNotNullOrEmpty())
                query = query.And(q => q.Name.Contains(request.Search));

            if (request.HasChildren.HasValue)
                query = query.And(q => q.HasChildren == request.HasChildren);

            var category = _repositoryManager.CategoryRepository.GetAll(query, request.PageNumber, request.PageSize);

            var count = _repositoryManager.CategoryRepository.Count();

            return new PageResponse<GetAllCategoryQueryResult>(_mapper.Map<IEnumerable<GetAllCategoryQueryResult>>(category), request.PageNumber, request.PageSize, count);
        }
    }
}
