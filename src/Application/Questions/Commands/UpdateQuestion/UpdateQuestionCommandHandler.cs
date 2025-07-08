using Application.Commons.Extentions;
using Application.Commons.Managers;
using MediatR;

namespace Application.Questions.Commands.UpdateQuestion
{
    public class UpdateQuestionCommandHandler(IServiceManager serviceManager) : IRequestHandler<UpdateQuestionCommand>
    {
        private readonly IServiceManager _serviceManager = serviceManager;

        /// <summary>
        /// Handles an update request for a question by retrieving the question, updating its properties, and persisting the changes.
        /// </summary>
        /// <param name="request">The command containing updated question details.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var question =  _serviceManager.QuestionService.GetQuestion(request.Id);

            question.UpdateBasicQueastion(request.QuestionText, request.Variants, request.Mark, request.RequireManulReview, request.Tags, request.Difficulty.GetMatrix());

            _serviceManager.QuestionService.UpdateQuestion(question);

        }
    }
}
