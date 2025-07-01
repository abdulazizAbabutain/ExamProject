using Domain.Entities.EntityLookup;
using Domain.Managers;
using MediatR;

namespace Application.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler(IRepositoryManager repositoryManager) : IRequestHandler<GetCategoryByIdQuery, GetCategoryByIdQueryResult>
    {
        private readonly IRepositoryManager _repositoryManager = repositoryManager;

        public async Task<GetCategoryByIdQueryResult> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var allCategories = _repositoryManager.CategoryRepository.GetCollection().FindAll().ToList();

            var root = allCategories.FirstOrDefault(c => c.Id == request.Id);
            if (root == null)
                return new GetCategoryByIdQueryResult();

            var descendants = GetAllDescendants(request.Id, allCategories);

            descendants.Add(root);

            var tree = BuildCategoryTree(root.Id, descendants); // root is unique


            return new GetCategoryByIdQueryResult
            {
                Id = root.Id,
                Level = root.Level,
                Name = root.Name,
                SubCategory = tree
            };

        }

        private List<Category> GetAllDescendants(Guid parentId, IEnumerable<Category> allCategories)
        {
            var result = new List<Category>();

            void FindChildren(Guid currentId)
            {
                var children = allCategories.Where(c => c.ParentId == currentId).ToList();
                foreach (var child in children)
                {
                    result.Add(child);
                    FindChildren(child.Id);
                }
            }

            FindChildren(parentId);
            return result;
        }

        public List<GetCategoryByIdQueryResult> BuildCategoryTree(Guid? parentId, IEnumerable<Category> allCategories)
        {
            return allCategories
                .Where(c => c.ParentId == parentId)
                .Select(c =>
                {
                    var children = BuildCategoryTree(c.Id, allCategories);

                    return new GetCategoryByIdQueryResult
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Level = c.Level,
                        SubCategory = children.Any() ? children : null
                    };
                })
                .ToList();
        }
    }
}
