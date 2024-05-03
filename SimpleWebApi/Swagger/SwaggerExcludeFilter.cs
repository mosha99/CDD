using System.Reflection;
using Microsoft.OpenApi.Models;
using SimpleWebApi.Infrastructure.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SimpleWebApi.Swagger;
public class SwaggerExcludeFilter : ISchemaFilter
{

    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var type = context.Type;
        if (schema?.Properties == null || type == null)
            return;

        var excludedProperties = type.GetProperties()
            .Where(t =>
                t.GetCustomAttribute<SwaggerBodyIgnoreAttribute>()
                != null).ToList();

        foreach (var excludedProperty in excludedProperties)
        {
            if (schema.Properties.ContainsKey(excludedProperty.Name.ToLower()))
                schema.Properties.Remove(excludedProperty.Name.ToLower());
        }
    }
}