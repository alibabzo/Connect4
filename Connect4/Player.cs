using System;
using System.Linq;

namespace Connect4
{
    interface IPlayer
    {
        int GetMove(Board currentBoard);
    }

    class Human : IPlayer
    {
        public int GetMove(Board currentBoard)
        {
            int x = 0;
            bool correct = false;
            Console.WriteLine("Which column do you want to play in?");
            while (!correct)
            {
                int.TryParse(Console.ReadLine(), out x);
                correct = currentBoard.ValidMove(x - 1);
            }
            return x - 1;
        }
    }

    class Computer : IPlayer
    {
        readonly Random rnd = new Random();
        private const int Depth = 8;

        public int GetMove(Board currentBoard)
        {
            int move;
            Negamax(currentBoard, Depth, out move);
            return move;
        }

        private int Negamax(Board board, int depth, out int bestmove)
        {
            bestmove = 0;
            int throwaway;
            if (board.GameOver || depth <= 0)
                return Evaluate(board);

            var bestScore = int.MinValue;

            var moves = board.PossibleMoves().OrderBy(x => rnd.Next()).ToArray();

            foreach (int move in moves)
            {
                board.AddPiece(move);
                int score = -Negamax(board, depth - 1, out throwaway);
                board.RemovePiece(move);
                if (score > bestScore)
                {
                    bestScore = score;
                    bestmove = move;
                }
            }
            return bestScore;
        }

        private int Evaluate(Board board)
        {
            if (board.CheckForWin(board.CurrentPlayer))
                return int.MaxValue - board.MoveCount;
            if (board.CheckForWin(Board.Opposite(board.CurrentPlayer)))
                return int.MinValue + board.MoveCount;
            if (board.CheckForDraw) return board.MoveCount;
            return 0;

        }
    }
}