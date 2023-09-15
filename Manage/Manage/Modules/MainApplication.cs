using Manage.Helpers;
using Manage.Models;
using Manage.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Modules
{
    public class MainApplication
    {
       
        public static async Task  Start() 
        {
            Console.WriteLine("1. Manage Incomes     2. Manage Expenses     3. Manage Categories");
            int input = ConsoleHelper.GetOptionInput();

            Console.Clear();

            switch (input)
            {
                case 3:
                     CategoryModule.ShowOptionsAsync();
                    break;

            }

        }



    }
}
