using SendGrid;
using SendGrid.Helpers.Mail;

namespace IdeeenBox;

public static class EmailSender
{
    private const string ApiKey = "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";
    private const string SenderAddress = "ErwinsIdeeenbox@outlook.com";
    private const string SenderName = "Erwins Ideeënbox";
    
    public static async Task<Response> SendPassword(User user) {
        const string subject = "Forgotten password";
        var receiverEmail = new EmailAddress(user.Email, user.Name);
        var password = user.RegeneratePassword();
        var textContent = "Hi " + user.Name + ". Your new password is: " + password;
        var htmlContent = "Hi " + user.Name + "<br><br>Your new password is: <strong>" + password + "</strong>";

        return await SendMail(receiverEmail, subject, textContent, htmlContent);
    }

    
    public static async Task<Response> SendIdeaSharingEmail(User sender, User receiver, Idea idea)
    {
        if (!sender.Ideas.Contains(idea)) return null;
        
        var receiverEmail = new EmailAddress(receiver.Email, receiver.Name);
        var subject = sender.Name + " has shared an Idea with you!";
        var textContent = "Hi " + receiver.Name + ". " + sender.Name + " has shared \"" + idea.Name + "\" with you. Go check it out!";
        var htmlContent = "Hi " + receiver.Name + ",<br><br>" + sender.Name + " has shared \"<strong>" + idea.Name + "</strong>\" with you. Go check it out!";

        return await SendMail(receiverEmail, subject, textContent, htmlContent);
    }
    
    public static async Task<Response> SendConfirmationCode(User user)
    {
        var receiverEmail = new EmailAddress(user.Email, user.Name);
        const string subject = "Confirm your email address";
        var textContent = "Hi " + user.Name + ". Your confirmation code is: " + user.EmailConfirmationCode;
        var htmlContent = "Hi " + user.Name + ",<br><br>Your confirmation code is: <strong>" + user.EmailConfirmationCode + "</strong>";

        return await SendMail(receiverEmail, subject, textContent, htmlContent);
    }

    private static async Task<Response> SendMail(EmailAddress receiverEmail, string subject, string textContent, string htmlContent)
    {
        // Console.WriteLine(textContent);
        var senderEmail = new EmailAddress(SenderAddress, SenderName);
        var client = new SendGridClient(ApiKey);
        var msg = MailHelper.CreateSingleEmail(senderEmail, receiverEmail, subject, textContent, htmlContent);
        
        Console.WriteLine("You will receive this email shortly.");
        return await client.SendEmailAsync(msg).ConfigureAwait(false);
    }
}