using System.Diagnostics;

class Program
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

    static void Main()
    {
        var input = File.ReadAllText("..//..//..//INPUT.TXT").Split();
        int a = int.Parse(input[0]);
        int b = int.Parse(input[1]);
        int c = int.Parse(input[2]);

        Console.WriteLine($"Received following values from input.txt: a={a}, b={b}, c={c}\n\nCalculating...");

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        int result = F(a, b, c);
        stopwatch.Stop();


        File.WriteAllText("..//..//..//OUTPUT.TXT", result.ToString());
        Console.WriteLine($"\nResult:{result}");
        Console.WriteLine($"Execution Time: {stopwatch.Elapsed.TotalMilliseconds} ms");
    }
}
