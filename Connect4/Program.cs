using System;

namespace Connect4
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                IPlayer player1;
                IPlayer player2;
                Console.WriteLine("Which type of game?");
                Console.WriteLine("1: 1 player (vs AI)");
                Console.WriteLine("2: 2 player");
                Console.WriteLine("3: AI vs AI");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        player2 = new Human();
                        player1 = new Computer();
                        break;
                    case '2':
                        player1 = new Human();
                        player2 = new Human();
                        break;
                    case '3':
                        player1 = new Computer();
                        player2 = new Computer();
                        break;
                    default:
                        continue;
                }
                var game = new Game(player1, player2);
                game.Play();
                Console.WriteLine("Press q to exit");
                if (Console.ReadLine().ToLower() == "q") break;
            }
        }
    }
}
