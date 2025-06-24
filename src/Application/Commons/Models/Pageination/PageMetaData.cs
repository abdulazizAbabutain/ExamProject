using Application.Commons.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Commons.Models.Pageination;

public class PageMetaData
{
    [SwaggerSchema("The current page number")]
    [SwaggerExample(3)]
    public int PageNumber { get; set; }
    [SwaggerSchema("The size of current page")]
    [SwaggerExample(50)]
    public int PageSize { get; set; }
    [SwaggerSchema("The total number of record")]
    [SwaggerExample(500)]
    public int TotalRecords { get; set; }
    [SwaggerSchema("The total number of pages")]
    [SwaggerExample(100)]
    public int TotalPages { get; set; }
    [SwaggerSchema("The number of next page")]
    [SwaggerExample(4)]
    public int? NextPage => PageNumber < TotalPages ? PageNumber + 1 : null;
    [SwaggerSchema("The number of previous page")]
    [SwaggerExample(2)]
    public int? PreviousPage => PageNumber > 1 ? PageNumber - 1 : null;
    [SwaggerExample(1)]
    public int FirstItemIndex => (PageNumber - 1) * PageSize + 1;
    [SwaggerExample(99)]
    public int LastItemIndex => Math.Min(PageNumber * PageSize, TotalRecords);
    [SwaggerSchema("has the next page?")]
    [SwaggerExample(true)]
    public bool HasNextPage => PageNumber < TotalPages;
    [SwaggerSchema("has the previous page?")]
    [SwaggerExample(true)]
    public bool HasPreviousPage => PageNumber > 1;
    [SwaggerSchema("Is this the last page")]
    [SwaggerExample(false)]
    public bool IsLastPage => PageNumber == TotalPages;


}
