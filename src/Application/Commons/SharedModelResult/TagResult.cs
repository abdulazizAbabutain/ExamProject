using Application.Commons.Attributes;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Application.Commons.SharedModelResult
{
    public class TagResult
    {
        [SwaggerSchema(Description = "An collection of tags", Nullable = false, Title = "Tag name")]
        [SwaggerExample("MATH-101")]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }
        [SwaggerSchema(Description = "An collection of tags", Nullable = false, Title = "Tag color")]
        [SwaggerExample("#RRGGBB")]
        [StringLength(7 ,MinimumLength = 7)]
        public string ColorCode { get; set; }
    }
}
