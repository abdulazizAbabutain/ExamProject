using Application.Commons.Models.Pageination;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Histories.Queries.GetQuestionHistory
{
    public class GetQuestionHistoryQuery : PageRequest, IRequest<PageResponse<GetQuestionHistoryQueryResult>>
    {
        [JsonIgnore]
        public Guid QuestionId { get; set; }
    }
}
