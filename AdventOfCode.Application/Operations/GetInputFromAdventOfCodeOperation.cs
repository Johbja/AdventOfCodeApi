using AdventOfCode.Application.Datasources.AdventOfCode;
using AdventOfCode.Application.Datasources.AdventOfCode.ApiCalls;

namespace AdventOfCode.Application.Operations;

public class GetInputFromAdventOfCodeOperation(
    AdventOfCodeRestService adventOfCodeRestService)
    : ApplicationOperation<GetInputFromAdventOfCodeOperation.Input, 
        GetInputFromAdventOfCodeOperation.Output>
{
    public class Input(
        int year, 
        int day,
        string sessionKey)
    {
        public string SessionKey {  get; } = sessionKey;
        public int Year { get; } = year;
        public int Day { get; } = day;
    }

    public class Output(string textInput, string executionTime)
    {
        public string TextInput { get; } = textInput;
        public string ExecutionTime { get; } = executionTime;
    }

    protected override async Task<Output> ExecuteApplicationLogic(Input input)
    {
        var payload = new GetAdventOfCodeInputForDayOfYearApiCall.Input(input.Year, input.Day, input.SessionKey);
        var response = adventOfCodeRestService.CreateApiCall<GetAdventOfCodeInputForDayOfYearApiCall>().Execute(payload);
        var result = await response.Output;
        response.Stopwatch.Stop();

        return new Output(result.InputText, response.TimeElapsed);
    }

}
