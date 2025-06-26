using Application.Commons.Managers;
using Domain.Extentions;
using Domain.Managers;
using MediatR;

namespace Application.Tags.Queries.GetTagDetails;

public class GetTagDetailsQueryHandler(IRepositoryManager repositoryManager) : IRequestHandler<GetTagDetailsQuery, GetTagDetailsQueryResult>
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;

    public async Task<GetTagDetailsQueryResult> Handle(GetTagDetailsQuery request, CancellationToken cancellationToken)
    {
        var tag = _repositoryManager.TagRepository.GetById(request.Id);
        if (tag.IsNull())
            return new GetTagDetailsQueryResult();
        return new GetTagDetailsQueryResult()
        {
            Id = tag.Id,
            Name = tag.Name,
            ColorGroup = tag.ColorGroup,
            ColorHexCode = tag.ColorHexCode,
            CreationDate = tag.CreationDate,
            IsArchived = tag.IsArchived,
            LastArchiveDate = tag.LastArchiveDate,
            LastModifiedDate = tag.LastModifiedDate,
            VersionNumber = tag.VersionNumber
        };
    }
}