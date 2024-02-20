using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Nio.Channels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using Bitmap = Android.Graphics.Bitmap;
using Context = Android.Content.Context;

namespace Checkers
{
    internal class Man : Piece
    {
        private int X;
        private int Y;
        public Bitmap manBitmap { get; set; }
        private Context context;
        public GameManager.Player player { get; set; }

        public Man(int x, int y, GameManager.Player player) : base(x,y, player)
        {
            this.X = x;
            this.Y = y;
            if(this.player != GameManager.Player.None) 
            {
                if (this.player == GameManager.Player.White) manBitmap = BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.whiteman);
                else manBitmap = BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.darkredman);
            }
        }
        public void Draw(Canvas c)
        {
            if (c != null && manBitmap != null)
            {
                c.DrawBitmap(manBitmap, X, Y, null);
            }
        }

    }
}