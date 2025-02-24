using PhoneBook.Models;

namespace PhoneBook.Controllers;

internal class CategoryController
{
    internal static void AddCategory(string categoryName)
    {
        using ContactContext db = new();
        db.Categories.Add(new Category { Name = categoryName });
        db.SaveChanges();
    }

    internal static void DeleteCategory(Category category)
    {
        using ContactContext db = new();
        db.Categories.Remove(category);
        db.SaveChanges();
    }

    internal static List<Category> GetAllCategories()
    {
        using ContactContext db = new();
        List<Category> categories = db.Categories.ToList();
        return categories;
    }

    internal static List<Category> GetCategories(int contactId)
    {
        using ContactContext db = new();
        List<ContactCategory> cC = db.ContactCategories.OrderBy(x => x.ContactId).ToList();
        cC.RemoveAll(x => x.ContactId != contactId);
        List<Category>? categories = new();
        foreach (ContactCategory c in cC)
        {
            categories.Add(new Category { CategoryId = c.CategoryId, Name = db.Categories.SingleOrDefault(x => x.CategoryId == c.CategoryId).Name });
        }
        return categories;
    }

    internal static List<Contact> GetCategoryContacts(Category category)
    {
        using ContactContext db = new();
        List<ContactCategory> contactCategories = db.ContactCategories.ToList();
        contactCategories.RemoveAll(x => x.CategoryId != category.CategoryId);
        List<Contact> contacts = new();
        foreach (ContactCategory contact in contactCategories)
        {
            contacts.Add(db.Contacts.SingleOrDefault(x => x.Id == contact.ContactId));
        }
        return contacts;
    }

    internal static int GetNewCategoryId(string categoryName)
    {
        using ContactContext db = new();
        Category category = db.Categories.SingleOrDefault(x => x.Name == categoryName);
        return category.CategoryId;
    }

    internal static void UpdateCategory(Category category)
    {
        using ContactContext db = new();
        db.Update(category);
        db.SaveChanges();
    }
}
