using System.Diagnostics;
namespace lab4_classes
{
    public class Lab3
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

        static (List<(double x, double y)> cities, string errorMessage) ReadAndValidateInput(string filePath)
        {
            string[] inputLines = File.ReadAllLines(filePath);

            if (!int.TryParse(inputLines[0].Trim(), out int n) || n <= 0 || n > 1000)
                return (null, "Error: Invalid number of cities. Please enter a positive integer less than or equal to 1000.");

            if (inputLines.Length != n + 1)
                return (null, "Error: The number of coordinate rows does not match the specified number of cities.");

            var cities = new List<(double x, double y)>(n);

            for (int i = 1; i <= n; i++)
            {
                var coordinates = inputLines[i].Trim().Split();

                if (coordinates.Length != 2 ||
                    !double.TryParse(coordinates[0], out double x) || x < -10000 || x > 10000 ||
                    !double.TryParse(coordinates[1], out double y) || y < -10000 || y > 10000)
                {
                    return (null, "Error: Invalid coordinates. Please ensure each line contains two real numbers between -10000 and 10000.");
                }

                cities.Add((x, y));
            }

            return (cities, null);
        }

        public static void Run(string inputPath, string outputPath)
        {
            var (cities, errorMessage) = ReadAndValidateInput(inputPath);

            if (errorMessage != null)
            {
                Console.WriteLine(errorMessage);
                return;
            }

            Console.WriteLine($"Received data from input.txt. Cities:{string.Join(",", cities)}\n\nCalculating...");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            double smallestRadius = FindSmallestRadius(cities);

            stopwatch.Stop();

            File.WriteAllText(outputPath, smallestRadius.ToString("F2"));

            Console.WriteLine($"Result: {smallestRadius.ToString("F2")}");
            Console.WriteLine($"\nExecution Time: {stopwatch.Elapsed.TotalMilliseconds} ms");
        }
    }
}