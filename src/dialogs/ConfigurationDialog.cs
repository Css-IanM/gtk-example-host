using System;
using System.IO;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;


namespace Jupiter
{
    class ConfigurationDialog : Dialog
    {

        [UI] private Entry _entryConnectionString = null;

        public ConfigurationDialog() : this(new Builder("ConfigurationDialog.ui")) { }

        private ConfigurationDialog(Builder builder) : base(builder.GetObject("ConfigurationDialog").Handle)
        {
            builder.Autoconnect(this);
            DefaultResponse = ResponseType.Cancel;
            try
            {
                IConfiguration config = new ConfigurationBuilder()
                    .SetBasePath($"{Directory.GetCurrentDirectory()}\\config")
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connString = config.GetConnectionString("DefaultConnection");
                _entryConnectionString.Text = connString;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Cannot find appsettings.json file");
            }

            Response += Dialog_Response;
        }

        private void Dialog_Response(object o, ResponseArgs args)
        {
            Hide();
        }
    }
}
