namespace PhoneBook.smkP13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PhoneBookContext context = new();
            // Uncomment below to reset db with seeding sample
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            Menus.MainMenu();
        }
    }
}
