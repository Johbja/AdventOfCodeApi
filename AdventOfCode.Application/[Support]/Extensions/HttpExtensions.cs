namespace AdventOfCode.Application.Extensions;

public static class HttpExtensions
{
    public static async Task<string> TryGetContentAsString(this HttpContent content, bool isStream = false)
    {
        if (isStream)
            return string.Empty;

        if (content == null)
            return null;

        return await content.ReadAsStringAsync();
    }
}
