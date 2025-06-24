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
                string typeName = exampleAttr?.Example?.GetType().Name ?? "null";

                if (exampleAttr != null && schema.Properties.ContainsKey(property.Name.FirstCharToLower()))
                {
                    switch (typeName)
                    {
                        case "Int32":
                            schema.Properties[property.Name.FirstCharToLower()].Example = new OpenApiInteger((int)exampleAttr.Example);
                            break; 
                            
                        case "String":
                        case "SourceType":
                            schema.Properties[property.Name.FirstCharToLower()].Example = new OpenApiString(exampleAttr.Example.ToString());
                            break;
                        case "Boolean":
                            schema.Properties[property.Name.FirstCharToLower()].Example = new OpenApiBoolean((bool)exampleAttr.Example);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
