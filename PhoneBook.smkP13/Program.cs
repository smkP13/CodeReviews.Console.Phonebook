using PhoneBook.Views;

namespace PhoneBook;

internal class Program
{
    static void Main(string[] args)
    {
        ContactContext context = new ContactContext();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        UserInterface.MainMenu();
    }
}
