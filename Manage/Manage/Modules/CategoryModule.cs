using Manage.Helpers;
using Manage.Models;
using Manage.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manage.Modules
{
    internal class CategoryModule
    {
        public static async Task ShowOptionsAsync()
        {
            Console.WriteLine("1. See all categories     2. Find category by id     3. Add Category");
            Console.WriteLine("4. Update category        5. Delete Category");

            int selectedOption = ConsoleHelper.GetOptionInput();
            Console.Clear();

            switch (selectedOption)
            {
                case 1:
                    await DisplayAllCategoriesAsync();
                    break;
                case 2:
                    await DisplayCategoryByIdAsync();
                    break;
                case 3:
                    await CreateCategoryAsync();
                    break;

                default:
                    return;
            }
        }

        public static async Task DisplayAllCategoriesAsync()
        {
            Console.WriteLine("disp");
            List<Category> categories = await CategoryService.DisplayAllCategoriesAsync();
            Console.WriteLine("list");
            foreach (var category in categories)
            {
                Console.WriteLine(category);
            }

            Console.ReadKey();
        }

        private static async Task CreateCategoryAsync()
        {
            Console.WriteLine("Please enter category information below.");
            Console.WriteLine();

            string categoryName = null;
            do
            {
                Console.Write("Enter the new category name: ");
                categoryName = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(categoryName));

            await CategoryService.CreateCategory(new Category(categoryName));

            Console.Write("Category created successfully. Press any key to continue...");
            Console.ReadKey();
        }

        private static async Task DisplayCategoryByIdAsync()
        {
            Console.Write("Enter the category ID: ");

            if (!int.TryParse(Console.ReadLine(), out int categoryId))
            {
                ConsoleHelper.WriteLineError("Invalid input. Please enter a valid category ID.");
                return;
            }

            Category category = await CategoryService.GetCategoryById(categoryId);

            if (category is null)
            {
                ConsoleHelper.WriteLineError($"Category with ID {categoryId} does not exist.");
            }
            else
            {
                Console.WriteLine(category);
            }

            Console.ReadKey();
        }

    }
}

