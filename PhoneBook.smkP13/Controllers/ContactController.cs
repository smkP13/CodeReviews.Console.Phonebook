using PhoneBook.smkP13.Models;
using Spectre.Console;

namespace PhoneBook.smkP13.Controllers;

public class ContactController
{
    public static void AddContact(Contact contact)
    {
        using PhoneBookContext context = new();
        context.Contacts.Add(contact);
        context.SaveChanges();
    }

    public static void DeleteContact(Contact contact)
    {
        using PhoneBookContext context = new();
        context.Contacts.Remove(contact);
        context.SaveChanges();
    }

    public static List<Contact> GetAllContacts()
    {
        using PhoneBookContext context = new();
        return context.Contacts.ToList();
    }

    public static bool UpdateContact(Contact contact)
    {
        try
        {
            using (PhoneBookContext context = new())
            {
                context.Update(contact);
                context.SaveChanges();
            }
            ContactCategoryController.UpdateByContact(contact);
            return true;
        }
        catch(Exception ex) { AnsiConsole.Write(ex.Message); return false; }
    }

    internal static List<Contact>? GetContactByCategory(int id)
    {
        using PhoneBookContext context = new();
        List<ContactCategory> contactCategories = context.ContactCategories.Where(x => x.CategoryId == id).ToList();
        List<Contact> allContacts = ContactController.GetAllContacts();
        List<Contact> contacts = new();
        foreach (ContactCategory contCat in contactCategories) contacts.Add(allContacts.FirstOrDefault(x => x.Id == contCat.ContactId));
        return contacts;
    }
}
