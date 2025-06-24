using Application.Commons.SharedModelResult;
using Application.Sources.Queries.GetAllSources;
using Application.Sources.Queries.GetSourceById.ResultModels;
using Domain.Enums;
using Domain.Extentions;
using Domain.Lookups;
using Domain.Managers;
using MediatR;

namespace Application.Sources.Queries.GetSourceById
{
    public class GetSourceByIdQueryHandler(IRepositoryManager repositoryManager) : IRequestHandler<GetSourceByIdQuery, GetSourceByIdQueryResult>
    {
        private readonly IRepositoryManager _repositoryManager = repositoryManager;

        public async Task<GetSourceByIdQueryResult> Handle(GetSourceByIdQuery request, CancellationToken cancellationToken)
        {
            var source = _repositoryManager.SourceRepository.GetById(request.Id);
            // check if not send return 404 
            // should be exception or flow?


            var tags = repositoryManager.TagRepository.GetCollection().Find(t => source.Tags.Contains(t.Id));

            return new GetSourceByIdQueryResult
            {
                Id = source.Id,
                Description = source.Description,
                Tags = tags.IsNotNull() ? tags.Select(e => new TagResult
                {
                    Name = e.Name,
                    ColorCode = e.ColorHexCode,
                }).ToList() : null,
                Title = source.Title,
                Type = source.Type,
                Metadata = source.Metadata.IsNotNull() ? source.Metadata.Select(e => new MetadataResult
                {
                    Id = e.Id,
                    FiledName = e.FiledName,
                    FiledType = e.FiledType,
                    IsRequired = e.IsRequired,
                    Value = e.Value,
                }).ToList() : null,
                References = source.References.Select(e => new SourceReferenceResult
                {
                    Id = e.Id,
                    Metadata = e.Metadata.Select(mt => new MetadataResult
                    {
                        Id = mt.Id,
                        FiledName = mt.FiledName,
                        FiledType = mt.FiledType,
                        IsRequired = mt.IsRequired,
                        Value = mt.Value,
                    }).ToList(),
                    Notes = e.Notes
                }).ToList()
            };

        }
    }
}
