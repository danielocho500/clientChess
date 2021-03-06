/******************************************************************/
/* Archivo: Configuration.xaml.cs                                 */
/* Programador: Raul Arturo Peredo Estudillo                      */
/* Fecha: 26/Oct/2021                                             */
/* Fecha modificación:  29/Oct/2021                               */
/* Descripción: Cambia de idioma el cliente                       */
/******************************************************************/

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
        int idUser;

        /// <summary>
        /// Incia la ventana Configuracion
        /// </summary>
        /// <param name="idUser"> id del usuario solicitante</param>
        public Configuration(int idUser)
        {
            InitializeComponent();
            this.idUser = idUser;
        }

        /// <summary>
        /// Verifica la seleccion y actualiza el idioma usando un archivo de tipo resx.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadClick(object sender, RoutedEventArgs e)
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

        /// <summary>
        /// Identifica la seleccion dentro del combo box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
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

        /// <summary>
        /// Cierra la ventana.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseClick(object sender, RoutedEventArgs e)
        {
            MainChess mainChess = new MainChess(idUser);
            mainChess.Show();
            this.Close();
        }
    }
}
