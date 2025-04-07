using System.Diagnostics;


public class ListHashset : TestBaseClass
{
    private uint iterations = 1_000_000;
    private uint startDateOffset = 50_000;
    private uint[] dateCount = { 10, 100, 1_000 };

    private List<DateTime> dates_list = new List<DateTime>();
    private HashSet<DateTime> dates_hashset = new HashSet<DateTime>();

    public void Run()
    {
        foreach (var count in dateCount)
        {
            dates_list.Clear();
            dates_hashset.Clear();
            for (int i = 0; i < count; i++)
            {
                var offset = startDateOffset + i;
                var date = new DateTime(offset);
                dates_list.Add(date);
                dates_hashset.Add(date);
            }

            // Warmup
            List();
            Hashset();

            TimeSpan list = List();
            TimeSpan hashset = Hashset();

            Console.WriteLine($"Object Count: {count}");
            Console.WriteLine($"List:      {list.ToString(@"ss\:fff")} 1,000x");
            Console.WriteLine($"Hashset:   {hashset.ToString(@"ss\:fff")} " + $"{(list / hashset).ToString("0.000")}x");
            Console.WriteLine();
        }
    }

    private TimeSpan List()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        for (int i = 0; i < iterations; i++)
        {
            var d = dates_list.Contains(new DateTime(i));
        }
        stopwatch.Stop();
        return stopwatch.Elapsed;
    }

    private TimeSpan Hashset()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        for (int i = 0; i < iterations; i++)
        {
            var d = dates_hashset.Contains(new DateTime(i));
        }
        stopwatch.Stop();
        return stopwatch.Elapsed;
    }
}