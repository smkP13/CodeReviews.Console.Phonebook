using System.Diagnostics.CodeAnalysis;

namespace PhoneBook.smkP13.Models;

public class UserEmail
{
    public int? Id { get; set; }
    [NotNull]public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? DisplayName => $"{FirstName} {LastName}";
    public string? AppPassword { get; set; }
}
