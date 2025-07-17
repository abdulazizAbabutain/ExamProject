using Application.Commons.Managers;
using Application.Commons.Models.Results;
using Application.Commons.SharedModelResult;
using Application.Commons.SharedModelResult.Source;
using Domain.Extentions;
using Domain.Managers;
using MapsterMapper;
using MediatR;

namespace Application.Tags.Queries.GetTagDetails;

public class GetTagDetailsQueryHandler(IRepositoryManager repositoryManager, IMapper mapper) : IRequestHandler<GetTagDetailsQuery, Result<GetTagDetailsQueryResult>>
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<GetTagDetailsQueryResult>> Handle(GetTagDetailsQuery request, CancellationToken cancellationToken)
    {
        var tag = _repositoryManager.TagRepository.GetById(request.Id);
        if (tag.IsNull())
            return Result<GetTagDetailsQueryResult>.NotFoundFailure(nameof(request.Id), $"tag with an {request.Id} was not found");

        IEnumerable<DuplicatedItem> tagDuplicates = null;
        if (tag.DuplicationReview.IsNotNull() && tag.DuplicationReview.IsDuplicated)
        {
            tagDuplicates = _repositoryManager.TagRepository.GetCollection()
                .Find(e => tag.DuplicationReview.DuplicateOf.Contains(e.Id) && !e.IsArchived).Select(t => new DuplicatedItem
                {
                    Id = t.Id,
                    Name = t.Name,
                }).ToList();
        }

        var tagDetails = _mapper.Map<GetTagDetailsQueryResult>(tag);

        if (tagDetails.ReviewResult.IsNotNull())
            tagDetails.ReviewResult.Items = tagDuplicates;



        return Result<GetTagDetailsQueryResult>.Success(tagDetails);
    }
}