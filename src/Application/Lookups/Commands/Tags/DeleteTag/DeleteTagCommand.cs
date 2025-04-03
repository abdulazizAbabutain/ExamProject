using MediatR;

namespace Application.Lookups.Commands.Tags.DeleteTag
{
    public class DeleteTagCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
