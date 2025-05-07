using PhoneBook.smkP13.Controllers;
using PhoneBook.smkP13.Models;
using Spectre.Console;

namespace PhoneBook.smkP13;

class UserInputs
{
    internal static List<Category>? ChooseCategories(Contact contact)
    {
        contact.Categories = CategoryController.GetContactCategories(contact.Id);
        List<Category> categories = CategoryController.GetAllCategories();
        categories.Add(new Category { Id = -1, Name = "Add new Category" });
        categories.Add(new Category { Id = 0, Name = "Cancel" });
        MultiSelectionPrompt<Category> prompt = new MultiSelectionPrompt<Category>().Title("Select Categories:")
            .AddChoices(categories)
            .UseConverter(x => x.Name);
        if (contact.Categories != null) foreach (Category category in contact.Categories) prompt.Select(categories.FirstOrDefault(x => x.Id == category.Id));
        List<Category> selectedCategories = AnsiConsole.Prompt(prompt);
        return selectedCategories;
    }

    internal static Category ChooseOneCategory()
    {
        List<Category> categories = CategoryController.GetAllCategories();
        categories.Add(new Category { Id = 0, Name = "Cancel" });
        return AnsiConsole.Prompt(new SelectionPrompt<Category>().Title("Select a Category below:").AddChoices(categories).UseConverter(x => x.Name));
    }

    internal static string[] CreateEmail()
    {
        string[] bodySubject = new string[2];
        string subject = AnsiConsole.Prompt(new TextPrompt<string>("Enter a Subject for the mail:"));
        string body = AnsiConsole.Prompt(new TextPrompt<string>("Enter a Body for the mail"));
        bodySubject[0] = subject;
        bodySubject[1] = body;
        return bodySubject;
    }

    internal static string? GetAppPassword()
    {
        return AnsiConsole.Prompt(new TextPrompt<string>("Enter an App Password (format: aaaa-aaaa-aaaa-aaaa (a = letter or digit)")
            .Validate(x => x.Length == 19
            && x.Substring(0, 4).All(y => char.IsLetterOrDigit(y))
            && x.Substring(5, 4).All(y => char.IsLetterOrDigit(y))
            && x.Substring(10, 4).All(y => char.IsLetterOrDigit(y))
            && x.Substring(15, 4).All(y => char.IsLetterOrDigit(y))
            && x[4] == ' ' && x[9] == ' ' && x[14] == ' '));
    }

    internal static string? GetCategoryName(string message)
    {
        List<Category> categories = CategoryController.GetAllCategories();
        string category = AnsiConsole.Prompt(new TextPrompt<string>(message)
            .Validate(x => x.All(y => char.IsLetterOrDigit(y)))
            .ValidationErrorMessage("Names can only contain Letters and Digits")
            .Validate(x => !categories.Any(y => y.Name == x))
            .ValidationErrorMessage($"Category [red]name[/] already exists."));
        return category;
    }

    internal static Contact GetContact()
    {
        List<Contact> contacts = ContactController.GetAllContacts();
        contacts.Add(new Contact { Id = 0, FirstName = "Cancel" });
        return AnsiConsole.Prompt(new SelectionPrompt<Contact>().Title("Select a Contact below").AddChoices(contacts)
            .UseConverter(x => $"{x.FirstName} {x.LastName}"));
    }

    internal static string GetEmail(string message)
    {
        AnsiConsole.WriteLine(message);
        return AnsiConsole.Prompt(new TextPrompt<string>("[green](Format: email@example.ex)[/] ").DefaultValue("").ShowDefaultValue(false)
            .Validate(x => x.IndexOf('@') != -1 && x.IndexOf('@') != 0 && x.IndexOf('@') == x.LastIndexOf('@') && x.LastIndexOf('.') > x.LastIndexOf('@')
            && x.All(y => char.IsLetterOrDigit(y) || y == '@' || y == '.')));
    }

    internal static string GetFirstName(string message)
    {
        return AnsiConsole.Prompt(new TextPrompt<string>(message)
            .Validate(x => !string.IsNullOrWhiteSpace(x) && x.All(y => char.IsLetterOrDigit(y) || y == ' ' || y == '-'))
            .ValidationErrorMessage("Names can only contain Letters and Digits"));
    }

    internal static string? GetLastName(string message)
    {
        return AnsiConsole.Prompt(new TextPrompt<string>(message)
            .Validate(x => x.All(y => char.IsLetterOrDigit(y) || y == ' ' || y == '-'))
            .ValidationErrorMessage("Names can only contain Letters and Digits")
            .AllowEmpty());
    }

    internal static List<Contact>? GetManyContacts(Category category)
    {
        List<Contact>? contacts = ContactController.GetAllContacts();
        contacts.Add(new Contact { Id = 0, FirstName = "Cancel" });
        MultiSelectionPrompt<Contact> prompt = new MultiSelectionPrompt<Contact>().Title("Choose Contacts below:").AddChoices(contacts);
        prompt.UseConverter(x => $"{x.FirstName} {x.LastName}");
        prompt.NotRequired();
        if (category.Contacts != null) foreach (Contact contact in category.Contacts) prompt.Select(contacts.FirstOrDefault(x => x.Id == contact.Id));
        return AnsiConsole.Prompt(prompt);
    }

    internal static string? GetPhoneNumber(string message)
    {
        AnsiConsole.WriteLine(message);
        return AnsiConsole.Prompt(new TextPrompt<string>("[green](Formats: 0112223344, +00112223344, 0011223334455)[/] ").DefaultValue("").ShowDefaultValue(false)
            .Validate(x => x.Length > 1 && x.Length <= 13 && x.All(y => char.IsAsciiDigit(y) || y == '+')
            && (x.IndexOf('+') == x.LastIndexOf('+') && (x.IndexOf('+') == -1 || x.IndexOf('+') == 0))));
    }

    internal static bool Validation(string message)
    {
        return AnsiConsole.Prompt(new SelectionPrompt<bool>().Title(message)
            .AddChoices(true, false).UseConverter(x => x ? "yes" : "no"));
    }
}
