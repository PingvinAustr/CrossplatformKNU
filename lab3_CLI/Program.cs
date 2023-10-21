using System.Diagnostics;

class Program
{
    static void Main()
    {
        var (cities, errorMessage) = MarsRadioStations.ReadAndValidateInput("..//..//..//INPUT.TXT");

        if (errorMessage != null)
        {
            Console.WriteLine(errorMessage);
            return;
        }

        Console.WriteLine($"Received data from input.txt. Cities:{string.Join(",", cities)}\n\nCalculating...");

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        double smallestRadius = MarsRadioStations.FindSmallestRadius(cities);

        stopwatch.Stop();

        File.WriteAllText("..//..//..//OUTPUT.TXT", smallestRadius.ToString("F2"));

        Console.WriteLine($"Result: {smallestRadius.ToString("F2")}");
        Console.WriteLine($"\nExecution Time: {stopwatch.Elapsed.TotalMilliseconds} ms");
    }
}
