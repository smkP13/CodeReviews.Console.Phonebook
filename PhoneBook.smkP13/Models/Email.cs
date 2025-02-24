namespace PhoneBook.Models;

internal class Email
{
    public string From { get; set; }
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}

internal class UserEmail
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string DisplayName { get; set; }
}
