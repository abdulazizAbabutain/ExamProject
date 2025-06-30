using Application.Commons.Models.Results;
using Application.Commons.SharedModelResult;
using Domain.Extentions;
using Domain.Managers;
using MapsterMapper;
using MediatR;

namespace Application.Sources.Queries.GetSourceById
{
    public class GetSourceByIdQueryHandler(IRepositoryManager repositoryManager, IMapper mapper) : IRequestHandler<GetSourceByIdQuery, Result<GetSourceByIdQueryResult>>
    {
        private readonly IRepositoryManager _repositoryManager = repositoryManager;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<GetSourceByIdQueryResult>> Handle(GetSourceByIdQuery request, CancellationToken cancellationToken)
        {
            var source = _repositoryManager.SourceRepository.GetById(request.Id);

            if (source.IsNull())
                return Result<GetSourceByIdQueryResult>.NotFoundFailure($"the source with id {request.Id} was not found");

            var tags = repositoryManager.TagRepository.GetCollection().Find(t => source.Tags.Contains(t.Id));

            var sourceResult = _mapper.Map<GetSourceByIdQueryResult>(source);
            sourceResult.Tags = _mapper.Map<IEnumerable<TagResult>>(tags.Where(t => !t.IsArchived));
            sourceResult.ArchivedTags = _mapper.Map<IEnumerable<TagResult>>(tags.Where(t => t.IsArchived));

            return Result<GetSourceByIdQueryResult>.Success(sourceResult);

        }
    }
}
