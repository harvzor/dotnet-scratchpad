using System;
using System.Diagnostics;

public interface IMyClass
{
    string ClassName { get; }
}

public class MyClass : IMyClass
{
    public string ClassName => nameof(MyClass);
}
                    
public class Program
{
    private static void Benchmark(Action act, int iterations)
    {
        // Warmup
        for (int i = 0; i < iterations; i++)
        {
            act.Invoke();
        }

        // Actually run
        GC.Collect();
        act.Invoke(); // run once outside of loop to avoid initialization costs
        Stopwatch sw = Stopwatch.StartNew();
        for (int i = 0; i < iterations; i++)
        {
            act.Invoke();
        }
        sw.Stop();
        Console.WriteLine((sw.ElapsedTicks).ToString());
    }

    public static void Main(string[] args)
    {
        Benchmark(() =>
        {
            IMyClass myClass = new MyClass();
            
            var name = myClass.ClassName;
        }, 10000);

        Benchmark(() =>
        {
            IMyClass myClass = new MyClass();
            
            var name = myClass.GetType().Name;
        }, 10000);
    }
}
