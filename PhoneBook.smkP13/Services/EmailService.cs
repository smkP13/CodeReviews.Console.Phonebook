using PhoneBook.Controllers;
using PhoneBook.Models;
using Spectre.Console;

namespace PhoneBook.Services;

internal class EmailService
{
    private static string GetToEmail()
    {
        List<Contact> contacts = ContactController.GetContacts();
        contacts.Add(new Contact { Id = 0, Name = "Enter New Email" });
        Contact contact = AnsiConsole.Prompt(new SelectionPrompt<Contact>().Title("Choose a contact").UseConverter(x => $"Name: {x.Name} - Email: {x.Email}").AddChoices(contacts));
        if (contact.Id == 0)
        {
            contact.Email = UserInputs.GetEmail();
        }
        return contact.Email;
    }

    internal static void SendEmail()
    {
        Email email = new();
        email.From = EmailController.GetUserEmail().Email;
        if (email.From == null)
        {
            SetUserEmail();
        }
        email.To = GetToEmail();
        email.Subject = UserInputs.GetEmailSubject();
        email.Body = UserInputs.GetEmailMessage();
        EmailController.SendEmail(email);
    }

    internal static void SetUserEmail()
    {
        UserEmail email = email = EmailController.GetUserEmail();
        if (email.Email == null)
        {
            AnsiConsole.WriteLine("Enter an new User Email.");
            email.Email = UserInputs.GetEmail();
            AnsiConsole.WriteLine("Choose a new Display Name");
            email.DisplayName = UserInputs.GetName();
            EmailController.AddUserEmail(email);
        }
        else
        {
            AnsiConsole.WriteLine("Enter an new User Email.");
            email.Email = UserInputs.GetEmail();
            AnsiConsole.WriteLine("Choose a new Display Name");
            email.DisplayName = UserInputs.GetName();
            EmailController.AddUserEmail(email);
            EmailController.UpdateUserEmail(email);
        }
    }
}
