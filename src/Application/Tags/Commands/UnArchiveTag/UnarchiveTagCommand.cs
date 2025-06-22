using MediatR;

namespace Application.Tags.Commands.UnArchiveTag
{
    public class UnarchiveTagCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
