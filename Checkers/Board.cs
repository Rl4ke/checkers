using Android.App;
using Android.Content;
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
using Android.Graphics;

namespace Checkers
{
    internal class Board
    {
        public int Left { get; }
        public int Top { get; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Lines { get; set; }
        public int Cols { get; set; }
        public int Cwidth { get; set; }
        public int Cheight { get; set; }
        public Cell[,] arr;

        public Board(int left, int top, int width, int height, int lines, int cols)
        {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
            Lines = lines;
            Cols = cols;
            Cwidth = 125;
            Cheight = 160;
            Initialize();
        }
        private void Initialize()
        {
            arr = new Cell[Lines, Cols];
            int x = Left, y = Top;
            Android.Graphics.Bitmap b1, b2;
            b1 = Android.Graphics.BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.whitesquare);
            b1 = Android.Graphics.Bitmap.CreateScaledBitmap(b1, Cwidth, Cheight, false);
            b2 = Android.Graphics.BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.blacksquare);
            b2 = Android.Graphics.Bitmap.CreateScaledBitmap(b2, Cwidth, Cheight, false);

            for (int i = 0; i < Lines; i++)
            {
                for(int j = 0; j < Cols; j++)
                {
                    arr[i, j] = new Cell(x, y, Cwidth, Cheight, Cell.KindEnum.Empty, (i + j) % 2 == 0 ? b1 : b2,null);
                    //arr[i, j].b = (i + j) % 2 == 0 ? b1 : b2;
                    
                    x += Cwidth;
                }
                y += Cheight;
                x = Left;
            }

        }

        public void Draw(Canvas canvas)
        {
            Paint paint = new Paint();
            Android.Graphics.Bitmap recordBitmap1, recordBitmap2,b;
            recordBitmap1 = Android.Graphics.BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.darkredman);
            recordBitmap2 = Android.Graphics.BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.whiteman);
            b = Android.Graphics.BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.blacksquare);
            b = Android.Graphics.Bitmap.CreateScaledBitmap(b, Cwidth, Cheight, false);

            for (int i = 0; i < Lines; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                       Cell currentCell = arr[i, j];
            
                       // Draw a rectangle for each cell
                       Rect rect = new Rect(currentCell.left, currentCell.top,
                                             currentCell.left + currentCell.width, currentCell.top + currentCell.height);

            
                       // Draw the cell on the canvas
                       canvas.DrawBitmap(currentCell.b, null, rect, paint);
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    if ((i + j) % 2 != 0)
                    {
                        // Draw the square
                        Rect rect = new Rect(arr[i, j].left, arr[i, j].top,
                                              arr[i, j].left + arr[i, j].width, arr[i, j].top + arr[i, j].height);
                        canvas.DrawBitmap(arr[i, j].b, null, rect, paint);

                        // Draw the "darkredman" image on top
                        Rect recordRect = new Rect(arr[i, j].left, arr[i, j].top,
                                                   arr[i, j].left + arr[i, j].width, arr[i, j].top + arr[i, j].height);
                        Man man = new Man(i, j, recordBitmap1);
                        canvas.DrawBitmap(recordBitmap1, null, recordRect, paint);
                    }

                }
            }
            for(int i = 3; i < 5; i++) 
            {
                for (int j = 0; j < Cols; j++)
                {
                    if ((i + j) % 2 != 0)
                    {
                        // Draw the square
                        Rect rect = new Rect(arr[i, j].left, arr[i, j].top,
                                              arr[i, j].left + arr[i, j].width, arr[i, j].top + arr[i, j].height);
                        canvas.DrawBitmap(arr[i, j].b, null, rect, paint);
                        arr[i, j] = new Cell(i, j, Cwidth, Cheight, Cell.KindEnum.Empty,b, null);

                    }

                }
            }
            for (int i = 5; i < 8; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    if ((i + j) % 2 != 0)
                    {
                        // Draw the square
                        Rect rect = new Rect(arr[i, j].left, arr[i, j].top,
                                              arr[i, j].left + arr[i, j].width, arr[i, j].top + arr[i, j].height);
                        canvas.DrawBitmap(arr[i, j].b, null, rect, paint);
            
                        // Draw the "whiteman" on the bottom of the board
                        Rect recordRect = new Rect(arr[i, j].left, arr[i, j].top,
                                                   arr[i, j].left + arr[i, j].width, arr[i, j].top + arr[i, j].height);
                        Man man = new Man(i, j, recordBitmap2);
                        canvas.DrawBitmap(recordBitmap2, null, recordRect, paint);
                    }
            
                }
            }
        }
        public Cell GetCellAtCoordinates(int x, int y)
        {
            int row = (y - Top) / Cheight;
            int col = (x - Left) / Cwidth;

            // Check if it's within bounds
            if (row >= 0 && row < Lines && col >= 0 && col < Cols)
            {
                return arr[row, col];
            }

            return null;
        }
        public void Move()
        {

        }
        public Tuple<int, int> GetSquareIndicesAtCoordinates(int x, int y)
        {
            // Calculate the row and column indices based on touch coordinates
            int row = (y - Top) / Cheight;
            int col = (x - Left) / Cwidth;

            // Check if it's within bounds
            if (row >= 0 && row < Lines && col >= 0 && col < Cols)
            {
                return new Tuple<int, int>(row, col);
            }

            // the coordinates are outside the board
            return null;
        }
    }
}