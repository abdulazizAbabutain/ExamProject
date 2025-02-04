using Application.Commons.Managers;
using MediatR;

namespace Application.Questions.Queries.GetQuestionsById
{
    internal class GetQuestionsByIdQueryHandler(IServiceManager serviceManager) : IRequestHandler<GetQuestionsByIdQuery, GetQuestionsByIdQueryResult>
    {
        private readonly IServiceManager _serviceManager = serviceManager;

        public async Task<GetQuestionsByIdQueryResult> Handle(GetQuestionsByIdQuery request, CancellationToken cancellationToken)
        {
            var question = _serviceManager.QuestionService.GetQuestion(request.Id);

            if (question == null)
                return null;

            return new GetQuestionsByIdQueryResult
            {
                Id = question.Id,
                Mark = question.Mark,
                QuestionText = question.QuestionText,
                QuestionType = question.QuestionType,
                RequireManulReview = question.RequireManulReview
            };
        }
    }
}
