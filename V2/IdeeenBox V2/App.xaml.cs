using IdeeenBox;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace IdeeenBox_V2
{
    public partial class App : Application
    {
        // ToDo: Delete confirmation windows
        // ToDo: Fix regstration back-end
        // ToDo: Make saved file public
        // ToDo: EditProfilePage Useless error
        // ToDo: Make password sending more secure
        // ToDo: Upload build V2 to GitHub
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            LoginSystem.Users = SaveSystem.Read();
        }
    }
}
