using Application.Commons.Extentions;
using Application.Commons.Managers;
using MediatR;

namespace Application.Questions.Commands.UpdateQuestion
{
    public class UpdateQuestionCommandHandler(IServiceManager serviceManager) : IRequestHandler<UpdateQuestionCommand>
    {
        private readonly IServiceManager _serviceManager = serviceManager;

        public async Task Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var question =  _serviceManager.QuestionService.GetQuestion(request.Id);

            question.UpdateBasicQueastion(request.QuestionText, request.Variants, request.Mark, request.RequireManulReview, request.Tags, request.Difficulty.GetMatrix());

            _serviceManager.QuestionService.UpdateQuestion(question);

        }
    }
}
