using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers
{
    internal class GameState
    {
        public Board Board { get; set; }
        public Player CurrentPlayer { get; set; }
        public GameState(Player player, Board board)
        {
            CurrentPlayer = player;
            Board = board;
        }
        public int Direction()
        {
            int direction = 0;
            if (CurrentPlayer == Player.Black)
                direction = -1;
            else
            {
                direction = 1;
            }
            return direction;
        }
    }
}