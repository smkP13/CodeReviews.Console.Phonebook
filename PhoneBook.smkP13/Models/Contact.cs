using System.ComponentModel;

namespace PhoneBook.smkP13.Models;

public class Contact
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public List<Category>? Categories { get; set; }
    public ICollection<ContactCategory>?  ContactCategories { get; set; }
}
