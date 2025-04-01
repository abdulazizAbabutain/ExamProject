using LiteDB;

namespace Application.Lookups.Queries.Languages.GetLanguages
{
    public class GetLanguagesQueryResult
    {
        public Guid Id { get; set; }
        public required string Code { get; set; }
        public required string DisplayName { get; set; }
    }
}
