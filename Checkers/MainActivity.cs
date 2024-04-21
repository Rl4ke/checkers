using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
//using Firebase;
using ImageButton = Android.Widget.ImageButton;
using Android.Content;


namespace Checkers
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        GridLayout checkersBoard;
        private Position toPos = null, fromPos = null;
        private Board board = new Board();
        private TextView winnerTextView;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.board);
            checkersBoard = FindViewById<GridLayout>(Resource.Id.checkersBoard);
            winnerTextView = FindViewById<TextView>(Resource.Id.winner);
            Result result = new Result(Player.White, Result.Reason.NoPlayers);
            Player winner = board.winner;

            if (winner != Player.None)
            {
                String winnerText = "Winner: " + (winner == Player.Black ? "Black" : "White");
                winnerTextView.Text = winnerText;
            }
            else
            {
                winnerTextView.Text = "No winner";
            }
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
            string transitionName = ((View)sender).TransitionName;
            int[] sq = Board.PositionFromStr(transitionName);
            Position pos = new Position(sq[0], sq[1]);
            Piece p = board.pieces[pos.Row, pos.Column];

            if (p != null && p.player != board.player)
            {
                fromPos = null;
                toPos = null;
                return;
            }

            if (p == null && fromPos == null) return;

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

        private void BtnStart_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MusicActivity));
            StartActivity(intent);
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