using Application.Commons.Managers;
using Application.Commons.Models.Results;
using MediatR;

namespace Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler(IServiceManager serviceManager) : IRequestHandler<UpdateCategoryCommand, Result>
    {
        private readonly IServiceManager _serviceManager = serviceManager;

        public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            return _serviceManager.CategoryService.UpdateCategory(request.CategoryId,request.Name,request.Description);
        }
    }
}
