using Application.Commons.Models.Pageination;
using Application.Commons.SharedModelResult;
using Domain.Entities.Sources;
using Domain.Managers;
using LinqKit;
using Mapster;
using MapsterMapper;
using MediatR;

namespace Application.Sources.Queries.GetAllSources
{
    public class GetAllSourceQueryHandler(IRepositoryManager repositoryManager, IMapper mapper) : IRequestHandler<GetAllSourceQuery, PageResponse<GetAllSourceQueryResult>>
    {
        private readonly IRepositoryManager _repositoryManager = repositoryManager;
        private readonly IMapper _mapper = mapper;

        public async Task<PageResponse<GetAllSourceQueryResult>> Handle(GetAllSourceQuery request, CancellationToken cancellationToken)
        {
            var query = PredicateBuilder.New<Source>(true);

            if (request.Tags.HasValue)
                query = query.And(q => q.Tags.Contains(request.Tags.Value));

            if (request.TypeId.HasValue)
                query = query.And(q => q.Type == request.TypeId);

            if (!string.IsNullOrEmpty(request.Title))
                query = query.And(q => q.Title.ToLower().Contains(request.Title.ToLower()));

            var sources = _repositoryManager.SourceRepository.GetAll(query, request.PageNumber, request.PageSize).ToList();

            var allTagIds = sources.SelectMany(s => s.Tags ?? Enumerable.Empty<Guid>()).Distinct().ToList();

            var tagMap = _repositoryManager.TagRepository.GetCollection().Find(t => allTagIds.Contains(t.Id) && !t.IsArchived).ToDictionary(t => t.Id);

            var result = sources.Select(source => new GetAllSourceQueryResult
            {
                Id = source.Id,
                Description = source.Description,
                Tags = source.Tags?.Select(tagId => tagMap.TryGetValue(tagId, out var tag) ? _mapper.Map<TagResult>(tag) : null).Where(t => t != null)!.ToList() ?? new(),
                Title = source.Title,
                Type = source.Type,
            }).ToList();

            var count = _repositoryManager.SourceRepository.Count();


            return new PageResponse<GetAllSourceQueryResult>(result, request.PageNumber, request.PageSize, count);
        }
    }
}
