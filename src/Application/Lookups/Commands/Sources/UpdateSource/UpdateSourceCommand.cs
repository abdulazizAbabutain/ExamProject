using Domain.Enums;
using MediatR;

namespace Application.Lookups.Commands.Sources.UpdateSource
{
    public class UpdateSourceCommand : IRequest
    {
        public Guid Id { get; set; }
        public SourceType Type { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string URL { get; set; }
        public List<Guid>? Tags { get; set; }
    }
}
