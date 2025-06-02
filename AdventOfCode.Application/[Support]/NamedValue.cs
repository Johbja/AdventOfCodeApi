using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Application;
public class NamedValue<T>
{
    public string Name { get; }
    public T Value { get; }

    public NamedValue(string name, T value)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

        Name = name;
        Value = value;
    }
}
