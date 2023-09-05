using System.Diagnostics;


public class Vector_Class
{
    public int x;
    public int y;

    public Vector_Class(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

struct Vector_Struct
{
    public int x;
    public int y;

    public Vector_Struct(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

public class Allocations : TestBaseClass
{
    private int iterations = 1_000_000;

    public void Run()
    {
        // Warmup phase
        Vector_Loop_Class();
        Vector_Loop_Struct();

        // Benchmark
        TimeSpan vec_class = Vector_Loop_Class();
        TimeSpan vec_struct = Vector_Loop_Struct();

        Console.WriteLine("Allocation Class:  1.000x");
        Console.WriteLine("Allocation Struct: " + $"{(vec_class / vec_struct).ToString("0.000")}x");
    }

    TimeSpan Vector_Loop_Class()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        var list = new Vector_Class[iterations];
        for (int i = 0; i < iterations; i++)
        {
            list[i] = new Vector_Class(i, i);
        }
        stopwatch.Stop();
        return stopwatch.Elapsed;
    }

    TimeSpan Vector_Loop_Struct()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        var list = new Vector_Struct[iterations];
        for (int i = 0; i < iterations; i++)
        {
            list[i] = new Vector_Struct(i, i);
        }
        stopwatch.Stop();
        return stopwatch.Elapsed;
    }
}