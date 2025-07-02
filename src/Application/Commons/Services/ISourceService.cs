using Application.Commons.Models.Results;
using Application.Commons.Models.ServicesModel.Source;
using Domain.Entities.Sources;

namespace Application.Commons.Services
{
    public interface ISourceService
    {
        Result<Source> AddSource(AddSourceServiceModel model);
        public Result<IEnumerable<SourceReference>> AddReference(IEnumerable<AddSourceReferenceServiceModel> sourceReferences, Guid sourceId);
        Result AddTag(Guid sourceId, Guid tagId);
        Result RemoveTag(Guid sourceId, Guid tagId);
    }
}
