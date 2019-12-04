using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace Jupiter
{
    class LoginWindow : Window
    {
        #region UI References
        [UI] private Button _btnLogin = null;
        [UI] private Button _btnCancel = null;
        [UI] private Button _btnConfig = null;

        [UI] private Entry _entryUsername = null;
        [UI] private Entry _entryPassword = null;
        #endregion

        public LoginWindow() : this(new Builder("LoginWindow.ui")) { }

        private LoginWindow(Builder builder) :
            base(builder.GetObject("LoginWindow").Handle)
        {
            builder.Autoconnect(this);

            // Window Event bindings
            Destroyed += (s, e) => Application.Quit();

            // UI Event Bindings
            _entryUsername.Activated += Entry_Username_Activated;
            _entryPassword.Activated += Entry_Password_Activated;
            _btnCancel.Clicked += Button_Cancel_Clicked;
            _btnLogin.Clicked += Button_Login_Clicked;
            _btnConfig.Clicked += Button_Config_Clicked;
        }

        private void Entry_Username_Activated(object sender, EventArgs e)
        {
            Console.WriteLine("Username Activated");
            _entryPassword.GrabFocus();
        }

        private void Entry_Password_Activated(object sender, EventArgs e)
        {
            Console.WriteLine("Password Activated");
        }

        private void Button_Login_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("User:" + _entryUsername.Buffer.Text);
            Console.WriteLine("Pass:" + _entryPassword.Buffer.Text);
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
