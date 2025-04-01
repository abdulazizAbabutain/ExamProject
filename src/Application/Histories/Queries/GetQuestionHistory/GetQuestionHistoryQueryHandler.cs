using Application.Commons.Managers;
using Application.Commons.Models.Pageination;
using Domain.Lookups;
using MediatR;

namespace Application.Histories.Queries.GetQuestionHistory
{
    internal class GetQuestionHistoryQueryHandler(IServiceManager serviceManager) : IRequestHandler<GetQuestionHistoryQuery, PageResponse<GetQuestionHistoryQueryResult>>
    {
        private readonly IServiceManager _serviceManager = serviceManager;

        public async Task<PageResponse<GetQuestionHistoryQueryResult>> Handle(GetQuestionHistoryQuery request, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
