using Application.Commons.Managers;
using MediatR;

namespace Application.Questions.Commands.DeleteQuestion
{
    internal class DeleteQuestionCommandHandler(IServiceManager serviceManager) : IRequestHandler<DeleteQuestionCommand>
    {
        private readonly IServiceManager _serviceManager = serviceManager;

        public async Task Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            _serviceManager.QuestionService.DeleteQuestion(request.Id);
        }
    }
}
