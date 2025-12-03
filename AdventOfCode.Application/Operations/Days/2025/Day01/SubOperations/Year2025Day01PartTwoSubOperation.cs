using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            public Func<int, (int cycles, int newDialValue)> ExecuteRotation { get; } = (direction == 'L')
                ? (int dialValue) =>
                {
                    var newDialValue = Modulo((dialValue - turns), 100);
                    var cycles = 0;
                    var stepsAfterZero = turns - dialValue;
                    if (stepsAfterZero < 0)
                        return (0, newDialValue);

                    cycles = 1 + (turns) / 100;

                    return (cycles, newDialValue);
                }
                : (int dialValue) =>
                {
                    var newDialValue = Modulo((dialValue + turns), 100);
                    var cycles = (turns)/ 100;

                    return (cycles, newDialValue);
                };
        }

        private static int Modulo(int a, int n)
        {
            var remainder = a % n;
            if(remainder < 0)
                remainder += n;

            return remainder;
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
                var result = instruction.ExecuteRotation(dialValue);
                dialValue = result.newDialValue;
                zeroCounter += result.cycles;
            }

            return new Output(zeroCounter);
        });
    }
}

