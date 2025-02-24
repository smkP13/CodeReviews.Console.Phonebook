using PhoneBook.Controllers;
using PhoneBook.Models;
using PhoneBook.Views;
using Spectre.Console;

namespace PhoneBook.Services;

internal class CategoryService
{
    internal static void AddCategory()
    {
        string name = UserInputs.GetName();
        CategoryController.AddCategory(name);
    }

    internal static void DeleteCategory()
    {
        List<Category> categories = CategoryController.GetAllCategories();
        SelectionPrompt<Category> selectionPrompt = new();
        selectionPrompt.AddChoice(new Category { CategoryId = 0 , Name = "Cancel"});
        selectionPrompt.AddChoices(categories).UseConverter(x => x.Name);
        Category category = AnsiConsole.Prompt(selectionPrompt);
        if (category.CategoryId != 0) CategoryController.DeleteCategory(category);
    }

    internal static void ShowAllCategories()
    {
        List<Category> categories = CategoryController.GetAllCategories();
        UserInterface.ShowAllCategories(categories);
    }

    internal static void ShowCategoryContacs()
    {
        Category category = UserInputs.GetCategory();
        if (category.CategoryId != 0)
        {
            category.Contacts = CategoryController.GetCategoryContacts(category);
            UserInterface.ShowCategoryContacts(category);
        }
    }

    internal static void UpdateCategory()
    {
        List<Category> categories = CategoryController.GetAllCategories();
        SelectionPrompt<Category> selectionPrompt = new();
        selectionPrompt.AddChoice(new Category { CategoryId = 0, Name = "Cancel" });
        selectionPrompt.AddChoices(categories).UseConverter(x => x.Name);
        Category category = AnsiConsole.Prompt(selectionPrompt);
        if (category.CategoryId != 0)
        {
            category.Name = UserInputs.GetName();
            CategoryController.UpdateCategory(category);
        }
    }
}
