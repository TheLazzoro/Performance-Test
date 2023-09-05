using System.Diagnostics;


public class MemoryTest : TestBaseClass
{
    private int iterations = 500;
    private int buffer_size = 1_000_000;

    private byte[] buffer1 = null;
    private byte[] buffer2 = null;

    public void Run()
    {
        buffer1 = new byte[buffer_size];
        buffer2 = new byte[buffer_size];

        // Warmup
        CopyLoop();
        CopyTo();
        BufferBlockCopy();

        TimeSpan copyLoop = CopyLoop();
        TimeSpan copyTo = CopyTo();
        TimeSpan bufferBlockCopy = BufferBlockCopy();

        Console.WriteLine("CopyLoop:          1.000x");
        Console.WriteLine("CopyTo:            " + $"{(copyLoop / copyTo).ToString("0.000")}x");
        Console.WriteLine("BufferBlockCopy:   " + $"{(copyLoop / bufferBlockCopy).ToString("0.000")}x");
    }

    private TimeSpan CopyLoop()
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

    private TimeSpan CopyTo()
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

    private TimeSpan BufferBlockCopy()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        for (int i = 0; i < iterations; i++)
        {
            Buffer.BlockCopy(buffer1, 0, buffer2, 0, buffer_size);
        }
        stopwatch.Stop();
        return stopwatch.Elapsed;
    }
}