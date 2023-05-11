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

    public static void Run()
    {
        // Warmup phase
        Vector_Loop_Class();
        Vector_Loop_Struct();

        // Benchmark
        TimeSpan oop = Vector_Loop_Class();
        TimeSpan dod = Vector_Loop_Struct();

        Console.WriteLine("Vectors Class:  1.000x");
        Console.WriteLine("Vectors Struct: " + $"{(oop / dod).ToString("0.000")}x");
    }

    static TimeSpan Vector_Loop_Class()
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

    static TimeSpan Vector_Loop_Struct()
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