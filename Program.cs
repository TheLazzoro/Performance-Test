namespace Program
{
    public static class Program
    {

        public static void Main(string[] args)
        {
            new Shapes().Run();
            new Persons().Run();
            new Allocations().Run();
            new Loops().Run();
            new MemoryTest().Run();
            new ExceptionTest().Run();
            new MathTest().Run();
            new ListHashset().Run();
        }
    }
}