using AdventOfCode.Application.Operations.Year2025.Days;

namespace AdventOfCode.Application.Operations.Years;

public class AdventOfCode2025DaySelectionOperation
    : ApplicationOperation<
        AdventOfCode2025DaySelectionOperation.Input,
        AdventOfCode2025DaySelectionOperation.Output>
{
    public class Input(int day, string payload)
    {
        public int Day { get; } = day;
        public string Payload { get; } = payload;
    }

    public class Output(string resultPartOne, string resultPartTwo)
    {
        public string ResultPartOne { get; } = resultPartOne;
        public string ResultPartTwo { get; } = resultPartTwo;
    }

    protected override async Task<Output> ExecuteApplicationLogic(Input input)
    {
        var operation = CreateSuboperationForDay(input);
        var result = await operation.Output;

        return new Output(result.ResultDayOne, result.ResultDayTwo);
    }

    private ITraceableOutput<Task<GenericOperationOutput>> CreateSuboperationForDay(Input input)
    {
        var dayInput = new GenericOperationInput(input.Payload);

        return input.Day switch
        {
            1 => CreateSubOperation<Year2025Day01Operation>().Execute(dayInput),
            2 => CreateSubOperation<Year2025Day02Operation>().Execute(dayInput),
            3 => CreateSubOperation<Year2025Day03Operation>().Execute(dayInput),
            4 => CreateSubOperation<Year2025Day04Operation>().Execute(dayInput),
            5 => CreateSubOperation<Year2025Day05Operation>().Execute(dayInput),
            6 => CreateSubOperation<Year2025Day06Operation>().Execute(dayInput),
            7 => CreateSubOperation<Year2025Day07Operation>().Execute(dayInput),
            8 => CreateSubOperation<Year2025Day08Operation>().Execute(dayInput),
            _ => throw new ArgumentOutOfRangeException($"The solution for day {input.Day} is not supported yet")
        };
    }
}

