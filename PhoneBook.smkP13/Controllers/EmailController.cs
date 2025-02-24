using PhoneBook.Models;
using System.Net.Mail;

namespace PhoneBook.Controllers;

internal class EmailController
{
    internal static void AddUserEmail(UserEmail email)
    {
        using ContactContext db = new();
        db.UserEmail.Add(email);
        db.SaveChanges();
    }

    internal static UserEmail GetUserEmail()
    {
        using ContactContext db = new();
        UserEmail email = db.UserEmail.FirstOrDefault();
        if (email == null)
        {
            return null;
        }
        return email;
    }

    public static void SendEmail(Email email)
    {
        MailMessage message = new MailMessage(email.From, email.To, email.Subject, email.Body);
        message.IsBodyHtml = true;
        SmtpClient client = new SmtpClient("smtp.freesmtpservers.com");

        client.UseDefaultCredentials = true;

        try
        {
            client.Send(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception caught in CreateTestMessage2(): {0}",
                ex.ToString());
            client.Dispose();
            message.Dispose();
        }
        client.Dispose();
        message.Dispose();
    }

    internal static void UpdateUserEmail(UserEmail email)
    {
        using ContactContext db = new();
        db.UserEmail.Update(email);
        db.SaveChanges();
    }

    // Here is some code to send email via other smtp servers
    // requesting an inscription and authentifications
    // I didn't made one as I didn't want to have some personnal data misused as I am not sure about all the security it comes with
    // The following example is using SendinBlue's API v3 smtp library by Brevo

    //
    //using System;
    //using System.Diagnostics;
    //using sib_api_v3_sdk.Api;
    //using sib_api_v3_sdk.Client;
    //using sib_api_v3_sdk.Model;
    //


    //internal static void SendWithSMTP()
    //{
    //    // Configure API key authorization: api-key
    //    Configuration.Default.ApiKey.Add("api-key", "YOUR_API_KEY");
    //    // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
    //    // Configuration.Default.ApiKeyPrefix.Add("api-key", "Bearer");
    //    // Configure API key authorization: partner-key
    //    Configuration.Default.ApiKey.Add("partner-key", "YOUR_PARTNER_KEY");
    //    // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
    //    // Configuration.Default.ApiKeyPrefix.Add("partner-key", "Bearer");
    //
    //    var apiInstance = new AccountApi();
    //
    //    try
    //    {
    //        // Get your account information, plan and credits details
    //        GetAccount result = apiInstance.GetAccount();
    //        Debug.WriteLine(result);
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.Print("Exception when calling AccountApi.GetAccount: " + e.Message);
    //        client.Dispose();
    //        message.Dispose();
    //    }
    //    client.Dispose();
    //    message.Dispose();
    //
    //}

    // The messages can look better using html language in the body property.
}
