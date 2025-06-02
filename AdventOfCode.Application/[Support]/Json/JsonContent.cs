using System.Text;

namespace AdventOfCode.Application.Json;

public class JsonContent : StringContent
{
    public JsonContent(string content, Encoding encoding = null) : base(
      content,
      encoding ?? Encoding.UTF8,
      "application/json")
    {
    }
}
