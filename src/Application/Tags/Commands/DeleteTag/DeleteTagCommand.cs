using Application.Commons.Models.Results;
using MediatR;

namespace Application.Tags.Commands.DeleteTag
{
    public class DeleteTagCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
