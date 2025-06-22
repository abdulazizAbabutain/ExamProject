using MediatR;

namespace Application.Tags.Commands.ArchiveTag
{
    public class ArchiveTagCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
