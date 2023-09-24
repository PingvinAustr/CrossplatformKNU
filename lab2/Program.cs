using System.Diagnostics;
class Program
{
    static void Main()
    {
        int n = int.Parse(File.ReadAllText("..//..//..//INPUT.TXT").Trim());
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
        File.WriteAllText("..//..//..//OUTPUT.TXT", dp[n].ToString());
    }
}
