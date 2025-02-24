using PhoneBook.Models;

namespace PhoneBook.Controllers;

internal class ContactCategoryController
{
    internal static void AddContactCategory(ContactCategory contactCategory)
    {
        using ContactContext db = new();
        db.ContactCategories.Add(contactCategory);
        db.SaveChanges();
    }

    internal static void DeleteContactCategory(ContactCategory contactCategory)
    {
        using ContactContext db = new();
        db.ContactCategories.Remove(contactCategory);
        db.SaveChanges();
    }
}
