using System.Text;
using SendGrid;

namespace IdeeenBox;

[Serializable]
public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string EmailConfirmationCode { get; set; }
    private string Password { get; set; }
    public List<Idea> Ideas { get; set; }
    public List<Idea> SharedIdeas { get; set; }

    public User(string name, string email, string password)
    {
        Name = name;
        Email = email;
        EmailConfirmationCode = RandomString(6);
        Password = password;
        Ideas = new List<Idea>();
        SharedIdeas = new List<Idea>();
    }
    
    public void AddIdea(Idea idea) { Ideas.Add(idea); }
    public void SetPassword(string previousPassword, string password) { if(ComparePassword(previousPassword)) Password = password; }

    public string RegeneratePassword()
    {
        var password = RandomString(10);
        Password = password;
        return password;
    }

    public bool ComparePassword(string password) { return password.Equals(Password); }

    public async Task<Response> ResendConfirmationCode()
    {
        EmailConfirmationCode = RandomString(6);
        return await EmailSender.SendConfirmationCode(this);
    }

    private static string RandomString(int size)
    {
        var builder = new StringBuilder();
        var random = new Random();
        
        for (var i = 0; i < size; i++)
            builder.Append(Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))));
        
        return builder.ToString();
    }
}