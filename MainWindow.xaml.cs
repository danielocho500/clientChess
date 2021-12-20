/******************************************************************/
/* Archivo: MainWindow.xaml.cs                                    */
/* Programador: Raul Peredo                                       */
/* Fecha: 18/Oct/2021                                             */
/* Fecha modificación:  18/Dic/2021                               */
/* Descripción: Ventana principal ponde se decide si quiere       */
/*              iniciar sesion o desea registrase                 */
/******************************************************************/




using Cliente.ChessService;
using Cliente.Properties.Langs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cliente
{
    /// <summary>
    /// Logica de interaccion para el archivo MainWindow.xaml.cs
    /// </summary>
    public partial class MainWindow : Window
    {
        public ConnectionServiceClient server;

        /// <summary>
        /// Inicia la ventana MainWindow.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Connected.is_Connected = false;
        }

        /// <summary>
        /// Incia la ventana Login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginClick (object sender, RoutedEventArgs e)
        {
            Login login =  new  Login();
            login.Show();
            this.Close();
        }

        /// <summary>
        /// Inicia la ventana Register
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Close();
        }

        /// <summary>
        /// Cierra la ventana MainWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseClick (object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
