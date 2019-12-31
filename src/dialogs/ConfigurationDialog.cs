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

        public ConfigurationDialog(
            IConfiguration config
            ) : this(new Builder("ConfigurationDialog.ui"), config) { }

        private ConfigurationDialog(Builder builder, IConfiguration config) : base(builder.GetObject("ConfigurationDialog").Handle)
        {
            builder.Autoconnect(this);
            DefaultResponse = ResponseType.Cancel;
            Response += Dialog_Response;
        }

        private void Dialog_Response(object o, ResponseArgs args)
        {
            Hide();
        }
    }
}
