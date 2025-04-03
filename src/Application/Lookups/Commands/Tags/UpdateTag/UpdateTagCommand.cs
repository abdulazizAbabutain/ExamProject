using MediatR;

namespace Application.Lookups.Commands.Tags.UpdateTag
{
    public class UpdateTagCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? ColorHexCode { get; set; }
    }
}
