using AdventOfCode.Application.RestServices;
using AdventOfCode.Application.RestServices.ApiCalls;
using System.Net;

namespace AdventOfCode.Application.Datasources.AdventOfCode.ApiCalls;

public class GetAdventOfCodeInputForDayOfYearApiCall : 
    RestApiCall<GetAdventOfCodeInputForDayOfYearApiCall.Input, 
        GetAdventOfCodeInputForDayOfYearApiCall.Output>,
    IAdventOfCodeServiceApiCall
{
    public class Input(
        int year, 
        int day, 
        string sessionKey)
    {
        public string SessionKey { get; } = sessionKey; 
        public int Year { get; } = year;
        public int Day { get; } = day;
    }

    public class Output(string inputText)
    {
        public string InputText { get; } = inputText;
    }

    protected override Output HandleSuccessResponse(HttpStatusCode httpStatusCode, string responseContent)
    {
        return new Output(responseContent);
    }

    protected override HttpRequestMessage BuildRequest(Input input, RequestBuilder requestbuilder)
    {
        var customheader = new Header("Cookie", $"session={input.SessionKey}");

        var request = requestbuilder.CreateGetRequest($"{input.Year}/day/{input.Day}/input", [customheader]);

        return request;
    }
}
