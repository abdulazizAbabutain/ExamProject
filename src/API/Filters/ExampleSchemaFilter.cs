using Application.Commons.Attributes;
using Application.Commons.Extensions;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace API.Filters
{
    public class ExampleSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            foreach (var property in context.Type.GetProperties())
            {
                var exampleAttr = property.GetCustomAttribute<SwaggerExampleAttribute>();
                if (exampleAttr != null && schema.Properties.ContainsKey(property.Name.FirstCharToLower()))
                {
                    schema.Properties[property.Name.FirstCharToLower()].Example = new OpenApiString(exampleAttr.Example.ToString());
                }
            }
        }
    }
}
