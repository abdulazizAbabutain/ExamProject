using Domain.Lookups;

namespace Domain.Entities.Translation
{
    public class QuestionTranslation
    {
        public Guid Id { get; set; }
        public required int LanguageId { get; set; }
        public Language Language { get; set; }
        public required string LanguageCode { get; set; }
        public required string QuestionText { get; set; }
    }
}
