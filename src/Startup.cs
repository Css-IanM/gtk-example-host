using System;
using Gtk;

namespace Jupiter
{
    public class Startup
    {
        private Application _app;
        private LoginWindow _loginWindow;

        public Startup(Application app, LoginWindow loginWindow)
        {
            _app = app;
            _loginWindow = loginWindow;
        }
        public void Start()
        {
            _app.Register(GLib.Cancellable.Current);
            _app.AddWindow(_loginWindow);
            _loginWindow.Show();
            _loginWindow.DeleteEvent += DeletedEvent;
        }

        private void DeletedEvent(object o, DeleteEventArgs args)
        {
            Application.Quit();
        }
    }
}