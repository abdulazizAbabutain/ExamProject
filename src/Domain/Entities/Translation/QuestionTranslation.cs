using Domain.Entities.Translation.Lookup;

namespace Domain.Entities.Translation
{
    public class QuestionTranslation
    {
        public Guid Id { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public string LanguageCode { get; set; }
        public required string QuestionText { get; set; }
    }
}
