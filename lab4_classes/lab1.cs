using System.Diagnostics;

namespace lab4_classes
{
    public class Lab1
    {
        // Dictionary to store already calculated values of F(a, b, c)
        static Dictionary<(int, int, int), int> memo = new Dictionary<(int, int, int), int>();

        static int F(int a, int b, int c)
        {
            // Check if the value is already calculated
            if (memo.TryGetValue((a, b, c), out int value))
                return value;

            if (a <= 0 || b <= 0 || c <= 0)
                return 1;

            if (a > 20 || b > 20 || c > 20)
                return F(20, 20, 20);

            if (a < b && b < c)
                value = F(a, b, c - 1) + F(a, b - 1, c - 1) - F(a, b - 1, c);
            else
                value = F(a - 1, b, c) + F(a - 1, b - 1, c) + F(a - 1, b, c - 1) - F(a - 1, b - 1, c - 1);

            // Store the calculated value in the dictionary
            memo[(a, b, c)] = value;

            return value;
        }

        static (int a, int b, int c, string errorMessage) ReadAndValidateInput(string filePath)
        {
            string[] inputLines = File.ReadAllLines(filePath);

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

        public static void Run(string inputPath, string outputPath)
        {
            var (a, b, c, errorMessage) = ReadAndValidateInput(inputPath);

            if (errorMessage != null)
            {
                Console.WriteLine(errorMessage);
                return;
            }

            Console.WriteLine($"Received following values from input.txt: a={a}, b={b}, c={c}\n\nCalculating...");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int result = F(a, b, c);
            stopwatch.Stop();

            File.WriteAllText(outputPath, result.ToString());
            Console.WriteLine($"\nResult:{result}");
            Console.WriteLine($"Execution Time: {stopwatch.Elapsed.TotalMilliseconds} ms");
        }
    }
}