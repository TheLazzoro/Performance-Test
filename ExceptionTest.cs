using System;
using System.Diagnostics;

public static class ExceptionTest
{
    private static string[] numbers = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "X" };
    private static uint iterations = 1_000_000;
    private static Random rand = new();

    public static void Run()
    {
        // Warmup
        Parse_Test();
        TryParse_Test();

        // Benchmark
        TimeSpan parse = Parse_Test();
        TimeSpan tryParse = TryParse_Test();

        Console.WriteLine("Parse:    1.000x");
        Console.WriteLine("TryParse: " + $"{(parse / tryParse).ToString("0.000")}x");
    }

    private static TimeSpan Parse_Test()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        for (uint i = 0; i < iterations; i++)
        {
            int j = rand.Next(0, numbers.Length);
            try
            {
                int.Parse(numbers[j]);
            }
            catch (Exception)
            {
                
            }
        }
        stopwatch.Stop();
        return stopwatch.Elapsed;
    }

    private static int test;
    private static TimeSpan TryParse_Test()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        for (uint i = 0; i < iterations; i++)
        {
            int j = rand.Next(0, numbers.Length);
            int.TryParse(numbers[j], out test);
        }
        stopwatch.Stop();
        return stopwatch.Elapsed;
    }
}