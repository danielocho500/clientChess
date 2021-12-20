/******************************************************************/
/* Archivo: Ranking.xaml.cs                                       */
/* Programador: Raul Arturo Peredo Estudillo                      */
/* Fecha: 15/Oct/2021                                             */
/* Fecha modificación:  15/Nov/2021                               */
/* Descripción: Ventana para mostrar loss estadisticas  de los    */
/*              mejores jugadores                                 */
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
    /// <summary>
    /// Logica de interaccion para el archivo Ranking.xaml.cs
    /// </summary>
    public partial class Ranking : Window, IRankingServiceCallback
    {
        public RankingServiceClient server;

        /// <summary>
        /// Inicializa la ventana y pide al servidor los datos del ranking
        /// </summary>
        /// <param name="idUser"> id del usuario solicitante </param>
        public Ranking(int idUser)
        {
            InitializeComponent();
            InstanceContext instanceContext = new InstanceContext(this);
            server = new RankingServiceClient(instanceContext);
            try
            {
                server.GetRanking(idUser);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Lang.noConecction);
                Connected.is_Connected = false;
                this.Close();
            }
        }

        /// <summary>
        /// Muestra los datos del ranking el una tabla, llamado por el servidor.
        /// </summary>
        /// <param name="rank"> contiene la informacion de los usuarios ordenado respecto al elo.</param>
        public void ShowRanking(Tuple<string, int>[] rank)
        {
            int position = 0;

            foreach (Tuple<string, int> element in rank)
            {
                position++;
                var player = new { UserName = element.Item1, Elo = element.Item2, place = position };
                lvRank.Items.Add(player);
            }
        }

        /// <summary>
        /// Cierra la ventana Ranking
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
