namespace AdventOfCode.Application;

public class GenericOperationOutput(string resultDayOne, string resultDayTwo )
{
    public string ResultDayOne { get; } = resultDayOne;
    public string ResultDayTwo { get; } = resultDayTwo;
}

public class GenericOperationInput(string textInput)
{
    public string TextInput { get; } = textInput;
}
