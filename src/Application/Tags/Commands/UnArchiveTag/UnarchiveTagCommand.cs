using Application.Commons.Models.Results;
using MediatR;

namespace Application.Tags.Commands.UnArchiveTag
{
    public class UnarchiveTagCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
