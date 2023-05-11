using System.Diagnostics;


public class Person
{
    public string name;
    public uint age;
    public uint height;

    public void IncreaseAge()
    {
        age++;
    }
}

struct Person_struct
{
    public string[] name;
    public uint[] age;
    public uint[] height;
}

public static class Persons
{
    public static void Run()
    {
        int person_count = 100000;

        // OOP
        List<Person> persons_OOP = new List<Person>();
        for (uint i = 0; i < person_count; i++)
        {
            persons_OOP.Add(
                new Person()
                {
                    name = "MyName",
                    age = i,
                    height = i
                }
            );
        }

        // DOD
        Person_struct persons_DOD = new Person_struct();
        persons_DOD.name = new string[person_count];
        persons_DOD.age = new uint[person_count];
        persons_DOD.height = new uint[person_count];
        for (uint i = 0; i < person_count; i++)
        {
            persons_DOD.name[i] = "MyName";
            persons_DOD.age[i] = i;
            persons_DOD.height[i] = i;
        }

        // Warmup phase
        IncreaseAge_OOP(persons_OOP);
        IncreaseAge_DOD(persons_DOD);

        // Benchmark
        TimeSpan oop = IncreaseAge_OOP(persons_OOP);
        TimeSpan dod = IncreaseAge_DOD(persons_DOD);

        Console.WriteLine("Person OOP: 1.000x");
        Console.WriteLine("Person DOD: " + $"{(oop / dod).ToString("0.000")}x");
    }

    static TimeSpan IncreaseAge_OOP(List<Person> list)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        for (int i = 0; i < list.Count; i++)
        {
            list[i].IncreaseAge();
        }
        stopwatch.Stop();
        return stopwatch.Elapsed;
    }

    static TimeSpan IncreaseAge_DOD(Person_struct persons)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        for (int i = 0; i < persons.age.Length; i++)
        {
            persons.age[i]++;
        }
        stopwatch.Stop();
        return stopwatch.Elapsed;
    }
}