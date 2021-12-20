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
        public int IdUser;
        public string Rival;
        public string MatchCode;
        public bool IsWhite;
        public string UsernameActual;
        public MatchServiceClient server_match;

        ImageBrush pawnImage = new ImageBrush();

        public Partida(int idUser_, string username_, string rival_, string MatchCode_, bool white_)
        {
            InitializeComponent();

            InstanceContext instanceContext = new InstanceContext(this);
            server_match = new MatchServiceClient(instanceContext);

            IdUser = idUser_;
            Rival = rival_;
            MatchCode = MatchCode_;
            IsWhite = white_;
            UsernameActual = username_;

            string color = (IsWhite) ? "whites" : "Blacks";

            prueba.Content += " " + Rival + " you are "+color;

            server_match.sendConnection(IsWhite, MatchCode);


            SetUpGame();
            
        }

        private void SetUpGame()
        {
            BitmapImage btm = new BitmapImage(new Uri("Images/WhitePawn.png", UriKind.Relative));
            Image img = new Image();
            img.Source = btm;
            img.Stretch = Stretch.Fill;
            a1.Content = img;
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBMessage.Text.Trim()))
            {
                MessageBox.Show("You must write a message");
                return;
            }
            string message = TextBMessage.Text;
            listVMessages.Items.Add(GetHourFormat() + " " + UsernameActual + ": " + message);
            listVMessages.ScrollIntoView(listVMessages.Items.Count - 1);
            server_match.SendMessage(IsWhite, message, MatchCode);
            TextBMessage.Text = "";
        }

        private void BtnRendirse_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to Surrender?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                server_match.giveUp(IsWhite, MatchCode);
            }
        }

        public void ReciveMessage(string message,string time)
        {
            listVMessages.Items.Add(time+" "+Rival + ": " + message);
            listVMessages.ScrollIntoView(listVMessages.Items.Count - 1);
        }

        private string GetHourFormat()
        {
            var date = DateTime.Now;
            int hour = date.Hour;
            int minute = date.Minute;
            return "[" + hour + ":" + minute + "]";
        }

        public void MatchEnds(bool youWon, int oldElo, int newElo)
        {
            string msg = (youWon) ? "You WIN!!!!" : "You Lose :c";
            msg += "  Elo: " + oldElo + " -> " + newElo;
            MessageBox.Show(msg);
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
