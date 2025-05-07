using PhoneBook.smkP13.Services;
using Spectre.Console;

namespace PhoneBook.smkP13;

class Menus
{

    internal static void MainMenu()
    {
        bool run = true;
        while (run)
        {
            AnsiConsole.Clear();
            string option = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Choose an Option below:")
                .AddChoices("Manage Contacts", "Manage Categories", "Manage Email", "Exit"));
            switch (option)
            {
                case "Manage Contacts":
                    ContactMenu();
                    break;
                case "Manage Categories":
                    CategoryMenu();
                    break;
                case "Manage Email":
                    EmailMenu();
                    break;
                default: run = false; break;
            }
            AnsiConsole.MarkupLine("Press Any [yellow]Key[/] To Continue.");
            Console.ReadLine();
        }
    }

    private static void EmailMenu()
    {
        AnsiConsole.Clear();
        string option = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Manage Email(only works with gmail and using an app password")
            .AddChoices("Set User Email", "Set App Password", "Send Email", "Return"));
        switch (option)
        {
            case "Set User Email":
                MailService.SetEmail();
                break;
            case "Set App Password":
                MailService.SetAppPassword();
                break;
            case "Send Email":
                MailService.SendEmail();
                break;
            case "Return":
                break;
            default: break;
        }
    }

    private static void CategoryMenu()
    {
        AnsiConsole.Clear();
        string option = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Managing Categories")
            .AddChoices("Add New Category", "Delete Category", "Change Category's Name", "Update Category Contacts", "Show All Categories and their Contacts", "Show One Category and its Contacts", "Return"));
        switch (option)
        {
            case "Add New Category":
                CategoryService.AddNewCategory();
                break;
            case "Delete Category":
                CategoryService.DeleteCategory();
                break;
            case "Change Category's Name":
                CategoryService.ChangeCategoryName();
                break;
            case "Update Category Contacts":
                CategoryService.UpdateCategoryContacts();
                break;
            case "Show All Categories and their Contacts":
                CategoryService.ShowAllCategoriesAndContacts();
                break;
            case "Show One Category and its Contacts":
                CategoryService.ShowOneCategoryAndContacts();
                break;
            default: break;
        }
    }

    internal static void ContactMenu()
    {
        AnsiConsole.Clear();
        ContactService service = new();
        string option = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Managing Contacts:")
            .AddChoices("Add Contact", "Delete Contact", "Update Contact", "Show One Contact", "Show All Contacts", "Return"));
        switch (option)
        {
            case "Add Contact":
                service.AddContact();
                break;
            case "Delete Contact":
                service.DeleteContact();
                break;
            case "Update Contact":
                service.UpdateContact();
                break;
            case "Show One Contact":
                service.PrintSingleContact();
                break;
            case "Show All Contacts":
                service.PrintAllContacts();
                break;
            default: break;
        }
    }
}
