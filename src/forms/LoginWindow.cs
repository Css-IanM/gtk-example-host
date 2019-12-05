using System;
using Gtk;
using Microsoft.Extensions.Logging;
using UI = Gtk.Builder.ObjectAttribute;

namespace Jupiter
{
    public class LoginWindow : Window
    {
        #region UI References
        [UI] private Button btnLogin = null;
        [UI] private Button btnCancel = null;
        [UI] private Button btnConfig = null;

        [UI] private Entry entryUsername = null;
        [UI] private Entry entryPassword = null;
        #endregion

        private readonly ILogger<LoginWindow> logger;

        public LoginWindow(
            ILogger<LoginWindow> logger
        ) : this(new Builder("LoginWindow.ui"), logger) { }

        private LoginWindow(
            Builder builder,
            ILogger<LoginWindow> logger
            ) : base(builder.GetObject("LoginWindow").Handle)
        {
            // Initialization
            builder.Autoconnect(this);
            this.logger = logger;

            bindEventsWindow();
            bindEventsUi();
        }

        private void bindEventsWindow()
        {
            Destroyed += (s, e) => Application.Quit();
        }

        private void bindEventsUi()
        {
            entryUsername.Activated += (s, e) => entryPassword.GrabFocus();
            entryPassword.Activated += entry_Password_Activated;
            entryUsername.Focused += (s, e) => entryUsername.SelectRegion(0, -1);
            entryPassword.Focused += (s, e) => entryPassword.SelectRegion(0, -1);
            btnCancel.Clicked += button_Cancel_Clicked;
            btnLogin.Clicked += button_Login_Clicked;
            btnConfig.Clicked += button_Config_Clicked;
        }

        private void entry_Password_Activated(object sender, EventArgs e)
        {
            // TODO: Authentication method for both login events (Activated and Clicked)
            Console.WriteLine("Password Activated");
        }

        private void button_Login_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("User:" + entryUsername.Buffer.Text);
            Console.WriteLine("Pass:" + entryPassword.Buffer.Text);
        }

        private void button_Config_Clicked(object sender, EventArgs e)
        {
            var confDialog = new ConfigurationDialog
            {
                TransientFor = this,
            };
            confDialog.Run();
            confDialog.Hide();
        }

        private void button_Cancel_Clicked(object sender, EventArgs e)
        {
            this.Destroy();
            this.Dispose();
        }

    }
}
