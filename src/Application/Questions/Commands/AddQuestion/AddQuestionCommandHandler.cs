using Application.Commons.Managers;
using Domain.Entities.Examiner;
using MediatR;

namespace Application.Questions.Commands.AddQuestion
{
    internal class AddQuestionCommandHandler(IServiceManager serviceManager) : IRequestHandler<AddQuestionCommand, AddQuestionCommandResult>
    {
        private readonly IServiceManager _serviceManager = serviceManager;

        public async Task<AddQuestionCommandResult> Handle(AddQuestionCommand request, CancellationToken cancellationToken)
        {
            var queastion = new Question
            {
                Id = new Guid(),
                QuestionText = request.QuestionText,
                CreataionDate = DateTime.Now,
                Mark = request.Mark,
                QuestionType = request.QuestionType,
                RequireManulReview = request.RequireManulReview,
                VerstionNumber = 1,
            };

            _serviceManager.QuestionService.InsertQuestion(queastion);
            return new AddQuestionCommandResult();
        }
    }
}
