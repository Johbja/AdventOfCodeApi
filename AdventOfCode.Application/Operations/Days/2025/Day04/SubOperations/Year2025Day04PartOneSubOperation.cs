using AdventOfCode.Application.Attributes;

namespace AdventOfCode.Application.Operations.Days.SubOperations;

[TestableOperation]
public class Year2025Day04PartOneSubOperation
    : ApplicationOperation<
        Year2025Day04PartOneSubOperation.Input,
        Year2025Day04PartOneSubOperation.Output>
{
    public class Input(int[][] paperGrid)
    {
        public int[][] PaperGrid { get; } = paperGrid;
    }

    public class Output(int rolls)
    {
        public int Rolls { get; } = rolls;
    }

    protected override Task<Output> ExecuteApplicationLogic(Input input)
    {
        (int row, int col)[][] kernal =
        [
            [(-1, -1), (-1, 0), (-1, 1)],
            [(0, -1), (0, 0), (0, 1)],
            [(1, -1), (1, 0), (1, 1)],
        ];

        return Task.Run(() =>
        {
            var rollCounter = 0;
            List < (int row, int col) > rollPositions = [];
            for (var row = 0; row < input.PaperGrid.Length; row++)
            {
                for (var col = 0; col < input.PaperGrid[row].Length; col++)
                {
                    //pos check grid[row][col]
                    if (input.PaperGrid[row][col] == 0)
                        continue;

                    var n_counter = 0;
                    for (var k_row = 0; k_row < kernal.Length; k_row++)
                    {
                        var kernalPos = kernal[k_row][0];
                        if(kernalPos.row + row < 0 || kernalPos.row + row >= input.PaperGrid.Length)
                            continue;

                        for (var k_col = 0; k_col < kernal[k_row].Length; k_col++)
                        {
                            kernalPos = kernal[k_row][k_col];
                            if(kernalPos.col + col < 0 || kernalPos.col + col >= input.PaperGrid[row].Length)
                                continue;

                            var r = row + kernalPos.row;
                            var c = col + kernalPos.col;

                            if(r == row && c == col)
                                continue;

                            n_counter += input.PaperGrid[r][c];
                        }
                    }

                    if (n_counter < 4)
                    {
                        rollCounter++;
                        rollPositions.Add((row,col));
                    }
                }
            }

            return new Output(rollCounter);
        });
    }
}

