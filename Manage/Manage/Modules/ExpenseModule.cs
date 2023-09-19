using Manage.Helpers;
using Manage.Models;
using Manage.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manage.Modules
{
    internal class ExpenseModule
    {

        public static async Task ShowOptionsAsync()
        {
            Console.WriteLine("1. See all expense     2. Find expense by id     3. Add expense");
            Console.WriteLine("4. Update expense        5. Delete expense");

            int selectedOption = ConsoleHelper.GetOptionInput();
            Console.Clear();

            switch (selectedOption)
            {
                case 1:

                    await GetAllExpensesAsync();

                    break;
                case 2:
                    await GetExpenseByIdAsync();
                    break;
                case 3:
                    await CreateExpensesAsync();
                    break;
                case 4:
                    await UpdateExpenseAsync();
                    break;
                case 5:
                    await DeleteExpenseAsync();
                    break;

                default:
                    return;
            }
        }

        private static async Task GetAllExpensesAsync()
        {

            List<Expense> expenses = await ExpenseService.GetAllExpenseAsync();

            foreach (var expense in expenses)
            {
                Console.WriteLine(expense);
            }

            Console.ReadKey();
        }

        private static async Task CreateExpensesAsync()
        {
            Console.WriteLine("Please, enter Expense values below.");
            Console.WriteLine();

           
            Console.Write("Enter category_id: ");
            int categoryId = int.Parse(Console.ReadLine());

            Console.Write("Enter description: ");
            string description = Console.ReadLine();

            Console.Write("Amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            DateTime date = DateTime.Now;

            await ExpenseService.CreateExpense(new Models.Expense(categoryId, description,  amount,date));

            Console.WriteLine("Enter any key to continue");
            Console.ReadKey();
        }

        private static async Task GetExpenseByIdAsync()
        {
            Console.Write("Enter the Expense ID: ");

            if (!int.TryParse(Console.ReadLine(), out int ExpenseId))
            {
                ConsoleHelper.WriteLineError("Invalid input. Please enter a valid Expense ID.");
                return;
            }

            Expense Expense = await ExpenseService.GetExpenseById(ExpenseId);

            if (Expense is null)
            {
                ConsoleHelper.WriteLineError($"Expense with ID {ExpenseId} does not exist.");
            }
            else
            {
                Console.WriteLine(Expense);
            }

            Console.ReadKey();
        }
        private static async Task UpdateExpenseAsync()
        {
            Console.Write("Enter the Expense ID: ");

            if (!int.TryParse(Console.ReadLine(), out int ExpenseId))
            {
                ConsoleHelper.WriteLineError("Invalid input. Please enter a valid Expense ID.");
                return;
            }

            Expense existingExpense = await ExpenseService.GetExpenseById(ExpenseId);

            if (existingExpense is null)
            {
                ConsoleHelper.WriteLineError($"Expense with ID {ExpenseId} does not exist.");
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



            existingExpense.CategoryId = categoryId;
            existingExpense.Description = description;
            existingExpense.Amount = amount;
            existingExpense.Date = startDate;

            await ExpenseService.UpdateExpense(existingExpense);

            Console.Write("Enter any key to continue");
            Console.ReadKey();
        }
        private static async Task DeleteExpenseAsync()
        {
            Console.Write("Enter the Expense ID: ");

            if (!int.TryParse(Console.ReadLine(), out int ExpenseId))
            {
                ConsoleHelper.WriteLineError("Invalid input. Please enter a valid Expense ID.");
                return;
            }

            await ExpenseService.DeleteExpense(ExpenseId);
            Console.ReadKey();
        }

    }
}

