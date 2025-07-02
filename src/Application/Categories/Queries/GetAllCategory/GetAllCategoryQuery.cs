using Application.Commons.Models.Pageination;
using MediatR;

namespace Application.Categories.Queries.GetAllCategory
{
    public class GetAllCategoryQuery : PageRequest, IRequest<PageResponse<GetAllCategoryQueryResult>>
    {
        public bool? IsRoot { get; set; }
        public int? Level { get; set; }
        public string? Search { get; set; }
        public bool? HasChildren { get; set; }
    }
}
