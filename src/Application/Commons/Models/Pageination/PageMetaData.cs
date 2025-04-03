namespace Application.Commons.Models.Pageination;

public class PageMetaData
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages { get; set; }

    public int? NextPage => PageNumber < TotalPages ? PageNumber + 1 : null;
    public int? PreviousPage => PageNumber > 1 ? PageNumber - 1 : null;
    public int FirstItemIndex => (PageNumber - 1) * PageSize + 1;
    public int LastItemIndex => Math.Min(PageNumber * PageSize, TotalRecords);
    public bool HasNextPage => PageNumber < TotalPages;
    public bool HasPreviousPage => PageNumber > 1;
    public bool IsLastPage => PageNumber == TotalPages;


}
