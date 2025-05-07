using PhoneBook.smkP13.Models;

namespace PhoneBook.smkP13.Controllers;

public class CategoryController
{
    public static Category? AddNewCategory(Category category)
    {
        using PhoneBookContext context = new();
        List<Category> oldCategoriyList = context.Categories.ToList();
        context.Categories.Add(category);
        context.SaveChanges();
        return context.Categories.FirstOrDefault(x => x.Name == category.Name);
    }

    public static List<Category> GetAllCategories()
    {
       using  PhoneBookContext context = new();
        return context.Categories.ToList();
    }

    public static List<Category>? GetContactCategories(int id)
    {
        using PhoneBookContext context = new();
        List<ContactCategory> contactCategories = context.ContactCategories.Where(x => x.ContactId == id).ToList();
        List<Category> categories = new();
        foreach(ContactCategory contactCategory in contactCategories) categories.AddRange(context.Categories.Where(x => x.Id == contactCategory.CategoryId));
        return categories;
    }

    internal static void DeleteCategory(Category category)
    {
        using PhoneBookContext context = new();
        context.Categories.Remove(category);
        context.SaveChanges();
    }

    internal static void UpdateCategory(Category category)
    {
        using (PhoneBookContext context = new())
        {
            context.Update(category);
            context.SaveChanges();
        }
        ContactCategoryController.UpdateByCategory(category);
    }

    internal static void UpdateCategoryName(Category category)
    {
        using PhoneBookContext context = new();
        context.Update(category);
        context.SaveChanges();
    }
}
