using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessTrainer.Models
{
    public class ChessPiece
    {
        public string Piece { get; set; } // e.g., "pawn", "knight", "king"
        public int X { get; set; } // Column (0-7)
        public int Y { get; set; } // Row (0-7)
    }
}