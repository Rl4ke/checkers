namespace Checkers
{
    internal class Result
    {
        public enum Reason
        {
            None,
            NoPlayers,
            PlayerStuck,
        }

        public Player Winner { get; set; }
        public Reason reason { get; set; }

        public Result(Player winner, Reason reason)
        {
            Winner = winner;
        }
        public Player DetermineWinner()
        {
            switch (Winner)
            {
                case Player.Black:
                    return Player.Black;
                case Player.White:
                    return Player.White;
                default:
                    return Player.None;
            }
        }
    }
}