using System.Collections.Generic;
using System.Linq;

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

    public static User Register(string username, string email, string password)
    {
        var newUser = new User(name: username, email: email, password: password);
        foreach (var user in Users)
            foreach (var idea in user.Ideas)
                foreach (var userEmail in idea.SharedWithEmail.ToList())
                    if (userEmail.Equals(email))
                    {
                        idea.SharedWithEmail.Remove(email);
                        idea.SharedWith.Add(newUser);
                        newUser.SharedIdeas.Add(idea);
                    }
        Users.Add(newUser);
        SaveSystem.Save(Users);
        return newUser;
    }

    public static bool ConfirmEmail(User user, string code)
    {
        if (!user.EmailConfirmationCode.Equals(code)) return false;
        user.EmailConfirmationCode = null;
        return true;
    }
}
