using System.Reflection;

namespace AdventOfCode.Application.Extensions;
public static class ReflectionExtensions
{
    public static IReadOnlyCollection<Type> GetImplementationsOfInterface(this Assembly assembly, Type tInterface)
    {
        return assembly
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract &&  tInterface.IsAssignableFrom(t) )
            .ToList();
    }

    public static Assembly GetAssemblyOfMarker(this Type markerType)
    {
        return markerType.Assembly;
    }
}
