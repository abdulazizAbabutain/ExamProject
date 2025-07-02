using Application.Commons.Models.Results;
using Domain.Enums;
using MediatR;

namespace Application.Tags.Queries.ExportTags
{
    public class ExportTagsQuery : IRequest<Result<ExportTagsQueryResult>>
    {
        public FileFormat Format { get; set; }
        public bool IncludeArchived { get; set; }
    }
}
