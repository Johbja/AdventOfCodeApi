using Microsoft.Playwright;
using System.Linq;

namespace AdventOfCode.POC;

internal class Program
{

    static async Task Main()
    {
        using HttpClient client = await GetAdcInput();
    }

    private static async Task<HttpClient> GetAdcInput()
    {
        var handler = new HttpClientHandler
        {
            AutomaticDecompression = System.Net.DecompressionMethods.GZip |
                                     System.Net.DecompressionMethods.Deflate |
                                     System.Net.DecompressionMethods.Brotli
        };
        var client = new HttpClient(handler);


        client.BaseAddress = new Uri("https://adventofcode.com/");
        var response = await client.GetAsync("2024/day/20/input");


        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Response received:");
            Console.WriteLine(content);
        }
        else
        {
            Console.WriteLine($"Failed: {response.StatusCode}");
        }

        return client;
    }
}
