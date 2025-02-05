using Application.Commons.Managers;
using Application.Commons.Models.Pageination;
using MediatR;

namespace Application.Histories.Queries.GetQuestionHistory
{
    internal class GetQuestionHistoryQueryHandler(IServiceManager serviceManager) : IRequestHandler<GetQuestionHistoryQuery, PageResponse<GetQuestionHistoryQueryResult>>
    {
        private readonly IServiceManager _serviceManager = serviceManager;

        public async Task<PageResponse<GetQuestionHistoryQueryResult>> Handle(GetQuestionHistoryQuery request, CancellationToken cancellationToken)
        {
            var result =  _serviceManager.QuestionService.GetHistories(request.QuestionId, request.PageNumber, request.PageNumber);
            return new PageResponse<GetQuestionHistoryQueryResult>(result.Select(e => new GetQuestionHistoryQueryResult 
            {
                ActionDate = e.ActionDate,
                Mark = e.Mark,
                QuestionText = e.QuestionText,
                QuestionType = e.QuestionType,
                RequireManulReview = e.RequireManulReview,
                ActionType = e.ActionType,
                Id = e.Id,
                QuestionId = e.QuestionId,
                VerstionNumber = e.VerstionNumber
            }).ToList(), request.PageNumber, request.PageSize, 10);
        }
    }
}
