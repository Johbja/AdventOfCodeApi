using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Application.Attributes;

namespace AdventOfCode.Application.Operations.Days.SubOperations;

[TestableOperation]
public class Year2025Day01PartOneSubOperation 
    : ApplicationOperation<
        Year2025Day01PartOneSubOperation.Input, 
        Year2025Day01PartOneSubOperation.Output>
{
    public class Input
    {
        public class DialInstruction(char direction, int turns)
        {
            public Func<int, int> ExecuteRotation { get; } = (direction == 'L') 
                ? (int dialValue) => (dialValue - turns) % 100 
                : (int dialValue) => (dialValue + turns) % 100;
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
            var dialPosition = 50;
            var zeroCounter = 0;

            foreach (var instruction in input.DialInstructions)
            {
                dialPosition = instruction.ExecuteRotation(dialPosition);
                if (dialPosition == 0)
                    zeroCounter++;
            }

            return new Output(zeroCounter);
        });
    }
}

