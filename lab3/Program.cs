using System.Diagnostics;
class Program
{
    static double CalculateDistance((double x, double y) point1, (double x, double y) point2)
    {
        double dx = point1.x - point2.x;
        double dy = point1.y - point2.y;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    static double FindSmallestRadius(List<(double x, double y)> cities)
    {
        int n = cities.Count;
        double[] minDistances = Enumerable.Repeat(double.MaxValue, n).ToArray();
        bool[] connected = new bool[n];
        double maxEdge = 0;

        minDistances[0] = 0;

        for (int count = 0; count < n; count++)
        {
            double minDistance = double.MaxValue;
            int currentCity = -1;

            for (int i = 0; i < n; i++)
            {
                if (!connected[i] && minDistances[i] < minDistance)
                {
                    minDistance = minDistances[i];
                    currentCity = i;
                }
            }

            if (currentCity == -1) break;

            maxEdge = Math.Max(maxEdge, minDistance);
            connected[currentCity] = true;

            for (int i = 0; i < n; i++)
            {
                if (!connected[i])
                {
                    double distance = CalculateDistance(cities[currentCity], cities[i]);
                    minDistances[i] = Math.Min(minDistances[i], distance);
                }
            }
        }

        return maxEdge;
    }

    static void Main()
    {
        var lines = File.ReadAllLines("..//..//..//INPUT.TXT");
        int n = int.Parse(lines[0]);
        var cities = new List<(double x, double y)>(n);

        Console.WriteLine($"Received data from input.txt. Cities:{string.Join(",", cities)}\n\nCalculating...");

        for (int i = 1; i <= n; i++)
        {
            var coordinates = lines[i].Split();
            cities.Add((double.Parse(coordinates[0]), double.Parse(coordinates[1])));
        }

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        double smallestRadius = FindSmallestRadius(cities);

        stopwatch.Stop();

        File.WriteAllText("..//..//..//OUTPUT.TXT", smallestRadius.ToString("F2"));

        Console.WriteLine($"Result: {smallestRadius.ToString("F2")}");
        Console.WriteLine($"\nExecution Time: {stopwatch.Elapsed.TotalMilliseconds} ms");
    }
}
