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

    public partial class Login : Window, ChessService.ILoginServiceCallback
    {
        public ChessService.LoginServiceClient server;
        public Login()
        {
            InitializeComponent();
            InstanceContext instanceContext = new InstanceContext(this);
            server = new ChessService.LoginServiceClient(instanceContext);
        }

        public void LoginStatus(bool status, string message)
        {
            MessageBox.Show(message);

        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsername.Text) && (!string.IsNullOrEmpty(pssPassword.Password)))
            {
                server.Login(txtUsername.Text, pssPassword.Password);
            }
            else
            {
                MessageBox.Show("Campos vacios");
                
            }
        }

        private void Regresar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainw = new MainWindow();
            mainw.Show();
            this.Close();
        }
    }
}
