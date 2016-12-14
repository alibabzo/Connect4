using System;
using System.Collections.Generic;

namespace Connect4
{
    internal class Board
    {
        private readonly int RowCount;
        private readonly int ColumnCount;

        public readonly Counter[,] Rows;
        public int MoveCount { get; private set; }

        public bool CheckForDraw
        {
            get
            {
                for (int row = 0; row < RowCount; row++)
                    for (int col = 0; col < ColumnCount; col++)
                        if (Rows[row, col] == Counter.None) return false;
                return true;
            }
        }
        public bool GameOver => CheckForWin(Counter.Red) || CheckForWin(Counter.Yellow);

        public Counter CurrentPlayer => MoveCount % 2 == 0 ? Counter.Red : Counter.Yellow;

        public static Counter Opposite(Counter counter)
        {
            return counter == Counter.Red ? Counter.Yellow : Counter.Red;
        }

        public Board(int rowCount, int columnCount)
        {
            RowCount = rowCount;
            ColumnCount = columnCount;
            MoveCount = 0;
            Rows = new Counter[RowCount, ColumnCount];
            for (var row = 0; row < RowCount; row++)
                for (int col = 0; col < ColumnCount; col++)
                    Rows[row, col] = Counter.None;
        }


        public enum Counter
        {
            Red,
            Yellow,
            None
        }

        public IEnumerable<int> PossibleMoves()
        {
            var moves = new List<int>();
            for (var i = 0; i < ColumnCount; i++)
                if (ValidMove(i)) moves.Add(i);
            return moves;
        }

        public void DrawBoard()
        {
            for (var i = 1; i <= ColumnCount; i++)
                Console.Write("|{0, -3}", i);
            Console.WriteLine("|");
            for (int row = 0; row < RowCount; row++)
            {
                for (int col = 0; col < ColumnCount; col++)
                {
                    switch (Rows[row, col])                 {
                        case Counter.Red:
                            Console.BackgroundColor = ConsoleColor.Red;
                            break;
                        case Counter.Yellow:
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            break;
                        case Counter.None:
                            break;
                    }
                    Console.Write("   ");
                    Console.ResetColor();
                    Console.Write(" ");
                }
                Console.Write(Environment.NewLine);
                Console.Write(Environment.NewLine);
            }
        }

        public bool ValidMove(int column)
        {
            if (column >= ColumnCount) return false;
            if (column < 0) return false;
            for (var i = RowCount - 1; i >= 0; i--)
                if (Rows[i, column] == Counter.None)
                    return true;
            return false;
        }

        public bool AddPiece(int column)
        {
            for (var i = RowCount - 1; i >= 0; i--)
                if (Rows[i, column] == Counter.None)
                {
                    Rows[i, column] = CurrentPlayer;
                    MoveCount++;
                    return true;
                }
            return false;
        }

        public bool RemovePiece(int column)
        {
            for (var i = 0; i < RowCount; i++)
                if (Rows[i, column] != Counter.None)
                {
                    Rows[i, column] = Counter.None;
                    MoveCount--;
                    return true;
                }
            return false;
        }



        public bool CheckForWin(Counter player)
        {
            // horizontal check
            for (var j = 0; j < RowCount; j++)
                for (var i = 0; i < ColumnCount - 3; i++)
                    if ((Rows[j, i] == player) && (Rows[j, i + 1] == player) &&
                        (Rows[j, i + 2] == player) && (Rows[j, i + 3] == player))
                        return true;

            // vertical check
            for (var i = 0; i < ColumnCount; i++)
                for (var j = 0; j < RowCount - 3; j++)
                    if ((Rows[j, i] == player) && (Rows[j + 1, i] == player) &&
                        (Rows[j + 2, i] == player) && (Rows[j + 3, i] == player))
                        return true;
            // ascendingDiagonalCheck (/)
            for (var i = 3; i < RowCount; i++)
                for (var j = 0; j < ColumnCount - 3; j++)
                    if ((Rows[i, j] == player) && (Rows[i - 1, j + 1] == player) &&
                        (Rows[i - 2, j + 2] == player) && (Rows[i - 3, j + 3] == player))
                        return true;
            // descendingDiagonalCheck (\)
            for (var i = 3; i < RowCount; i++)
                for (var j = 3; j < ColumnCount; j++)
                    if ((Rows[i, j] == player) && (Rows[i - 1, j - 1] == player) &&
                        (Rows[i - 2, j - 2] == player) && (Rows[i - 3, j - 3] == player))
                        return true;
            return false;
        }
    }
}