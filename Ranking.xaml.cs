using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for Ranking.xaml
    /// </summary>
    public partial class Ranking : Window, ChessService.IRankingServiceCallback
    {
        public Ranking()
        {
            InitializeComponent();
        }

        public void ShowRanking(Tuple<string, int> rank)
        {
            var player = new { UserName = rank.Item1, Elo = rank.Item2 };
            lvRank.Items.Add(player);
        }
    }
}
