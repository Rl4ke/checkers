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
using static Android.Views.View;

namespace Checkers
{
    internal class MoveTouchListener : Java.Lang.Object, IOnTouchListener
    {
        private GameManager gameManager;
        private PaintView paintView;

        public MoveTouchListener(GameManager gameManager, PaintView paintView)
        {
            this.gameManager = gameManager;
            this.paintView = paintView;
        }
        public bool OnTouch(View v, MotionEvent e)
        {
            if (e.Action == Android.Views.MotionEventActions.Down)
            {
                
                int selectedX = (int)e.GetX();
                int selectedY = (int)e.GetY();
                Cell CurrentselectedCell = gameManager.GetCellAtCoordinates(selectedX, selectedY);
                
                if (CurrentselectedCell != null && CurrentselectedCell.kind == Cell.KindEnum.Empty)
                {
                    int destinatedCellX = (int)e.GetX(), destinatedCellY = (int)e.GetY();
                
                    int row = 0, col = 0;
                
                    for (int i = 0; i < gameManager.BoardGame.Lines;i++)
                    {
                        for(int j = 0; j < gameManager.BoardGame.Cols;j++)
                        {
                            if (gameManager.BoardGame.arr[i, j].Equals(gameManager.BoardGame.arr[destinatedCellX, destinatedCellY]))
                            {
                                row = i;
                                col = j;
                            }
                        }
                    }
                    Cell destinationCell = gameManager.BoardGame.arr[row,col];
                
                    // updating the cell contents
                    destinationCell.b = CurrentselectedCell.b;
                    CurrentselectedCell.Cancel();
                    v.Invalidate();
                }
            }
            return false;
        }
    }
}