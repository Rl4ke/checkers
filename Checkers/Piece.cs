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
using Bitmap = Android.Graphics.Bitmap;

namespace Checkers
{
    internal class Piece
    {
        private int X { get; set; }
        private int Y { get; set; }

        private Bitmap bitmap;
        public PieceType Type { get; set; }
        public Piece(int x, int y, Bitmap bitmap)
        {
            this.X = x; 
            this.Y = y;
            this.bitmap = bitmap;
            //this.Type = type;
        }
    }
    internal enum PieceType
    {
        WhiteMan,
        DarkMan
    }
}