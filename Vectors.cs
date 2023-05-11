using System.Diagnostics;


public class Vector_Class
{
    public uint x;
    public uint y;

    public Vector_Class(uint x, uint y)
    {
        this.x = x;
        this.y = y;
    }
}

struct Vector_Struct
{
    public uint x;
    public uint y;

    public Vector_Struct(uint x, uint y)
    {
        this.x = x;
        this.y = y;
    }
}

public static class Vectors
{
    private static uint iterations = 1000000;
    private static string char_name = "Lazzoro";

    public static void Run()
    {
        // Warmup phase
        IncreaseAge_OOP();
        IncreaseAge_DOD();

        // Benchmark
        TimeSpan oop = IncreaseAge_OOP();
        TimeSpan dod = IncreaseAge_DOD();

        Console.WriteLine("Vectors OOP: 1.000x");
        Console.WriteLine("Vectors DOD: " + $"{(oop / dod).ToString("0.000")}x");
    }

    static TimeSpan IncreaseAge_OOP()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        var list = new Vector_Class[iterations];
        for (uint i = 0; i < iterations; i++)
        {
            list[i] = new Vector_Class(i, i);
        }
        stopwatch.Stop();
        return stopwatch.Elapsed;
    }

    static TimeSpan IncreaseAge_DOD()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        var list = new Vector_Struct[iterations];
        for (uint i = 0; i < iterations; i++)
        {
            list[i] = new Vector_Struct(i, i);
        }
        stopwatch.Stop();
        return stopwatch.Elapsed;
    }
}