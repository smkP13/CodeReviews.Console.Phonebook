namespace PhoneBook.Models;

internal class ContactCategory
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public int ContactId { get; set; }
    public Category Category { get; set; }
    public Contact Contact { get; set; }
}
