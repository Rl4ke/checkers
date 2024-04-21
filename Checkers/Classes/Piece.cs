
namespace Checkers
{
    internal class Piece
    {
        public Player player { get; set; }
        public bool isKing = false;
        public Piece(Player player)
        {
            this.player = player;
        }
    }
}