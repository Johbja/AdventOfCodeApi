using AdventOfCode.Application.Attributes;

namespace AdventOfCode.Application.Operations.Days.SubOperations;

[TestableOperation]
public class Year2025Day01PartTwoSubOperation 
    : ApplicationOperation<
        Year2025Day01PartTwoSubOperation.Input,
        Year2025Day01PartTwoSubOperation.Output>
{
    public class Input
    {
        public class DialInstruction(char direction, int turns)
        {
            public int Direction { get; } = (direction == 'L') ? -1 : 1;
            public int TurnValue { get; } = turns;
        }

        public Input(string[] dialRotations)
        {
            DialInstructions = dialRotations
                .Select(instruction
                    => new DialInstruction(instruction[0], int.Parse(instruction[1..])))
                .ToList();
        }

        public IReadOnlyList<DialInstruction> DialInstructions { get; }
    }

    public class Output(int password)
    {
        public int Password { get; } = password;
    }

    protected override async Task<Output> ExecuteApplicationLogic(Input input)
    {
        return await Task.Run(() =>
        {
            var dialValue = 50;
            var zeroCounter = 0;
            foreach (var instruction in input.DialInstructions)
            {
                for (int i = 0; i < instruction.TurnValue; i++)
                {
                    dialValue += instruction.Direction;
                    dialValue %= 100;
                    if (dialValue == 0)
                        zeroCounter++;
                }
            }

            return new Output(zeroCounter);
        });
    }
}

