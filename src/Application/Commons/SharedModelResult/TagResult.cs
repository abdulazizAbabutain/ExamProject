using Application.Commons.Attributes;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Application.Commons.SharedModelResult
{
    public class TagResult
    {
        [SwaggerSchema(Description = "The Tag id", Nullable = false, Title = "Tag Id")]
        [SwaggerExample("0197c089-8943-736d-b211-b7f4b6bf9728")]
        [StringLength(100, MinimumLength = 1)]
        public Guid Id { get; set; }
        [SwaggerSchema(Description = "An collection of tags", Nullable = false, Title = "Tag name")]
        [SwaggerExample("MATH-101")]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }
        [SwaggerSchema(Description = "An collection of tags", Nullable = false, Title = "Tag color")]
        [SwaggerExample("#8910A3")]
        [StringLength(7 ,MinimumLength = 7)]
        public string ColorCode { get; set; }
    }
}
