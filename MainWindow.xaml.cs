/******************************************************************/
/* Archivo: MainWindow.xaml.cs                                    */
/* Programador: Raul Peredo                                       */
/* Fecha: 18/Oct/2021                                             */
/* Fecha modificación:  22/Oct/2021                               */
/* Descripción: Ventana principal ponde se decide si quiere       */
/*              iniciar sesion o desea registrase                 */
/******************************************************************/



using Cliente.SuperChess;
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

    public partial class MainWindow : Window, IConnectionServiceCallback
    {

        public ConnectionServiceClient server;
        public MainWindow()
        {
            InitializeComponent();

            btnLogin.IsEnabled = false;
            btnRegister.IsEnabled = false;

            InstanceContext instanceContext = new InstanceContext(this);
            server = new ConnectionServiceClient(instanceContext);

            try
            {
                server.check();
            }
            catch (Exception e)
            {
                MessageBox.Show("No connection with the server");
                Application.Current.Shutdown();
                Console.WriteLine(e);
            }
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
        }

        private void Close_Click (object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void isConnected(bool status)
        {
            if (status)
            {
                btnLogin.IsEnabled = true;
                btnRegister.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("No connection with the server");
                Application.Current.Shutdown();
            }
        }
    }
}
