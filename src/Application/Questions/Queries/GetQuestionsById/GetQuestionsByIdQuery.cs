using MediatR;

namespace Application.Questions.Queries.GetQuestionsById;

public class GetQuestionsByIdQuery : IRequest<GetQuestionsByIdQueryResult>
{
    public Guid Id { get; set; }
}
