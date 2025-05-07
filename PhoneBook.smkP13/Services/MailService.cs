using PhoneBook.smkP13.Controllers;
using PhoneBook.smkP13.Models;
using Spectre.Console;

namespace PhoneBook.smkP13.Services;

class MailService
{
    internal static void SendEmail()
    {
        UserEmail? userEmail = EmailController.GetUserEmail();
        if (userEmail != null && userEmail.AppPassword != null && userEmail.Email != "" && userEmail.AppPassword != "")
        {
            string[] bodySubject = UserInputs.CreateEmail();
            EmailController emailController = new();
            bool sent = emailController.SendEmail(userEmail, bodySubject[0], bodySubject[1]);
            if (sent) AnsiConsole.MarkupLine("[Blue]Email[/] mail sent successfully.");
            else AnsiConsole.MarkupLine("$\"An [Red]error[/] occured with the [red]mail[/], try to verify your email [green]Adress[/] and your [green]App Password[/].\nOr verify your connection.");
        }
        else AnsiConsole.WriteLine("Please set the user Email and App Password before trying to send any message");
    }

    internal static void SetAppPassword()
    {
        UserEmail? userEmail = EmailController.GetUserEmail();
        userEmail.AppPassword = UserInputs.GetAppPassword();
        EmailController.UpdateUserEmail(userEmail);
        AnsiConsole.WriteLine("Your App Password has been changed");
    }

    internal static void SetEmail()
    {
        UserEmail? userEmail = EmailController.GetUserEmail();
        userEmail.Email = UserInputs.GetEmail("Enter a new User Mail");
        userEmail.FirstName = UserInputs.GetFirstName("Enter User First Name");
        if (UserInputs.Validation("Do you want to enter a Last Name?")) userEmail.LastName = UserInputs.GetLastName("Enter User Last Name");
        else userEmail.LastName = "";
        EmailController.UpdateUserEmail(userEmail);
    }
}