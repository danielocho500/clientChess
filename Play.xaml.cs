/******************************************************************/
/* Archivo: MainWindow.xaml.cs                                    */
/* Programador: Raul Arturo Peredo Estudillo                      */
/*              Daniel Diaz Rossell                               */
/* Fecha: 10/Nov/2021                                             */
/* Fecha modificación:  10/Dic/2021                               */
/* Descripción: Ventana del juego creacion de tablero,            */
/*              funcionalidad etc                                 */
/******************************************************************/


using Cliente.ChessService;
using Cliente.playLogic;
using Cliente.Properties.Langs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Cliente
{
    /// <summary>
    /// Logica de interaccion para el archivo Play.xaml.cs
    /// </summary>
    public partial class Play : Window, IMatchServiceCallback
    {
        public int idUser;
        public string rival;
        public string matchCode;
        public bool isWhite;
        public string usernameActual;

        public bool isTurn = true;
        public bool givenUp = false;

        public MatchServiceClient serverMatch;

        private Dictionary<string, ButtonInfo> squares = new Dictionary<string, ButtonInfo>();

        private string selectedSquare;
        private SquareStatus selectedSquareValue = SquareStatus.disabled;

        private SquareStatus[] whitePieces = new SquareStatus[] { SquareStatus.whitePawn, SquareStatus.whiteBishop, SquareStatus.whiteKing, SquareStatus.whiteQueen, SquareStatus.whiteKnight, SquareStatus.whiteTower };
        private SquareStatus[] blackPieces = new SquareStatus[] { SquareStatus.blackPawn, SquareStatus.blackBishop, SquareStatus.blackKing, SquareStatus.blackQueen, SquareStatus.blackKnight, SquareStatus.blackTower };
        private SquareStatus[] toDefeatPieces = new SquareStatus[] { SquareStatus.blackPawnToDefeat, SquareStatus.whitePawnToDefeat, SquareStatus.blackBishopToDefeat, SquareStatus.whiteBishopToDefeat, SquareStatus.blackKingToDefeat, SquareStatus.whiteKingToDefeat, SquareStatus.blackKnightToDefeat, SquareStatus.whiteKnightToDefeat, SquareStatus.blackQueenToDefeat, SquareStatus.whiteQueenToDefeat, SquareStatus.blackTowerToDefeat, SquareStatus.whiteTowerToDefeat };

        System.Windows.Threading.DispatcherTimer dispatcherTimer;
        private int timeLeftuser = 100;
        private int timeLeftRival = 100;

        /// <summary>
        /// Inicia la ventana con los datos de la partida.
        /// </summary>
        /// <param name="idUser"> id usuario</param>
        /// <param name="username"> nombre del usuario</param>
        /// <param name="rival"> usuario del oponente</param>
        /// <param name="MatchCode"> codigo de la partida</param>
        /// <param name="white"> si esta jugando blancas o negras</param>
        public Play(int idUser, string username, string rival, string MatchCode, bool isWhite)
        {
            InitializeComponent();
            InstanceContext instanceContext = new InstanceContext(this);
            serverMatch = new MatchServiceClient(instanceContext);

            this.idUser = idUser;
            this.rival = rival;
            this.matchCode = MatchCode;
            this.isWhite = isWhite;
            this.usernameActual = username;

            if (!isWhite)
            {
                imageYou.Margin = new Thickness(247, 44, 0, 0);
                imageRival.Margin = new Thickness(56, 44, 0, 0);
            }

            try
            {
                serverMatch.SendConnection(isWhite, matchCode);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Lang.noConecction);
                Connected.is_Connected = false;
                this.Close();
            }

            if (isWhite)
            {
                isTurn = true;
            }
            else
            {
                isTurn = false;
            }

            lbRivalName.Content = rival;

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(DispatcherTimerTick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();


            PrepareDictionary();
            Update();

        }

        private void DispatcherTimerTick(object sender, EventArgs e)
        {
            if (isTurn)
                timeLeftuser--;
            else
                timeLeftRival--;

            lbUserTime.Content = GetTimeLeftFormat(timeLeftuser);
            lbRivalTime.Content = GetTimeLeftFormat(timeLeftRival);

            if (timeLeftRival != 0 && timeLeftuser != 0)
                return;

            dispatcherTimer.Stop();

            if (timeLeftRival == 0)
            {
                try
                {
                    serverMatch.Win(!isWhite, true, matchCode);
                }
                catch (Exception)
                {
                    MessageBox.Show(Lang.noConecction);
                    Connected.is_Connected = false;
                    this.Close();
                }
            }

        }

        /// <summary>
        /// Convierte el tiempo en segundos a formato con minutos.
        /// </summary>
        /// <param name="secs"> segundos </param>
        /// <returns>el tiempo en formato con minutos format </returns>
        public string GetTimeLeftFormat(int secs)
        {
            string format;
            int minutes = 0;
            while (secs >= 60)
            {
                minutes++;
                secs -= 60;
            }

            format = "0" + minutes.ToString() + ":";
            format += (secs >= 10) ? secs.ToString() : "0" + secs.ToString();
            return format;
        }

        /// <summary>
        /// Evalua el campo tbMessage, la conexion y manda un mensaje a el otro usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbMessage.Text.Trim()))
            {
                MessageBox.Show(Lang.mustWrite);
                return;
            }

            string message = tbMessage.Text;
            lvMessages.Items.Add(GetHourFormat() + " " + usernameActual + ": " + message);
            lvMessages.ScrollIntoView(lvMessages.Items.Count - 1);

            try
            {
                serverMatch.SendMessage(isWhite, message, matchCode);
            }
            catch (Exception)
            {
                MessageBox.Show(Lang.noConecction);
                Connected.is_Connected = false;
                this.Close();
            }

            tbMessage.Text = "";
        }

        /// <summary>
        /// verifica conexion con el server y Manda al servidor peticion de rendicion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RendirseClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(Lang.surrender, Lang.confirm, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    serverMatch.GiveUp(isWhite, matchCode);
                }
                catch (Exception)
                {
                    MessageBox.Show(Lang.noConecction);
                    Connected.is_Connected = false;
                    this.Close();
                }
            }
        }

        /// <summary>
        /// Recive mensajes y escribe en el list view
        /// </summary>
        /// <param name="message"></param>
        /// <param name="time"></param>
        public void ReciveMessage(string message, string time)
        {
            lvMessages.Items.Add(time + " " + rival + ": " + message);
            lvMessages.ScrollIntoView(lvMessages.Items.Count - 1);
        }

        private string GetHourFormat()
        {
            var date = DateTime.Now;
            int hour = date.Hour;
            int minute = date.Minute;
            return "[" + hour + ":" + minute + "]";
        }

        /// <summary>
        /// Es llamada por el server e indica si el jugador gano o perdio la partida
        /// </summary>
        /// <param name="youWon"> si ganaste o no</param>
        /// <param name="oldElo"> elo anterior </param>
        /// <param name="newElo"> elo actual</param>
        public void MatchEnds(bool youWon, int oldElo, int newElo)
        {
            givenUp = true;
            string msg = (youWon) ? Lang.youWin : Lang.youLose;
            msg += "  Elo: " + oldElo + " -> " + newElo;
            MessageBox.Show(msg);
            this.Close();
        }

        /// <summary>
        /// Actualiza las imagenes de los botones segun su estado
        /// </summary>
        private void Update()
        {
            if (isTurn)
                lbTurn.Content = Lang.you;
            else
                lbTurn.Content = rival;
            foreach (string key in squares.Keys)
            {
                ButtonInfo buttonInfo = squares[key];
                Button button = buttonInfo.GetButton();

                switch (buttonInfo.GetSquareStatus())
                {
                    case SquareStatus.disabled:
                        Image imgDisable = new Image();
                        imgDisable.Source = new BitmapImage(new Uri("Images/Disable.png", UriKind.Relative));
                        imgDisable.Stretch = Stretch.Fill;
                        button.Content = imgDisable;
                        break;

                    case SquareStatus.toMoveWhite:
                        Image imgToMove = new Image();
                        imgToMove.Source = new BitmapImage(new Uri("Images/Knicks.png", UriKind.Relative));
                        imgToMove.Stretch = Stretch.Fill;
                        button.Content = imgToMove;
                        break;

                    case SquareStatus.toMoveBlack:
                        Image imgToMoveB = new Image();
                        imgToMoveB.Source = new BitmapImage(new Uri("Images/Celtics.png", UriKind.Relative));
                        imgToMoveB.Stretch = Stretch.Fill;
                        button.Content = imgToMoveB;
                        break;

                    case SquareStatus.whitePawn:
                        Image imgwhitePawn = new Image();
                        imgwhitePawn.Source = new BitmapImage(new Uri("Images/whitePawn.png", UriKind.Relative));
                        imgwhitePawn.Stretch = Stretch.Fill;
                        button.Content = imgwhitePawn;
                        break;

                    case SquareStatus.whitePawnToDefeat:
                        Image imgwhitePawnToDefeat = new Image();
                        imgwhitePawnToDefeat.Source = new BitmapImage(new Uri("Images/whitePawnToDefeat.png", UriKind.Relative));
                        imgwhitePawnToDefeat.Stretch = Stretch.Fill;
                        button.Content = imgwhitePawnToDefeat;
                        break;

                    case SquareStatus.blackPawn:
                        Image imgblackPawn = new Image();
                        imgblackPawn.Source = new BitmapImage(new Uri("Images/blackPawn.png", UriKind.Relative));
                        imgblackPawn.Stretch = Stretch.Fill;
                        button.Content = imgblackPawn;
                        break;

                    case SquareStatus.blackPawnToDefeat:
                        Image imgblackPawnToDefeat = new Image();
                        imgblackPawnToDefeat.Source = new BitmapImage(new Uri("Images/blackPawnToDefeat.png", UriKind.Relative));
                        imgblackPawnToDefeat.Stretch = Stretch.Fill;
                        button.Content = imgblackPawnToDefeat;
                        break;

                    case SquareStatus.whiteKing:
                        Image imgwhiteKing = new Image();
                        imgwhiteKing.Source = new BitmapImage(new Uri("Images/whiteKing.png", UriKind.Relative));
                        imgwhiteKing.Stretch = Stretch.Fill;
                        button.Content = imgwhiteKing;
                        break;

                    case SquareStatus.whiteKingToDefeat:
                        Image imgwhiteKingToDefeat = new Image();
                        imgwhiteKingToDefeat.Source = new BitmapImage(new Uri("Images/whiteKingToDefeat.png", UriKind.Relative));
                        imgwhiteKingToDefeat.Stretch = Stretch.Fill;
                        button.Content = imgwhiteKingToDefeat;
                        break;

                    case SquareStatus.blackKing:
                        Image imgblackKing = new Image();
                        imgblackKing.Source = new BitmapImage(new Uri("Images/blackKing.png", UriKind.Relative));
                        imgblackKing.Stretch = Stretch.Fill;
                        button.Content = imgblackKing;
                        break;

                    case SquareStatus.blackKingToDefeat:
                        Image imgblackKingToDefeat = new Image();
                        imgblackKingToDefeat.Source = new BitmapImage(new Uri("Images/blackKingToDefeat.png", UriKind.Relative));
                        imgblackKingToDefeat.Stretch = Stretch.Fill;
                        button.Content = imgblackKingToDefeat;
                        break;

                    case SquareStatus.whiteKnight:
                        Image imgwhiteKnight = new Image();
                        imgwhiteKnight.Source = new BitmapImage(new Uri("Images/whiteKnight.png", UriKind.Relative));
                        imgwhiteKnight.Stretch = Stretch.Fill;
                        button.Content = imgwhiteKnight;
                        break;

                    case SquareStatus.whiteKnightToDefeat:
                        Image imgwhiteKnightToDefeat = new Image();
                        imgwhiteKnightToDefeat.Source = new BitmapImage(new Uri("Images/whiteKnightToDefeat.png", UriKind.Relative));
                        imgwhiteKnightToDefeat.Stretch = Stretch.Fill;
                        button.Content = imgwhiteKnightToDefeat;
                        break;

                    case SquareStatus.blackKnight:
                        Image imgblackKnightg = new Image();
                        imgblackKnightg.Source = new BitmapImage(new Uri("Images/blackKnight.png", UriKind.Relative));
                        imgblackKnightg.Stretch = Stretch.Fill;
                        button.Content = imgblackKnightg;
                        break;

                    case SquareStatus.blackKnightToDefeat:
                        Image imgblackKnightToDefeat = new Image();
                        imgblackKnightToDefeat.Source = new BitmapImage(new Uri("Images/blackKnightToDefeat.png", UriKind.Relative));
                        imgblackKnightToDefeat.Stretch = Stretch.Fill;
                        button.Content = imgblackKnightToDefeat;
                        break;

                    case SquareStatus.whiteTower:
                        Image imgwhiteTower = new Image();
                        imgwhiteTower.Source = new BitmapImage(new Uri("Images/whiteTower.png", UriKind.Relative));
                        imgwhiteTower.Stretch = Stretch.Fill;
                        button.Content = imgwhiteTower;
                        break;

                    case SquareStatus.whiteTowerToDefeat:
                        Image imgwhiteTowerToDefeat = new Image();
                        imgwhiteTowerToDefeat.Source = new BitmapImage(new Uri("Images/whiteTowerToDefeat.png", UriKind.Relative));
                        imgwhiteTowerToDefeat.Stretch = Stretch.Fill;
                        button.Content = imgwhiteTowerToDefeat;
                        break;

                    case SquareStatus.blackTower:
                        Image imgblackTower = new Image();
                        imgblackTower.Source = new BitmapImage(new Uri("Images/blackTower.png", UriKind.Relative));
                        imgblackTower.Stretch = Stretch.Fill;
                        button.Content = imgblackTower;
                        break;

                    case SquareStatus.blackTowerToDefeat:
                        Image imgblackTowerToDefeat = new Image();
                        imgblackTowerToDefeat.Source = new BitmapImage(new Uri("Images/blackTowerToDefeat.png", UriKind.Relative));
                        imgblackTowerToDefeat.Stretch = Stretch.Fill;
                        button.Content = imgblackTowerToDefeat;
                        break;

                    case SquareStatus.whiteBishop:
                        Image imgwhiteBishop = new Image();
                        imgwhiteBishop.Source = new BitmapImage(new Uri("Images/whiteBishop.png", UriKind.Relative));
                        imgwhiteBishop.Stretch = Stretch.Fill;
                        button.Content = imgwhiteBishop;
                        break;

                    case SquareStatus.whiteBishopToDefeat:
                        Image imgwhiteBishopToDefeat = new Image();
                        imgwhiteBishopToDefeat.Source = new BitmapImage(new Uri("Images/whiteBishopToDefeat.png", UriKind.Relative));
                        imgwhiteBishopToDefeat.Stretch = Stretch.Fill;
                        button.Content = imgwhiteBishopToDefeat;
                        break;

                    case SquareStatus.blackBishop:
                        Image imgblackBishop = new Image();
                        imgblackBishop.Source = new BitmapImage(new Uri("Images/blackBishop.png", UriKind.Relative));
                        imgblackBishop.Stretch = Stretch.Fill;
                        button.Content = imgblackBishop;
                        break;

                    case SquareStatus.blackBishopToDefeat:
                        Image imgblackBishopToDefeat = new Image();
                        imgblackBishopToDefeat.Source = new BitmapImage(new Uri("Images/blackBishopToDefeat.png", UriKind.Relative));
                        imgblackBishopToDefeat.Stretch = Stretch.Fill;
                        button.Content = imgblackBishopToDefeat;
                        break;

                    case SquareStatus.whiteQueen:
                        Image imgwhiteQueen = new Image();
                        imgwhiteQueen.Source = new BitmapImage(new Uri("Images/whiteQueen.png", UriKind.Relative));
                        imgwhiteQueen.Stretch = Stretch.Fill;
                        button.Content = imgwhiteQueen;
                        break;

                    case SquareStatus.whiteQueenToDefeat:
                        Image imgwhiteQueenToDefeat = new Image();
                        imgwhiteQueenToDefeat.Source = new BitmapImage(new Uri("Images/whiteQueenToDefeat.png", UriKind.Relative));
                        imgwhiteQueenToDefeat.Stretch = Stretch.Fill;
                        button.Content = imgwhiteQueenToDefeat;
                        break;

                    case SquareStatus.blackQueen:
                        Image imgblackQueen = new Image();
                        imgblackQueen.Source = new BitmapImage(new Uri("Images/blackQueen.png", UriKind.Relative));
                        imgblackQueen.Stretch = Stretch.Fill;
                        button.Content = imgblackQueen;
                        break;

                    case SquareStatus.blackQueenToDefeat:
                        Image imgblackQueenToDefeat = new Image();
                        imgblackQueenToDefeat.Source = new BitmapImage(new Uri("Images/blackQueenToDefeat.png", UriKind.Relative));
                        imgblackQueenToDefeat.Stretch = Stretch.Fill;
                        button.Content = imgblackQueenToDefeat;
                        break;
                }
            }
        }

        /// <summary>
        /// checa el estado del boton para realizar una accion, verifica conexion, ver a que boton cliqueaste y en base a eso realiza un accion
        /// </summary>
        /// <param name="button"> cordenada del boton</param>
        private void Clic(string button)
        {
            if (!isTurn)
            {
                return;
            }

            ButtonInfo buttonInfo = squares[button];
            SquareStatus buttonStatus = buttonInfo.GetSquareStatus();

            if (isWhite && blackPieces.Contains(buttonStatus))
            {
                return;
            }

            if (!isWhite && whitePieces.Contains(buttonStatus))
            {
                return;
            }

            if (buttonStatus == SquareStatus.toMoveWhite || buttonStatus == SquareStatus.toMoveBlack || toDefeatPieces.Contains(buttonStatus))
            {
                try
                {
                    serverMatch.Move(isWhite, matchCode, selectedSquare, button, timeLeftuser);
                }
                catch (Exception)
                {
                    MessageBox.Show(Lang.noConecction);
                    Connected.is_Connected = false;
                    this.Close();
                }

                isTurn = false;
                squares[selectedSquare].SetSquareStatus(SquareStatus.disabled);
                squares[button].SetSquareStatus(selectedSquareValue);
                selectedSquareValue = SquareStatus.disabled;
                selectedSquare = "";
                DisableOptionsToMove();
                Update();
                return;
            }

            selectedSquareValue = SquareStatus.disabled;
            selectedSquare = "";
            DisableOptionsToMove();

            if (buttonInfo.GetSquareStatus() == SquareStatus.disabled)
            {
                return;
            }

            selectedSquare = button;
            List<string> btnMove = new List<string>();

            switch (buttonStatus)
            {
                case SquareStatus.whitePawn:
                    btnMove = Moves.GetMovesWhitePawn(buttonInfo.GetColumn(), buttonInfo.GetRow(), squares);
                    selectedSquareValue = SquareStatus.whitePawn;
                    break;
                case SquareStatus.blackPawn:
                    btnMove = Moves.GetMovesBlackPawn(buttonInfo.GetColumn(), buttonInfo.GetRow(), squares);
                    selectedSquareValue = SquareStatus.blackPawn;
                    break;
                case SquareStatus.whiteKing:
                    btnMove = Moves.GetMovesKing(true, buttonInfo.GetColumn(), buttonInfo.GetRow(), squares);
                    selectedSquareValue = SquareStatus.whiteKing;
                    break;
                case SquareStatus.blackKing:
                    btnMove = Moves.GetMovesKing(false, buttonInfo.GetColumn(), buttonInfo.GetRow(), squares);
                    selectedSquareValue = SquareStatus.blackKing;
                    break;
                case SquareStatus.whiteKnight:
                    btnMove = Moves.GetMovesKnight(true, buttonInfo.GetColumn(), buttonInfo.GetRow(), squares);
                    selectedSquareValue = SquareStatus.whiteKnight;
                    break;
                case SquareStatus.blackKnight:
                    btnMove = Moves.GetMovesKnight(false, buttonInfo.GetColumn(), buttonInfo.GetRow(), squares);
                    selectedSquareValue = SquareStatus.blackKnight;
                    break;
                case SquareStatus.whiteTower:
                    btnMove = Moves.GetMovesTower(true, buttonInfo.GetColumn(), buttonInfo.GetRow(), squares);
                    selectedSquareValue = SquareStatus.whiteTower;
                    break;
                case SquareStatus.blackTower:
                    btnMove = Moves.GetMovesTower(false, buttonInfo.GetColumn(), buttonInfo.GetRow(), squares);
                    selectedSquareValue = SquareStatus.blackTower;
                    break;
                case SquareStatus.whiteBishop:
                    btnMove = Moves.GetMovesBishop(true, buttonInfo.GetColumn(), buttonInfo.GetRow(), squares);
                    selectedSquareValue = SquareStatus.whiteBishop;
                    break;
                case SquareStatus.blackBishop:
                    btnMove = Moves.GetMovesBishop(false, buttonInfo.GetColumn(), buttonInfo.GetRow(), squares);
                    selectedSquareValue = SquareStatus.blackBishop;
                    break;
                case SquareStatus.whiteQueen:
                    btnMove = Moves.GetMovesQueen(true, buttonInfo.GetColumn(), buttonInfo.GetRow(), squares);
                    selectedSquareValue = SquareStatus.whiteQueen;
                    break;
                case SquareStatus.blackQueen:
                    btnMove = Moves.GetMovesQueen(false, buttonInfo.GetColumn(), buttonInfo.GetRow(), squares);
                    selectedSquareValue = SquareStatus.blackQueen;
                    break;
            }

            SetOptionMoves(btnMove);
        }

        /// <summary>
        /// Cambia las imagende los botones indicando que es posible moverse a ellas.
        /// </summary>
        /// <param name="btnMove"></param>
        public void SetOptionMoves(List<string> btnMove)
        {
            string moveColor = blackPieces.Contains(selectedSquareValue) ? Lang.black : Lang.white;
            foreach (string btn in btnMove)
            {
                if (squares[btn].GetSquareStatus() == SquareStatus.disabled)
                {
                    if (moveColor == Lang.black)
                        squares[btn].SetSquareStatus(SquareStatus.toMoveBlack);
                    else
                        squares[btn].SetSquareStatus(SquareStatus.toMoveWhite);
                }
                else
                {
                    switch (squares[btn].GetSquareStatus())
                    {
                        case SquareStatus.blackPawn:
                            squares[btn].SetSquareStatus(SquareStatus.blackPawnToDefeat);
                            break;
                        case SquareStatus.whitePawn:
                            squares[btn].SetSquareStatus(SquareStatus.whitePawnToDefeat);
                            break;
                        case SquareStatus.blackKing:
                            squares[btn].SetSquareStatus(SquareStatus.blackKingToDefeat);
                            break;
                        case SquareStatus.whiteKing:
                            squares[btn].SetSquareStatus(SquareStatus.whiteKingToDefeat);
                            break;
                        case SquareStatus.whiteKnight:
                            squares[btn].SetSquareStatus(SquareStatus.whiteKnightToDefeat);
                            break;
                        case SquareStatus.blackKnight:
                            squares[btn].SetSquareStatus(SquareStatus.blackKnightToDefeat);
                            break;
                        case SquareStatus.whiteTower:
                            squares[btn].SetSquareStatus(SquareStatus.whiteTowerToDefeat);
                            break;
                        case SquareStatus.blackTower:
                            squares[btn].SetSquareStatus(SquareStatus.blackTowerToDefeat);
                            break;
                        case SquareStatus.whiteBishop:
                            squares[btn].SetSquareStatus(SquareStatus.whiteBishopToDefeat);
                            break;
                        case SquareStatus.blackBishop:
                            squares[btn].SetSquareStatus(SquareStatus.blackBishopToDefeat);
                            break;
                        case SquareStatus.whiteQueen:
                            squares[btn].SetSquareStatus(SquareStatus.whiteQueenToDefeat);
                            break;
                        case SquareStatus.blackQueen:
                            squares[btn].SetSquareStatus(SquareStatus.blackQueenToDefeat);
                            break;
                    }
                }
            }
            Update();
        }

        /// <summary>
        /// Cierra la ventana, termina la partida y elimna la conexion.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!givenUp)
            {
                try
                {
                    serverMatch.Win(isWhite, false, matchCode);
                }
                catch (Exception)
                {
                    MessageBox.Show(Lang.noConecction);
                    Connected.is_Connected = false;
                    try
                    {
                        this.Close();
                    }
                    catch (Exception)
                    {
                    }
                }
            }

        }


        /// <summary>
        /// Lo llama el servidor e indica un  movimiento del enemigo
        /// </summary>
        /// <param name="previousPosition"> posicion previa </param>
        /// <param name="newPosition"> entrega Nueva posicion</param>
        /// <param name="newTime"> nuevo tiempo de juego</param>
        public void MovePiece(string previousPosition, string newPosition, int newTime)
        {
            if ((isWhite && squares[newPosition].GetSquareStatus() == SquareStatus.whiteKing) || (!isWhite && squares[newPosition].GetSquareStatus() == SquareStatus.blackKing))
            {
                try
                {
                    serverMatch.Win(isWhite, false, matchCode);
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show(Lang.noConecction);
                    Connected.is_Connected = false;
                    this.Close();
                }
            }


            timeLeftRival = newTime;

            isTurn = true;
            SquareStatus sqAuxiliar = squares[previousPosition].GetSquareStatus();
            squares[previousPosition].SetSquareStatus(SquareStatus.disabled);

            squares[newPosition].SetSquareStatus(sqAuxiliar);

            Update();
        }

        /// <summary>
        /// Crear el diccionario de los botones
        /// </summary>
        public void PrepareDictionary()
        {
            squares["a1"] = new ButtonInfo(SquareStatus.whiteTower, 1, 1, a1);
            squares["a2"] = new ButtonInfo(SquareStatus.whitePawn, 1, 2, a2);
            squares["a3"] = new ButtonInfo(SquareStatus.disabled, 1, 3, a3);
            squares["a4"] = new ButtonInfo(SquareStatus.disabled, 1, 4, a4);
            squares["a5"] = new ButtonInfo(SquareStatus.disabled, 1, 5, a5);
            squares["a6"] = new ButtonInfo(SquareStatus.disabled, 1, 6, a6);
            squares["a7"] = new ButtonInfo(SquareStatus.blackPawn, 1, 7, a7);
            squares["a8"] = new ButtonInfo(SquareStatus.blackTower, 1, 8, a8);

            squares["b1"] = new ButtonInfo(SquareStatus.whiteKnight, 2, 1, b1);
            squares["b2"] = new ButtonInfo(SquareStatus.whitePawn, 2, 2, b2);
            squares["b3"] = new ButtonInfo(SquareStatus.disabled, 2, 3, b3);
            squares["b4"] = new ButtonInfo(SquareStatus.disabled, 2, 4, b4);
            squares["b5"] = new ButtonInfo(SquareStatus.disabled, 2, 5, b5);
            squares["b6"] = new ButtonInfo(SquareStatus.disabled, 2, 6, b6);
            squares["b7"] = new ButtonInfo(SquareStatus.blackPawn, 2, 7, b7);
            squares["b8"] = new ButtonInfo(SquareStatus.blackKnight, 2, 8, b8);

            squares["c1"] = new ButtonInfo(SquareStatus.whiteBishop, 3, 1, c1);
            squares["c2"] = new ButtonInfo(SquareStatus.whitePawn, 3, 2, c2);
            squares["c3"] = new ButtonInfo(SquareStatus.disabled, 3, 3, c3);
            squares["c4"] = new ButtonInfo(SquareStatus.disabled, 3, 4, c4);
            squares["c5"] = new ButtonInfo(SquareStatus.disabled, 3, 5, c5);
            squares["c6"] = new ButtonInfo(SquareStatus.disabled, 3, 6, c6);
            squares["c7"] = new ButtonInfo(SquareStatus.blackPawn, 3, 7, c7);
            squares["c8"] = new ButtonInfo(SquareStatus.blackBishop, 3, 8, c8);

            squares["d1"] = new ButtonInfo(SquareStatus.whiteQueen, 4, 1, d1);
            squares["d2"] = new ButtonInfo(SquareStatus.whitePawn, 4, 2, d2);
            squares["d3"] = new ButtonInfo(SquareStatus.disabled, 4, 3, d3);
            squares["d4"] = new ButtonInfo(SquareStatus.disabled, 4, 4, d4);
            squares["d5"] = new ButtonInfo(SquareStatus.disabled, 4, 5, d5);
            squares["d6"] = new ButtonInfo(SquareStatus.disabled, 4, 6, d6);
            squares["d7"] = new ButtonInfo(SquareStatus.blackPawn, 4, 7, d7);
            squares["d8"] = new ButtonInfo(SquareStatus.blackQueen, 4, 8, d8);

            squares["e1"] = new ButtonInfo(SquareStatus.whiteKing, 5, 1, e1);
            squares["e2"] = new ButtonInfo(SquareStatus.whitePawn, 5, 2, e2);
            squares["e3"] = new ButtonInfo(SquareStatus.disabled, 5, 3, e3);
            squares["e4"] = new ButtonInfo(SquareStatus.disabled, 5, 4, e4);
            squares["e5"] = new ButtonInfo(SquareStatus.disabled, 5, 5, e5);
            squares["e6"] = new ButtonInfo(SquareStatus.disabled, 5, 6, e6);
            squares["e7"] = new ButtonInfo(SquareStatus.blackPawn, 5, 7, e7);
            squares["e8"] = new ButtonInfo(SquareStatus.blackKing, 5, 8, e8);

            squares["f1"] = new ButtonInfo(SquareStatus.whiteBishop, 6, 1, f1);
            squares["f2"] = new ButtonInfo(SquareStatus.whitePawn, 6, 2, f2);
            squares["f3"] = new ButtonInfo(SquareStatus.disabled, 6, 3, f3);
            squares["f4"] = new ButtonInfo(SquareStatus.disabled, 6, 4, f4);
            squares["f5"] = new ButtonInfo(SquareStatus.disabled, 6, 5, f5);
            squares["f6"] = new ButtonInfo(SquareStatus.disabled, 6, 6, f6);
            squares["f7"] = new ButtonInfo(SquareStatus.blackPawn, 6, 7, f7);
            squares["f8"] = new ButtonInfo(SquareStatus.blackBishop, 6, 8, f8);

            squares["g1"] = new ButtonInfo(SquareStatus.whiteKnight, 7, 1, g1);
            squares["g2"] = new ButtonInfo(SquareStatus.whitePawn, 7, 2, g2);
            squares["g3"] = new ButtonInfo(SquareStatus.disabled, 7, 3, g3);
            squares["g4"] = new ButtonInfo(SquareStatus.disabled, 7, 4, g4);
            squares["g5"] = new ButtonInfo(SquareStatus.disabled, 7, 5, g5);
            squares["g6"] = new ButtonInfo(SquareStatus.disabled, 7, 6, g6);
            squares["g7"] = new ButtonInfo(SquareStatus.blackPawn, 7, 7, g7);
            squares["g8"] = new ButtonInfo(SquareStatus.blackKnight, 7, 8, g8);

            squares["h1"] = new ButtonInfo(SquareStatus.whiteTower, 8, 1, h1);
            squares["h2"] = new ButtonInfo(SquareStatus.whitePawn, 8, 2, h2);
            squares["h3"] = new ButtonInfo(SquareStatus.disabled, 8, 3, h3);
            squares["h4"] = new ButtonInfo(SquareStatus.disabled, 8, 4, h4);
            squares["h5"] = new ButtonInfo(SquareStatus.disabled, 8, 5, h5);
            squares["h6"] = new ButtonInfo(SquareStatus.disabled, 8, 6, h6);
            squares["h7"] = new ButtonInfo(SquareStatus.blackPawn, 8, 7, h7);
            squares["h8"] = new ButtonInfo(SquareStatus.blackTower, 8, 8, h8);
        }

        /// <summary>
        /// Desactiva botones a los que te puedes mover
        /// </summary>
        public void DisableOptionsToMove()
        {
            foreach (string key in squares.Keys)
            {
                SquareStatus buttonStatus = squares[key].GetSquareStatus();
                if (buttonStatus == SquareStatus.toMoveWhite || buttonStatus == SquareStatus.toMoveBlack)
                {
                    squares[key].SetSquareStatus(SquareStatus.disabled);
                }

                else if (toDefeatPieces.Contains(buttonStatus))
                {
                    switch (buttonStatus)
                    {
                        case SquareStatus.blackPawnToDefeat:
                            squares[key].SetSquareStatus(SquareStatus.blackPawn);
                            break;
                        case SquareStatus.whitePawnToDefeat:
                            squares[key].SetSquareStatus(SquareStatus.whitePawn);
                            break;
                        case SquareStatus.blackKingToDefeat:
                            squares[key].SetSquareStatus(SquareStatus.blackKing);
                            break;
                        case SquareStatus.whiteKingToDefeat:
                            squares[key].SetSquareStatus(SquareStatus.whiteKing);
                            break;
                        case SquareStatus.blackKnightToDefeat:
                            squares[key].SetSquareStatus(SquareStatus.blackKnight);
                            break;
                        case SquareStatus.whiteKnightToDefeat:
                            squares[key].SetSquareStatus(SquareStatus.whiteKnight);
                            break;
                        case SquareStatus.blackBishopToDefeat:
                            squares[key].SetSquareStatus(SquareStatus.blackBishop);
                            break;
                        case SquareStatus.whiteBishopToDefeat:
                            squares[key].SetSquareStatus(SquareStatus.whiteBishop);
                            break;
                        case SquareStatus.blackQueenToDefeat:
                            squares[key].SetSquareStatus(SquareStatus.blackQueen);
                            break;
                        case SquareStatus.whiteQueenToDefeat:
                            squares[key].SetSquareStatus(SquareStatus.whiteQueen);
                            break;
                    }
                }

            }

            Update();
        }

        private void a8Click(object sender, RoutedEventArgs e)
        {
            Clic("a8");
        }

        private void b8Click(object sender, RoutedEventArgs e)
        {
            Clic("b8");
        }

        private void c8Click(object sender, RoutedEventArgs e)
        {
            Clic("c8");
        }

        private void d8Click(object sender, RoutedEventArgs e)
        {
            Clic("d8");
        }

        private void e8Click(object sender, RoutedEventArgs e)
        {
            Clic("e8");
        }

        private void f8Click(object sender, RoutedEventArgs e)
        {
            Clic("f8");
        }

        private void g8Click(object sender, RoutedEventArgs e)
        {
            Clic("g8");
        }

        private void h8Click(object sender, RoutedEventArgs e)
        {
            Clic("h8");
        }

        private void a7Click(object sender, RoutedEventArgs e)
        {
            Clic("a7");
        }

        private void b7Click(object sender, RoutedEventArgs e)
        {
            Clic("b7");
        }

        private void c7Click(object sender, RoutedEventArgs e)
        {
            Clic("c7");
        }

        private void d7Click(object sender, RoutedEventArgs e)
        {
            Clic("d7");
        }

        private void e7Click(object sender, RoutedEventArgs e)
        {
            Clic("e7");
        }

        private void f7Click(object sender, RoutedEventArgs e)
        {
            Clic("f7");
        }

        private void g7Click(object sender, RoutedEventArgs e)
        {
            Clic("g7");
        }

        private void h7Click(object sender, RoutedEventArgs e)
        {
            Clic("h7");
        }

        private void a6Click(object sender, RoutedEventArgs e)
        {
            Clic("a6");
        }

        private void b6Click(object sender, RoutedEventArgs e)
        {
            Clic("b6");
        }

        private void c6Click(object sender, RoutedEventArgs e)
        {
            Clic("c6");
        }

        private void d6Click(object sender, RoutedEventArgs e)
        {
            Clic("d6");
        }

        private void e6Click(object sender, RoutedEventArgs e)
        {
            Clic("e6");
        }

        private void f6Click(object sender, RoutedEventArgs e)
        {
            Clic("f6");
        }

        private void g6Click(object sender, RoutedEventArgs e)
        {
            Clic("g6");
        }

        private void h6Click(object sender, RoutedEventArgs e)
        {
            Clic("h6");
        }

        private void a5Click(object sender, RoutedEventArgs e)
        {
            Clic("a5");
        }

        private void b5Click(object sender, RoutedEventArgs e)
        {
            Clic("b5");
        }

        private void c5Click(object sender, RoutedEventArgs e)
        {
            Clic("c5");
        }

        private void d5Click(object sender, RoutedEventArgs e)
        {
            Clic("d5");
        }

        private void e5Click(object sender, RoutedEventArgs e)
        {
            Clic("e5");
        }

        private void f5Click(object sender, RoutedEventArgs e)
        {
            Clic("f5");
        }

        private void g5Click(object sender, RoutedEventArgs e)
        {
            Clic("g5");
        }

        private void h5Click(object sender, RoutedEventArgs e)
        {
            Clic("h5");
        }

        private void a4Click(object sender, RoutedEventArgs e)
        {
            Clic("a4");
        }

        private void b4Click(object sender, RoutedEventArgs e)
        {
            Clic("b4");
        }

        private void c4Click(object sender, RoutedEventArgs e)
        {
            Clic("c4");
        }

        private void d4Click(object sender, RoutedEventArgs e)
        {
            Clic("d4");
        }

        private void e4Click(object sender, RoutedEventArgs e)
        {
            Clic("e4");
        }

        private void f4Click(object sender, RoutedEventArgs e)
        {
            Clic("f4");
        }

        private void g4Click(object sender, RoutedEventArgs e)
        {
            Clic("g4");
        }

        private void h4Click(object sender, RoutedEventArgs e)
        {
            Clic("h4");
        }

        private void a3Click(object sender, RoutedEventArgs e)
        {
            Clic("a3");
        }

        private void b3Click(object sender, RoutedEventArgs e)
        {
            Clic("b3");
        }

        private void c3Click(object sender, RoutedEventArgs e)
        {
            Clic("c3");
        }

        private void d3Click(object sender, RoutedEventArgs e)
        {
            Clic("d3");
        }

        private void e3Click(object sender, RoutedEventArgs e)
        {
            Clic("e3");
        }

        private void f3Click(object sender, RoutedEventArgs e)
        {
            Clic("f3");
        }

        private void g3Click(object sender, RoutedEventArgs e)
        {
            Clic("g3");
        }

        private void h3Click(object sender, RoutedEventArgs e)
        {
            Clic("h3");
        }

        private void a2Click(object sender, RoutedEventArgs e)
        {
            Clic("a2");
        }

        private void b2Click(object sender, RoutedEventArgs e)
        {
            Clic("b2");
        }

        private void c2Click(object sender, RoutedEventArgs e)
        {
            Clic("c2");
        }

        private void d2Click(object sender, RoutedEventArgs e)
        {
            Clic("d2");
        }

        private void e2Click(object sender, RoutedEventArgs e)
        {
            Clic("e2");
        }

        private void f2Click(object sender, RoutedEventArgs e)
        {
            Clic("f2");
        }

        private void g2Click(object sender, RoutedEventArgs e)
        {
            Clic("g2");
        }

        private void h2Click(object sender, RoutedEventArgs e)
        {
            Clic("h2");
        }

        private void a1Click(object sender, RoutedEventArgs e)
        {
            Clic("a1");
        }

        private void b1Click(object sender, RoutedEventArgs e)
        {
            Clic("b1");
        }

        private void c1Click(object sender, RoutedEventArgs e)
        {
            Clic("c1");
        }

        private void d1Click(object sender, RoutedEventArgs e)
        {
            Clic("d1");
        }

        private void e1Click(object sender, RoutedEventArgs e)
        {
            Clic("e1");
        }

        private void f1Click(object sender, RoutedEventArgs e)
        {
            Clic("f1");
        }

        private void g1Click(object sender, RoutedEventArgs e)
        {
            Clic("g1");
        }

        private void h1Click(object sender, RoutedEventArgs e)
        {
            Clic("h1");
        }
    }
}