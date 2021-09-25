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

            Console.ReadKey(true);
        }

        static partial void HelloFrom(string name);
    }
}
