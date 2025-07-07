using Application.Commons.Models.Results;
using MediatR;

namespace Application.Tags.Commands.UpdateTag
{
    public class UpdateTagCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? BackgroundColorCode { get; set; }
        public string? TextColorCode { get; set; }
    }
}
