using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Application.Json;
public interface IJsonSerializer
{
    string ToJsonString(
    object value,
    bool useCamelCaseNamingStrategy = true,
    bool? writeIndented = null);

    T FromJsonString<T>(string jsonString);
    object FromJsonString(string jsonString, Type type);
}
