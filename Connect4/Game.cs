using System;

namespace Connect4
{
    class Game
    {
        private const int RowCount = 6;
        private const int ColumnCount = 7;
        private readonly IPlayer Player1;
        private readonly IPlayer Player2;
        private readonly Board board = new Board(RowCount, ColumnCount);

        public Game(IPlayer player1, IPlayer player2)
        {
            Player1 = player1;
            Player2 = player2;
        }

        private IPlayer CurrentPlayer => board.MoveCount % 2 == 0 ? Player1 : Player2;

        public void Play()
        {
            while (true)
            {
                Console.Clear();
                board.DrawBoard();
                if (board.CheckForDraw)
                {
                    Console.WriteLine("It's a draw!");
                    break;
                }
                if (board.CheckForWin(Board.Opposite(board.CurrentPlayer)))
                {
                    Console.WriteLine("Player {0} wins!", Board.Opposite(board.CurrentPlayer));
                    break;
                }
                Console.WriteLine(board.CurrentPlayer + "'s turn");
                board.AddPiece(CurrentPlayer.GetMove(board));
            }
        }
    }
}