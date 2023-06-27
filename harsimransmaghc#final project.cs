using System;

namespace ConnectFour
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }

    class Game
    {
        private Board board;
        private Player player1;
        private Player player2;
        private Player currentPlayer;

        public void Start()
        {
            board = new Board();
            player1 = new HumanPlayer('X');
            player2 = new HumanPlayer('O');
            currentPlayer = player1;

            while (true)
            {
                board.PrintBoard();
                int column = currentPlayer.GetMove();

                if (!board.IsValidMove(column))
                {
                    Console.WriteLine("Invalid move! Please try again.");
                    continue;
                }

                board.MakeMove(column, currentPlayer.Symbol);

                if (board.IsGameWon(currentPlayer.Symbol))
                {
                    board.PrintBoard();
                    Console.WriteLine("Player {0} wins!", currentPlayer.Symbol);
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                    board.ResetBoard();
                }
                else if (board.IsBoardFull())
                {
                    board.PrintBoard();
                    Console.WriteLine("It's a draw!");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                    board.ResetBoard();
                }
                else
                {
                    currentPlayer = (currentPlayer == player1) ? player2 : player1;
                }
            }
        }
    }

    class Board
    {
        private char[,] board;

        public Board()
        {
            board = new char[6, 7];

            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    board[row, col] = ' ';
                }
            }
        }

        public void PrintBoard()
        {
            Console.Clear();

            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    Console.Write("|{0}", board[row, col]);
                }

                Console.WriteLine("|");
                Console.WriteLine("---------------");
            }
        }

        public bool IsValidMove(int column)
        {
            return board[0, column] == ' ';
        }

        public void MakeMove(int column, char symbol)
        {
            for (int row = 5; row >= 0; row--)
            {
                if (board[row, column] == ' ')
                {
                    board[row, column] = symbol;
                    break;
                }
            }
        }

        public bool IsGameWon(char symbol)
        {
            // Check rows
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (board[row, col] != ' ' &&
                        board[row, col] == board[row, col + 1] &&
                        board[row, col] == board[row, col + 2] &&
                        board[row, col] == board[row, col + 3])
                    {
                        return true;
                    }
                }
            }

            // Check columns
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    if (board[row, col] != ' ' &&
                        board[row, col] == board[row + 1, col] &&
                        board[row, col] == board[row + 2, col] &&
                        board[row, col] == board[row + 3, col])
                    {
                        return true;
                    }
                }
            }

            // Check diagonals (top-left to bottom-right)
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (board[row, col] != ' ' &&
                        board[row, col] == board[row + 1, col + 1] &&
                        board[row, col] == board[row + 2, col + 2] &&
                        board[row, col] == board[row + 3, col + 3])
                    {
                        return true;
                    }
                }
            }

            // Check diagonals (top-right to bottom-left)
            for (int row = 0; row < 3; row++)
            {
                for (int col = 3; col < 7; col++)
                {
                    if (board[row, col] != ' ' &&
                        board[row, col] == board[row + 1, col - 1] &&
                        board[row, col] == board[row + 2, col - 2] &&
                        board[row, col] == board[row + 3, col - 3])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool IsBoardFull()
        {
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    if (board[row, col] == ' ')
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void ResetBoard()
        {
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    board[row, col] = ' ';
                }
            }
        }
    }

    abstract class Player
    {
        public char Symbol { get; }

        public Player(char symbol)
        {
            Symbol = symbol;
        }

        public abstract int GetMove();
    }

    class HumanPlayer : Player
    {
        public HumanPlayer(char symbol) : base(symbol)
        {
        }

        public override int GetMove()
        {
            Console.Write("Player {0}, enter the column number to drop your symbol: ", Symbol);
            int column = int.Parse(Console.ReadLine());
            return column;
        }
    }
}
