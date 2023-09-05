using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


class loop_class
{
    public int x;
    public int y;
    public bool hi;
    public bool hello;

    public loop_class(int x, int y, bool hi, bool hello)
    {
        this.x = x;
        this.y = y;
        this.hi = hi;
        this.hello = hello;
    }
}

struct loop_struct
{
    public int x;
    public int y;
    public bool hi;
    public bool hello;

    public loop_struct(int x, int y, bool hi, bool hello)
    {
        this.x = x;
        this.y = y;
        this.hi = hi;
        this.hello = hello;
    }
}


public class Loops : TestBaseClass
{
    int count = 1_000_000;

    loop_class[] vec_classes;
    loop_struct[] vec_structs;

    public void Run()
    {
        vec_classes = new loop_class[count];
        vec_structs = new loop_struct[count];

        // Populate
        for (int i = 0; i < count; i++)
        {
            vec_classes[i] = new loop_class(i, i, true, false);
            vec_structs[i] = new loop_struct(i, i, true, false);
        }

        // Warmup phase
        Loop_Class();
        Loop_Struct();

        // Benchmark
        TimeSpan time_classes = Loop_Class();
        TimeSpan time_structs = Loop_Struct();

        Console.WriteLine("Loop Classes:  1.000x");
        Console.WriteLine("Loop Structs:  " + $"{(time_classes / time_structs).ToString("0.000")}x");
    }

    TimeSpan Loop_Class()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        for (int i = 0; i < count; i++)
        {
            int x = vec_classes[i].x;
            int y = vec_classes[i].y;
            bool hi = vec_classes[i].hi;
            bool hello = vec_classes[i].hello;
        }
        stopwatch.Stop();
        return stopwatch.Elapsed;
    }

    TimeSpan Loop_Struct()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        for (int i = 0; i < count; i++)
        {
            int x = vec_structs[i].x;
            int y = vec_structs[i].y;
            bool hi = vec_structs[i].hi;
            bool hello = vec_structs[i].hello;
        }
        stopwatch.Stop();
        return stopwatch.Elapsed;
    }
}