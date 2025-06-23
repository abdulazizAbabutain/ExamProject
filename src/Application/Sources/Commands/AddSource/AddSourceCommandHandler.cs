using Application.Commons.Managers;
using Application.Sources.Commands.AddSource.Requests;
using Domain.Entities.Sources;
using Domain.Enums;
using MediatR;

namespace Application.Sources.Commands.AddSource
{
    public class AddSourceCommandHandler(IServiceManager serviceManager) : IRequestHandler<AddSourceCommand>
    {
        private readonly IServiceManager _serviceManager = serviceManager;

        public async Task Handle(AddSourceCommand request, CancellationToken cancellationToken)
        {
            var source = new Source(request.Type, request.Title, request.Description, request.HasAttachment, request.FileExtension, request.FilePath, request.Tags);

            if(request.Metadata.Any())
                source.AddMetadata(request.Metadata.Select(BuildMetadata()));


            foreach (var reference in request.References)
            {
                var sourceReference = new SourceReference(reference.Notes);
                sourceReference.AddMetadata(reference.Metadata.Select(BuildMetadata()));
                source.AddReference(sourceReference);
            }

            _serviceManager.LookupService.AddSource(source);
        }


        private Func<AddMetadataCommand, Metadata> BuildMetadata() => (command) =>
        {
            return new Metadata(command.FiledName, command.Value, command.IsRequired, command.FiledType);
        };
    }
}
