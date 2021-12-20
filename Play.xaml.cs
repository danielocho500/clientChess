﻿using Cliente.ChessService;
using Cliente.play_logic;
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
    public partial class Play : Window, IMatchServiceCallback
    {
        public int IdUser;
        public string Rival;
        public string MatchCode;
        public bool IsWhite;
        public string UsernameActual;

        public bool isTurn = true;
        public bool givenUP = false;

        public MatchServiceClient server_match;

        private Dictionary<string, ButtonInfo> Squares = new Dictionary<string, ButtonInfo>();

        private string selectedSquare;
        private SquareStatus selectedSquareValue = SquareStatus.disabled;

        private SquareStatus[] whitePieces = new SquareStatus[] { SquareStatus.WhitePawn, SquareStatus.WhiteBishop, SquareStatus.WhiteKing, SquareStatus.WhiteQueen, SquareStatus.WhiteKnight, SquareStatus.WhiteTower };
        private SquareStatus[] blackPieces = new SquareStatus[] { SquareStatus.BlackPawn, SquareStatus.BlackBishop, SquareStatus.BlackKing, SquareStatus.BlackQueen, SquareStatus.BlackKnight, SquareStatus.WhiteTower };
        private SquareStatus[] toDefeatPieces = new SquareStatus[] { SquareStatus.BlackPawnToDefeat, SquareStatus.WhitePawnToDefeat, SquareStatus.BlackBishopToDefeat, SquareStatus.WhiteBishopToDefeat, SquareStatus.BlackKingToDefeat, SquareStatus.WhiteKingToDefeat, SquareStatus.BlackKnightToDefeat, SquareStatus.WhiteKingToDefeat, SquareStatus.BlackQueenToDefeat, SquareStatus.WhiteQueenToDefeat, SquareStatus.BlackTowerToDefeat, SquareStatus.WhiteTowerToDefeat };

        System.Windows.Threading.DispatcherTimer dispatcherTimer;
        private int timeLeftuser = 100;
        private int timeLeftRival = 100;


        public Play(int idUser_, string username_, string rival_, string MatchCode_, bool white_)
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

            LbColor.Content = color;
            server_match.sendConnection(IsWhite, MatchCode);

            if (IsWhite)
                isTurn = true;
            else
                isTurn = false;

            LbRivalName.Content = Rival;

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();


            PrepareDictionary();
            Update();

        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (isTurn)
                timeLeftuser--;
            else
                timeLeftRival--;

            lbUserTime.Content = getTimeLeftFormat(timeLeftuser);
            LbRivalTime.Content = getTimeLeftFormat(timeLeftRival);

            if (timeLeftRival != 0 && timeLeftuser != 0)
                return;

            dispatcherTimer.Stop();

            if (timeLeftRival == 0)
                server_match.win(IsWhite, true, MatchCode);
        }

        public string getTimeLeftFormat(int secs)
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

        public void ReciveMessage(string message, string time)
        {
            listVMessages.Items.Add(time + " " + Rival + ": " + message);
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
            
            givenUP = true;
            string msg = (youWon) ? "You WIN!!!!" : "You Lose :c";
            msg += "  Elo: " + oldElo + " -> " + newElo;
            MessageBox.Show(msg);
            this.Close();
        }

        private void Update()
        {
            if (isTurn)
                LbTurn.Content = "You";
            else
                LbTurn.Content = Rival;
            foreach (string key in Squares.Keys)
            {
                ButtonInfo buttonInfo = Squares[key];
                Button button = buttonInfo.GetBtn();

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

                    case SquareStatus.WhitePawn:
                        Image imgWhitePawn = new Image();
                        imgWhitePawn.Source = new BitmapImage(new Uri("Images/WhitePawn.png", UriKind.Relative));
                        imgWhitePawn.Stretch = Stretch.Fill;
                        button.Content = imgWhitePawn;
                        break;

                    case SquareStatus.WhitePawnToDefeat:
                        Image imgWhitePawnToDefeat = new Image();
                        imgWhitePawnToDefeat.Source = new BitmapImage(new Uri("Images/WhitePawnToDefeat.png", UriKind.Relative));
                        imgWhitePawnToDefeat.Stretch = Stretch.Fill;
                        button.Content = imgWhitePawnToDefeat;
                        break;

                    case SquareStatus.BlackPawn:
                        Image imgBlackPawn = new Image();
                        imgBlackPawn.Source = new BitmapImage(new Uri("Images/BlackPawn.png", UriKind.Relative));
                        imgBlackPawn.Stretch = Stretch.Fill;
                        button.Content = imgBlackPawn;
                        break;

                    case SquareStatus.BlackPawnToDefeat:
                        Image imgBlackPawnToDefeat = new Image();
                        imgBlackPawnToDefeat.Source = new BitmapImage(new Uri("Images/BlackPawnToDefeat.png", UriKind.Relative));
                        imgBlackPawnToDefeat.Stretch = Stretch.Fill;
                        button.Content = imgBlackPawnToDefeat;
                        break;

                    case SquareStatus.WhiteKing:
                        Image imgWhiteKing = new Image();
                        imgWhiteKing.Source = new BitmapImage(new Uri("Images/WhiteKing.png", UriKind.Relative));
                        imgWhiteKing.Stretch = Stretch.Fill;
                        button.Content = imgWhiteKing;
                        break;

                    case SquareStatus.WhiteKingToDefeat:
                        Image imgWhiteKingToDefeat = new Image();
                        imgWhiteKingToDefeat.Source = new BitmapImage(new Uri("Images/WhiteKingToDefeat.png", UriKind.Relative));
                        imgWhiteKingToDefeat.Stretch = Stretch.Fill;
                        button.Content = imgWhiteKingToDefeat;
                        break;

                    case SquareStatus.BlackKing:
                        Image imgBlackKing = new Image();
                        imgBlackKing.Source = new BitmapImage(new Uri("Images/BlackKing.png", UriKind.Relative));
                        imgBlackKing.Stretch = Stretch.Fill;
                        button.Content = imgBlackKing;
                        break;

                    case SquareStatus.BlackKingToDefeat:
                        Image imgBlackKingToDefeat = new Image();
                        imgBlackKingToDefeat.Source = new BitmapImage(new Uri("Images/BlackKingToDefeat.png", UriKind.Relative));
                        imgBlackKingToDefeat.Stretch = Stretch.Fill;
                        button.Content = imgBlackKingToDefeat;
                        break;
                }
            }
        }

        private void Clic(string btn)
        {
            if (!isTurn)
                return;

            ButtonInfo buttonInfo = Squares[btn];
            SquareStatus buttonStatus = buttonInfo.GetSquareStatus();

            if (IsWhite && blackPieces.Contains(buttonStatus))
                return;
            if (!IsWhite && whitePieces.Contains(buttonStatus))
                return;

            if (buttonStatus == SquareStatus.toMoveWhite || buttonStatus == SquareStatus.toMoveBlack || toDefeatPieces.Contains(buttonStatus))
            {
                server_match.move(IsWhite, MatchCode, selectedSquare, btn, timeLeftuser);
                isTurn = false;

                Squares[selectedSquare].setSquareStatus(SquareStatus.disabled);
                Squares[btn].setSquareStatus(selectedSquareValue);


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


            selectedSquare = btn;

            List<string> btnMove = new List<string>();

            switch (buttonStatus)
            {
                case SquareStatus.WhitePawn:
                    btnMove = Moves.getMovesWhitePawn(buttonInfo.GetColumn(), buttonInfo.GetRow(),Squares);
                    selectedSquareValue = SquareStatus.WhitePawn;
                    break;
                case SquareStatus.BlackPawn:
                    btnMove = Moves.getMovesBlackPawn(buttonInfo.GetColumn(), buttonInfo.GetRow(), Squares);
                    selectedSquareValue = SquareStatus.BlackPawn;
                    break;
                case SquareStatus.WhiteKing:
                    btnMove = Moves.getMovesKing(true, buttonInfo.GetColumn(), buttonInfo.GetRow(), Squares);
                    selectedSquareValue = SquareStatus.WhiteKing;
                    break;
                case SquareStatus.BlackKing:
                    btnMove = Moves.getMovesKing(false, buttonInfo.GetColumn(), buttonInfo.GetRow(), Squares);
                    selectedSquareValue = SquareStatus.BlackKing;
                    break;
            }
            setOptionMoves(btnMove);
        }

        public void setOptionMoves(List<string> btnMove)
        {
            string moveColor = blackPieces.Contains(selectedSquareValue) ? "black" : "white";
            foreach (string btn in btnMove)
            {
                if (Squares[btn].GetSquareStatus() == SquareStatus.disabled)
                {
                    if (moveColor == "black")
                        Squares[btn].setSquareStatus(SquareStatus.toMoveBlack);
                    else
                        Squares[btn].setSquareStatus(SquareStatus.toMoveWhite);
                }
                else
                {
                    switch (Squares[btn].GetSquareStatus())
                    {
                        case SquareStatus.BlackPawn:
                            Squares[btn].setSquareStatus(SquareStatus.BlackPawnToDefeat);
                            break;
                        case SquareStatus.WhitePawn:
                            Squares[btn].setSquareStatus(SquareStatus.WhitePawnToDefeat);
                            break;
                        case SquareStatus.BlackKing:
                            Squares[btn].setSquareStatus(SquareStatus.BlackKingToDefeat);
                            break;
                        case SquareStatus.WhiteKing:
                            Squares[btn].setSquareStatus(SquareStatus.WhiteKingToDefeat);
                            break;
                    }
                }
            }
            Update();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(!givenUP)
                server_match.win(IsWhite, false, MatchCode);
        }

        public void movePiece(string previousPosition, string newPosition, int newTime)
        {
            if ((IsWhite && Squares[newPosition].GetSquareStatus() == SquareStatus.WhiteKing) || (!IsWhite && Squares[newPosition].GetSquareStatus() == SquareStatus.BlackKing))
                server_match.win(IsWhite, false, MatchCode);

            timeLeftRival = newTime;

            isTurn = true;
            SquareStatus sqAuxiliar = Squares[previousPosition].GetSquareStatus();
            Squares[previousPosition].setSquareStatus(SquareStatus.disabled);

            Squares[newPosition].setSquareStatus(sqAuxiliar);

            Update();
        }

        public void PrepareDictionary()
        {
            Squares["a1"] = new ButtonInfo(SquareStatus.disabled, 1, 1, a1);
            Squares["a2"] = new ButtonInfo(SquareStatus.WhitePawn, 1, 2, a2);
            Squares["a3"] = new ButtonInfo(SquareStatus.disabled, 1, 3, a3);
            Squares["a4"] = new ButtonInfo(SquareStatus.disabled, 1, 4, a4);
            Squares["a5"] = new ButtonInfo(SquareStatus.disabled, 1, 5, a5);
            Squares["a6"] = new ButtonInfo(SquareStatus.disabled, 1, 6, a6);
            Squares["a7"] = new ButtonInfo(SquareStatus.BlackPawn, 1, 7, a7);
            Squares["a8"] = new ButtonInfo(SquareStatus.disabled, 1, 8, a8);

            Squares["b1"] = new ButtonInfo(SquareStatus.disabled, 2, 1, b1);
            Squares["b2"] = new ButtonInfo(SquareStatus.WhitePawn, 2, 2, b2);
            Squares["b3"] = new ButtonInfo(SquareStatus.disabled, 2, 3, b3);
            Squares["b4"] = new ButtonInfo(SquareStatus.disabled, 2, 4, b4);
            Squares["b5"] = new ButtonInfo(SquareStatus.disabled, 2, 5, b5);
            Squares["b6"] = new ButtonInfo(SquareStatus.disabled, 2, 6, b6);
            Squares["b7"] = new ButtonInfo(SquareStatus.BlackPawn, 2, 7, b7);
            Squares["b8"] = new ButtonInfo(SquareStatus.disabled, 2, 8, b8);

            Squares["c1"] = new ButtonInfo(SquareStatus.disabled, 3, 1, c1);
            Squares["c2"] = new ButtonInfo(SquareStatus.WhitePawn, 3, 2, c2);
            Squares["c3"] = new ButtonInfo(SquareStatus.disabled, 3, 3, c3);
            Squares["c4"] = new ButtonInfo(SquareStatus.disabled, 3, 4, c4);
            Squares["c5"] = new ButtonInfo(SquareStatus.disabled, 3, 5, c5);
            Squares["c6"] = new ButtonInfo(SquareStatus.disabled, 3, 6, c6);
            Squares["c7"] = new ButtonInfo(SquareStatus.BlackPawn, 3, 7, c7);
            Squares["c8"] = new ButtonInfo(SquareStatus.disabled, 3, 8, c8);

            Squares["d1"] = new ButtonInfo(SquareStatus.disabled, 4, 1, d1);
            Squares["d2"] = new ButtonInfo(SquareStatus.WhitePawn, 4, 2, d2);
            Squares["d3"] = new ButtonInfo(SquareStatus.disabled, 4, 3, d3);
            Squares["d4"] = new ButtonInfo(SquareStatus.disabled, 4, 4, d4);
            Squares["d5"] = new ButtonInfo(SquareStatus.disabled, 4, 5, d5);
            Squares["d6"] = new ButtonInfo(SquareStatus.disabled, 4, 6, d6);
            Squares["d7"] = new ButtonInfo(SquareStatus.BlackPawn, 4, 7, d7);
            Squares["d8"] = new ButtonInfo(SquareStatus.disabled, 4, 8, d8);

            Squares["e1"] = new ButtonInfo(SquareStatus.WhiteKing, 5, 1, e1);
            Squares["e2"] = new ButtonInfo(SquareStatus.WhitePawn, 5, 2, e2);
            Squares["e3"] = new ButtonInfo(SquareStatus.disabled, 5, 3, e3);
            Squares["e4"] = new ButtonInfo(SquareStatus.disabled, 5, 4, e4);
            Squares["e5"] = new ButtonInfo(SquareStatus.disabled, 5, 5, e5);
            Squares["e6"] = new ButtonInfo(SquareStatus.disabled, 5, 6, e6);
            Squares["e7"] = new ButtonInfo(SquareStatus.BlackPawn, 5, 7, e7);
            Squares["e8"] = new ButtonInfo(SquareStatus.BlackKing, 5, 8, e8);

            Squares["f1"] = new ButtonInfo(SquareStatus.disabled, 6, 1, f1);
            Squares["f2"] = new ButtonInfo(SquareStatus.WhitePawn, 6, 2, f2);
            Squares["f3"] = new ButtonInfo(SquareStatus.disabled, 6, 3, f3);
            Squares["f4"] = new ButtonInfo(SquareStatus.disabled, 6, 4, f4);
            Squares["f5"] = new ButtonInfo(SquareStatus.disabled, 6, 5, f5);
            Squares["f6"] = new ButtonInfo(SquareStatus.disabled, 6, 6, f6);
            Squares["f7"] = new ButtonInfo(SquareStatus.BlackPawn, 6, 7, f7);
            Squares["f8"] = new ButtonInfo(SquareStatus.disabled, 6, 8, f8);

            Squares["g1"] = new ButtonInfo(SquareStatus.disabled, 7, 1, g1);
            Squares["g2"] = new ButtonInfo(SquareStatus.WhitePawn, 7, 2, g2);
            Squares["g3"] = new ButtonInfo(SquareStatus.disabled, 7, 3, g3);
            Squares["g4"] = new ButtonInfo(SquareStatus.disabled, 7, 4, g4);
            Squares["g5"] = new ButtonInfo(SquareStatus.disabled, 7, 5, g5);
            Squares["g6"] = new ButtonInfo(SquareStatus.disabled, 7, 6, g6);
            Squares["g7"] = new ButtonInfo(SquareStatus.BlackPawn, 7, 7, g7);
            Squares["g8"] = new ButtonInfo(SquareStatus.disabled, 7, 8, g8);

            Squares["h1"] = new ButtonInfo(SquareStatus.disabled, 8, 1, h1);
            Squares["h2"] = new ButtonInfo(SquareStatus.WhitePawn, 8, 2, h2);
            Squares["h3"] = new ButtonInfo(SquareStatus.disabled, 8, 3, h3);
            Squares["h4"] = new ButtonInfo(SquareStatus.disabled, 8, 4, h4);
            Squares["h5"] = new ButtonInfo(SquareStatus.disabled, 8, 5, h5);
            Squares["h6"] = new ButtonInfo(SquareStatus.disabled, 8, 6, h6);
            Squares["h7"] = new ButtonInfo(SquareStatus.BlackPawn, 8, 7, h7);
            Squares["h8"] = new ButtonInfo(SquareStatus.disabled, 8, 8, h8);
        }

        public void DisableOptionsToMove()
        {
            foreach (string key in Squares.Keys)
            {
                SquareStatus buttonStatus = Squares[key].GetSquareStatus();
                if (buttonStatus == SquareStatus.toMoveWhite || buttonStatus == SquareStatus.toMoveBlack)
                {
                    Squares[key].setSquareStatus(SquareStatus.disabled);
                }

                else if (toDefeatPieces.Contains(buttonStatus))
                {
                    switch (buttonStatus)
                    {
                        case SquareStatus.BlackPawnToDefeat:
                            Squares[key].setSquareStatus(SquareStatus.BlackPawn);
                            break;
                        case SquareStatus.WhitePawnToDefeat:
                            Squares[key].setSquareStatus(SquareStatus.WhitePawn);
                            break;
                        case SquareStatus.BlackKingToDefeat:
                            Squares[key].setSquareStatus(SquareStatus.BlackKing);
                            break;
                        case SquareStatus.WhiteKingToDefeat:
                            Squares[key].setSquareStatus(SquareStatus.WhiteKing);
                            break;
                    }
                }

            }

            Update();
        }

        private void a8_Click(object sender, RoutedEventArgs e)
        {
            Clic("a8");
        }

        private void b8_Click(object sender, RoutedEventArgs e)
        {
            Clic("b8");
        }

        private void c8_Click(object sender, RoutedEventArgs e)
        {
            Clic("c8");
        }

        private void d8_Click(object sender, RoutedEventArgs e)
        {
            Clic("d8");
        }

        private void e8_Click(object sender, RoutedEventArgs e)
        {
            Clic("e8");
        }

        private void f8_Click(object sender, RoutedEventArgs e)
        {
            Clic("f8");
        }

        private void g8_Click(object sender, RoutedEventArgs e)
        {
            Clic("g8");
        }

        private void h8_Click(object sender, RoutedEventArgs e)
        {
            Clic("h8");
        }

        private void a7_Click(object sender, RoutedEventArgs e)
        {
            Clic("a7");
        }

        private void b7_Click(object sender, RoutedEventArgs e)
        {
            Clic("b7");
        }

        private void c7_Click(object sender, RoutedEventArgs e)
        {
            Clic("c7");
        }

        private void d7_Click(object sender, RoutedEventArgs e)
        {
            Clic("d7");
        }

        private void e7_Click(object sender, RoutedEventArgs e)
        {
            Clic("e7");
        }

        private void f7_Click(object sender, RoutedEventArgs e)
        {
            Clic("f7");
        }

        private void g7_Click(object sender, RoutedEventArgs e)
        {
            Clic("g7");
        }

        private void h7_Click(object sender, RoutedEventArgs e)
        {
            Clic("h7");
        }

        private void a6_Click(object sender, RoutedEventArgs e)
        {
            Clic("a6");
        }

        private void b6_Click(object sender, RoutedEventArgs e)
        {
            Clic("b6");
        }

        private void c6_Click(object sender, RoutedEventArgs e)
        {
            Clic("c6");
        }

        private void d6_Click(object sender, RoutedEventArgs e)
        {
            Clic("d6");
        }

        private void e6_Click(object sender, RoutedEventArgs e)
        {
            Clic("e6");
        }

        private void f6_Click(object sender, RoutedEventArgs e)
        {
            Clic("f6");
        }

        private void g6_Click(object sender, RoutedEventArgs e)
        {
            Clic("g6");
        }

        private void h6_Click(object sender, RoutedEventArgs e)
        {
            Clic("h6");
        }

        private void a5_Click(object sender, RoutedEventArgs e)
        {
            Clic("a5");
        }

        private void b5_Click(object sender, RoutedEventArgs e)
        {
            Clic("b5");
        }

        private void c5_Click(object sender, RoutedEventArgs e)
        {
            Clic("c5");
        }

        private void d5_Click(object sender, RoutedEventArgs e)
        {
            Clic("d5");
        }

        private void e5_Click(object sender, RoutedEventArgs e)
        {
            Clic("e5");
        }

        private void f5_Click(object sender, RoutedEventArgs e)
        {
            Clic("f5");
        }

        private void g5_Click(object sender, RoutedEventArgs e)
        {
            Clic("g5");
        }

        private void h5_Click(object sender, RoutedEventArgs e)
        {
            Clic("h5");
        }

        private void a4_Click(object sender, RoutedEventArgs e)
        {
            Clic("a4");
        }

        private void b4_Click(object sender, RoutedEventArgs e)
        {
            Clic("b4");
        }

        private void c4_Click(object sender, RoutedEventArgs e)
        {
            Clic("c4");
        }

        private void d4_Click(object sender, RoutedEventArgs e)
        {
            Clic("d4");
        }

        private void e4_Click(object sender, RoutedEventArgs e)
        {
            Clic("e4");
        }

        private void f4_Click(object sender, RoutedEventArgs e)
        {
            Clic("f4");
        }

        private void g4_Click(object sender, RoutedEventArgs e)
        {
            Clic("g4");
        }

        private void h4_Click(object sender, RoutedEventArgs e)
        {
            Clic("h4");
        }

        private void a3_Click(object sender, RoutedEventArgs e)
        {
            Clic("a3");
        }

        private void b3_Click(object sender, RoutedEventArgs e)
        {
            Clic("b3");
        }

        private void c3_Click(object sender, RoutedEventArgs e)
        {
            Clic("c3");
        }

        private void d3_Click(object sender, RoutedEventArgs e)
        {
            Clic("d3");
        }

        private void e3_Click(object sender, RoutedEventArgs e)
        {
            Clic("e3");
        }

        private void f3_Click(object sender, RoutedEventArgs e)
        {
            Clic("f3");
        }

        private void g3_Click(object sender, RoutedEventArgs e)
        {
            Clic("g3");
        }

        private void h3_Click(object sender, RoutedEventArgs e)
        {
            Clic("h3");
        }

        private void a2_Click(object sender, RoutedEventArgs e)
        {
            Clic("a2");
        }

        private void b2_Click(object sender, RoutedEventArgs e)
        {
            Clic("b2");
        }

        private void c2_Click(object sender, RoutedEventArgs e)
        {
            Clic("c2");
        }

        private void d2_Click(object sender, RoutedEventArgs e)
        {
            Clic("d2");
        }

        private void e2_Click(object sender, RoutedEventArgs e)
        {
            Clic("e2");
        }

        private void f2_Click(object sender, RoutedEventArgs e)
        {
            Clic("f2");
        }

        private void g2_Click(object sender, RoutedEventArgs e)
        {
            Clic("g2");
        }

        private void h2_Click(object sender, RoutedEventArgs e)
        {
            Clic("h2");
        }

        private void a1_Click(object sender, RoutedEventArgs e)
        {
            Clic("a1");
        }

        private void b1_Click(object sender, RoutedEventArgs e)
        {
            Clic("b1");
        }

        private void c1_Click(object sender, RoutedEventArgs e)
        {
            Clic("c1");
        }

        private void d1_Click(object sender, RoutedEventArgs e)
        {
            Clic("d1");
        }

        private void e1_Click(object sender, RoutedEventArgs e)
        {
            Clic("e1");
        }

        private void f1_Click(object sender, RoutedEventArgs e)
        {
            Clic("f1");
        }

        private void g1_Click(object sender, RoutedEventArgs e)
        {
            Clic("g1");
        }

        private void h1_Click(object sender, RoutedEventArgs e)
        {
            Clic("h1");
        }

    }
}