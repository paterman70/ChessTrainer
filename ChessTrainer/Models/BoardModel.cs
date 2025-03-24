using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChessGame;

namespace ChessTrainer.Models
{
    public class BoardModel
    {
        private List<ChessPiece> boardpieces;
        private List<global::ChessGame.Board> TheGame;
        private int moveindex=0;
        public BoardModel()
        {
         
        }
        public int MoveIndex  // property
        {
            get { return moveindex; }   // get method

        }

        public List<ChessPiece> Pieces   // property
        {
            get { return boardpieces; }   // get method
           
        }

        public void SetGame(List<global::ChessGame.Board> G)
        {
            TheGame = G;
        }
        public void SetMove(int m)
        {
           if (TheGame.Count>m)
            {
                List<global::ChessGame.Piece> lp;
                global::ChessGame.Piece p;
                ChessPiece cp=null;
                Board B = TheGame[m];
                moveindex = m;
                if (!(boardpieces is object))
                    boardpieces = new List<ChessPiece>();

                boardpieces.Clear();
                lp=B.GetPieces();
                for(int i=0;i<lp.Count;i++)
                {
                    p = lp[i];
                    if (p is object)
                    {
                        if (p.PieceColor == Color.White)
                        {
                           switch(p.PieceName)
                            {
                                case "King":
                                    cp = new ChessPiece { Piece = "White-King", X = p.cell.Position.FileIndex, Y = p.cell.Position.Rank -1};
                                    break;
                                case "Queen":
                                    cp = new ChessPiece { Piece = "White-Queen", X = p.cell.Position.FileIndex, Y = p.cell.Position.Rank -1};
                                    break;
                                case "Rook":
                                    cp = new ChessPiece { Piece = "White-Rook", X = p.cell.Position.FileIndex, Y = p.cell.Position.Rank -1};
                                    break;
                                case "Bishop":
                                    cp = new ChessPiece { Piece = "White-Bishop", X = p.cell.Position.FileIndex, Y = p.cell.Position.Rank -1};
                                    break;
                                case "Knight":
                                    cp = new ChessPiece { Piece = "White-Knight", X = p.cell.Position.FileIndex, Y = p.cell.Position.Rank -1};
                                    break;
                                case "Pawn":
                                    cp = new ChessPiece { Piece = "White-Pawn", X = p.cell.Position.FileIndex, Y = p.cell.Position.Rank -1};
                                    break;

                            }
                        //    WMobility += p.Mobility();
                         //   WValue += p.Value;
                        }
                        else
                        {
                            switch (p.PieceName)
                            {
                                case "King":
                                    cp = new ChessPiece { Piece = "Black-King", X = p.cell.Position.FileIndex, Y = p.cell.Position.Rank -1};
                                    break;
                                case "Queen":
                                    cp = new ChessPiece { Piece = "Black-Queen", X = p.cell.Position.FileIndex, Y = p.cell.Position.Rank-1 };
                                    break;
                                case "Rook":
                                    cp = new ChessPiece { Piece = "Black-Rook", X = p.cell.Position.FileIndex, Y = p.cell.Position.Rank -1};
                                    break;
                                case "Bishop":
                                    cp = new ChessPiece { Piece = "Black-Bishop", X = p.cell.Position.FileIndex, Y = p.cell.Position.Rank -1};
                                    break;
                                case "Knight":
                                    cp = new ChessPiece { Piece = "Black-Knight", X = p.cell.Position.FileIndex, Y = p.cell.Position.Rank -1};
                                    break;
                                case "Pawn":
                                    cp = new ChessPiece { Piece = "Black-Pawn", X = p.cell.Position.FileIndex, Y = p.cell.Position.Rank-1 };
                                    break;

                            }
                            //   BMobility += p.Mobility();
                            //  BValue += p.Value;
                        }
                        boardpieces.Add(cp);
                    }

                }
            }
        }
        public List<ChessPiece> InitialPosition()
        {
            boardpieces = new List<ChessPiece>
                {
                    new ChessPiece { Piece = "White-Rook", X = 0, Y = 0 },
                    new ChessPiece { Piece = "White-Knight", X = 1, Y = 0 },
                    new ChessPiece { Piece = "White-Bishop", X = 2, Y = 0 },
                    new ChessPiece { Piece = "White-Queen", X = 3, Y = 0 },
                    new ChessPiece { Piece = "White-King", X = 4, Y = 0 },
                    new ChessPiece { Piece = "White-Bishop", X = 5, Y = 0 },
                    new ChessPiece { Piece = "White-Knight", X = 6, Y = 0 },
                    new ChessPiece { Piece = "White-Rook", X = 7, Y = 0 },
                    new ChessPiece { Piece = "White-Pawn", X = 0, Y = 1 },
                    new ChessPiece { Piece = "White-Pawn", X = 1, Y = 1 },
                    new ChessPiece { Piece = "White-Pawn", X = 2, Y = 1 },
                    new ChessPiece { Piece = "White-Pawn", X = 3, Y = 1 },
                    new ChessPiece { Piece = "White-Pawn", X = 4, Y = 1 },
                    new ChessPiece { Piece = "White-Pawn", X = 5, Y = 1 },
                    new ChessPiece { Piece = "White-Pawn", X = 6, Y = 1 },
                    new ChessPiece { Piece = "White-Pawn", X = 7, Y = 1 },

                     new ChessPiece { Piece = "Black-Rook", X = 0, Y = 7 },
                    new ChessPiece { Piece = "Black-Knight", X = 1, Y = 7 },
                    new ChessPiece { Piece = "Black-Bishop", X = 2, Y = 7 },
                    new ChessPiece { Piece = "Black-Queen", X = 3, Y = 7 },
                    new ChessPiece { Piece = "Black-King", X = 4, Y = 7 },
                    new ChessPiece { Piece = "Black-Bishop", X = 5, Y = 7 },
                    new ChessPiece { Piece = "Black-Knight", X = 6, Y = 7 },
                    new ChessPiece { Piece = "Black-Rook", X = 7, Y = 7 },
                    new ChessPiece { Piece = "Black-Pawn", X = 0, Y = 6 },
                    new ChessPiece { Piece = "Black-Pawn", X = 1, Y = 6 },
                    new ChessPiece { Piece = "Black-Pawn", X = 2, Y = 6 },
                    new ChessPiece { Piece = "Black-Pawn", X = 3, Y = 6 },
                    new ChessPiece { Piece = "Black-Pawn", X = 4, Y = 6 },
                    new ChessPiece { Piece = "Black-Pawn", X = 5, Y = 6 },
                    new ChessPiece { Piece = "Black-Pawn", X = 6, Y = 6 },
                    new ChessPiece { Piece = "Black-Pawn", X = 7, Y = 6 },

                };



            return boardpieces;
        }

    }
}