using Application.Commons.Models.Pageination;
using Domain.Entities.EntityLookup;
using Domain.Extentions;
using Domain.Lookups;
using Domain.Managers;
using LinqKit;
using MediatR;

namespace Application.Lookups.Queries.Sources.GetAllSources
{
    public class GetAllSourceQueryHandler(IRepositoryManager repositoryManager) : IRequestHandler<GetAllSourceQuery, PageResponse<GetAllSourceQueryResult>>
    {
        private readonly IRepositoryManager _repositoryManager = repositoryManager;

        public async Task<PageResponse<GetAllSourceQueryResult>> Handle(GetAllSourceQuery request, CancellationToken cancellationToken)
        {
            var query = PredicateBuilder.New<Source>(true);

            if (request.Tags.HasValue)
                query = query.And(q => q.Tags.Contains(request.Tags.Value));

            if (request.TypeId.HasValue)
                query = query.And(q => q.Type == request.TypeId);

            if (!string.IsNullOrEmpty(request.Title))
                query = query.And(q => q.Title.ToLower().Contains(request.Title.ToLower()));

            
            var sources = _repositoryManager.SourceRepository.GetAll(query, request.PageNumber,request.PageSize).ToList();

            var result = sources.Select(e => new GetAllSourceQueryResult
            {
                Id = e.Id,
                Description = e.Description,
                Tags = e.Tags.IsNotNull() ? _repositoryManager.TagRepository.GetCollection().Find(t => e.Tags.Contains(t.Id)).Select(e => new TagDto
                {
                    Name = e.Name,
                    ColorCode = e.ColorHexCode
                }).ToList() : null,
                Title = e.Title,
                Type = new SourceTypeLookup(e.Type),
                URL = e.URL
            }).ToList();

            var count = _repositoryManager.SourceRepository.Count();


            return new PageResponse<GetAllSourceQueryResult>(result,request.PageNumber,request.PageSize, count);
        }
    }
}
