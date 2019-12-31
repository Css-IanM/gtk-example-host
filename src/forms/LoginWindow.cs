using System;
using Gtk;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration config;

        public LoginWindow(
            ILogger<LoginWindow> logger,
            IConfiguration config
        ) :
        this(new Builder("LoginWindow.ui"),
            logger,
            config)
        { }

        private LoginWindow(
            Builder builder,
            ILogger<LoginWindow> logger,
            IConfiguration config
            ) : base(builder.GetObject("LoginWindow").Handle)
        {
            // Initialization
            builder.Autoconnect(this);
            this.logger = logger;
            this.config = config;
            bindEventsWindow();
            bindEventsUi();
            setControlState();
        }

        private void bindEventsWindow()
        {
            Destroyed += (s, e) => Application.Quit();
        }

        private void setControlState()
        {
            btnLogin.Sensitive = false;
        }

        private void bindEventsUi()
        {
            entryUsername.Activated += (s, e) => entryPassword.GrabFocus();
            entryPassword.Activated += entry_Password_Activated;
            entryUsername.Changed += validateEntry;
            entryPassword.Changed += validateEntry;
            entryUsername.Focused += (s, e) => entryUsername.SelectRegion(0, -1);
            entryPassword.Focused += (s, e) => entryPassword.SelectRegion(0, -1);
            btnCancel.Clicked += button_Cancel_Clicked;
            btnLogin.Clicked += button_Login_Clicked;
            btnConfig.Clicked += button_Config_Clicked;
        }

        private void validateEntry(object sender, EventArgs e)
        {
            if (entryUsername.Text.Length > 0 && entryPassword.Text.Length > 0)
                btnLogin.Sensitive = true;

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
            var confDialog = new ConfigurationDialog(config)
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
