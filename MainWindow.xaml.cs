/******************************************************************/
/* Archivo: MainWindow.xaml.cs                                    */
/* Programador: Raul Peredo                                       */
/* Fecha: 18/Oct/2021                                             */
/* Fecha modificación:  13/Dic/2021                               */
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

    public partial class MainWindow : Window
    {

        public ConnectionServiceClient server;
        public MainWindow()
        {
            InitializeComponent();
            Connected.IsConnected = false;
        }

        private void Login_Click (object sender, RoutedEventArgs e)
        {
            Login login =  new  Login();
            login.Show();
            this.Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Close();
        }

        private void Close_Click (object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
