using Manage.Helpers;
using System;
using System.Threading.Tasks;

namespace Manage.Modules
{
    public class MainApplication
    {
        public static async Task Start()
        {
            Console.WriteLine("1. Manage Incomes     2. Manage Expenses     3. Manage Categories");
            int input = ConsoleHelper.GetOptionInput();

            Console.Clear();

            switch (input)
            {
                case 1:
                    await IncomeModule.ShowOptionsAsync();
                    break;
                case 2:
                    await ExpenseModule.ShowOptionsAsync();
                    break;
                case 3:
                    await CategoryModule.ShowOptionsAsync();
                    break;
            }
        }
    }
}
