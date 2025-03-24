using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessTrainer.Models
{
    public class ChessGame
    {
        private string[,] board;

        public ChessGame()
        {
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            board = new string[8, 8]
            {
            { "r", "n", "b", "q", "k", "b", "n", "r" },
            { "p", "p", "p", "p", "p", "p", "p", "p" },
            { "", "", "", "", "", "", "", "" },
            { "", "", "", "", "", "", "", "" },
            { "", "", "", "", "", "", "", "" },
            { "", "", "", "", "", "", "", "" },
            { "P", "P", "P", "P", "P", "P", "P", "P" },
            { "R", "N", "B", "Q", "K", "B", "N", "R" }
            };
        }

        public object GetBoardState()
        {
            var boardState = new object[8][];
            for (int row = 0; row < 8; row++)
            {
                boardState[row] = new object[8];
                for (int col = 0; col < 8; col++)
                {
                    boardState[row][col] = new
                    {
                        piece = board[row, col] // Example: "p" for pawn, "r" for rook, "" for empty
                    };
                }
            }
            return boardState;
        }

        public void NextMove()
        { }
        public void PreviousMove()
        {

        }
    }
}