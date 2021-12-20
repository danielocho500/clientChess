using Cliente.Properties.Langs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cliente
{
    /// <summary>
    /// Lógica de interacción para Configuration.xaml
    /// </summary>
    public partial class Configuration : Window
    {
        int seleccionLanguage = -1;
        int id;
        public Configuration(int id_)
        {
            InitializeComponent();
            id = id_;
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            if (seleccionLanguage == 0)
            {
                Properties.Settings.Default.languageCode = "en-US";
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
                this.InitializeComponent();
            }
            else
            {
                Properties.Settings.Default.languageCode = "es-MX";
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-MX");
                this.InitializeComponent();
            }
            Properties.Settings.Default.Save();
            MessageBox.Show(Lang.refreshedLang);
            
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbLanguage.SelectedIndex == 0)
            {
                seleccionLanguage = 0;
            }
            else
            {
                seleccionLanguage = 1;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            MainChess mainChess = new MainChess(id);
            mainChess.Show();
            this.Close();
        }
    }
}
