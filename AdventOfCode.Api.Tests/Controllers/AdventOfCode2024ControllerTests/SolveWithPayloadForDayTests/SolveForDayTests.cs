using AdventOfCode.Api.Tests;
using Microsoft.AspNetCore.Http;
using Xunit.Abstractions;

namespace Controllers.AdventOfCode2024;

public class SolveForDayTests(ApiTestContext testContext)
    : AdventOfCodeApiTestHarness(testContext, "api/AdventOfCode2024")
{
    [Fact]
    public async Task Test1()
    {
        var adapter = CreateTestAdapter();
        var payload = CreatePayload();

        //var content = new MultipartFormDataContent();
        //var fileContent = new StreamContent(new MemoryStream(System.Text.Encoding.UTF8.GetBytes("Test content")));
        //content.Add(fileContent, "file", "test.txt");



        //var t = await adapter.PostAsJsonAsync("v1/solve-with-payload-for/1", payload);

        //var result = await _apiTestContext.ApiCallHelper.GetAsync("v1/solve-with-payload-for/1", ensureSuccessStatusCode: true);
        //if (payload.ContentType != "text/plain")
        //    throw new ArgumentException("payload is not a text file");

        //using var reader = new StreamReader(payload.OpenReadStream());
        //var fileContent = await reader.ReadToEndAsync();

        //var input = new AdventOfCode2024DaySelectionOperation.Input(day, fileContent);
        //var traceableOutput = CreateOperation<AdventOfCode2024DaySelectionOperation>().Execute(input);
        //var result = await CreateBaseResponse(traceableOutput);
        //return Ok(result);

        Assert.Equal(1, 1); // Placeholder assertion for the test to compile and run

    }

    private IFormFile CreatePayload()
    {
        var content = "Test content";
        var bytes = System.Text.Encoding.UTF8.GetBytes(content);
        var stream = new MemoryStream(bytes);

        var file = new FormFile(
            stream,
            0,
            bytes.Length,
            "file",
            "test.txt");

        file.Headers = new HeaderDictionary();
        file.ContentType = "text/plain"; 
        file.ContentDisposition = "form-data; name=\"file\"; filename=\"test.txt\"";

        return file;
    }

}
