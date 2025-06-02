namespace AdventOfCode.Application.RestServices;

public class FileFormValue(string fileName, HttpContent content, string contentType)
{
    public string FileName { get; } = fileName;
    public string ContentType { get; } = contentType;
    public HttpContent Content { get; } = content;
}