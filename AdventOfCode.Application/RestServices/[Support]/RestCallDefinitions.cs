namespace AdventOfCode.Application.RestServices;

public static class RestCallDefinitions
{
    public const string AdventOfCode = "advent-of-code";

    public static readonly HttpClientHandler AdventOfCodeClientHandler = new HttpClientHandler
    {
        AutomaticDecompression = System.Net.DecompressionMethods.GZip |
                                     System.Net.DecompressionMethods.Deflate |
                                     System.Net.DecompressionMethods.Brotli
    };
}
