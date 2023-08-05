using System.Diagnostics;


public static class MathTest
{
    private static uint iterations = 1_000_000;
    private static Random rand = new Random();
    private static float a = rand.Next();
    private static float b = rand.Next();

    public static void Run()
    {
        // Warmup
        Divide();
        Multiply();

        TimeSpan divide = Divide();
        TimeSpan multiply = Multiply();

        Console.WriteLine("Divide:     1,000x");
        Console.WriteLine("Multiply:   " + $"{(divide / multiply).ToString("0.000")}x");
    }

    private static TimeSpan Divide()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        for (int i = 0; i < iterations; i++)
        {
            float f = a / b;
        }
        stopwatch.Stop();
        return stopwatch.Elapsed;
    }

    private static TimeSpan Multiply()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        for (int i = 0; i < iterations; i++)
        {
            float f = a * b;
        }
        stopwatch.Stop();
        return stopwatch.Elapsed;
    }
}