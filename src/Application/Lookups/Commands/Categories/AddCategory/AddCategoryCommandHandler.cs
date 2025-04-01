using Application.Commons.Managers;
using MediatR;

namespace Application.Lookups.Commands.Categories.AddCategory
{
    public class AddCategoryCommandHandler(IServiceManager serviceManager) : IRequestHandler<AddCategoryCommand>
    {
        private readonly IServiceManager _serviceManager = serviceManager;

        public Task Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            _serviceManager.LookupService.AddCategory(request.name,request.ParentId);
            return Task.CompletedTask;
        }
    }
}
