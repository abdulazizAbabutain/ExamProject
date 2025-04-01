using MediatR;

namespace Application.Lookups.Commands.Tags.AddTag
{
    public class AddTagCommand : IRequest
    {
        public string Name { get; set; }
    }
}
