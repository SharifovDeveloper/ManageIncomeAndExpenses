using System;
using System.Threading.Tasks;
using Manage.Modules;

namespace Manage
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await MainApplication.Start();

            Console.ReadKey();
        }

        static async Task Execute()
        {
            var task = DisplayHello();
            var task2 = DisplayWorld();
            var task3 = CalculateSum(2, 2);

            await Task.WhenAll(task, task2, task3);
        }

        static async Task DisplayHello()
        {
            Console.WriteLine("Hello");
            await Task.Delay(100);
            Console.WriteLine("Salom");
        }

        static async Task DisplayWorld()
        {
            Console.WriteLine("World");
            await Task.Delay(100);
            Console.WriteLine("Dunyo");
        }

        static async Task CalculateSum(int a, int b)
        {
            Console.WriteLine(a + b);
            await Task.Delay(100);
            Console.WriteLine(a * b);
        }
    }
}
