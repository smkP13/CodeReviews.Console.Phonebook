using PhoneBook.Models;

namespace PhoneBook.Controllers;
internal class ContactController
{
    internal static void AddContact(Contact contact)
    {
        using ContactContext db = new();
        db.Contacts.Add(contact);
        db.SaveChanges();
    }

    internal static void DeleteContact(Contact contact)
    {
        using ContactContext db = new();
        db.Contacts.Remove(contact);
        db.SaveChanges();
    }

    internal static int GetContactId(Contact contact)
    {
        using ContactContext db = new();
        int id = db.Contacts.SingleOrDefault(x => x.Name == contact.Name && x.Email == contact.Email && x.PhoneNumber == contact.PhoneNumber).Id;
        return id;
    }

    internal static List<Contact> GetContacts()
    {
        using ContactContext db = new();
        List<Contact> contacts = db.Contacts.OrderBy(x => x.Name).ToList();
        return contacts;
    }

    internal static void UpdateContact(Contact contact)
    {
        using ContactContext db = new();
        db.Update(contact);
        db.SaveChanges();
    }
}
