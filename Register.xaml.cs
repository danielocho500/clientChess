using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
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

    public partial class Register : Window, ChessService.IRegisterServiceCallback
    {
        public ChessService.RegisterServiceClient server;
        public Register()
        {
            InitializeComponent();
            InstanceContext instanceContext = new InstanceContext(this);
            server = new ChessService.RegisterServiceClient(instanceContext);
        }
        private void Registrarse_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsername.Text) || !string.IsNullOrEmpty(txtEmail.Text) || !string.IsNullOrEmpty(pssPassword1.Password) || !string.IsNullOrEmpty(pssPassword2.Password))
            {
                if (Verificar_Contrasenas_Iguales() == true)
                {
                    if (Validar_Correo())
                    {
                        if (Contraseña_Segura(pssPassword1.Password))
                        {
                            bool generatedCode = server.generateCode(txtUsername.Text, pssPassword1.Password, txtEmail.Text);
                            if (generatedCode)
                            {
                                btnValidar.Visibility = Visibility.Visible;
                                lbCodigo.Visibility = Visibility.Visible;
                                txtCodigo.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                MessageBox.Show("Code was no generated");
                            }
                                
                        }
                    }
                }
                else
                {
                    if (Verificar_Contrasenas_Iguales() == false)
                    {
                        MessageBox.Show("Las contraseñas no coinciden");
                    }
                }
            }
            else
            {
                MessageBox.Show("Campos Faltantes");
            }
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Validar_Click(object sender, RoutedEventArgs e)
        {
            server.verificateCode(txtCodigo.Text);
        }

        //Validaciones de campos

        private bool Verificar_Contrasenas_Iguales()
        {
            string pass1 = pssPassword1.Password;
            string pass2 = pssPassword2.Password;
            if (pass1 == pass2)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private bool Validar_Correo()
        {
            Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.CultureInvariant | RegexOptions.Singleline);
            return regex.IsMatch(txtEmail.Text);
        }

        static bool Contraseña_Segura(string passWord)
        {
            int validConditions = 0;
            foreach (char c in passWord)
            {
                if (c >= 'a' && c <= 'z')
                {
                    validConditions++;
                    break;
                }
            }
            foreach (char c in passWord)
            {
                if (c >= 'A' && c <= 'Z')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 0) return false;
            foreach (char c in passWord)
            {
                if (c >= '0' && c <= '9')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 1) return false;
            if (validConditions == 2)
            {
                char[] special = { '@', '#', '$', '%', '^', '&', '+', '=' };  
                if (passWord.IndexOfAny(special) == -1) return false;
            }
            return true;
        }

        public void ValidateCode(bool codeStatus, string message)
        {
            MessageBox.Show(message);
            if (codeStatus)
                this.Close();
        }
    }
}
