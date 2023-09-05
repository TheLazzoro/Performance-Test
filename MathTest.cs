using System.Diagnostics;


public class MathTest : TestBaseClass
{
    private uint iterations = 1_000_000;
    private Random rand = new Random();
    private float a;
    private float b;

    public void Run()
    {
        a = rand.Next();
        b = rand.Next();

        // Warmup
        Divide();
        Multiply();

        TimeSpan divide = Divide();
        TimeSpan multiply = Multiply();

        Console.WriteLine("Divide:     1,000x");
        Console.WriteLine("Multiply:   " + $"{(divide / multiply).ToString("0.000")}x");
    }

    private TimeSpan Divide()
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

    private TimeSpan Multiply()
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