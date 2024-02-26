using Android.App;
using Android.Content;
using Android.Net.Wifi.Aware;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Android.Graphics;
using Android.Hardware.Lights;
using static Android.Icu.Text.ListFormatter;
using Android.Bluetooth;
using System.Runtime.CompilerServices;

namespace Checkers
{
    internal class Board
    {

        private readonly Piece[,] pieces = new Piece[8, 8];
        public Piece this[int row, int col]
        {
            get 
            {
                if (pieces[row, col] == null) return new Man(Player.None);
                return pieces[row, col]; 
            }
            set { pieces[row, col] = value; }
        }
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

        public void Move(Player CurrentPlayer, int currentRow, int currentColumn, int newRow, int newColumn)
        {
            if (!InBoard(newRow, newColumn)) return;
            if (pieces[newRow, newColumn].player != Player.None) return;

            if (IsDiagonal(currentRow, currentColumn, newRow, newColumn))
            {
                pieces[newRow, newColumn].player = CurrentPlayer;
                pieces[currentRow, currentColumn].player = Player.None;
            }
        }
        public static bool IsDiagonal(int currentRow, int currentColumn, int newRow, int newColumn)
        {
            return Math.Abs(newRow - currentRow) == Math.Abs(newColumn - currentColumn);
        }
        public static bool InBoard(int row, int column)
        {
            return (row >= 0 && column >= 0) && (row < 8 && column < 8);
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