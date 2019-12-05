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

            BindEventsWindow();
            BindEventsUi();
        }

        private void BindEventsWindow()
        {
            Destroyed += (s, e) => Application.Quit();
        }

        private void BindEventsUi()
        {
            entryUsername.Activated += Entry_Username_Activated;
            entryPassword.Activated += Entry_Password_Activated;
            entryUsername.Focused += (s, e) => entryUsername.SelectRegion(0, -1);
            entryPassword.Focused += (s, e) => entryPassword.SelectRegion(0, -1);
            btnCancel.Clicked += Button_Cancel_Clicked;
            btnLogin.Clicked += Button_Login_Clicked;
            btnConfig.Clicked += Button_Config_Clicked;
        }

        private void Entry_Username_Activated(object sender, EventArgs e)
        {
            entryPassword.GrabFocus();
        }

        private void Entry_Password_Activated(object sender, EventArgs e)
        {
            Console.WriteLine("Password Activated");
        }

        private void Button_Login_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("User:" + entryUsername.Buffer.Text);
            Console.WriteLine("Pass:" + entryPassword.Buffer.Text);
        }

        private void Button_Config_Clicked(object sender, EventArgs e)
        {
            var confDialog = new ConfigurationDialog
            {
                TransientFor = this,
            };
            confDialog.Run();
            confDialog.Hide();
        }

        private void Button_Cancel_Clicked(object sender, EventArgs e)
        {
            this.Destroy();
            this.Dispose();
        }

    }
}
