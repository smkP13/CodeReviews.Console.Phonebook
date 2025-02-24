using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;

namespace PhoneBook;

internal class ContactContext : DbContext
{
    internal DbSet<Contact> Contacts { get; set; }
    internal DbSet<Category> Categories { get; set; }
    internal DbSet<ContactCategory> ContactCategories { get; set; }
    internal DbSet<UserEmail> UserEmail { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=PhoneBookByP13;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContactCategory>().HasKey(x => new { x.Id });
        modelBuilder.Entity<ContactCategory>().HasOne(x => x.Contact).WithMany(x => x.Categories).HasForeignKey(x => x.ContactId);
        modelBuilder.Entity<ContactCategory>().HasOne(x => x.Category).WithMany(x => x.Categories).HasForeignKey(x => x.CategoryId);
        modelBuilder.Entity<Category>().HasKey(x => new { x.CategoryId });
        modelBuilder.Entity<Contact>().HasKey(x => new { x.Id });
        modelBuilder.Entity<UserEmail>().HasKey(x => new { x.Id });

        modelBuilder.Entity<ContactCategory>().HasData(
            new ContactCategory { Id = 1, CategoryId = 1, ContactId = 2 },
            new ContactCategory { Id = 2, CategoryId = 2, ContactId = 2 },
            new ContactCategory { Id = 3, CategoryId = 1, ContactId = 5 },
            new ContactCategory { Id = 4, CategoryId = 3, ContactId = 5 }
            );
        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, Name = "Favourite" },
            new Category { CategoryId = 2, Name = "Family" },
            new Category { CategoryId = 3, Name = "Friend" }
            );
        modelBuilder.Entity<Contact>().HasData
            (new List<Contact>
            {
                new Contact{Id = 1, Name = "John Wick", PhoneNumber = "7674452523", Email = "john.wick7@gmail.com" },
                new Contact{Id = 2, Name = "Mom", PhoneNumber = "4864567575", Email = "bestmom@mom.com"},
                new Contact{Id = 3, Name = "Coffeezilla", PhoneNumber = "6435351438", Email = "therealcoffeezilla@antiscam.com"},
                new Contact{Id = 4, Name = "Sam Smith", PhoneNumber = "8752576865", Email = "sam.smith@gmail.com" },
                new Contact{Id = 5, Name = "Mah Boi", PhoneNumber ="+41635688888", Email ="mahboi@gmail.com"}
            }
            );
        modelBuilder.Entity<UserEmail>().HasData(new UserEmail { Id = 1, Email = "pbexample@phonebook.ch", DisplayName = "pbexp13" });
    }
}
