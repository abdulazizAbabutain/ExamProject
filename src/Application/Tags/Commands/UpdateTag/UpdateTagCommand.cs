using MediatR;

namespace Application.Tags.Commands.UpdateTag
{
    public class UpdateTagCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? ColorHexCode { get; set; }
    }
}
