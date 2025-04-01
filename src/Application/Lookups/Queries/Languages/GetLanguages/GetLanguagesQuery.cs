using MediatR;

namespace Application.Lookups.Queries.Languages.GetLanguages
{
    public class GetLanguagesQuery : IRequest<IEnumerable<GetLanguagesQueryResult>>
    {
    }
}
