/******************************************************************/
/* Archivo: ButtonInfo.xaml.cs                                    */
/* Programador: Raul Arturo Peredo Estudillo                      */
/* Fecha: 10/Nov/2021                                             */
/* Fecha modificación:  10/Dic/2021                               */
/* Descripción: Estado de los botones                             */
/******************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Cliente.playLogic
{
    /// <summary>
    /// Lógica de interacción para ButtonInfo.cs
    /// </summary>
    public class ButtonInfo
    {
        private SquareStatus squareStatus;
        private int column;
        private int row;
        private Button button;

        /// <summary>
        /// Informacion del boton
        /// </summary>
        /// <param name="squareStatus"> estado del botón</param>
        /// <param name="column"> Columna en la que esta el botón</param>
        /// <param name="row"> fila en la que esta el botón  </param>
        /// <param name="button"> referencia al elemento axml</param>
        public ButtonInfo(SquareStatus squareStatus, int column, int row, Button button)
        {
            this.squareStatus = squareStatus;
            this.column = column;
            this.row = row;
            this.button = button;
        }

        public SquareStatus GetSquareStatus()
        {
            return squareStatus;
        }

        public void SetSquareStatus(SquareStatus squareStatus)
        {
            this.squareStatus = squareStatus;
        }


        public int GetRow()
        {
            return row;
        }

        public int GetColumn()
        {
            return column;
        }

        public Button GetButton()
        {
            return button;
        }
    }

    /// <summary>
    /// Estados en lo que puede estar un boton.
    /// </summary>
    public enum SquareStatus
    {
        disabled,
        toMoveWhite,
        toMoveBlack,
        whitePawn,
        whitePawnToDefeat,
        blackPawn,
        blackPawnToDefeat,
        whiteKing,
        whiteKingToDefeat,
        blackKing,
        blackKingToDefeat,
        whiteTower,
        whiteTowerToDefeat,
        blackTower,
        blackTowerToDefeat,
        whiteKnight,
        whiteKnightToDefeat,
        blackKnight,
        blackKnightToDefeat,
        whiteBishop,
        whiteBishopToDefeat,
        blackBishop,
        blackBishopToDefeat,
        whiteQueen,
        whiteQueenToDefeat,
        blackQueen,
        blackQueenToDefeat,
    }
}
