namespace PhoneBook.Models;

internal class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    internal List<Contact> Contacts { get; set; }
    public ICollection<ContactCategory> Categories { get; set; }
}
