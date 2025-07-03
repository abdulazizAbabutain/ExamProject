using Application.Commons.Managers;
using Application.Commons.Models.Results;
using Application.Commons.Services;
using Domain.Entities.EntityLookup;
using Domain.Enums;
using Domain.Extentions;
using Domain.Managers;
using FastDeepCloner;
using MapsterMapper;

namespace Infrastructure.Services
{
    public class CategoryService(IRepositoryManager repositoryManager, IAuditManager auditManager, IMapper mapper) : ICategoryService
    {
        private readonly IRepositoryManager _repositoryManager = repositoryManager;
        private readonly IAuditManager _auditManager = auditManager;
        private readonly IMapper _mapper = mapper;

        public Result<Category> AddCategory(string name, string description, Guid? parentId)
        {
            Category category;

            if (parentId.HasValue)
            {
                var parentCategory = _repositoryManager.CategoryRepository.GetById(parentId.Value);
                if (parentCategory.IsNull())
                    return Result<Category>.UnprocessableEntityFailure($"The parent id {parentId} was not found");

                category = new Category(name, description, parentId.Value, parentCategory.Level);


                var parentCategoryClone = DeepCloner.Clone(parentCategory);
                parentCategory.EnableChildrenFlag();
                
                _repositoryManager.CategoryRepository.Update(parentCategory);
                _auditManager.AuditTrailService.UpdateEntity(EntityName.Category, parentCategory.Id, ActionType.Modified, ActionBy.System, parentCategoryClone, parentCategory, parentCategory.VersionNumber, "Enable an flag for children");
            }
            else
            {
                category = new Category(name, description);
            } 

            _repositoryManager.CategoryRepository.Insert(category);
            _auditManager.AuditTrailService.AddNewEntity(EntityName.Category, category.Id, ActionBy.User, category, category.VersionNumber);
            return Result<Category>.CreatedSuccess(category);
        }
        
        public Result UpdateCategory(Guid categoryId,string name, string description)
        {
            var category = _repositoryManager.CategoryRepository.GetById(categoryId);

            if (category.IsNull())
                return Result.NotFoundFailure($"the category with id {categoryId} was not found");

            var categoryClone = DeepCloner.Clone(category);
            category.UpdateCategory(name,description);

            _repositoryManager.CategoryRepository.Update(category);
            _auditManager.AuditTrailService.UpdateEntity(EntityName.Category, category.Id, ActionType.Modified, ActionBy.System, categoryClone, category, category.VersionNumber);
            return Result.NoContentSuccess();
        }

    }
}
