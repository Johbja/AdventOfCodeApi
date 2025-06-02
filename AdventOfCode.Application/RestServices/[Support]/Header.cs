namespace AdventOfCode.Application.RestServices;

public class Header(string name, string value)
{
    public string Name { get; } = name;
    public string Value { get; } = value;
}
