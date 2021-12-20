﻿using System;
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
using Cliente.ChessService;
using Cliente.Properties.Langs;

namespace Cliente
{
    /// <summary>
    /// Interaction logic for GetCodeMatch.xaml
    /// </summary>
    public partial class GetCodeMatch : Window, ISendInvitationServiceCallback
    {
        public SendInvitationServiceClient server;
        public int idUser;
        public string codeMatch;
        public GetCodeMatch(int idUser_)
        {
            InitializeComponent();
            idUser = idUser_;

            InstanceContext instanceContext = new InstanceContext(this);
            server = new SendInvitationServiceClient(instanceContext);
            try
            {
                server.GenerateCodeInvitation(idUser);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Lang.noConecction);
                Connected.IsConnected = false;
                this.Close();
            }
            
        }

        public void JoinMatch(string usernameRival, string username, string codeMatch, bool white)
        {
            Play play = new Play(idUser, usernameRival, username, codeMatch, white);
            play.Show();
            this.Close();
        }

        public void ValidateCodeStatus(int status, string usernameRival, string username, string MatchCode, bool white)
        {
            throw new NotImplementedException();

        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                server.DeleteCodeInvitation(codeMatch);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Lang.noConecction);
                Connected.IsConnected = false;
                this.Close();

            }
            
            this.Close();
        }

        void ISendInvitationServiceCallback.GetCodeMatch(bool status, string code)
        {
            codeMatch = code;
            lbCode.Content = code;
        }
    }
}
