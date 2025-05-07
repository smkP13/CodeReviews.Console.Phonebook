using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhoneBook.smkP13.Models;
namespace PhoneBook.smkP13;

class PhoneBookContext : DbContext
{
    internal DbSet<Contact> Contacts { get; set; }
    internal DbSet<Category> Categories { get; set; }
    internal DbSet<ContactCategory> ContactCategories { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        optionsBuilder.UseSqlServer(config.GetConnectionString("default"));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>().HasKey(x => new { x.Id });
        modelBuilder.Entity<Category>().HasKey(x => new { x.Id });
        modelBuilder.Entity<ContactCategory>().HasKey(x => new { x.Id });
        modelBuilder.Entity<ContactCategory>().HasOne(x => x.Contact).WithMany(x => x.ContactCategories);
        modelBuilder.Entity<ContactCategory>().HasOne(x => x.Category).WithMany(x => x.ContactCategories);

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Favourite" },
            new Category { Id = 2, Name = "Family" },
            new Category { Id = 3, Name = "Friend" }
            );

        modelBuilder.Entity<Contact>().HasData(
                new Contact { Id = 1, FirstName = "John", LastName = "Wick", PhoneNumber = "7674452523", Email = "john.wick7@gmail.com" },
                new Contact { Id = 2, FirstName = "Mom", PhoneNumber = "4864567575", Email = "bestmom@mom.com" },
                new Contact { Id = 3, FirstName = "Coffeezilla", PhoneNumber = "6435351438", Email = "therealcoffeezilla@antiscam.com" },
                new Contact { Id = 4, FirstName = "Sam", LastName = " Smith", PhoneNumber = "8752576865", Email = "sam.smith@gmail.com" },
                new Contact { Id = 5, FirstName = "Mah Boi", PhoneNumber = "+41635688888", Email = "mahboi@gmail.com" }
            );

        modelBuilder.Entity<ContactCategory>().HasData(
            new ContactCategory { Id = 1, CategoryId = 1, ContactId = 2 },
            new ContactCategory { Id = 2, CategoryId = 2, ContactId = 2 },
            new ContactCategory { Id = 3, CategoryId = 1, ContactId = 5 },
            new ContactCategory { Id = 4, CategoryId = 3, ContactId = 5 }
            );
    }
}
