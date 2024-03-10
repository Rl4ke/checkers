using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using ImageButton = Android.Widget.ImageButton;

namespace Checkers
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        GridLayout checkersBoard;
        Player player = Player.Black;
        private GameState gameState;
        private Position toPos = null, fromPos = null;
        private Board board = new Board();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.board);

            gameState = new GameState(Player.Black, board);

            checkersBoard = FindViewById<GridLayout>(Resource.Id.checkersBoard);

            BuildBoard();
        }

        void BuildBoard()
        {
            int width = Resources.DisplayMetrics.WidthPixels;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    FrameLayout container = new FrameLayout(this)
                    {
                        LayoutParameters = new LinearLayout.LayoutParams(width / 8, width / 8),
                        Background = Resources.GetDrawable(Resource.Drawable.border, Theme)
                    };

                    ImageButton buttonView = new ImageButton(this)
                    {
                        LayoutParameters = new FrameLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent),
                    };
                    buttonView.SetScaleType(ImageView.ScaleType.FitCenter);
                    buttonView.SetPadding(0, 0, 0, 0);
                    buttonView.TransitionName = $"{i}{j}";

                    if ((j + i) % 2 == 1)
                    {
                        buttonView.SetBackgroundColor(Resources.GetColor(Resource.Color.m3_ref_palette_black));
                    }
                    else
                    {
                        buttonView.SetBackgroundColor(Resources.GetColor(Resource.Color.m3_ref_palette_white));
                    }

                    buttonView.Click += SquareClick;

                    container.AddView(buttonView);
                    checkersBoard.AddView(container);
                }
            }
            DrawPieces();
        }

        private void SquareClick(object sender, EventArgs e)
        {
            //if (gameState.CurrentPlayer != player) return;

            string transitionName = ((View)sender).TransitionName;
            int[] sq = Board.PositionFromStr(transitionName);
            Position pos = new Position(sq[0], sq[1]);

            if (fromPos == null)
            {
                fromPos = pos;
                return;
            }
            if (toPos == null)
            {
                toPos = pos; 
                board.Move(fromPos, toPos);
                fromPos = null;
                toPos = null;
                DrawPieces();
            }
        }

        private void DrawPieces()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    ImageButton ib = (ImageButton)((FrameLayout)checkersBoard.GetChildAt(8 * i + j)).GetChildAt(0);

                    Piece piece = board[i, j];

                    if (piece == null) piece = new Piece(Player.None);

                    if (piece.player == Player.White)
                    {
                        if (piece.isKing) ib.SetImageDrawable(Resources.GetDrawable(Resource.Drawable.whiteking, Theme));
                        else ib.SetImageDrawable(Resources.GetDrawable(Resource.Drawable.whiteman, Theme));

                    }
                    else if (piece.player == Player.Black) 
                    {
                        if (piece.isKing) ib.SetImageDrawable(Resources.GetDrawable(Resource.Drawable.darkredking, Theme));
                        else ib.SetImageDrawable(Resources.GetDrawable(Resource.Drawable.darkredman, Theme)); 
                    }
                    else ib.SetImageDrawable(null);
                }
            }
        }

    }
}