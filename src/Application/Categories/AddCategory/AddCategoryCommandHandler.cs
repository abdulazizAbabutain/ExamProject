using Application.Commons.Managers;
using Application.Commons.Models.Results;
using MapsterMapper;
using MediatR;

namespace Application.Categories.AddCategory
{
    public class AddCategoryCommandHandler(IServiceManager serviceManager, IMapper mapper) : IRequestHandler<AddCategoryCommand, Result<AddCategoryCommandResult>>
    {
        private readonly IServiceManager _serviceManager = serviceManager;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<AddCategoryCommandResult>> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryServiceResult =  _serviceManager.CategoryService.AddCategory(request.Name, request.Description, request.ParentId);
            if (!categoryServiceResult.IsSuccess)
                return Result<AddCategoryCommandResult>.Failure(categoryServiceResult.Errors,categoryServiceResult.StatusCode);
            return Result<AddCategoryCommandResult>.Success(_mapper.Map<AddCategoryCommandResult>(categoryServiceResult.Value),categoryServiceResult.StatusCode);

        }
    }
}
