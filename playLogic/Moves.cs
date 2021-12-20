/******************************************************************/
/* Archivo: Moves.xaml.cs                                    */
/* Programador: Raul Arturo Peredo Estudillo                      */
/* Fecha: 10/Nov/2021                                             */
/* Fecha modificación:  10/Dic/2021                               */
/* Descripción: Metodo de las piezas                              */
/******************************************************************/

using Cliente.playLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.playLogic
{
    /// <summary>
    /// Lógica de interacción para Moves.cs
    /// </summary>
    public class Moves
    {
        /// <summary>
        /// Obtiene las posiciones a las que se puede mover el peon blanco 
        /// </summary>
        /// <param name="column"> colomnas en la que se encuentra</param>
        /// <param name="row"> fila en la que se encuentra</param>
        /// <param name="Squares"> a las que se puede mover</param>
        /// <returns></returns>
        public static List<string> GetMovesWhitePawn(int column, int row, Dictionary<string, ButtonInfo> Squares)
        {
            SquareStatus[] blackPieces = new SquareStatus[] { SquareStatus.blackPawn, SquareStatus.blackBishop, SquareStatus.blackKing, SquareStatus.blackQueen, SquareStatus.blackKnight, SquareStatus.whiteTower };
            List<string> positions = new List<string>();

            if (!(column > 0 && column <= 8 && row > 0 && row < 8))
            {
                return positions;
            }

            string frontSquare = GetSquareName(column, row + 1);

            if (Squares[frontSquare].GetSquareStatus() == SquareStatus.disabled)
            {
                positions.Add(frontSquare);

                if (row == 2)
                {
                    string doubleFrontSquare = GetSquareName(column, row + 2);
                    if (Squares[doubleFrontSquare].GetSquareStatus() == SquareStatus.disabled)
                    {
                        positions.Add(doubleFrontSquare);
                    }
                }
            }

            if (column < 8)
            {
                string rightFrontSquare = GetSquareName(column + 1, row + 1);
                Console.WriteLine(rightFrontSquare);

                if (Squares[rightFrontSquare].GetSquareStatus() != SquareStatus.disabled && blackPieces.Contains(Squares[rightFrontSquare].GetSquareStatus()))
                {
                    positions.Add(rightFrontSquare);
                }
            }

            if (column > 1)
            {
                string leftFrontSquare = GetSquareName(column - 1, row + 1);

                if (Squares[leftFrontSquare].GetSquareStatus() != SquareStatus.disabled && blackPieces.Contains(Squares[leftFrontSquare].GetSquareStatus()))
                {
                    positions.Add(leftFrontSquare);
                }
            }
            return positions;
        }

        /// <summary>
        /// Obtiene las posiciones a las que se puede mover el peon negro 
        /// </summary>
        /// <param name="column"> colomnas en la que se encuentra</param>
        /// <param name="row"> fila en la que se encuentra</param>
        /// <param name="Squares"> a las que se puede mover</param>
        /// <returns></returns>
        public static List<string> GetMovesBlackPawn(int column, int row, Dictionary<string, ButtonInfo> Squares)
        {
            SquareStatus[] whitePieces = new SquareStatus[] { SquareStatus.whitePawn, SquareStatus.whiteBishop, SquareStatus.whiteKing, SquareStatus.whiteQueen, SquareStatus.whiteKnight, SquareStatus.whiteTower };
            List<string> positions = new List<string>();

            if (!(column > 0 && column <= 8 && row > 1 && row <= 8))
            {
                return positions;
            }

            string frontSquare = GetSquareName(column, row - 1);

            if (Squares[frontSquare].GetSquareStatus() == SquareStatus.disabled)
            {
                positions.Add(frontSquare);
                if (row == 7)
                {
                    string doubleFrontSquare = GetSquareName(column, row - 2);

                    if (Squares[doubleFrontSquare].GetSquareStatus() == SquareStatus.disabled)
                    {
                        positions.Add(doubleFrontSquare);
                    }
                }
            }

            if (column < 8)
            {
                string rightFrontSquare = GetSquareName(column + 1, row - 1);

                if (Squares[rightFrontSquare].GetSquareStatus() != SquareStatus.disabled && whitePieces.Contains(Squares[rightFrontSquare].GetSquareStatus()))
                {
                    positions.Add(rightFrontSquare);
                }
            }

            if (column > 1)
            {
                string leftFrontSquare = GetSquareName(column - 1, row - 1);

                if (Squares[leftFrontSquare].GetSquareStatus() != SquareStatus.disabled && whitePieces.Contains(Squares[leftFrontSquare].GetSquareStatus()))
                {
                    positions.Add(leftFrontSquare);
                }
            }
            return positions;
        }
        /// <summary>
        /// Obtiene las posiciones a las que se puede mover el rey
        /// </summary>
        /// <param name="column"> colomnas en la que se encuentra</param>
        /// <param name="row"> fila en la que se encuentra</param>
        /// <param name="Squares"> a las que se puede mover</param>
        /// <param name="isWhite"> para saber si el jugador juga blancas o negras</param>
        /// <returns></returns>
        public static List<string> GetMovesKing(bool isWhite, int column, int row, Dictionary<string, ButtonInfo> Squares)
        {
            SquareStatus[] whitePieces = new SquareStatus[] { SquareStatus.whitePawn, SquareStatus.whiteBishop, SquareStatus.whiteKing, SquareStatus.whiteQueen, SquareStatus.whiteKnight, SquareStatus.whiteTower };
            SquareStatus[] blackPieces = new SquareStatus[] { SquareStatus.blackPawn, SquareStatus.blackBishop, SquareStatus.blackKing, SquareStatus.blackQueen, SquareStatus.blackKnight, SquareStatus.blackTower };
            List<string> positions = new List<string>();
            SquareStatus[] pieces = isWhite ? blackPieces : whitePieces;

            if (!(column > 0 && column <= 8 && row > 0 && row <= 8))
            {
                return positions;
            }

            List<string> squares = new List<string>();
            //up
            if (row < 8)
                squares.Add(GetSquareName(column, row + 1));
            //bottom
            if (row > 1)
                squares.Add(GetSquareName(column, row - 1));
            //right
            if (column < 8)
                squares.Add(GetSquareName(column + 1, row));
            //left
            if (column > 1)
                squares.Add(GetSquareName(column - 1, row));
            //upRight
            if (column < 8 && row < 8)
                squares.Add(GetSquareName(column + 1, row + 1));
            //upLeft
            if (column > 1 && row < 8)
                squares.Add(GetSquareName(column - 1, row + 1));
            //bottomLeft
            if (column > 1 && row > 1)
                squares.Add(GetSquareName(column - 1, row - 1));
            //bottomRight
            if (column < 8 && row > 1)
                squares.Add(GetSquareName(column + 1, row - 1));

            foreach (string square in squares)
            {
                if (Squares[square].GetSquareStatus() == SquareStatus.disabled || pieces.Contains(Squares[square].GetSquareStatus()))
                    positions.Add(square);
            }
            return positions;
        }

        /// <summary>
        /// Obtiene las posiciones a las que se puede mover el caballo
        /// </summary>
        /// <param name="column"> colomnas en la que se encuentra</param>
        /// <param name="row"> fila en la que se encuentra</param>
        /// <param name="Squares"> a las que se puede mover</param>
        /// <param name="isWhite"> para saber si el jugador juga blancas o negras</param>
        /// <returns></returns>
        public static List<string> GetMovesKnight(bool isWhite, int column, int row, Dictionary<string, ButtonInfo> Squares)
        {
            SquareStatus[] whitePieces = new SquareStatus[] { SquareStatus.whitePawn, SquareStatus.whiteBishop, SquareStatus.whiteKing, SquareStatus.whiteQueen, SquareStatus.whiteKnight, SquareStatus.whiteTower };
            SquareStatus[] blackPieces = new SquareStatus[] { SquareStatus.blackPawn, SquareStatus.blackBishop, SquareStatus.blackKing, SquareStatus.blackQueen, SquareStatus.blackKnight, SquareStatus.blackTower };
            List<string> positions = new List<string>();
            SquareStatus[] pieces = isWhite ? blackPieces : whitePieces;

            if (!(column > 0 && column <= 8 && row > 0 && row <= 8))
            {
                return positions;
            }

            List<string> squares = new List<string>();
            //up right
            if (row <= 6 && column < 8)
                squares.Add(GetSquareName(column + 1, row + 2));
            //up left
            if (row <= 6 && column > 1)
                squares.Add(GetSquareName(column - 1, row + 2));
            //right up
            if (row < 8 && column <= 6)
                squares.Add(GetSquareName(column + 2, row + 1));
            //right bottom
            if (row > 1 && column <= 6)
                squares.Add(GetSquareName(column + 2, row - 1));
            //bottom right
            if (row > 2 && column < 8)
                squares.Add(GetSquareName(column + 1, row - 2));
            //bottom left
            if (row > 2 && column > 1)
                squares.Add(GetSquareName(column - 1, row - 2));
            //left bottom
            if (row > 1 && column > 2)
                squares.Add(GetSquareName(column - 2, row - 1));
            //left up
            if (row < 8 && column > 2)
                squares.Add(GetSquareName(column - 2, row + 1));

            foreach (string square in squares)
            {
                if (Squares[square].GetSquareStatus() == SquareStatus.disabled || pieces.Contains(Squares[square].GetSquareStatus()))
                    positions.Add(square);
            }
            return positions;
        }

        /// <summary>
        /// Obtiene las posiciones a las que se puede mover la torre
        /// </summary>
        /// <param name="column"> colomnas en la que se encuentra</param>
        /// <param name="row"> fila en la que se encuentra</param>
        /// <param name="Squares"> a las que se puede mover</param>
        /// <param name="isWhite"> para saber si el jugador juga blancas o negras</param>
        /// <returns></returns>
        public static List<string> GetMovesTower(bool isWhite, int column, int row, Dictionary<string, ButtonInfo> Squares)
        {
            SquareStatus[] whitePieces = new SquareStatus[] {SquareStatus.whitePawn, SquareStatus.whiteBishop, SquareStatus.whiteKing, SquareStatus.whiteQueen, SquareStatus.whiteKnight, SquareStatus.whiteTower};
            SquareStatus[] blackPieces = new SquareStatus[] {SquareStatus.blackPawn, SquareStatus.blackBishop, SquareStatus.blackKing, SquareStatus.blackQueen, SquareStatus.blackKnight, SquareStatus.blackTower };
            List<string> positions = new List<string>();
            SquareStatus[] pieces = isWhite ? blackPieces : whitePieces;

            if (!(column > 0 && column <= 8 && row > 0 && row <= 8))
            {
                return positions;
            }

            int flag = 0;   //b
            int rowIteration = row + 1;  // r
            int columnIteration = column + 1; //c
            
            //up
            while (flag == 0 && rowIteration <= 8)
            {
                string square = GetSquareName(column, rowIteration);
                if(Squares[square].GetSquareStatus() == SquareStatus.disabled)
                {
                    positions.Add(square);
                    rowIteration++;
                }
                else if(pieces.Contains(Squares[square].GetSquareStatus()))
                {
                    positions.Add(square);
                    flag = 1;
                }
                else
                {
                    flag = 1;
                }
            }

            //right
            flag = 0;
            columnIteration = column + 1;
            while (flag == 0 && columnIteration <= 8)
            {
                string square = GetSquareName(columnIteration, row);
                if (Squares[square].GetSquareStatus() == SquareStatus.disabled)
                {
                    positions.Add(square);
                    columnIteration++;
                }
                else if (pieces.Contains(Squares[square].GetSquareStatus()))
                {
                    positions.Add(square);
                    flag = 1;
                }
                else
                {
                    flag = 1;
                }
            }

            //left
            flag = 0;
            columnIteration = column - 1;

            while (flag == 0 && columnIteration > 0)
            {
                string square = GetSquareName(columnIteration, row);
                if (Squares[square].GetSquareStatus() == SquareStatus.disabled)
                {
                    positions.Add(square);
                    columnIteration--;
                }
                else if (pieces.Contains(Squares[square].GetSquareStatus()))
                {
                    positions.Add(square);
                    flag = 1;
                }
                else
                {
                    flag = 1;
                }
            }

            //bottom      

            flag = 0;
            rowIteration = row - 1;
            while (flag == 0 && rowIteration > 0)
            {
                string square = GetSquareName(column, rowIteration);
                if (Squares[square].GetSquareStatus() == SquareStatus.disabled)
                {
                    positions.Add(square);
                    rowIteration--;
                }
                else if (pieces.Contains(Squares[square].GetSquareStatus()))
                {
                    positions.Add(square);
                    flag = 1;
                }
                else
                {
                    flag = 1;
                }
            }
            return positions;
        }

        /// <summary>
        /// Obtiene las posiciones a las que se puede mover el alfil
        /// </summary>
        /// <param name="column"> colomnas en la que se encuentra</param>
        /// <param name="row"> fila en la que se encuentra</param>
        /// <param name="Squares"> a las que se puede mover</param>
        /// <param name="isWhite"> para saber si el jugador juga blancas o negras</param>
        /// <returns></returns>
        public static List<string> GetMovesBishop(bool isWhite, int column, int row, Dictionary<string, ButtonInfo> Squares)
        {
            SquareStatus[] whitePieces = new SquareStatus[] { SquareStatus.whitePawn, SquareStatus.whiteBishop, SquareStatus.whiteKing, SquareStatus.whiteQueen, SquareStatus.whiteKnight, SquareStatus.whiteTower };
            SquareStatus[] blackPieces = new SquareStatus[] { SquareStatus.blackPawn, SquareStatus.blackBishop, SquareStatus.blackKing, SquareStatus.blackQueen, SquareStatus.blackKnight, SquareStatus.blackTower };

            List<string> positions = new List<string>();

            SquareStatus[] pieces = isWhite ? blackPieces : whitePieces;

            if (!(column > 0 && column <= 8 && row > 0 && row <= 8))
            {
                return positions;
            }
            //corner up right

            int flag = 0;
            int rowIteration = row + 1;
            int columnIteration = column + 1;

            while (flag == 0 && rowIteration <= 8 && columnIteration <= 8)
            {
                string square = GetSquareName(columnIteration, rowIteration);
                if (Squares[square].GetSquareStatus() == SquareStatus.disabled)
                {
                    positions.Add(square);
                    rowIteration++;
                    columnIteration++;
                }
                else if (pieces.Contains(Squares[square].GetSquareStatus()))
                {
                    positions.Add(square);
                    flag = 1;
                }
                else
                {
                    flag = 1;
                }
            }

            //corner bottom right
            flag = 0;
            rowIteration = row - 1;
            columnIteration = column + 1;

            while (flag == 0 && rowIteration > 0 && columnIteration <= 8)
            {
                string square = GetSquareName(columnIteration, rowIteration);
                if (Squares[square].GetSquareStatus() == SquareStatus.disabled)
                {
                    positions.Add(square);
                    rowIteration--;
                    columnIteration++;
                }
                else if (pieces.Contains(Squares[square].GetSquareStatus()))
                {
                    positions.Add(square);
                    flag = 1;
                }
                else
                {
                    flag = 1;
                }
            }

            //corner up left
            flag = 0;
            rowIteration = row + 1;
            columnIteration = column - 1;

            while (flag == 0 && rowIteration <= 8 && columnIteration > 0)
            {
                string square = GetSquareName(columnIteration, rowIteration);
                if (Squares[square].GetSquareStatus() == SquareStatus.disabled)
                {
                    positions.Add(square);
                    rowIteration++;
                    columnIteration--;
                }
                else if (pieces.Contains(Squares[square].GetSquareStatus()))
                {
                    positions.Add(square);
                    flag = 1;
                }
                else
                {
                    flag = 1;
                }
            }

            //corner bottom left
            flag = 0;
            rowIteration = row - 1;
            columnIteration = column - 1;

            while (flag == 0 && rowIteration > 0 && columnIteration > 0)
            {
                string square = GetSquareName(columnIteration, rowIteration);
                if (Squares[square].GetSquareStatus() == SquareStatus.disabled)
                {
                    positions.Add(square);
                    rowIteration--;
                    columnIteration--;
                }
                else if (pieces.Contains(Squares[square].GetSquareStatus()))
                {
                    positions.Add(square);
                    flag = 1;
                }
                else
                {
                    flag = 1;
                }
            }
            return positions;
        }

        /// <summary>
        /// Obtiene las posiciones a las que se puede mover la reyna
        /// </summary>
        /// <param name="column"> colomnas en la que se encuentra</param>
        /// <param name="row"> fila en la que se encuentra</param>
        /// <param name="Squares"> a las que se puede mover</param>
        /// <param name="isWhite"> para saber si el jugador juga blancas o negras</param>
        /// <returns></returns>
        public static List<string> GetMovesQueen(bool isWhite, int column, int row, Dictionary<string, ButtonInfo> Squares)
        {
            SquareStatus[] whitePieces = new SquareStatus[] { SquareStatus.whitePawn, SquareStatus.whiteBishop, SquareStatus.whiteKing, SquareStatus.whiteQueen, SquareStatus.whiteKnight, SquareStatus.whiteTower };
            SquareStatus[] blackPieces = new SquareStatus[] { SquareStatus.blackPawn, SquareStatus.blackBishop, SquareStatus.blackKing, SquareStatus.blackQueen, SquareStatus.blackKnight, SquareStatus.blackTower };

            List<string> positions = new List<string>();

            SquareStatus[] pieces = isWhite ? blackPieces : whitePieces;

            if (!(column > 0 && column <= 8 && row > 0 && row <= 8))
            {
                return positions;
            }

            int flag = 0;
            int rowIteration = row + 1;
            int columnIteration = column + 1;

            //up
            while (flag == 0 && rowIteration <= 8)
            {
                string square = GetSquareName(column, rowIteration);
                if (Squares[square].GetSquareStatus() == SquareStatus.disabled)
                {
                    positions.Add(square);
                    rowIteration++;
                }
                else if (pieces.Contains(Squares[square].GetSquareStatus()))
                {
                    positions.Add(square);
                    flag = 1;
                }
                else
                {
                    flag = 1;
                }
            }

            //right
            flag = 0;
            columnIteration = column + 1;
            while (flag == 0 && columnIteration <= 8)
            {
                string square = GetSquareName(columnIteration, row);
                if (Squares[square].GetSquareStatus() == SquareStatus.disabled)
                {
                    positions.Add(square);
                    columnIteration++;
                }
                else if (pieces.Contains(Squares[square].GetSquareStatus()))
                {
                    positions.Add(square);
                    flag = 1;
                }
                else
                {
                    flag = 1;
                }
            }

            //left
            flag = 0;
            columnIteration = column - 1;

            while (flag == 0 && columnIteration > 0)
            {
                string square = GetSquareName(columnIteration, row);
                if (Squares[square].GetSquareStatus() == SquareStatus.disabled)
                {
                    positions.Add(square);
                    columnIteration--;
                }
                else if (pieces.Contains(Squares[square].GetSquareStatus()))
                {
                    positions.Add(square);
                    flag = 1;
                }
                else
                {
                    flag = 1;
                }
            }

            //bottom      

            flag = 0;
            rowIteration = row - 1;
            while (flag == 0 && rowIteration > 0)
            {
                string square = GetSquareName(column, rowIteration);
                if (Squares[square].GetSquareStatus() == SquareStatus.disabled)
                {
                    positions.Add(square);
                    rowIteration--;
                }
                else if (pieces.Contains(Squares[square].GetSquareStatus()))
                {
                    positions.Add(square);
                    flag = 1;
                }
                else
                {
                    flag = 1;
                }
            }

            if (!(column > 0 && column <= 8 && row > 0 && row <= 8))
            {
                return positions;
            }
            //corner up right

            flag = 0;
            rowIteration = row + 1;
            columnIteration = column + 1;

            while (flag == 0 && rowIteration <= 8 && columnIteration <= 8)
            {
                string square = GetSquareName(columnIteration, rowIteration);
                if (Squares[square].GetSquareStatus() == SquareStatus.disabled)
                {
                    positions.Add(square);
                    rowIteration++;
                    columnIteration++;
                }
                else if (pieces.Contains(Squares[square].GetSquareStatus()))
                {
                    positions.Add(square);
                    flag = 1;
                }
                else
                {
                    flag = 1;
                }
            }

            //corner bottom right
            flag = 0;
            rowIteration = row - 1;
            columnIteration = column + 1;

            while (flag == 0 && rowIteration > 0 && columnIteration <= 8)
            {
                string square = GetSquareName(columnIteration, rowIteration);
                if (Squares[square].GetSquareStatus() == SquareStatus.disabled)
                {
                    positions.Add(square);
                    rowIteration--;
                    columnIteration++;
                }
                else if (pieces.Contains(Squares[square].GetSquareStatus()))
                {
                    positions.Add(square);
                    flag = 1;
                }
                else
                {
                    flag = 1;
                }
            }

            //corner up left
            flag = 0;
            rowIteration = row + 1;
            columnIteration = column - 1;

            while (flag == 0 && rowIteration <= 8 && columnIteration > 0)
            {
                string square = GetSquareName(columnIteration, rowIteration);
                if (Squares[square].GetSquareStatus() == SquareStatus.disabled)
                {
                    positions.Add(square);
                    rowIteration++;
                    columnIteration--;
                }
                else if (pieces.Contains(Squares[square].GetSquareStatus()))
                {
                    positions.Add(square);
                    flag = 1;
                }
                else
                {
                    flag = 1;
                }
            }

            //corner bottom left
            flag = 0;
            rowIteration = row - 1;
            columnIteration = column - 1;

            while (flag == 0 && rowIteration > 0 && columnIteration > 0)
            {
                string square = GetSquareName(columnIteration, rowIteration);
                if (Squares[square].GetSquareStatus() == SquareStatus.disabled)
                {
                    positions.Add(square);
                    rowIteration--;
                    columnIteration--;
                }
                else if (pieces.Contains(Squares[square].GetSquareStatus()))
                {
                    positions.Add(square);
                    flag = 1;
                }
                else
                {
                    flag = 1;
                }
            }
            return positions;
        }

        /// <summary>
        /// Obtiene el nombre de la coordenada.
        /// </summary>
        /// <param name="column"> columnas en las que se encuentra</param>
        /// <param name="row">filas en las que se encuentra</param>
        /// <returns></returns>
        public static string GetSquareName(int column, int row)
        {
            string result = "";
            string[] columnValues = new string[] { "", "a", "b", "c", "d", "e", "f", "g", "h" };
            result = columnValues[column] + row.ToString();
            return result;
        }
    }

}
