namespace Application.Commons.Models.Pageination
{
    public class PageResponse<T> where T : class
    {
        public List<T> Data { get; set; }
        public PageMetaData MetaData { get; set; }

        public PageResponse(List<T> data, int pageNumber, int pageSize, int totalRecords)
        {
            Data = data;
            MetaData = new PageMetaData
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize),                
            };
        }
    }
}
