using System;

namespace ConsoleApp
{
    partial class Program
    {
        static void Main(string[] args)
        {

            HelloFrom("Bnaya");

            Console.WriteLine("Types in this assembly:");
            foreach (Type t in typeof(Program).Assembly.GetTypes())
            {
                Console.WriteLine(t.FullName);
            }

            var r = new ClassR();
            Console.WriteLine(r.Exec());
            var z = new ClassZ();
            Console.WriteLine(z.Invoke());
            Console.ReadKey(true);
            new ISampleGenerated().StartAsync(1, "");
        }

        static partial void HelloFrom(string name);
    }
}
