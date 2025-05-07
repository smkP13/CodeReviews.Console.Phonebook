using System.Diagnostics.CodeAnalysis;

namespace PhoneBook.smkP13.Models;

public class Category
{
    public int Id { get; set; }
    [NotNull]public string?  Name { get; set; }
    public List<Contact>? Contacts { get; set; }
    public ICollection<ContactCategory>? ContactCategories { get; set; }
}
