using PhoneBook.smkP13.Models;
using Spectre.Console;
using System.Net;
using System.Net.Mail;
using System.Text.Json;

namespace PhoneBook.smkP13.Controllers;

public class EmailController
{
    internal static UserEmail? GetUserEmail()
    {
        string docPath = AppContext.BaseDirectory;
        int pathLength = docPath.Length - 17;
        docPath = docPath.Substring(0, pathLength);
        string fileStr = File.ReadAllText($"{docPath}UserEmail.json");
        return JsonSerializer.Deserialize<UserEmail>(fileStr);
    }

    internal static void UpdateUserEmail(UserEmail? email)
    {
        string docPath = AppContext.BaseDirectory;
        int pathLength = docPath.Length - 17;
        docPath = docPath.Substring(0, pathLength);
        string fileStr = JsonSerializer.Serialize(email);
        File.WriteAllText($"{docPath}UserEmail.json", fileStr);
    }

    public bool SendEmail(UserEmail user, string subject, string body)
    {
        using MailMessage mail = new();
        mail.From = new MailAddress(user.Email);
        mail.To.Add(user.Email);
        mail.Subject = subject;
        mail.Body = body;
        mail.IsBodyHtml = true;
        try
        {
            using SmtpClient client = new("smtp.gmail.com", 587);
            {
                client.Credentials = new NetworkCredential(user.Email, user.AppPassword);
                client.EnableSsl = true;
                client.Send(mail);
                return true;
            }
        }
        catch (Exception ex) { AnsiConsole.Write(ex.Message); return false; }
    }
}
