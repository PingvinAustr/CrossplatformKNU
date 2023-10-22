using System.Diagnostics;
namespace lab4_classes
{
    public class Lab2
    {
        public static void Run(string inputPath, string outputPath)
        {
            string fileContent = File.ReadAllText(inputPath).Trim();
            if (!int.TryParse(fileContent, out int n))
            {
                Console.WriteLine("Error: Invalid format in input file. Please provide a valid integer.");
                return;
            }

            if (n < 1 || n > 106)
            {
                Console.WriteLine("Error: The value of N is out of the allowed range (1 ≤ N ≤ 106).");
                return;
            }

            Console.WriteLine($"Received data from input.txt. N = {n}\n\nCalculating...");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

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

            stopwatch.Stop();


            Console.WriteLine($"Result:{dp[n]}");
            Console.WriteLine($"Execution Time: {stopwatch.Elapsed.TotalMilliseconds} ms");
            // Write the result to OUTPUT.TXT
            File.WriteAllText(outputPath, dp[n].ToString());
        }
    }
}