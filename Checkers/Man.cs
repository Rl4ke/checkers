using Android.App;
using Android.Content;
using Android.Graphics;
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
        private PieceType Type;

        public Man(int x, int y, Bitmap b) : base(x,y,b)
        {
            this.X = x;
            this.Y = y;
            this.manBitmap = b;
            if(this.manBitmap != null ) 
            {
                if (this.manBitmap.Equals(Android.Graphics.BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.darkredman)))
                    Type = PieceType.DarkMan;
                else Type = PieceType.WhiteMan;
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