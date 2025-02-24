using PhoneBook.Controllers;
using PhoneBook.Models;
using Spectre.Console;

namespace PhoneBook;

internal class UserInputs
{
    internal static Category GetCategory()
    {
        List<Category> categories = CategoryController.GetAllCategories();
        categories.Add(new Category { CategoryId = 0, Name = "Cancel" });
        Category category = AnsiConsole.Prompt(new SelectionPrompt<Category>().Title("Choose a Category Below:").AddChoices(categories).UseConverter(x => x.Name));
        return category;

    }

    internal static ContactCategory GetContactCategories(Contact contact, string add = "")
    {
        List<Category> categories = CategoryController.GetAllCategories();
        if (add == "Add") categories.Add(new Category { CategoryId = 0, Name = "Add New Category" });
        categories.Add(new Category { Name = "Return to menu" });
        Category category = AnsiConsole.Prompt(new SelectionPrompt<Category>().Title("Choose a Category Below:").AddChoices(categories).UseConverter(x => x.Name));
        if (category.CategoryId == 0)
        {
            string categoryName = GetName();
            CategoryController.AddCategory(categoryName);
            category.CategoryId = CategoryController.GetNewCategoryId(categoryName);
            ContactCategory contactCategory = new ContactCategory { CategoryId = category.CategoryId, ContactId = contact.Id };
            return contactCategory;
        }
        if (category != null) return new ContactCategory { CategoryId = category.CategoryId, ContactId = contact.Id };
        return null;
    }

    internal static string GetEmail()
    {
        AnsiConsole.WriteLine("Enter an Email:");
        string email = AnsiConsole.Prompt(new TextPrompt<string>("[green](Format: email@example.ex)[/] ").DefaultValue("").ShowDefaultValue(false)
            .Validate(x => x.IndexOf('@') != -1 && x.IndexOf('@') != 0 && x.IndexOf('@') == x.LastIndexOf('@') && x.LastIndexOf('.') > x.LastIndexOf('@')
            && x.All(y => char.IsLetterOrDigit(y) || y == '@' || y == '.')));
        return email;
    }

    internal static string GetEmailMessage()
    {
        AnsiConsole.WriteLine("Enter your message");
        string message = AnsiConsole.Ask<string>("Message: ");
        return message;
    }

    internal static string GetEmailSubject()
    {
        AnsiConsole.WriteLine("Enter a subject:");
        string subject = AnsiConsole.Prompt(new TextPrompt<string>("[green](only whitespaces, letters and digits allowed)[/] ")
            .Validate(x => !string.IsNullOrWhiteSpace(x) && x.All(y => char.IsLetterOrDigit(y) || char.IsWhiteSpace(y))));
        return subject;
    }

    internal static string GetName()
    {
        AnsiConsole.WriteLine("Enter a Name:");
        string name = AnsiConsole.Prompt(new TextPrompt<string>("[green](only letters and digits allowed)[/] ")
            .Validate(x => !string.IsNullOrWhiteSpace(x) && x.All(y => char.IsLetterOrDigit(y))));
        return name;
    }

    internal static string GetPhoneNumber()
    {
        AnsiConsole.WriteLine("Enter a Phone Number:");
        string phoneNumber = AnsiConsole.Prompt(new TextPrompt<string>("[green](Formats: 0112223344, +00112223344, 0011223334455)[/] ").DefaultValue("").ShowDefaultValue(false)
            .Validate(x => x.Length > 1 && x.Length <= 13 && x.All(y => char.IsAsciiDigit(y) || y == '+')
            && (x.IndexOf('+') == x.LastIndexOf('+') && (x.IndexOf('+') == -1 || x.IndexOf('+') == 0))));
        string test = "0795348924";
        string compareTest = "+001112233";
        test.CompareTo(compareTest);
        return phoneNumber;
    }
}