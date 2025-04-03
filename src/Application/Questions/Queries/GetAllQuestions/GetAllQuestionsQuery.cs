using Application.Commons.Models.Pageination;
using Domain.Enums;
using MediatR;

namespace Application.Questions.Queries.GetAllQuestions
{
    public class GetAllQuestionsQuery : PageRequest, IRequest<PageResponse<GetAllQuestionsQueryResult>>
    {
        public string? Tags { get; set; }
        public QuestionType? QuestionType { get; set; }
        public bool? RequireManualReview { get; set; }
        public string? Category { get; set; }
        public string? Search { get; set; }
    }
}
