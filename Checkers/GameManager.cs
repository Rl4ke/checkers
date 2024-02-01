using Android.App;
using Android.Content;
using Android.Graphics;
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
    internal class GameManager
    {
        public Board BoardGame { get; set;}

        public GameManager(int w, int h, int lines, int cols)
        {
            int left = (int)(w * 0.1);
            int top = (int)(h * 0.1);
            int width = (int)(w * 0.8);
            int height = (int)(h * 0.8);
            BoardGame = new Board(left, top, width, height, lines, cols);
        }
        public Cell GetCellAtCoordinates(int x, int y)
        {
            return BoardGame.GetCellAtCoordinates(x, y);
        }
        public void Draw(Canvas canvas)
        {
            BoardGame.Draw(canvas);
        }
    }
}