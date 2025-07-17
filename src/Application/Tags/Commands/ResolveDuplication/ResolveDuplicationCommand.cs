using Application.Commons.Models.Results;
using Domain.Enums;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Tags.Commands.ResolveDuplication
{
    public class ResolveDuplicationCommand : IRequest<Result>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public ReviewDuplicationStatus Status { get; set; }
    }
}
