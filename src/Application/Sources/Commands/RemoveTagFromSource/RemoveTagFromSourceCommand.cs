using Application.Commons.Models.Results;
using MediatR;

namespace Application.Sources.Commands.RemoveTagFromSource
{
    public class RemoveTagFromSourceCommand : IRequest<Result>
    {
        public Guid SourceId { get; set; }
        public Guid TagId { get; set; }
    }
}
