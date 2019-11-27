using System;
using Gtk;

namespace Jupiter
{
    public class Startup
    {
        public void Run()
        {
            Application.Init();

            var app = new Application("org.Jupiter.Jupiter", GLib.ApplicationFlags.None);
            app.Register(GLib.Cancellable.Current);

            var win = new MainWindow();
            var login = new LoginWindow();
            app.AddWindow(login);
            app.AddWindow(win);
            //win.Show();
            login.Show();
            login.DeleteEvent += DeletedEvent;
            Application.Run();

        }

        private void DeletedEvent(object o, DeleteEventArgs args)
        {
            Application.Quit();
        }
    }
}