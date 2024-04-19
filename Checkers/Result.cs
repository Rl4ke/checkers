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
    public class Result
    {
        public enum Reason
        {
            None,
            NoPlayers,
            PlayerStuck,
        }

        public Player Winner { get; set; }
        public Reason reason { get; set; }

        public Result(Player winner, Reason reason)
        {
            Winner = winner;
        }
        public Player DetermineWinner()
        {
            switch (Winner)
            {
                case Player.Black:
                    return Player.Black;
                case Player.White:
                    return Player.White;
                default:
                    return Player.None;
            }
        }
    }
}