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
        // ToDo: Make saved file public accessible by mutliple users simultaneously
        // ToDo: Make password sending more secure
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            LoginSystem.Users = SaveSystem.Read();
        }
    }
}
