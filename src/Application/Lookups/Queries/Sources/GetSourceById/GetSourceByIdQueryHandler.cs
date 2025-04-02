using Application.Lookups.Queries.Sources.GetAllSources;
using Domain.Extentions;
using Domain.Lookups;
using Domain.Managers;
using MediatR;

namespace Application.Lookups.Queries.Sources.GetSourceById
{
    public class GetSourceByIdQueryHandler(IRepositoryManager repositoryManager) : IRequestHandler<GetSourceByIdQuery, GetSourceByIdQueryResult>
    {
        private readonly IRepositoryManager _repositoryManager = repositoryManager;

        public async Task<GetSourceByIdQueryResult> Handle(GetSourceByIdQuery request, CancellationToken cancellationToken)
        {
            var source = _repositoryManager.SourceRepository.GetById(request.Id);
            // check if not send return 404 
            // should be exception or flow?

            return new GetSourceByIdQueryResult
            {
                Id = source.Id,
                CreationDate = source.CreationDate,
                Description = source.Description,
                LastModifiedDate = source.LastModifiedDate,
                Tags = source.Tags.IsNotNull() ? _repositoryManager.TagRepository.GetCollection().Find(t => source.Tags.Contains(t.Id)).Select(e => new TagDto
                {
                    Name = e.Name,
                    ColorCode = e.ColorHexCode, 
                }).ToList() : null,
                Title = source.Title,
                Type = new SourceTypeLookup()
                {
                    Id = source.Type
                },
                URL = source.URL,
                VersionNumber = source.VersionNumber
            };

        }
    }
}
