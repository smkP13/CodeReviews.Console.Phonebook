namespace PhoneBook;

internal class Enums
{
    internal enum MainMenuOptions
    {
        ManageContacts,
        ManageCategories,
        ManageEmails,
        Exit,
    }

    internal enum ContactOptions
    {
        AddContact,
        DeleteContact,
        UpdateContact,
        ShowContact,
        ShowAllContacts,
        Return,
    }

    internal enum CategoryOptions
    {
        AddCategory,
        DeleteCategory,
        UpdateCategory,
        ShowCategoryContacts,
        ShowAllCategories,
        Return,
    }

    internal enum EmailOptions
    {
        SendEmail,
        SetUserEmail,
        Return,
    }
}
