using System;

namespace Checkers
{
    internal class Board
    {
        public readonly Piece[,] pieces = new Piece[8, 8];
        public Piece this[int row, int col]
        {
            get 
            {
                if (pieces[row, col] == null) return new Man(Player.None);
                return pieces[row, col]; 
            }
            set { pieces[row, col] = value; }
        }

        public int currentDirection = 1; // 1 = downwards, -1 = upwards

        public Board()
        {
            StartPieces();
        }

        public void StartPieces()
        {
            this[0, 1] = new Man(Player.White);
            this[0, 3] = new Man(Player.White);
            this[0, 5] = new Man(Player.White);
            this[0, 7] = new Man(Player.White);
            this[1, 0] = new Man(Player.White);
            this[1, 2] = new Man(Player.White);
            this[1, 4] = new Man(Player.White);
            this[1, 6] = new Man(Player.White);
            this[2, 1] = new Man(Player.White);
            this[2, 3] = new Man(Player.White);
            this[2, 5] = new Man(Player.White);
            this[2, 7] = new Man(Player.White);

            this[5, 0] = new Man(Player.Black);
            this[5, 2] = new Man(Player.Black);
            this[5, 4] = new Man(Player.Black);
            this[5, 6] = new Man(Player.Black);
            this[6, 1] = new Man(Player.Black);
            this[6, 3] = new Man(Player.Black);
            this[6, 5] = new Man(Player.Black);
            this[6, 7] = new Man(Player.Black);
            this[7, 0] = new Man(Player.Black);
            this[7, 2] = new Man(Player.Black);
            this[7, 4] = new Man(Player.Black);
            this[7, 6] = new Man(Player.Black);
        }

        public void Move(Position fromPos, Position toPos)
        {
            if (!InBoard(toPos.Row, toPos.Column)) return;
            Piece movingPiece = this[fromPos.Row, fromPos.Column];
            currentDirection = (movingPiece.player == Player.Black || movingPiece.player == Player.White) ? -1 : 1;

            if (pieces[toPos.Row, toPos.Column] == null)
            {

                if (CanCaptureOpponent(this[fromPos.Row, fromPos.Column].player, fromPos.Row, fromPos.Column, toPos.Row, toPos.Column))
                {
                    PerformMove(toPos.Row, toPos.Column, fromPos.Row, fromPos.Column);

                    if (CanMakeDoubleJump(toPos.Row, toPos.Column))
                    {
                        //IsKing(toPos.Row + 2, toPos.Column + 2);
                        Move(toPos, new Position(toPos.Row + 2, toPos.Column + 2));
                    }
                }
                else
                {
                    PerformMove(toPos.Row, toPos.Column, fromPos.Row, fromPos.Column);
                }

                IsKing(toPos.Row, toPos.Column);
            }
        }

        private void PerformMove(int toRow, int toColumn, int fromRow, int fromColumn)
        {
            Piece movingPiece = this[fromRow, fromColumn];
            int direction = (movingPiece.player == Player.White) ? 1 : -1;

            if (IsDiagonal(fromRow, fromColumn, toRow, toColumn) && IsOneSpaceMove(toRow, toColumn, fromRow, fromColumn) && movingPiece.isKing)
            {
                    this[toRow, toColumn] = this[fromRow, fromColumn];
                    this[fromRow, fromColumn] = null;
            }
            else
            {
                if (direction == 1 && toRow > fromRow || direction == -1 && toRow < fromRow)
                {
                    if (IsDiagonal(fromRow, fromColumn, toRow, toColumn) && IsOneSpaceMove(toRow, toColumn, fromRow, fromColumn))
                    {
                        this[toRow, toColumn] = this[fromRow, fromColumn];
                        this[fromRow, fromColumn] = null;
                    }
                }
            }
        }

        public bool CanMakeDoubleJump(int fromRow, int fromColumn)
        {
            Player currentPlayer = this[fromRow, fromColumn].player;
            int direction = (currentPlayer == Player.White) ? 1 : -1;

            for (int dr = -2; dr <= 2; dr += 4)
            {
                for (int dc = -2; dc <= 2; dc += 4)
                {
                    //if (dr * direction <= 0 || dc * direction <= 0)
                        //continue;

                    int toRow = fromRow + dr;
                    int toColumn = fromColumn + dc;
                    int capturedRow = fromRow + dr / 2;
                    int capturedColumn = fromColumn + dc / 2;

                    if (InBoard(toRow, toColumn) && IsEmptyCell(toRow, toColumn) &&
                        CanCaptureOpponent(currentPlayer, fromRow, fromColumn, toRow, toColumn))
                    {
                        if (this[capturedRow, capturedColumn]?.player != currentPlayer &&
                            this[capturedRow, capturedColumn]?.player != Player.None)
                        {
                            if (CanMakeDoubleJump(toRow, toColumn))
                                return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool IsKing(int row, int column)
        {
            if (row == 0 && this[row, column].player == Player.Black)
            {
                for(int i = 1; i <= 7; i += 2)
                {
                    if (this[0, i].player == Player.Black) { this[0, i].isKing = true; } 
                }
            }
            if (row == 7 && this[row, column].player == Player.White)
            {
                for(int i = 0; i <= 6; i += 2)
                {
                    if (this[7, i].player == Player.White) { this[7, i].isKing = true; }
                }
            }
            return this[row, column].isKing;
        }

        public bool CanCaptureOpponent(Player currentPlayer, int fromRow, int fromColumn, int toRow, int toColumn)
        {
            if (!InBoard(toRow, toColumn)) return false;

            if (IsDiagonal(fromRow, fromColumn, toRow, toColumn) && Math.Abs(toRow - fromRow) == 2 && Math.Abs(toColumn - fromColumn) == 2)
            {
                int capturedRow = (fromRow + toRow) / 2;
                int capturedColumn = (fromColumn + toColumn) / 2;

                if (this[capturedRow, capturedColumn].player != currentPlayer && this[capturedRow, capturedColumn].player != Player.None
                    && IsEmptyCell(toRow, toColumn))
                {
                    this[toRow, toColumn] = this[fromRow, fromColumn];
                    this[capturedRow, capturedColumn] = null;
                    this[fromRow, fromColumn] = null;
                        
                    return true;
                }
            }
            return false;
        }

        private bool IsOneSpaceMove(int toRow, int toColumn, int fromRow, int fromColumn)
        {
            // Check if the move is one space horizontally or vertically
            return Math.Abs(toRow - fromRow) == 1 && Math.Abs(toColumn - fromColumn) == 1;
        }

        public static bool IsDiagonal(int currentRow, int currentColumn, int newRow, int newColumn)
        {
            return Math.Abs(newRow - currentRow) == Math.Abs(newColumn - currentColumn);
        }

        public static bool InBoard(int row, int column)
        {
            return (row >= 0 && column >= 0) && (row < 8 && column < 8);
        }

        public bool IsEmptyCell(int row, int column)
        {
            return this[row, column].player == Player.None;
        }

        public static int[] PositionFromStr(string pos)
        {
            char[] posStr = pos.ToCharArray();
            int[] posInt = new int[2];

            for (int i = 0; i < 2; i++)
            {
                posInt[i] = int.Parse(posStr[i].ToString());
            }

            return posInt;
        }
    }
}