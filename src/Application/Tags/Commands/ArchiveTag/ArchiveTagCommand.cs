using Application.Commons.Models.Results;
using MediatR;

namespace Application.Tags.Commands.ArchiveTag
{
    public class ArchiveTagCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
