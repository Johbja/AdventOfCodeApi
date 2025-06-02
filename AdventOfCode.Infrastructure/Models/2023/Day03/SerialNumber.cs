
namespace AdventOfCode.Infrastructure.Models._2023.Day03;

public class SerialNumber
{
    public List<(int x, int y, int value)> Positions { get; set; } = new();

    public int Value
    {
        get
        {
            return int.Parse(Positions.Aggregate("", (a, b) => a + b.value.ToString()));
        }
    }

    public List<(int x, int y)> BoundingArea
    {
        get
        {
            List<(int x, int y)> result = new();
            var first = Positions.First();
            var last = Positions.Last();

            var hrizontalRange = Enumerable.Range(first.x - 1, (last.x - first.x + 3)).ToList();
            result.AddRange(hrizontalRange.Select(x => (x, first.y + 1)));
            result.AddRange(hrizontalRange.Select(x => (x, first.y - 1)));
            result.Add((first.x - 1, first.y));
            result.Add((last.x + 1, last.y));

            return result;
        }
    }

}
