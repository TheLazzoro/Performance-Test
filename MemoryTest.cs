using System.Diagnostics;


public static class MemoryTest
{
    private static uint iterations = 500;
    private static uint buffer_size = 1_000_000;

    private static byte[] buffer1 = null;
    private static byte[] buffer2 = null;

    public static void Run()
    {
        buffer1 = new byte[buffer_size];
        buffer2 = new byte[buffer_size];

        // Warmup
        CopyLoop();
        CopyTo();

        TimeSpan copyLoop = CopyLoop();
        TimeSpan copyTo = CopyTo();

        Console.WriteLine("CopyLoop: 1.000x");
        Console.WriteLine("CopyTo:   " + $"{(copyLoop / copyTo).ToString("0.000")}x");
    }

    private static TimeSpan CopyLoop()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        for (int i = 0; i < iterations; i++)
        {
            for (int j = 0; j < buffer_size; j++)
            {
                buffer2[j] = buffer1[j];
            }
        }
        stopwatch.Stop();
        return stopwatch.Elapsed;
    }

    private static TimeSpan CopyTo()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        for (int i = 0; i < iterations; i++)
        {
            buffer1.CopyTo(buffer2, 0);
        }
        stopwatch.Stop();
        return stopwatch.Elapsed;
    }
}