using Application.Commons.Managers;
using Application.Commons.Models.Results;
using Application.Commons.Models.ServicesModel.Source;
using Application.Commons.Services;
using Domain.Entities.Sources;
using Domain.Enums;
using Domain.Extentions;
using Domain.Managers;
using MapsterMapper;

namespace Infrastructure.Services;

public class SourceService(IRepositoryManager repositoryManager, IAuditManager auditManager, IMapper mapper) : ISourceService
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;
    private readonly IAuditManager _auditManager = auditManager;
    private readonly IMapper _mapper = mapper;

    public Result<Source> AddSource(AddSourceServiceModel model)
    {
        if(model.Tags.IsNotNull())
        {
            var missingTags = _repositoryManager.TagRepository.GetNotFoundTags(model.Tags);
            if (missingTags.Any())
                return Result<Source>.UnprocessableEntityFailure(missingTags.Select(e => $"tag with id {e} was not found").ToList());
        }

        var source = new Source(model.Type,model.Title,model.Description,model.HasAttachment,model.FilePath,model.FilePath,model.Tags);
        var metadata = _mapper.Map<IEnumerable<Metadata>>(model.Metadata);
        source.AddMetadata(metadata);


        _repositoryManager.SourceRepository.Insert(source);
        _auditManager.AuditTrailService.AddNewEntity(EntitiesName.Source,source.Id,ActionBy.User,source,source.VersionNumber);
        return Result<Source>.Success(source);
    }

    public void AddReference(SourceReference reference)
    {
        
        _repositoryManager.ReferenceRepository.Insert(reference);

        _auditManager.AuditTrailService.AddNewEntity(EntitiesName.Source, reference.Id, ActionBy.User, reference, reference.VersionNumber);
    }

    public Result<IEnumerable<SourceReference>> AddReference(IEnumerable<AddSourceReferenceServiceModel> sourceReferences, Guid sourceId)
    {
        if (_repositoryManager.SourceRepository.IsNotExist(sourceId))
            return Result<IEnumerable<SourceReference>>.NotFoundFailure($"source with {sourceId} was not found");

        var referencesEntity = sourceReferences.Select(reference =>
        {
            var referenceEntity = new SourceReference(reference.Note, sourceId);
            
            if(reference.Metadata.IsNotNull())
                referenceEntity.AddMetadata(_mapper.Map<Metadata>(reference.Metadata));
            
            return referenceEntity;
        }).ToList();

        _repositoryManager.ReferenceRepository.Insert(referencesEntity);
        
        foreach (var item in referencesEntity)
            _auditManager.AuditTrailService.AddNewEntity(EntitiesName.Reference,item.Id,ActionBy.User,item,item.VersionNumber);

        return Result<IEnumerable<SourceReference>>.Success(referencesEntity);
        
    }

}
