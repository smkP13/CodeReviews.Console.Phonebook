using PhoneBook.Controllers;
using PhoneBook.Models;
using Spectre.Console;

namespace PhoneBook.Services;

internal class ContactCategoryService
{
    internal static void UpdateContactCategories(Contact contact)
    {
        string option = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Choose an option").AddChoices("Add Category", "DeleteCategories", "Cancel"));
        switch (option)
        {
            case "Add Category":
                ContactCategory contactCategory = UserInputs.GetContactCategories(contact, "Add");
                if (contactCategory.ContactId != null)
                {
                    contactCategory.ContactId = ContactController.GetContactId(contact);
                    ContactCategoryController.AddContactCategory(contactCategory);
                }
                break;
            case "Delete Categories":
                contactCategory = UserInputs.GetContactCategories(contact);
                ContactController.UpdateContact(contact);
                if (contactCategory.ContactId != null)
                {
                    contactCategory.ContactId = ContactController.GetContactId(contact);
                    ContactCategoryController.DeleteContactCategory(contactCategory);
                }
                break;
            default:
                break;
        }
    }
}
