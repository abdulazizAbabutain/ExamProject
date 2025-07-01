using Application.Commons.Models.Results;
using MediatR;

namespace Application.Sources.Commands.AddTagToSource
{
    public class AddTagsToSourceCommand : IRequest<Result>
    {
        public Guid SourceId { get; set; }
        public Guid TagId { get; set; }
    }
}