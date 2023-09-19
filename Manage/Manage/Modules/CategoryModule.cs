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
                    await GetAllCategoriesAsync();
                    break;
                case 2:
                    await GetCategoryByIdAsync();
                    break;
                case 3:
                    await CreateCategoryAsync();
                    break;
                case 4:
                    await UpdateCategoryAsync();
                    break;
                case 5:
                    await DeleteCategoryAsync();
                    break;

                default:
                    return;
            }
        }

        private static async Task GetAllCategoriesAsync()
        {

            List<Category> categories = await CategoryService.GetAllCategoriesAsync();

            foreach (var category in categories)
            {
                Console.WriteLine(category);
            }

            Console.ReadKey();
        }

        private static async Task CreateCategoryAsync()
        {
            Console.WriteLine("Please, enter category values below.");
            Console.WriteLine();

            string categoryName = null;
            do
            {
                Console.Write("Enter new category name: ");
                categoryName = Console.ReadLine();
            }
            while (categoryName == null);

            await CategoryService.CreateCategory(new Models.Category(categoryName));

            Console.Write("Enter any key to continue");
            Console.ReadKey();
        }

        private static async Task GetCategoryByIdAsync()
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
        private static async Task UpdateCategoryAsync()
        {
            Console.Write("Enter the category ID: ");

            if (!int.TryParse(Console.ReadLine(), out int categoryId))
            {
                ConsoleHelper.WriteLineError("Invalid input. Please enter a valid category ID.");
                return;
            }

            Category existingCategory = await CategoryService.GetCategoryById(categoryId);

            if (existingCategory is null)
            {
                ConsoleHelper.WriteLineError($"Category with ID {categoryId} does not exist.");
                Console.ReadKey();
                return;
            }

            string categoryName = null;
            do
            {
                Console.Write("Enter new category name: ");
                categoryName = Console.ReadLine();
            }
            while (categoryName == null);

            existingCategory.Name = categoryName;

            await CategoryService.UpdateCategory(existingCategory);

            Console.Write("Enter any key to continue");
            Console.ReadKey();
        }
        private static async Task DeleteCategoryAsync()
        {
            Console.Write("Enter the category ID: ");

            if (!int.TryParse(Console.ReadLine(), out int categoryId))
            {
                ConsoleHelper.WriteLineError("Invalid input. Please enter a valid category ID.");
                return;
            }

            await CategoryService.DeleteCategory(categoryId);
            Console.ReadKey();
        }

    }
}

