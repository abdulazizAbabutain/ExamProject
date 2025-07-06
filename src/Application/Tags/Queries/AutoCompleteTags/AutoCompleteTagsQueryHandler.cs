using Application.Commons.Models.Results;
using Domain.Managers;
using MapsterMapper;
using MediatR;

namespace Application.Tags.Queries.AutoCompleteTags;

public class AutoCompleteTagsQueryHandler(IRepositoryManager repositoryManager, IMapper mapper) : IRequestHandler<AutoCompleteTagsQuery, Result<IEnumerable<AutoCompleteTagsQueryResult>>>
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<IEnumerable<AutoCompleteTagsQueryResult>>> Handle(AutoCompleteTagsQuery request, CancellationToken cancellationToken)
    {
        _repositoryManager.TagRepository.GetCollection().EnsureIndex(e => e.Name);

        var tags = _repositoryManager.TagRepository.GetCollection().Find(e => e.Name.Contains(request.Name)).Take(10).ToList();

        return Result<IEnumerable<AutoCompleteTagsQueryResult>>.Success(_mapper.Map<IEnumerable<AutoCompleteTagsQueryResult>>(tags));

    }
}
