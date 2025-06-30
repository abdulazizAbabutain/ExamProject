using Application.Commons.Models.Pageination;
using Application.Commons.Models.Results;
using Application.Commons.SharedModelResult;
using Domain.Entities.Sources;
using Domain.Extentions;
using Domain.Managers;
using LinqKit;
using Mapster;
using MapsterMapper;
using MediatR;

namespace Application.Sources.Queries.GetAllSources
{
    public class GetAllSourceQueryHandler(IRepositoryManager repositoryManager, IMapper mapper) : IRequestHandler<GetAllSourceQuery, Result<PageResponse<GetAllSourceQueryResult>>>
    {
        private readonly IRepositoryManager _repositoryManager = repositoryManager;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<PageResponse<GetAllSourceQueryResult>>> Handle(GetAllSourceQuery request, CancellationToken cancellationToken)
        {
            var query = PredicateBuilder.New<Source>(true);

            if (request.Tags.HasValue)
            {
                var isArchived = _repositoryManager.TagRepository.GetCollection().FindOne(t => t.Id == request.Tags.Value)?.IsArchived ?? true;
                if(!isArchived)
                    query = query.And(q => q.Tags.IsNotNull() && q.Tags.Contains(request.Tags.Value));
            }

            if (request.TypeId.HasValue)
                query = query.And(q => q.Type == request.TypeId);

            if (!string.IsNullOrEmpty(request.Title))
                query = query.And(q => q.Title.ToLower().Contains(request.Title.ToLower()));

            

            var sources = _repositoryManager.SourceRepository.GetAll(query, request.PageNumber, request.PageSize).ToList();
            var count = _repositoryManager.SourceRepository.Count();

            if (sources.IsNull())
                return Result<PageResponse<GetAllSourceQueryResult>>.Success(new PageResponse<GetAllSourceQueryResult>(request.PageNumber,request.PageSize, count));

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



            return Result<PageResponse<GetAllSourceQueryResult>>.Success(new PageResponse<GetAllSourceQueryResult>(result, request.PageNumber, request.PageSize, count));
        }
    }
}
