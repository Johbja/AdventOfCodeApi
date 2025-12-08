using AdventOfCode.Application.Attributes;

namespace AdventOfCode.Application.Operations.Year2025.Days.SubOperations;

[TestableOperation]
public class Year2025Day08PartOneSubOperation
    : ApplicationOperation<
        Year2025Day08PartOneSubOperation.Input,
        Year2025Day08PartOneSubOperation.Output>
{
    public class Input(Vector3d[] points, int connectionCap)
    {
        public int ConnectionCap { get; } = connectionCap;
        public Vector3d[] Points { get; } = points;
    }

    public class Output(int cordLength)
    {
        public int CordLength { get; } = cordLength;
    }
    public class Vector3d
    {
        public int Id { get; }
        public int x => vector[0];
        public int y => vector[1];
        public int z => vector[2];

        private readonly int[] vector;

        public Vector3d(int[] cords, int id = 0)
        {
            Id = id;
            vector = cords;
        }

        public Vector3d(int x, int y, int z, int id = 0)
        {
            Id = id;
            vector =
            [
                x, y, z
            ];
        }

        public double DistanceTo(Vector3d other)
        {
            return vector.Select((cord, i) => SquareDistance(cord, other.vector[i])).Sum();

            double SquareDistance(int a, int b)
            {
                return Math.Abs((a - b) * (a - b));
            }
        }
    }

    public readonly struct Connection(
        int pointIndexA,
        int pointIndexB,
        double distance)
    {
        public int PointIndexA { get; } = pointIndexA;
        public int PointIndexB { get; } = pointIndexB;
        public double Distance { get; } = distance;
    }

    public class Dsu(int n)
    {
        readonly int[] parent = Enumerable.Range(0, n).ToArray();
        readonly int[] size = Enumerable.Repeat(1, n).ToArray();
        readonly int[] rank = new int[n];

        public int Count { get; private set; } = n;

        public IEnumerable<int> Sizes =>
            Enumerable.Range(0, parent.Length)
                .Select(Find)
                .Distinct()
                .Select(root => size[root]);

        public int Find(int x)
            => parent[x] == x ? x : parent[x] = Find(parent[x]);

        public void Union(int x, int y)
        {
            var xRoot = Find(x);
            var yRoot = Find(y);

            if (xRoot == yRoot)
                return;

            if (rank[xRoot] < rank[yRoot])
                (xRoot, yRoot) = (yRoot, xRoot);

            parent[yRoot] = xRoot;
            size[xRoot] += size[yRoot];

            if (rank[xRoot] == rank[yRoot])
                rank[xRoot]++;

            Count--;
        }
    }

    protected override async Task<Output> ExecuteApplicationLogic(Input input)
    {
        return await Task.Run(() =>
        {
            var len = input.Points.Length;
            List<Connection> connections = [];
            for (var i = 0; i < len - 1; i++)
            {
                for (var j = i + 1; j < len; j++)
                {
                    var distance = input.Points[i].DistanceTo(input.Points[j]);
                    var connection = new Connection(i, j, distance);
                    connections.Add(connection);
                }
            }

            var dsu = new Dsu(len);

            foreach (var connection in connections.OrderBy(c => c.Distance).Take(input.ConnectionCap))
            {
                dsu.Union(connection.PointIndexA, connection.PointIndexB);
            }

            var circuits = dsu.Sizes.OrderByDescending(c => c);
            var cordLength = circuits.Take(3).Aggregate(1, (a, b) => a * b);

            return new Output(cordLength);
        });
    }
}

