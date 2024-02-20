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
    internal class PaintView : View
    {
        private Piece piece;
        private Context context;
        private GameManager gameManager;


        public PaintView(Context context) : base(context)
        {
            this.context = context;
            this.gameManager = new GameManager(570, 570, 8, 8);
            //SetOnTouchListener(new MoveTouchListener(gameManager, this));
        }
        protected override void OnDraw(Canvas canvas)
        {
            GameManager gm = new GameManager(570,570,8,8);
            base.OnDraw(canvas);
            gm.Draw(canvas);
            Cell selectedCell = gm.GetCellAtCoordinates(100, 100);
            //Bitmap pieceBitmap = BitmapFactory.DecodeResource(context.Resources, Resource.Drawable.record);
            //piece = new Piece(0, 0, pieceBitmap);
            //canvas.DrawBitmap(pieceBitmap, 0,0, null);
            //piece.Draw(canvas);
        }
        public override bool OnTouchEvent(MotionEvent e)
        {
            if (e.Action == MotionEventActions.Move)
            {
                float x = e.GetX();
            }
            return gameManager.OnTouch(this,e);
        }

    }

}