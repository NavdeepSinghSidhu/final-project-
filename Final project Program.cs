using System;

namespace ConnectFour
{
    enum Player
    {
        None,
        Player1,
        Player2
    }

    class GameBoard
    {
        private const int Rows = 6; 
        private const int Columns = 7;
        private readonly Player[,] board;

        public GameBoard()
        {
            board = new Player[Rows, Columns];
        }

        public bool MakeMove(int column, Player player)
        {
            if (column < 1 || column > Columns || board[0, column - 1] != Player.None)
                return false;

            for (int row = Rows - 1; row >= 0; row--)
            {
                if (board[row, column - 1] == Player.None)
                {
                    board[row, column - 1] = player;
                    return true;
                }
            }

            return false;
        }

        public bool CheckWin(Player player)
        {
            // Check horizontal
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col <= Columns - 4; col++)
                {
                    if (board[row, col] == player && board[row, col + 1] == player &&
                        board[row, col + 2] == player && board[row, col + 3] == player)
                        return true;
                }
            }

            // Check vertical
            for (int row = 0; row <= Rows - 4; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    if (board[row, col] == player && board[row + 1, col] == player &&
                        board[row + 2, col] == player && board[row + 3, col] == player)
                        return true;
                }
            }

            // Check diagonal (left to right)
            for (int row = 0; row <= Rows - 4; row++)
            {
                for (int col = 0; col <= Columns - 4; col++)
                {
                    if (board[row, col] == player && board[row + 1, col + 1] == player &&
                        board[row + 2, col + 2] == player && board[row + 3, col + 3] == player)
                        return true;
                }
            }

            // Check diagonal (right to left)
            for (int row = 0; row <= Rows - 4; row++)
            {
                for (int col = 3; col < Columns; col++)
                {
                    if (board[row, col] == player && board[row + 1, col - 1] == player &&
                        board[row + 2, col - 2] == player && board[row + 3, col - 3] == player)
                        return true;
                }
            }

            return false;
        }

        public bool IsBoardFull()
        {
            for (int col = 0; col < Columns; col++)
            {
                if (board[0, col] == Player.None)
                    return false;
            }

            return true;
        }

        public void PrintBoard()
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    Console.Write(board[row, col] == Player.None ? "- " : (board[row, col] == Player.Player1 ? "X " : "O "));
                }
                Console.WriteLine();
            }
            Console.WriteLine("1 2 3 4 5 6 7"); // Adjusted column numbers
        }
    }

    class GameController
    {
        private readonly GameBoard board;
        private Player currentPlayer;

        public GameController()
        {
            board = new GameBoard();
            currentPlayer = Player.Player1;
        }

        public void StartGame()
        {
            Console.WriteLine("Connect Four - Let's Play!");

            while (true)
            {
                board.PrintBoard();
                Console.WriteLine($"It's Player {(currentPlayer == Player.Player1 ? "1" : "2")}'s turn.");
                Console.Write("Enter the column number (1-7) to make a move: ");

                if (int.TryParse(Console.ReadLine(), out int column))
                {
                    if (board.MakeMove(column, currentPlayer))
                    {
                        if (board.CheckWin(currentPlayer))
                        {
                            Console.WriteLine($"Player {(currentPlayer == Player.Player1 ? "1" : "2")} wins!");
                            break;
                        }
                        else if (board.IsBoardFull())
                        {
                            Console.WriteLine("It's a draw!");
                            break;
                        }

                        currentPlayer = currentPlayer == Player.Player1 ? Player.Player2 : Player.Player1;
                    }
                    else
                    {
                        Console.WriteLine("Invalid move. Try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Try again.");
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            GameController game = new GameController();
            game.StartGame();
        }
    }
}
