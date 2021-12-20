﻿/******************************************************************/
/* Archivo: Login.xaml.cs                                         */
/* Programador: Raul Peredo                                       */
/* Fecha: 18/Oct/2021                                             */
/* Fecha modificación:  22/Oct/2021                               */
/* Descripción: Ventana para iniciar sesion en el sistema         */
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
using System.Windows.Shapes;

namespace Cliente
{
    public partial class Login: Window, ILoginServiceCallback
    {
        public LoginServiceClient server;
        public Login()
        {
            InitializeComponent();
            /*InstanceContext instanceContext = new InstanceContext(this);
            server = new LoginServiceClient(instanceContext);*/
        }

        public void LoginStatus(int status, int idUser) 
        { 
            if (status == 0)
            {
                MainChess mainchess = new MainChess(idUser);
                mainchess.Show();
                this.Close();
            }
            else if (status == 1)
            {
                MessageBox.Show(Lang.errorLogin);
            }
            else if (status == 2)
            {
                MessageBox.Show(Lang.incorrectCredentials);
            }
            else if (status == 4)
            {
                MessageBox.Show(Lang.alreadyLogged);
            }

        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsername.Text) && (!string.IsNullOrEmpty(pssPassword.Password)))
            {
                //server.Login(txtUsername.Text, pssPassword.Password);
                // solo por de prueba 
                MainChess mainchess = new MainChess(1);
                mainchess.Show();
                this.Close();
                // solo por de prueba
            }
            else
            {
                MessageBox.Show(Lang.emptyFields);
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
