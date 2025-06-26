using Application.Commons.Managers;
using Application.Commons.Models.Results;
using MediatR;

namespace Application.Tags.Commands.ArchiveAllTags
{
    public class ArchiveAllTagsCommandHandler(IServiceManager serviceManager) : IRequestHandler<ArchiveAllTagsCommand, Result>
    {
        private readonly IServiceManager _serviceManager = serviceManager;

        public async Task<Result> Handle(ArchiveAllTagsCommand request, CancellationToken cancellationToken)
        {
            return _serviceManager.TagService.ArchiveAllTag();
        }
    }
}
