using MediatR;

namespace Application.Questions.Queries.GetAllQuestions
{
    public class GetAllQuestionsQuery : IRequest<IEnumerable<GetAllQuestionsQueryResult>>
    {

    }
}
