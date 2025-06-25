using Swashbuckle.AspNetCore.Annotations;

namespace Application.Commons.Models.Pageination
{
    [SwaggerSchema("")]
    public class PageResponse<T> where T : class
    {
        [SwaggerSchema()]
        public IEnumerable<T> Data { get; set; }
        [SwaggerSchema("a meta data for pagination object")]
        public PageMetaData MetaData { get; set; }

        public PageResponse(IEnumerable<T> data, int pageNumber, int pageSize, int totalRecords)
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
