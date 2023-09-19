using Manage.Helpers;
using Manage.Models;
using Manage.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Manage.Modules
{
    internal class IncomeModule
    {

        public static async Task ShowOptionsAsync()
        {
            Console.WriteLine("1. See all income     2. Find income by id     3. Add income");
            Console.WriteLine("4. Update income        5. Delete income");

            int selectedOption = ConsoleHelper.GetOptionInput();
            Console.Clear();

            switch (selectedOption)
            {
                case 1:

                    await GetAllIncomesAsync();

                    break;
                case 2:
                    await GetIncomeByIdAsync();
                    break;
                case 3:
                    await CreateIncomesAsync();
                    break;
                case 4:
                    await UpdateIncomeAsync();
                    break;
                case 5:
                    await DeleteIncomeAsync();
                    break;

                default:
                    return;
            }
        }

        private static async Task GetAllIncomesAsync()
        {

            List<Income> incomes = await IncomeService.GetAllIncomeAsync();

            foreach (var income in incomes)
            {
                Console.WriteLine(income);
            }

            Console.ReadKey();
        }

        private static async Task CreateIncomesAsync()
        {
            Console.WriteLine("Please, enter Income values below.");
            Console.WriteLine();


            Console.Write("Enter category_id: ");
            int categoryId = int.Parse(Console.ReadLine());

            Console.Write("Enter description: ");
            string description = Console.ReadLine();

            Console.Write("Amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            DateTime date = DateTime.Now;

            await IncomeService.CreateIncome(new Models.Income(categoryId, description, amount, date));

            Console.WriteLine("Enter any key to continue");
            Console.ReadKey();
        }

        private static async Task GetIncomeByIdAsync()
        {
            Console.Write("Enter the Income ID: ");

            if (!int.TryParse(Console.ReadLine(), out int IncomeId))
            {
                ConsoleHelper.WriteLineError("Invalid input. Please enter a valid Income ID.");
                return;
            }

            Income Income = await IncomeService.GetIncomeById(IncomeId);

            if (Income is null)
            {
                ConsoleHelper.WriteLineError($"Income with ID {IncomeId} does not exist.");
            }
            else
            {
                Console.WriteLine(Income);
            }

            Console.ReadKey();
        }
        private static async Task UpdateIncomeAsync()
        {
            Console.Write("Enter the Income ID: ");

            if (!int.TryParse(Console.ReadLine(), out int IncomeId))
            {
                ConsoleHelper.WriteLineError("Invalid input. Please enter a valid Income ID.");
                return;
            }

            Income existingIncome = await IncomeService.GetIncomeById(IncomeId);

            if (existingIncome is null)
            {
                ConsoleHelper.WriteLineError($"Income with ID {IncomeId} does not exist.");
                Console.ReadKey();
                return;
            }

            int categoryId;
            string description = null;
            decimal amount;
            DateTime startDate;

            Console.Write("Enter new category Id: ");
            categoryId = int.Parse(Console.ReadLine());

            Console.Write("Enter the new description: ");
            description = Console.ReadLine();

            Console.Write("Enter the new amount: ");
            amount = int.Parse(Console.ReadLine());

            startDate = DateTime.Now;



            existingIncome.CategoryId = categoryId;
            existingIncome.Description = description;
            existingIncome.Amount = amount;
            existingIncome.Date = startDate;

            await IncomeService.UpdateIncome(existingIncome);

            Console.Write("Enter any key to continue");
            Console.ReadKey();
        }
        private static async Task DeleteIncomeAsync()
        {
            Console.Write("Enter the Income ID: ");

            if (!int.TryParse(Console.ReadLine(), out int IncomeId))
            {
                ConsoleHelper.WriteLineError("Invalid input. Please enter a valid Income ID.");
                return;
            }

            await IncomeService.DeleteIncome(IncomeId);
            Console.ReadKey();
        }
    }
}
