/******************************************************************/
/* Archivo: Login.xaml.cs                                         */
/* Programador: Raul Peredo                                       */
/* Fecha: 18/Oct/2021                                             */
/* Fecha modificación:  22/Oct/2021                               */
/* Descripción: Ventana para iniciar sesion en el sistema         */
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
using System.Windows.Shapes;

namespace Cliente
{
    public partial class Login : Window, ILoginServiceCallback
    {
        public LoginServiceClient server;
        public Login()
        {
            InitializeComponent();
            InstanceContext instanceContext = new InstanceContext(this);
            server = new LoginServiceClient(instanceContext);
        }

        public void LoginStatus(bool status, string message, int idUser) 
        { 
            if (status == true)
            {
                MessageBox.Show(message);
                MainChess mainchess = new MainChess(idUser);
                mainchess.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show(message);
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsername.Text) && (!string.IsNullOrEmpty(pssPassword.Password)))
            {
                server.Login(txtUsername.Text, pssPassword.Password);
            }
            else
            {
                MessageBox.Show("Empty fields");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainw = new MainWindow();
            mainw.Show();
            this.Close();
        }
    }
}
