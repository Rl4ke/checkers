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
        public Bitmap manBitmap { get; set; }
        private Context context;
        public Player player { get; set; }

        public Man(Player player) : base(player)
        {
            if (this.player != Player.None)
            {
                if (this.player == Player.White) manBitmap = BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.whiteman);
                else manBitmap = BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.darkredman);
            }
            else manBitmap = null;
        }
        public void Draw(Canvas c)
        {
            if (c != null && manBitmap != null)
            {
                //c.DrawBitmapMesh(manBitmap, null);
            }
        }

    }
}