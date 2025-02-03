using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Pedidos.Api.Configurations;

public class SetVersionInPath : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var updatedPaths = new OpenApiPaths();
        foreach (var path in swaggerDoc.Paths)
        {
            var newPath = path.Key.Replace("v{version}", swaggerDoc.Info.Version);
            updatedPaths.Add(newPath, path.Value);
        }

        swaggerDoc.Paths = updatedPaths;
    }
}