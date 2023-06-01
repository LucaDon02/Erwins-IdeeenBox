using System.Net;

namespace IdeeenBox;

internal static class Program
{

    private static async Task Main()
    {
        // ToDo: A lot of testing
        // ToDo: Email for creating an account when an idea is shared with an non existing account
        // ToDo: Make password sending more secure
        Console.Title = "Erwin's ideeënbox";
        LoginSystem.Users = SaveSystem.Read();

        while (true)
        {
            while (LoginSystem.CurrentUser == null)
            {
                if (LoginSystem.Users.Count == 0)
                {
                    Console.WriteLine("There were no existing users found.");
                    RegisterMenu();
                }
                else await LoginOrRegisterMenu();
            }

            while (LoginSystem.CurrentUser != null) MainMenu();
        }
    }
    
    private static async Task LoginOrRegisterMenu()
    {
        while (true)
        {
            switch (AskFeedback("\nDo you want to login or register?\n" +
                                "1. Login\n" +
                                "2. Register\n" +
                                "3. Confirm email code\n" +
                                "4. Forgot password"))
            {
                case "1":
                    LoginMenu();
                    break;
                case "2":
                    RegisterMenu();
                    break;
                case "3":
                    await ConfirmEmailMenu();
                    break;
                case "4":
                    ForgotPasswordMenu();
                    break;
                default:
                    Console.WriteLine("Please enter one of the following numbers.");
                    continue;
            } break;
        }
    }

    private static void LoginMenu()
    {
        while (true)
        {
            var email = AskFeedback("\nWhat is your email? Or press enter to cancel.");

            if (email == "") return;
            
            var password = AskFeedback("\nWhat is your password?");

            if (password == "" || !LoginSystem.Login(email, password))
            {
                Console.WriteLine("Login failed. Wrong email and/or password or the email isn't confirmed. Please confirm the email and try again.");
                continue;
            }

            Console.WriteLine("\n\nWelcome " + LoginSystem.CurrentUser.Name);
            break;
        }
    }

    private static void RegisterMenu()
    {
        while (true)
        {
            var username = AskFeedback("\nWhat is your username? Or press enter to cancel.");

            if (username == "") return;
            if (LoginSystem.Users.Any(user => user.Name.Equals(username)))
            {
                Console.WriteLine("This username already exists. Please try another username.");
                continue;
            }
            
            var email = AskFeedback("\nWhat is your email address? Or press enter to cancel.");

            if (email == "") return;
            if (LoginSystem.Users.Any(user => user.Email.Equals(email)))
            {
                Console.WriteLine("This email already exists. Please try another email address.");
                continue;
            }
            
            var password = AskFeedback("\nWhat is your password?");
            while (password == "") password = AskFeedback("Password cannot be empty. Please enter another password.");
            
            LoginSystem.Register(username, email, password);
            Console.WriteLine("Please check your email to confirm your account.");
            break;
        }
    }

    private static async Task ConfirmEmailMenu()
    {
        while (true)
        {
            var email = AskFeedback("\nWhat is your email address?");
            var user = LoginSystem.Users.SingleOrDefault(user => user.Email.Equals(email));
            if (user == null)
            {
                Console.WriteLine("No user with this email exists. Please try again.");
                continue;
            }
            
            if (user.EmailConfirmationCode == null)
            {
                Console.WriteLine("This email is already confirmed.");
                break;
            }

            var response = AskFeedback("Please enter the code that you have received in your email inbox or type \"resend\" to resend a code.");
            if (response.Equals("resend"))
            {
                await user.ResendConfirmationCode();
                Console.WriteLine("Please check your email inbox for the code.");
                SaveSystem.Save(LoginSystem.Users);
                break;
            }
            if (LoginSystem.ConfirmEmail(user, response))
            {
                Console.WriteLine("Confirmation successful.");
                SaveSystem.Save(LoginSystem.Users);
                break;
            }
            Console.WriteLine("Wrong code. Please try again.");
        }
    }

    private static void ForgotPasswordMenu()
    {
        while (true)
        {
            var name = AskFeedback("\nWhat is your username? Press enter to cancel.");
            if (name.Equals("")) return;
            var user = LoginSystem.Users.SingleOrDefault(user => user.Name.Equals(name));
            if (user != null)
            {
                Console.WriteLine(EmailSender.SendPassword(user).Result.StatusCode == HttpStatusCode.Accepted
                    ? "An email has been sent."
                    : "An error has occurred");
                return;
            }
            Console.WriteLine("This username does not exist. Please try again.");
            continue;
        }
    }

