using GameEnvironment;
using MCTSLib;

namespace GamePlay
{
    internal class Program
    {
        public static void NextIteration(TicTacToe game)
        {
            // Displaying the current game player
            Console.WriteLine($"Current Player: {game.CurrentPlayer}");
            // Making a Random Move
            game.MakeRandomMove();
            // Dsiplaying game Board
            Console.WriteLine($"Game Board Position:\n{game.DisplayBoard()}");
            // Displaying the game state
            Console.WriteLine($"Continue Playing Game: {game.ContinueGamePlay}");
        }

        /*static void Main(string[] args)
        {
            Console.WriteLine("Hello, Welcome to AI Game of Tic Tac Toe");
            byte size = 0;
            // Fetching the correct board size
            while (true)
            {
                Console.Write("Enter the board size: ");
                string? readBoardSize = Console.ReadLine();
                // converting string to byte
                try
                {
                    size = byte.Parse(readBoardSize);
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input provided !!\nPlease enter the correct number value.");
                    //continue;
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("Please enter a number.");
                    //continue;
                }
            }
            // Creating an instance of the Tic Tac Toe game
            TicTacToe game = new TicTacToe(sizeOfBoard: size);
            Console.WriteLine($"Displaying the Initial board position:\n{game.DisplayBoard()}");

            while (game.ContinueGamePlay) 
            {
                NextIteration(game); 
            }
            
            if (game.GameWin)
            {
                Console.WriteLine($"Congrats, Player {game.WinningPlayer} won the game");
            }
            else if (!game.GameDraw)
            {
                Console.WriteLine($"The Game is draw");
            }
        }*/

        public static void Main(string[] args)
        {
            // iteration limit
            int limitsForIteration = 5000;
            // time limit
            double limitedTime = 4;
            Console.WriteLine("Hello, Welcome to AI Game of Tic Tac Toe");
            // defining the board size
            byte boardSize = 3;
            // Creating an instance of the Tic Tac Toe game
            TicTacToe game = new TicTacToe(boardSize);
            Console.WriteLine($"Displaying the Initial Board Position:\n{game.DisplayBoard()}");
            // Creating MCTS Instance for both the players
            MCTS playerXMCTS = new MCTS(limitsForIteration: limitsForIteration, limitsForTime: limitedTime, boardSize: boardSize);
            MCTS playerOMCTS = new MCTS(limitsForIteration: limitsForIteration, limitsForTime: limitedTime, boardSize: boardSize);

            while (game.ContinueGamePlay)
            {
                // Displaying the current game player
                Console.WriteLine($"Current Player: {game.CurrentPlayer}");
                // MCTS Move
                if (game.CurrentPlayer == 'X')
                {
                    Console.WriteLine("Making AI move for Player X: ");
                    playerXMCTS.BuildTree(boardPositions: game.BoardPositions);
                    // Updating the board positions using AI move
                    game.BoardPositions = playerXMCTS.BestMove();
                }
                else if (game.CurrentPlayer == 'O')
                {
                    Console.WriteLine("Making AI move for Player O: ");
                    playerOMCTS.BuildTree(boardPositions: game.BoardPositions);
                    // Updating the board positions using AI move
                    game.BoardPositions = playerOMCTS.BestMove();
                }
                // Dsiplaying game Board
                Console.WriteLine($"Game Board Position:\n{game.DisplayBoard()}");
                // Displaying the game state
                Console.WriteLine($"Continue Playing Game: {game.ContinueGamePlay}");
            }
        }
    }
}
