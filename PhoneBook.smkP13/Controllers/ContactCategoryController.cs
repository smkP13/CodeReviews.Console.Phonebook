using PhoneBook.smkP13.Models;

namespace PhoneBook.smkP13.Controllers;

class ContactCategoryController
{
    internal static void AddForNewContact(Contact contact, List<Category> selectedCategories)
    {
        using PhoneBookContext context = new();
        selectedCategories.Remove(selectedCategories.FirstOrDefault(x => x.Id == -1));
        foreach (Category category in selectedCategories)
        {
            context.ContactCategories.Add(new ContactCategory { ContactId = contact.Id, CategoryId = category.Id });
        }
        context.SaveChanges();
    }

    internal static void UpdateByContact(Contact contact)
    {
        using PhoneBookContext context = new();
        context.ContactCategories.RemoveRange(context.ContactCategories.Where(x => x.ContactId == contact.Id));
        if (contact.Categories != null)
        {
            foreach (Category cat in contact.Categories) { context.ContactCategories.Add(new ContactCategory { ContactId = contact.Id, CategoryId = cat.Id }); }
        }
        context.SaveChanges();
    }

    internal static void UpdateByCategory(Category category)
    {
        using PhoneBookContext context = new();
        context.ContactCategories.RemoveRange(context.ContactCategories.Where(x => x.CategoryId == category.Id));
        if (category.Contacts != null)
        {
            foreach (Contact cont in category.Contacts) { context.ContactCategories.Add(new ContactCategory { ContactId = cont.Id, CategoryId = category.Id }); }
        }
        context.SaveChanges();
    }
}