    private static void MainMenu()
    {
        var displayText = "\nWhat do you want to do?\n" +
                          "1. Log out\n" +
                          "2. Edit profile\n" +
                          "3. Save new idea\n";
        if (LoginSystem.CurrentUser.Ideas.Count > 0 || LoginSystem.CurrentUser.SharedIdeas.Count > 0) displayText += "4. Read current ideas";
        
        switch (AskFeedback(displayText))
        {
            case "1":
                LoginSystem.CurrentUser = null;
                break;
            case "2":
                EditProfileMenu();
                break;
            case "3":
                AddNewIdea();
                break;
            case "4":
                if (LoginSystem.CurrentUser.Ideas.Count > 0 || LoginSystem.CurrentUser.SharedIdeas.Count > 0) IdeasListMenu();
                else Console.WriteLine("Please enter one of the following numbers.");
                break;
            default:
                Console.WriteLine("Please enter one of the following numbers.");
                break;
        }
    }

    private static void EditProfileMenu()
    {
        while (true)
        {
            switch (AskFeedback("\nWhat do you want to do?\n" +
                                "1. Go back\n" +
                                "2. Change your username\n" +
                                "3. Change your email address\n" +
                                "4. Change your password"))
            {
                case "1":
                    break;
                case "2":
                    var name = AskFeedback("What do you want your username to be? Leave it blank to cancel.");
                    if (!name.Equals("") && !name.Equals(LoginSystem.CurrentUser.Name))
                    {
                        if (!LoginSystem.Users.Any(user => user.Name.Equals(name)))
                        {
                            LoginSystem.CurrentUser.Name = name;
                            SaveSystem.Save(LoginSystem.Users);
                            Console.WriteLine("Your username has been changed.");
                            break;
                        }
                        Console.WriteLine("This username already exists. Please try another username.");
                    }
                    break;
                case "3":
                    var email = AskFeedback("What do you want your email address to be? Leave it blank to cancel.");
                    if (!email.Equals("") && !email.Equals(LoginSystem.CurrentUser.Email))
                    {
                        if (!LoginSystem.Users.Any(user => user.Email.Equals(email)))
                        {
                            LoginSystem.CurrentUser.Email = email;
                            SaveSystem.Save(LoginSystem.Users);
                            Console.WriteLine("Your email address has been changed.");
                            break;
                        }
                        Console.WriteLine("This email address already exists. Please try another email address.");
                    }
                    break;
                case "4":
                    ChangePasswordMenu();
                    break;
                default:
                    Console.WriteLine("Please choose a valid option");
                    continue;
            } break;
        }
    }

    private static void ChangePasswordMenu()
    {
        while (true)
        {
            var oldPassword = AskFeedback("Enter your old password? Leave it blank to cancel.");
            if (!LoginSystem.CurrentUser.ComparePassword(oldPassword))
            {
                Console.WriteLine("Please enter the correct password");
                continue;
            }
            if (oldPassword.Equals("")) return;

            var newPassword = AskFeedback("What do you want your password to be? Leave it blank to cancel.");
            if (newPassword.Equals("")) return;

            var confirmationPassword = AskFeedback("Confirm new password? Leave it blank to cancel.");
            if (confirmationPassword.Equals("")) return;
            while (!newPassword.Equals(confirmationPassword))
            {
                Console.WriteLine("The two passwords don't match. Please try again.");
                
                newPassword = AskFeedback("What do you want your password to be? Leave it blank to cancel.");
                if (newPassword.Equals("")) return;

                confirmationPassword = AskFeedback("Confirm new password? Leave it blank to cancel.");
                if (confirmationPassword.Equals("")) return;
            }
            
            LoginSystem.CurrentUser.SetPassword(oldPassword, newPassword);
            SaveSystem.Save(LoginSystem.Users);
            break;
        }
    }

