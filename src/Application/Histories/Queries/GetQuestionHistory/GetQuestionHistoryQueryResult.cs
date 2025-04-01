using Application.Histories.Queries.GetQuestionHistory.Models;
using Domain.Enums;
using Domain.Lookups;
using Microsoft.AspNetCore.Identity;

namespace Application.Histories.Queries.GetQuestionHistory
{
    internal class GetQuestionHistoryQueryResult
    {
        public Guid Id { get; set; }
        public EntityHistoryTypeLookup Type { get; set; }
        public int VersionNumber { get; set; }
        public DateTimeOffset ActionDate { get; set; }
        public List<PropertyChangesQueryResult> PropertyChanges { get; set; }
    }
}
