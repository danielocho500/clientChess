using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.play_logic
{
    public class Moves
    {
        public static List<string> getMovesWhitePawn(int column, int row, Dictionary<string, ButtonInfo> Squares)
        {

        SquareStatus[] blackPieces = new SquareStatus[] { SquareStatus.BlackPawn, SquareStatus.BlackBishop, SquareStatus.BlackKing, SquareStatus.BlackQueen, SquareStatus.BlackKnight, SquareStatus.WhiteTower };

        List<string> positions = new List<string>();

            if (!(column > 0 && column <= 8 && row > 0 && row < 8))
                return positions;

            string frontSquare = getSquareName(column, row + 1);
            if (Squares[frontSquare].GetSquareStatus() == SquareStatus.disabled)
            {
                positions.Add(frontSquare);
                if (row == 2)
                {
                    string doubleFrontSquare = getSquareName(column, row + 2);
                    if (Squares[doubleFrontSquare].GetSquareStatus() == SquareStatus.disabled)
                    {
                        positions.Add(doubleFrontSquare);
                    }
                }
            }

            if (column < 8)
            {
                string rightFrontSquare = getSquareName(column + 1, row + 1);
                Console.WriteLine(rightFrontSquare);

                if (Squares[rightFrontSquare].GetSquareStatus() != SquareStatus.disabled && blackPieces.Contains(Squares[rightFrontSquare].GetSquareStatus()))
                {
                    positions.Add(rightFrontSquare);
                }
            }

            if (column > 1)
            {

                string leftFrontSquare = getSquareName(column - 1, row + 1);

                if (Squares[leftFrontSquare].GetSquareStatus() != SquareStatus.disabled && blackPieces.Contains(Squares[leftFrontSquare].GetSquareStatus()))
                {
                    positions.Add(leftFrontSquare);
                }
            }

            return positions;
        }

        public static List<string> getMovesBlackPawn(int column, int row, Dictionary<string, ButtonInfo> Squares)
        {
            SquareStatus[] whitePieces = new SquareStatus[] { SquareStatus.WhitePawn, SquareStatus.WhiteBishop, SquareStatus.WhiteKing, SquareStatus.WhiteQueen, SquareStatus.WhiteKnight, SquareStatus.WhiteTower };

            List<string> positions = new List<string>();

            if (!(column > 0 && column <= 8 && row > 1 && row <= 8))
                return positions;

            string frontSquare = getSquareName(column, row - 1);
            if (Squares[frontSquare].GetSquareStatus() == SquareStatus.disabled)
            {
                positions.Add(frontSquare);
                if (row == 7)
                {
                    string doubleFrontSquare = getSquareName(column, row - 2);
                    if (Squares[doubleFrontSquare].GetSquareStatus() == SquareStatus.disabled)
                    {
                        positions.Add(doubleFrontSquare);
                    }
                }
            }


            if (column < 8)
            {

                string rightFrontSquare = getSquareName(column + 1, row - 1);

                if (Squares[rightFrontSquare].GetSquareStatus() != SquareStatus.disabled && whitePieces.Contains(Squares[rightFrontSquare].GetSquareStatus()))
                {
                    positions.Add(rightFrontSquare);
                }
            }

            if (column > 1)
            {

                string leftFrontSquare = getSquareName(column - 1, row - 1);

                if (Squares[leftFrontSquare].GetSquareStatus() != SquareStatus.disabled && whitePieces.Contains(Squares[leftFrontSquare].GetSquareStatus()))
                {
                    positions.Add(leftFrontSquare);
                }
            }

            return positions;
        }

        public static List<string> getMovesKing(bool isWhite, int column, int row, Dictionary<string, ButtonInfo> Squares)
        {
            SquareStatus[] whitePieces = new SquareStatus[] { SquareStatus.WhitePawn, SquareStatus.WhiteBishop, SquareStatus.WhiteKing, SquareStatus.WhiteQueen, SquareStatus.WhiteKnight, SquareStatus.WhiteTower };
            SquareStatus[] blackPieces = new SquareStatus[] { SquareStatus.BlackPawn, SquareStatus.BlackBishop, SquareStatus.BlackKing, SquareStatus.BlackQueen, SquareStatus.BlackKnight, SquareStatus.WhiteTower };
            
            List<string> positions = new List<string>();

            SquareStatus[] pieces = isWhite ? blackPieces : whitePieces;

            if (!(column > 0 && column <= 8 && row > 0 && row <= 8))
            {
                return positions;
            }


            List<string> squares = new List<string>();
            //up
            if (row < 8)
                squares.Add(getSquareName(column, row + 1));
            //bottom
            if (row > 1)
                squares.Add(getSquareName(column, row - 1));
            //right
            if (column < 8)
                squares.Add(getSquareName(column + 1, row));
            //left
            if (column > 1)
                squares.Add(getSquareName(column - 1, row));
            //upRight
            if (column < 8 && row < 8)
                squares.Add(getSquareName(column + 1, row + 1));
            //upLeft
            if (column > 1 && row < 8)
                squares.Add(getSquareName(column - 1, row + 1));
            //bottomLeft
            if (column > 1 && row > 1)
                squares.Add(getSquareName(column - 1, row - 1));
            //bottomRight
            if (column < 8 && row > 1)
                squares.Add(getSquareName(column + 1, row - 1));


            foreach (string sq in squares)
            {
                if (Squares[sq].GetSquareStatus() == SquareStatus.disabled || pieces.Contains(Squares[sq].GetSquareStatus()))
                    positions.Add(sq);
            }

            return positions;
        }

        public static string getSquareName(int column, int row)
        {
            string result = "";
            string[] columnValues = new string[] { "", "a", "b", "c", "d", "e", "f", "g", "h" };

            result = columnValues[column] + row.ToString();
            return result;
        }
    }

}
