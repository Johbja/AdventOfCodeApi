using System.Reflection;
using AdventOfCode.Application.Attributes;

namespace AdventOfCode.Application.Extensions;
public static class ReflectionExtensions
{
    public static IReadOnlyCollection<Type> GetImplementationsOfInterface(this Assembly assembly, Type tInterface, bool isTest =false)
    {
        var implementations = assembly
            .GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false } && tInterface.IsAssignableFrom(t));
        if (isTest)
            implementations = implementations.Where(t => t.IsDefined(typeof(TestableOperationAttribute), inherit: false));

        return implementations.ToList();
    }

    public static Assembly GetAssemblyOfMarker(this Type markerType)
    {
        return markerType.Assembly;
    }
}
