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
     class GameManager
    {
        public Board BoardGame { get; set;}
        public Player player = Player.White; // saves the current player
        
        public enum Player
        {
            Black,
            White,
            None
        }
        public GameManager(int w, int h, int lines, int cols)
        {
            int left = (int)(w * 0.1);
            int top = (int)(h * 0.1);
            int width = (int)(w * 0.8);
            int height = (int)(h * 0.8);
            BoardGame = new Board(left, top, width, height, lines, cols,this);
        }
        public bool OnTouch(View v, MotionEvent e)
        {
            if (e.Action == Android.Views.MotionEventActions.Down)
            {
                int selectedX = (int)e.GetX();
                int selectedY = (int)e.GetY();
                Cell CurrentselectedCell = this.GetCellAtCoordinates(selectedX, selectedY);

                if (CurrentselectedCell != null && CurrentselectedCell.kind == Cell.KindEnum.Occupied)
                {

                    int destinatedCellX = (int)e.GetX(), destinatedCellY = (int)e.GetY();
                    Cell DestinatedCell = this.GetCellAtCoordinates(selectedX, selectedY);

                    if (DestinatedCell.kind == Cell.KindEnum.Empty)
                        BoardGame.MovePiece(selectedX, selectedY, destinatedCellX, destinatedCellY);

                    v.Invalidate();
                }
            }
            return false;
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