    private static void IdeasListMenu()
    {
        while (true)
        {
            var question = "\nWhat do you want to do?\n" +
                                "1. Go back";
            var counter = 2;
            if (LoginSystem.CurrentUser.Ideas.Count > 0) { question += "\n" + counter + ". Show own ideas"; counter++; }
            if (LoginSystem.CurrentUser.Ideas.Any(i => i.SharedWith.Count > 0)) { question += "\n" + counter + ". Show shared ideas"; counter++; }
            if (LoginSystem.CurrentUser.SharedIdeas.Count > 0) question += "\n" + counter + ". Show ideas shared with me";

            switch (AskFeedback(question))
            {
                case "1":
                    return;
                case "2":
                    if (LoginSystem.CurrentUser.Ideas.Count > 0) OwnIdeasListMenu();
                    else if (LoginSystem.CurrentUser.SharedIdeas.Count > 0) SharedWithMeIdeasListMenu();
                    else Console.WriteLine("Please choose a valid option.");
                    continue;
                case "3":
                    if (LoginSystem.CurrentUser.Ideas.Count > 0)
                    {
                        if (LoginSystem.CurrentUser.Ideas.Any(i => i.SharedWith.Count > 0)) SharedIdeasListMenu();
                        else SharedWithMeIdeasListMenu();
                    }
                    else Console.WriteLine("Please choose a valid option.");
                    continue;
                case "4":
                    if (LoginSystem.CurrentUser.Ideas.Count > 0 &&
                        LoginSystem.CurrentUser.Ideas.Any(i => i.SharedWith.Count > 0) &&
                        LoginSystem.CurrentUser.SharedIdeas.Count > 0)
                        SharedWithMeIdeasListMenu();
                    continue;
                default:
                    Console.WriteLine("Please choose a valid option.");
                    continue;
            }
            break;
        }
    }
    
    private static void SharedIdeasListMenu()
    {
        while (true)
        {
            var ideas = LoginSystem.CurrentUser.Ideas.Where(idea => idea.SharedWith.Count > 0);
            var sharedIdeas = ideas as Idea[] ?? ideas.ToArray();
            var question = "\nChoose any of the following shared ideas to get more details or press enter to go back.";
            for (var i = 0; i < sharedIdeas.Length; i++) question += "\n" + (i+1) + ". " + sharedIdeas[i].Name;

            var response = AskFeedback(question);
            if (int.TryParse(response, out var input))
            {
                input--;
                if (input >= 0 && input < sharedIdeas.Length)
                {
                    var sharedIdea = sharedIdeas[input];
                    var question2 = "\n" + sharedIdea.Name + "\n" +
                                        sharedIdea.Description + "\n" +
                                        "Shared with:";
                    for (var i = 0; i < sharedIdea.SharedWith.Count; i++) question2 += "\n" + (i+1) + ". " + sharedIdea.SharedWith[i].Name;
                    question2 += "\nEnter the number of the user you want to unshare this idea with or press enter to go back.";
                    
                    var response2 = AskFeedback(question2);
                    if (int.TryParse(response2, out var input2))
                    {
                        input2--;
                        if (input2 >= 0 && input2 < sharedIdea.SharedWith.Count)
                        {
                            var user = sharedIdea.SharedWith[input2];
                            user.SharedIdeas.Remove(sharedIdea);
                            sharedIdea.SharedWith.Remove(user);
                            SaveSystem.Save(LoginSystem.Users);
                            continue;
                        }
                    }
                    if (response2.Equals("")) continue;
                    Console.WriteLine("Please choose a valid option.");
                }
            }
            if (response.Equals("")) break;
            Console.WriteLine("Please choose a valid option.");
        }
    }
    
    private static void SharedWithMeIdeasListMenu()
    {
        while (true)
        {
            var displayText = "\nChoose any of the following ideas to get more details or press enter to go back.";
            for (var i = 0; i < LoginSystem.CurrentUser.SharedIdeas.Count; i++) displayText += "\n" + (i+1) + ". " + LoginSystem.CurrentUser.SharedIdeas[i].Name;
            
            var response = AskFeedback(displayText);
            if (int.TryParse(response, out var input))
            {
                input--;
                if (input >= 0 && input < LoginSystem.CurrentUser.SharedIdeas.Count)
                {
                    var sharedIdea = LoginSystem.CurrentUser.SharedIdeas[input];
                    var response2 = AskFeedback("\n" + sharedIdea.Name + "\n" +
                                                     sharedIdea.Description + "\n" +
                                                     "Owner: " + sharedIdea.Owner + "\n" +
                                                     "\n" +
                                                     "Type \"delete\" to delete or press enter to go back");
                    if (string.Equals(response2, "delete", StringComparison.OrdinalIgnoreCase))
                    {
                        sharedIdea.SharedWith.Remove(LoginSystem.CurrentUser);
                        LoginSystem.CurrentUser.SharedIdeas.Remove(sharedIdea);
                        SaveSystem.Save(LoginSystem.Users);
                    }
                    continue;
                }
            }
            if (response.Equals("")) break;
            Console.WriteLine("Please choose a valid option.");
        }
    }

