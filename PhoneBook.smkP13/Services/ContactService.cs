using PhoneBook.Controllers;
using PhoneBook.Models;
using PhoneBook.Views;
using Spectre.Console;
using System.Globalization;

namespace PhoneBook.Services;
internal class ContactService
{
    internal static void AddContact()
    {
        Contact contact = new();
        contact.Name = UserInputs.GetName();
        contact.PhoneNumber = AnsiConsole.Confirm("Add a Phone Number?") ? UserInputs.GetPhoneNumber() : "";
        contact.Email = AnsiConsole.Confirm("Add an Email?") ? UserInputs.GetEmail() : "";
        ContactCategory? contactCategory = null;
        if (AnsiConsole.Confirm("Add a Category?")) { contactCategory = UserInputs.GetContactCategories(contact, "Add"); }
        ContactController.AddContact(contact);
        if (contactCategory.ContactId != null)
        { contactCategory.ContactId = ContactController.GetContactId(contact); ContactCategoryController.AddContactCategory(contactCategory); }
    }

    internal static void DeleteContact()
    {
        Contact contact = GetContact();
        if (contact.PhoneNumber != "0") ContactController.DeleteContact(contact);
    }

    internal static Contact GetContact()
    {
        List<Contact> contacts = ContactController.GetContacts();
        SelectionPrompt<Contact> selectionPrompt = new();
        selectionPrompt.Title("Choose a Contact:").AddChoices(contacts).UseConverter(x => $"[mediumpurple4]Name[/]: {x.Name}").WrapAround();
        selectionPrompt.AddChoice(new Contact { Name = "Go back to Menu", PhoneNumber = "0" });
        Contact contact = AnsiConsole.Prompt(selectionPrompt);
        return contact;
    }

    internal static void ShowAllContacts()
    {
        List<Contact> contacts = ContactController.GetContacts();
        UserInterface.ShowAllContacts(contacts);
    }

    internal static void ShowContact()
    {
        Contact contact = GetContact();
        UserInterface.ShowContact(contact);
    }

    internal static void UpdatContact()
    {
        Contact contact = GetContact();
        if (contact.PhoneNumber != "0")
        {
            string option = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Choose an option below:").AddChoices("Name", "Phone Number", "Email", "Return"));
            if (option != "Return")
            {
                switch (option)
                {
                    case "Name":
                        contact.Name = AnsiConsole.Prompt(new TextPrompt<string>("Enter a Name").Validate(x => !string.IsNullOrWhiteSpace(x) && x.IndexOfAny(['\\', '[', '{', '&', ',', '\"', '(', ')', '[', ']', '{', '}']) == -1)); ;
                        ContactController.UpdateContact(contact);
                        break;
                    case "Phone Number":
                        int outValue;
                        contact.PhoneNumber = AnsiConsole.Prompt(new TextPrompt<string>("Enter a phone Number").Validate(x => x.Length >= 10 && x.Length <= 13 && int.TryParse(x, CultureInfo.CurrentCulture, out outValue)));
                        ContactController.UpdateContact(contact);
                        break;
                    case "Email":
                        contact.Email = AnsiConsole.Prompt(new TextPrompt<string>("Enter an email").Validate(x => x.IndexOf('@') != -1 && x.IndexOf('@') == x.LastIndexOf('@') && x.LastIndexOf('.') > x.LastIndexOf('@') &&
                                        x.IndexOfAny(['\\', '/', '$', '!', '?', '[', '{', '&', ',', '\"', '(', ')', '[', ']', '{', '}', ' ']) == -1));
                        ContactController.UpdateContact(contact);
                        break;
                    case "Categories":
                        ContactCategoryService.UpdateContactCategories(contact);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
