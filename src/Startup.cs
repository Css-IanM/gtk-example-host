using System;
using Gtk;

namespace Jupiter
{
    public class Startup
    {
        private LoginWindow _loginWindow;

        public Startup(LoginWindow loginWindow)
        {
            _loginWindow = loginWindow;
        }
        public void Run()
        {
            Application.Init();

            var app = new Application("org.Jupiter.Jupiter", GLib.ApplicationFlags.None);
            app.Register(GLib.Cancellable.Current);

            app.AddWindow(_loginWindow);
            //win.Show();
            _loginWindow.Show();
            _loginWindow.DeleteEvent += DeletedEvent;
            Application.Run();

        }

        private void DeletedEvent(object o, DeleteEventArgs args)
        {
            Application.Quit();
        }
    }
}