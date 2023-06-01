namespace IdeeenBox;

public static class LoginSystem
{
    public static List<User> Users;
    public static User CurrentUser;
    
    public static bool Login(string email, string password)
    {
        var user = Users.FirstOrDefault(user => user.Email.Equals(email));
        if (user is not { EmailConfirmationCode: null } || !user.ComparePassword(password)) return false;
        CurrentUser = user;
        return true;
    }

    public static void Register(string username, string email, string password)
    {
        var user = new User(name: username, email: email, password: password);
        Users.Add(user);
        SaveSystem.Save(Users);
    }

    public static bool ConfirmEmail(User user, string code)
    {
        if (!user.EmailConfirmationCode.Equals(code)) return false;
        user.EmailConfirmationCode = null;
        return true;
    }
}