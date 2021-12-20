using Cliente.ChessService;
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
    /// <summary>
    /// Interaction logic for Partida.xaml
    /// </summary>
    /// 
    public partial class Partida : Window, IMatchServiceCallback
    {
        public int idUser;
        public string rival;
        public string MatchCode;
        public bool white;
        public MatchServiceClient server_match;

        public Partida(int idUser_, string rival_, string MatchCode_, bool white_)
        {
            InitializeComponent();

            InstanceContext instanceContext = new InstanceContext(this);
            server_match = new MatchServiceClient(instanceContext);

            idUser = idUser_;
            rival = rival_;
            MatchCode = MatchCode_;
            white = white_;

            string color = (white) ? "whites" : "Blacks";

            prueba.Content += " " + rival + " you are "+color;

            server_match.sendConnection(white, MatchCode);
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnRendirse_Click(object sender, RoutedEventArgs e)
        {

        }

        public void ReciveMessage(string message)
        {
            
        }
    }
}
