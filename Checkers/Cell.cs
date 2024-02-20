using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Checkers
{
    internal class Cell
    {
        public int left;
        public int top;
        public int width;
        public int height;
        public KindEnum kind;
        public Android.Graphics.Bitmap b;
        public Man Man { get; set; }

        public enum KindEnum
        {
            Empty,
            Occupied
        }

        public Cell(int left, int top, int width, int height, KindEnum kind, Android.Graphics.Bitmap b, Man man)
        {
            this.left = left;
            this.top = top;
            this.width = width;
            this.height = height;
            this.kind = kind;
            this.b = b;
            Man = man;
        }

        public void Cancel()
        {
            this.kind = KindEnum.Empty;
            this.Man = null;
        }


    }
}