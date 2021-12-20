using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Cliente.play_logic
{
    public class ButtonInfo
    {
        private SquareStatus squareStatus;
        private int column;
        private int row;
        private Button btn;

        public ButtonInfo(SquareStatus squareStatus_, int column_, int row_, Button btn_)
        {
            squareStatus = squareStatus_;
            column = column_;
            row = row_;
            btn = btn_;
        }

        public SquareStatus GetSquareStatus()
        {
            return squareStatus;
        }

        public void setSquareStatus(SquareStatus sq)
        {
            squareStatus = sq;
        }


        public int GetRow()
        {
            return row;
        }

        public int GetColumn()
        {
            return column;
        }

        public Button GetBtn()
        {
            return btn;
        }
    }

    public enum SquareStatus
    {
        disabled,
        toMoveWhite,
        toMoveBlack,
        WhitePawn,
        WhitePawnToDefeat,
        BlackPawn,
        BlackPawnToDefeat,
        WhiteKing,
        WhiteKingToDefeat,
        BlackKing,
        BlackKingToDefeat,
        WhiteTower,
        WhiteTowerToDefeat,
        BlackTower,
        BlackTowerToDefeat,
        WhiteKnight,
        WhiteKnightToDefeat,
        BlackKnight,
        BlackKnightToDefeat,
        WhiteBishop,
        WhiteBishopToDefeat,
        BlackBishop,
        BlackBishopToDefeat,
        WhiteQueen,
        WhiteQueenToDefeat,
        BlackQueen,
        BlackQueenToDefeat,
    }
}
