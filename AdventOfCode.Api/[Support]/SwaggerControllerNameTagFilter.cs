using AdventOfCode.Api.Attributes;
using AdventOfCode.Application.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace AdventOfCode.Api;

public class SwaggerControllerNameTagFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var controllerType = context.MethodInfo.DeclaringType;
        if (controllerType == null)
            return;

        var routeAttribute = controllerType.GetCustomAttribute<ControllerNameAttribute>();
        if (routeAttribute != null && routeAttribute.Name.HasValue())
        {
            operation.Tags = new[] { new OpenApiTag { Name = routeAttribute.Name } }.ToList();
        }
    }
}
