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
        Player player = Player.White;
        private GameState gameState;
        private Position selectedPos = null;
        private Board board = new Board();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.board);

            gameState = new GameState(Player.White, board);

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
                    buttonView.SetBackgroundColor(Resources.GetColor((j + i) % 2 == 1 ? Resource.Color.m3_ref_palette_black : Resource.Color.m3_ref_palette_white, Theme));
                    buttonView.SetPadding(0, 0, 0, 0);
                    buttonView.TransitionName = $"{i}{j}";

                    buttonView.Click += SquareClick;
                    // Add the buttonView to the container
                    container.AddView(buttonView);

                    // Add the container to the checkersBoard GridLayout
                    checkersBoard.AddView(container);

                }
            }
            DrawPieces();
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
                    if (piece.player == Player.White) ib.SetImageDrawable(Resources.GetDrawable(Resource.Drawable.whiteman, Theme));
                    else if(piece.player == Player.Black) ib.SetImageDrawable(Resources.GetDrawable(Resource.Drawable.darkredman, Theme));
                    else ib.SetImageDrawable(null);
                }
            }
        }

        private void SquareClick(object sender, EventArgs e)
        {
            if (gameState.CurrentPlayer != player) return;

            int[] sq = Board.PositionFromStr(((Android.Views.View)sender).TransitionName);
            Position pos = new Position(sq[0], sq[1]);

            if (selectedPos == null) 
            {
                From(pos);
            }
            else
            {
                To(pos);
            }
        }

        private void From(Position pos)
        {
            selectedPos = pos;
        }

        private void To(Position pos)
        {
            board.Move(player, selectedPos.Row, selectedPos.Column, pos.Row, pos.Column);
        }

    }
}