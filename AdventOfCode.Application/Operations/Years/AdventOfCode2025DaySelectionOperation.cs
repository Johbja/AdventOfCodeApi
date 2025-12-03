using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Application.Operations.Days;

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
            //2 => CreateSubOperation<Year2024Day01Operation>().Execute(dayInput),
            //3 => CreateSubOperation<Year2024Day01Operation>().Execute(dayInput),
            _ => throw new ArgumentOutOfRangeException($"The solution for day {input.Day} is not supported yet")
        };
    }
}

