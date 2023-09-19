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
            Console.Clear();
            Console.WriteLine("Do you want continue");
            Console.ReadKey();
            
            await Main(args);
            Console.ReadKey();
        }

       
    }
}
