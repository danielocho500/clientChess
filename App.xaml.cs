using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Cliente
{
    /// <summary>
    /// Lógica de interacción para App.xaml.cs
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Para inicar archivos de lang cuando con el idioma especificado.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            var langCode = Cliente.Properties.Settings.Default.languageCode;
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(langCode);
            base.OnStartup(e);
        }
    }
}
