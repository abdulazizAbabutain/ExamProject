using MediatR;

namespace Application.Lookups.Commands.Tags.AddTag
{
    public partial class AddTagCommand : IRequest
    {
        public string Name { get; set; }
        public string? ColorCode { get; set; }
    }
}