    private static void OwnIdeasListMenu()
    {
        while (true)
        {
            var displayText = "\nChoose any of the following ideas to get more details or press enter to go back.";
            for (var i = 0; i < LoginSystem.CurrentUser.Ideas.Count; i++) displayText += "\n" + (i+1) + ". " + LoginSystem.CurrentUser.Ideas[i].Name;
            
            var response = AskFeedback(displayText);
            if (int.TryParse(response, out var input))
            {
                if (input > 0 && input <= LoginSystem.CurrentUser.Ideas.Count)
                {
                    OwnIdeaMenu(input-1);
                    continue;
                }
            }
            if (response.Equals("")) break;
            Console.WriteLine("Please choose a valid option.");
        }
    }

    private static void OwnIdeaMenu(int ideaIndex)
    {
        while (true)
        {
            var idea = LoginSystem.CurrentUser.Ideas[ideaIndex];
            var question = "\n" + idea.Name + "\n" +
                                idea.Description + "\n" +
                                "\n" +
                                "What do you want to do?\n" +
                                "1. Go back\n" +
                                "2. Edit name\n" +
                                "3. Edit description\n" +
                                "4. Delete";
            if (LoginSystem.Users.Count > 1) question += "\n5. Share";

            switch (AskFeedback(question))
            {
                case "1":
                    return;
                case "2":
                    var name = AskFeedback("\nWhat do you want the name to be? Leave it blank to cancel.");
                    if (name != "")
                    {
                        LoginSystem.CurrentUser.Ideas[ideaIndex].Name = name;
                        SaveSystem.Save(LoginSystem.Users);
                    }
                    break;
                case "3":
                    var description = AskFeedback("\nWhat do you want the description to be? Leave it blank to cancel.");
                    if (description != "")
                    {
                        LoginSystem.CurrentUser.Ideas[ideaIndex].Description = description;
                        SaveSystem.Save(LoginSystem.Users);
                    }
                    break;
                case "4":
                    var response2 = AskFeedback("Are you sure that you want to delete \"" + idea.Name + "\"?");
                    while (!string.Equals(response2, "yes", StringComparison.OrdinalIgnoreCase)
                           && !string.Equals(response2, "no", StringComparison.OrdinalIgnoreCase))
                        response2 = AskFeedback("Please enter a valid option.\n" +
                                               "Are you sure that you want to delete \"" + idea.Name + "\"?");
                    if (string.Equals(response2, "yes", StringComparison.OrdinalIgnoreCase))
                    {
                        foreach (var user in idea.SharedWith) user.SharedIdeas.Remove(idea);
                        LoginSystem.CurrentUser.Ideas.Remove(idea);
                    }
                    break;
                case "5":
                    if (LoginSystem.Users.Count == 1)
                    {
                        Console.WriteLine("Please enter one of the following numbers.");
                        break;
                    }
                    while (true)
                    {
                        var question2 = "\nWho do you want to share this idea with? Or press enter to cancel.";
                        var i = 1;
                        foreach (var user in LoginSystem.Users.Where(user => user != LoginSystem.CurrentUser)) { question2 += "\n" + i + ". " + user.Name; i++; }

                        var response = AskFeedback(question2);
                        if (int.TryParse(response, out var input))
                        {
                            input--;
                            if (input >= 0 && input < LoginSystem.Users.Count)
                            {
                                if (input >= LoginSystem.Users.IndexOf(LoginSystem.CurrentUser)) input++;
                                var sharedWith = LoginSystem.Users[input];
                                sharedWith.SharedIdeas.Add(idea);
                                idea.SharedWith.Add(sharedWith);
                                EmailSender.SendIdeaSharingEmail(LoginSystem.CurrentUser, sharedWith, idea);
                                Console.WriteLine("Shared successfully with " + sharedWith.Name);
                                SaveSystem.Save(LoginSystem.Users);
                                break;
                            }
                        }
                        if (response.Equals("")) break;
                        Console.WriteLine("Please choose a valid option.");
                    }
                    break;
                default:
                    Console.WriteLine("Please enter one of the following numbers.");
                    break;
            }
        }
    }

    private static void AddNewIdea()
    {
        var name = AskFeedback("\nWhat do you want the name to be? Leave it blank to cancel.");
        if (name == "") return;
        
        var description = AskFeedback("\nWhat do you want the description to be? Leave it blank to cancel.");
        if (description == "") return;
        
        LoginSystem.CurrentUser.Ideas.Add(new Idea(owner: LoginSystem.CurrentUser, name: name, description: description));
        SaveSystem.Save(LoginSystem.Users);
        Console.WriteLine("Idea added!");
    }
    
    private static string AskFeedback(string question)
    {
        Console.WriteLine(question);
        return Console.ReadLine() ?? throw new InvalidOperationException();
    }
}