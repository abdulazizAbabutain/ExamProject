using Application.Commons.Managers;
using MediatR;

namespace Application.Lookups.Queries.Languages.GetLanguages
{
    public class GetLanguagesQueryHandler(IServiceManager serviceManager) : IRequestHandler<GetLanguagesQuery, IEnumerable<GetLanguagesQueryResult>>
    {
        private readonly IServiceManager _serviceManager = serviceManager;

        public async Task<IEnumerable<GetLanguagesQueryResult>> Handle(GetLanguagesQuery request, CancellationToken cancellationToken)
        {
            var languages = _serviceManager.LookupService.GetLanguages().Select(e => new GetLanguagesQueryResult
            {
                Code = e.Code,
                DisplayName = e.DisplayName,
                Id = e.Id
            }).ToList();

            _serviceManager.Dispose();
            
            return languages;
        }
    }
}
