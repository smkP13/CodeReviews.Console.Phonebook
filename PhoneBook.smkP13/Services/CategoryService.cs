using PhoneBook.smkP13.Controllers;
using PhoneBook.smkP13.Models;
using Spectre.Console;

namespace PhoneBook.smkP13.Services;

class CategoryService
{
    internal static Category? AddNewCategory()
    {
        Category? category = new();
        category.Name = UserInputs.GetCategoryName("Enter a Category Name");
        category = CategoryController.AddNewCategory(category);
        return category;
    }

    internal static void ChangeCategoryName()
    {
        Category category = UserInputs.ChooseOneCategory();
        if (category.Id != 0)
        {
            category.Name = UserInputs.GetCategoryName($"Enter a new Name for {category.Name}");
            if (category.Name != null) CategoryController.UpdateCategoryName(category);
        }
    }

    internal static void DeleteCategory()
    {
        Category category = UserInputs.ChooseOneCategory();
        if(category.Id != 0)
        {
            if (UserInputs.Validation($"Are you sure you want to delete the category \"{category.Name}\"?")) CategoryController.DeleteCategory(category);
        }
    }

    internal static void ShowAllCategoriesAndContacts()
    {
        List<Category> categories = CategoryController.GetAllCategories();
        Panel panel;
        foreach(Category category in categories)
        {
            string contactsInfo = "";
            category.Contacts = ContactController.GetContactByCategory(category.Id);
            if (category.Contacts != null)
                foreach(Contact contact in category.Contacts)
                {
                    contactsInfo += contact.LastName == null ? $"{contact.FirstName}\n" : $"{contact.FirstName} - {contact.LastName}\n";
                }
            panel = new(contactsInfo);
            panel.Header(category.Name).PadRight(category.Name.Count());
            AnsiConsole.Write(panel);
        }
    }

    internal static void ShowOneCategoryAndContacts()
    {
        Category category = UserInputs.ChooseOneCategory();
        Panel panel;
        category.Contacts = ContactController.GetContactByCategory(category.Id);
        Table table = new();
        table.Title(category.Name);
        table.AddColumn("Contacts");
        if (category.Contacts != null)
            foreach (Contact contact in category.Contacts)
            {
                panel = new($"Phone Number: {contact.PhoneNumber}\nEmail Address: {contact.Email}");
                if (contact.LastName == null) panel.Header($"{contact.FirstName}"); else panel.Header($"{contact.FirstName} - {contact.LastName}");
                table.AddRow(panel);
            }
        AnsiConsole.Write(table);
    }

    internal static void UpdateCategoryContacts()
    {
        Category category = UserInputs.ChooseOneCategory();
        if(category.Id != 0)
        {
            category.Contacts = ContactController.GetContactByCategory(category.Id);
            category.Contacts = UserInputs.GetManyContacts(category) ;
            if (category.Contacts != null && !category.Contacts.Any(x => x.Id == 0)) { ContactCategoryController.UpdateByCategory(category); }
        }
    }
}
