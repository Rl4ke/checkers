using Android.App;
using Android.Content;
using Android.Graphics;
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
using static Checkers.Board;
using Bitmap = Android.Graphics.Bitmap;

namespace Checkers
{
    internal class Piece
    {
        public Player player { get; set; }
        public BackgroundColor bc { get; set; }
        public Piece(Player player)
        {
            this.player = player;
        }
    }
}