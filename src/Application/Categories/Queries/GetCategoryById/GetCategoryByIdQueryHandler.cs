﻿using Application.Commons.Models.Results;
using Domain.Entities.EntityLookup;
using Domain.Extentions;
using Domain.Managers;
using MapsterMapper;
using MediatR;

namespace Application.Categories.Queries.GetCategoryById;

public class GetCategoryByIdQueryHandler(IRepositoryManager repositoryManager, IMapper mapper) : IRequestHandler<GetCategoryByIdQuery, Result<GetCategoryByIdQueryResult>>
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<GetCategoryByIdQueryResult>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        //var allCategories = _repositoryManager.CategoryRepository.GetCollection().FindAll().ToList();

        //var root = allCategories.FirstOrDefault(c => c.Id == request.Id);
        //if (root == null)
        //    return new GetCategoryByIdQueryResult();

        //var descendants = GetAllDescendants(request.Id, allCategories);

        //descendants.Add(root);

        //var tree = BuildCategoryTree(root.Id, descendants); // root is unique


        var category = _repositoryManager.CategoryRepository.GetById(request.Id);
        if (category.IsNull())
            return Result<GetCategoryByIdQueryResult>.NotFoundFailure(nameof(request.Id), $"category with id {request.Id} were not found");

        return Result<GetCategoryByIdQueryResult>.Success(_mapper.Map<GetCategoryByIdQueryResult>(category));

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
                   // SubCategory = children.Any() ? children : null
                };
            })
            .ToList();
    }
}
