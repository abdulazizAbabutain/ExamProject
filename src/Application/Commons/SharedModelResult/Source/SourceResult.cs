using Application.Commons.Attributes;
using Domain.Enums;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Application.Commons.SharedModelResult.Source
{
    public class SourceResult
    {
        [SwaggerSchema(Description = "An UUID unique id for source", ReadOnly = true, Format = "UUID", Nullable = false, Title = "Source Unique Identifier")]
        [SwaggerExample("01979c44-b446-7380-ad8b-df6b2a2be0bc")]
        public Guid Id { get; set; }
        [SwaggerSchema(Description = "An type of a source", ReadOnly = true, Format = "UUID", Nullable = false, Title = "Source Unique Identifier")]
        [SwaggerExample(SourceType.Article)]
        [StringLength(50, MinimumLength = 1)]
        public SourceType Type { get; set; }
        [SwaggerSchema(Description = "A source Title", Nullable = false, Title = "Source title")]
        [SwaggerExample("Intro programming")]
        [StringLength(100, MinimumLength = 1)]
        public string Title { get; set; }
        [SwaggerSchema(Description = "short description for a source", Nullable = true, Title = "Source description")]
        [SwaggerExample("Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus. Donec vitae sapien ut libero venenatis faucibus. Nullam quis ante. Etiam sit amet orci eget eros faucibus tincidunt. Duis leo. Sed fringilla mauris sit amet nibh. Donec sodales sagittis magna. Sed consequat, leo eget bibendum sodales, augue velit cursus nunc")]
        [StringLength(2500, MinimumLength = 1)]
        public string? Description { get; set; }
    }
}
