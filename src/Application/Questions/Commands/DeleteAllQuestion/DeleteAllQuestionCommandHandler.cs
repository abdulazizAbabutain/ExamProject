using Application.Commons.Managers;
using MediatR;

namespace Application.Questions.Commands.DeleteAllQuestion;

internal class DeleteAllQuestionCommandHandler(IServiceManager serviceManager) : IRequestHandler<DeleteAllQuestionCommand>
{
    private readonly IServiceManager _serviceManager = serviceManager;

    public async Task Handle(DeleteAllQuestionCommand request, CancellationToken cancellationToken)
    {
        _serviceManager.QuestionService.DeleteAllQuestions();   
    }
}
