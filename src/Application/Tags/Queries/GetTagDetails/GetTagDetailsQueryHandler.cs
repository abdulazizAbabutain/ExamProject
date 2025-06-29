using Application.Commons.Managers;
using Application.Commons.Models.Results;
using Domain.Extentions;
using Domain.Managers;
using MediatR;

namespace Application.Tags.Queries.GetTagDetails;

public class GetTagDetailsQueryHandler(IRepositoryManager repositoryManager) : IRequestHandler<GetTagDetailsQuery, Result<GetTagDetailsQueryResult>>
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;

    public async Task<Result<GetTagDetailsQueryResult>> Handle(GetTagDetailsQuery request, CancellationToken cancellationToken)
    {
        var tag = _repositoryManager.TagRepository.GetById(request.Id);
        if (tag.IsNull())
            return Result<GetTagDetailsQueryResult>.NotFoundFailure($"tag with an {request.Id} was not found");

        var tagRess =  new GetTagDetailsQueryResult()
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

        return Result<GetTagDetailsQueryResult>.Success(tagRess);
    }
}