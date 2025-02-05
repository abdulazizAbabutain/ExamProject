using Application.Commons.Models.Pageination;
using MediatR;

namespace Application.Questions.Queries.GetAllQuestions
{
    public class GetAllQuestionsQuery : PageRequest, IRequest<PageResponse<GetAllQuestionsQueryResult>>
    {

    }
}
