using Domain.Constants;

namespace Application.Commons.Models.Pageination;

public class PageRequest
{
    private const int MaxPageSize = MaxLength.PAGESIZE_MAX_VALUE;

    public int PageNumber { get; set; } = 1;
    private int _pageSize = 10;
    
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }
}
