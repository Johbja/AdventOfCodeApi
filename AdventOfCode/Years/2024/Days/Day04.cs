using AdventOfCode.Infrastructure.Attributes;
using AdventOfCode.Infrastructure.Interfaces;
using AdventOfCode.Infrastructure.Services;

namespace AdventOfCode.Years._2024.Days;

[DayInfo("Ceres Search", "Day 4")]
public class Day04(LoadInputService inputService) : ISolution
{
    private readonly string[] _input = inputService.GetInputAsLines(nameof(Day04), 2024);

    public void SolvePartOne()
    {
        List<string[]> searchKernels = new()
        {
            new string[1]
            {
                "XMAS",
            },
            new string[1]
            {
                "SAMX",
            },
            new string[4]
            {
                "X",
                "M",
                "A",
                "S",
            },
            new string[4]
            {
                "S",
                "A",
                "M",
                "X",
            },
            new string[4]
            {
                "X...",
                ".M..",
                "..A.",
                "...S"
            },
            new string[4]
            {
                "S...",
                ".A..",
                "..M.",
                "...X"
            },
            new string[4]
            {
                "...X",
                "..M.",
                ".A..",
                "S..."
            },
            new string[4]
            {
                "...S",
                "..A.",
                ".M..",
                "X..."
            },
        };
        int counter = 0;

        for (int row = 0; row < _input.Length; row++)
        {
            for (int col = 0; col <= _input[row].Length; col++)
            {
                foreach (var kernel in searchKernels)
                {
                    if (MatchKernel(kernel, row, col))
                        counter++;
                }
            }
        }

        Console.WriteLine(counter);
    }

    public void SolvePartTwo()
    {
        int kernelSize = 3;
        List<string[]> searchKernels = new()
        {
            new string[3]
            {
                "M.S",
                ".A.",
                "M.S",
            },
            new string[3]
            {
                "M.M",
                ".A.",
                "S.S",
            },
            new string[3]
            {
                "S.M",
                ".A.",
                "S.M",
            },
            new string[3]
            {
                "S.S",
                ".A.",
                "M.M"
            }
        };

        int counter = 0;

        for (int row = 0; row <= _input.Length - kernelSize; row++)
        {
            for (int col = 0; col <= _input[0].Length - kernelSize; col++)
            {
                foreach (var kernel in searchKernels)
                {
                    if (MatchKernel(kernel, row, col))
                    {
                        counter++;
                        break;
                    }
                }
            }
        }

        Console.WriteLine(counter);
    }

    private bool MatchKernel(string[] kernel, int startRow, int startCol)
    {

        int kernelRows = kernel.Length;
        int kernelCols = kernel[0].Length;

        if (startRow + kernelRows > _input.Length || startCol + kernelCols > _input[0].Length)
            return false;

        for (int i = 0; i < kernelRows; i++)
        {
            for (int j = 0; j < kernelCols; j++)
            {
                if (kernel[i][j] != '.' && kernel[i][j] != _input[startRow + i][startCol + j])
                {
                    return false;
                }
            }
        }

        return true;
    }
}
