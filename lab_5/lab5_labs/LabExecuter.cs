using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace lab5_labs
{
    public class LabExecuter
    {
        
        // Dictionary to store already calculated values of F(a, b, c)
        static Dictionary<(int, int, int), int> memo = new Dictionary<(int, int, int), int>();

        static int FuncLab1(int a, int b, int c)
        {
            // Check if the value is already calculated
            if (memo.TryGetValue((a, b, c), out int value))
                return value;

            if (a <= 0 || b <= 0 || c <= 0)
                return 1;

            if (a > 20 || b > 20 || c > 20)
                return FuncLab1(20, 20, 20);

            if (a < b && b < c)
                value = FuncLab1(a, b, c - 1) + FuncLab1(a, b - 1, c - 1) - FuncLab1(a, b - 1, c);
            else
                value = FuncLab1(a - 1, b, c) + FuncLab1(a - 1, b - 1, c) + FuncLab1(a - 1, b, c - 1) - FuncLab1(a - 1, b - 1, c - 1);

            // Store the calculated value in the dictionary
            memo[(a, b, c)] = value;

            return value;
        }

        static (int a, int b, int c, string errorMessage) ReadAndValidateInputLab1(string[] inputLines)
        {
            if (inputLines.Length == 0)
                return (0, 0, 0, "Error: The input file is empty.");

            var input = inputLines[0].Trim().Split();

            if (input.Length != 3)
                return (0, 0, 0, "Error: Invalid input. Please enter exactly three integer values.");

            if (!int.TryParse(input[0], out int a) || !int.TryParse(input[1], out int b) || !int.TryParse(input[2], out int c))
                return (0, 0, 0, "Error: Invalid input. Please ensure all inputs are integers.");

            if (a < -104 || a > 104 || b < -104 || b > 104 || c < -104 || c > 104)
                return (0, 0, 0, "Error: Input values out of range. Please enter values between -104 and 104.");

            return (a, b, c, null);
        }


        public static int? ExecuteLab1(string input)
        {
            string[] lines = input.Split(Environment.NewLine);

            var(a, b, c, errorMessage) = ReadAndValidateInputLab1(lines);

            if (errorMessage != null)
            {
                Console.WriteLine(errorMessage);
                return null;
            }

            return FuncLab1(a, b, c);
        }

        public static long? ExecuteLab2(string input)
        {

            if (!int.TryParse(input, out int n))
            {
                Console.WriteLine("Error: Invalid format in input file. Please provide a valid integer.");
                return null;
            }

            if (n < 1 || n > 106)
            {
                Console.WriteLine("Error: The value of N is out of the allowed range (1 ≤ N ≤ 106).");
                return null;
            }

            Console.WriteLine($"Received data from input.txt. N = {n}\n\nCalculating...");

            // Initialize dp array
            int[] dp = new int[n + 1];
            dp[1] = 0;

            // Calculate the minimum number of operations for each number i
            for (int i = 2; i <= n; i++)
            {
                dp[i] = dp[i - 1] + 1;
                if (i % 2 == 0) dp[i] = Math.Min(dp[i], dp[i / 2] + 1);
                if (i % 3 == 0) dp[i] = Math.Min(dp[i], dp[i / 3] + 1);
            }

            return dp[n];
        }




        static (List<(double x, double y)> cities, string errorMessage) ReadAndValidateInputLab3(string[] inputLines)
        {
            if (!int.TryParse(inputLines[0].Trim(), out int n) || n <= 0 || n > 1000)
                return (null, "Error: Invalid number of cities. Please enter a positive integer less than or equal to 1000.");

            if (inputLines.Length - 1 != n + 1)
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

        public static double? ExecuteLab3(string inputText)
        {
            var (cities, errorMessage) = ReadAndValidateInputLab3(inputText.Split(Environment.NewLine));
            if (errorMessage != null)
            {
                Console.WriteLine(errorMessage);
                return null;
            }

            Console.WriteLine($"Received data from input.txt. Cities:{string.Join(",", cities)}\n\nCalculating...");

            double smallestRadius = FindSmallestRadius(cities);

            return smallestRadius;
        }

    }
}
