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
            int limitsForIteration = 2000;
            // time limit
            double limitedTime = 4;
            Console.WriteLine("Hello, Welcome to AI Game of Tic Tac Toe");
            // defining the board size
            byte boardSize = 3;
            // Creating an instance of the Tic Tac Toe game
            TicTacToe game = new TicTacToe(boardSize);
            Console.WriteLine($"Displaying the Initial Board Position:\n{game.DisplayBoard()}");
            // Creating an MCTS for the Player X
            MCTS playerXMCTS = new MCTS(limitsForIteration: limitsForIteration, limitsForTime: limitedTime, boardSize: boardSize);
            // Creating an MCTS for the Player O
            MCTS playerOMCTS = new MCTS(limitsForIteration: limitsForIteration, limitsForTime: limitedTime, boardSize: boardSize);
            // Saving the Game Tree for the player X
            Tree PlayerXTree = null;
            Tree PlayerOTree = null;

            while (game.ContinueGamePlay)
            {
                // Displaying the current game player
                Console.WriteLine($"Current Player: {game.CurrentPlayer}");
                // MCTS Move
                if (game.CurrentPlayer == 'X')
                {
                    Console.WriteLine("Making AI move for Player X: ");
                    // Creating MCTS Instance for both the players
                    // Building the Game Tree for the player X
                    if (PlayerXTree == null)
                    {
                        // Building Game Tree if it's empty
                        playerXMCTS.BuildTree(boardPositions: game.BoardPositions);
                        // Stroring the game tree
                        PlayerXTree = playerXMCTS.nodeData;
                    }
                    else
                    {
                        // Finding exisiting board state in the tree
                        PlayerXTree = playerXMCTS.FindState(PlayerXTree, game.BoardPositions);
                        // Building tree using the existing tree
                        playerXMCTS.BuildTree(boardPositions: game.BoardPositions, existingTree: PlayerXTree);
                    }
                    // Displaying the Node Tree
                    Console.WriteLine("%%%%%%%%%%%% NODE TREE for X %%%%%%%%%%");
                    Console.WriteLine("graph TD;");
                    playerXMCTS.DisplayTree();
                    // Updating the board positions using AI move
                    game.BoardPositions = playerXMCTS.BestMove();
                    // Updating the game tree based on board position
                    PlayerXTree = playerXMCTS.FindState(PlayerXTree, game.BoardPositions) ;
                }
                else if (game.CurrentPlayer == 'O')
                {
                    Console.WriteLine("Making AI move for Player O: ");
                    // Creating MCTS Instance for both the players
                    // Building the Game Tree for the player X
                    if (PlayerOTree == null)
                    {
                        // Building Game Tree if it's empty
                        playerOMCTS.BuildTree(boardPositions: game.BoardPositions);
                        // Stroring the game tree
                        PlayerOTree = playerOMCTS.nodeData;
                    }
                    else
                    {
                        // Finding exisiting board state in the tree
                        PlayerOTree = playerOMCTS.FindState(PlayerOTree, game.BoardPositions);
                        // Building tree using the existing tree
                        playerOMCTS.BuildTree(boardPositions: game.BoardPositions, existingTree: PlayerOTree);
                    }
                    // Displaying the Node Tree
                    Console.WriteLine("%%%%%%%%%%%% NODE TREE for O %%%%%%%%%%");
                    playerOMCTS.DisplayTree();
                    // Updating the board positions using AI move
                    game.BoardPositions = playerOMCTS.BestMove();
                    // Updating the game tree based on board position
                    PlayerOTree = playerOMCTS.FindState(PlayerOTree, game.BoardPositions);
                }
                // Displaying game Board
                Console.WriteLine($"Game Board Position:\n{game.DisplayBoard()}");
                // Displaying the game state
                Console.WriteLine($"Continue Playing Game: {game.ContinueGamePlay}");
                // Displaying the game state
            }
            // Displaying the game state
            if (game.GameWin)
            {
                Console.WriteLine($"The game is won by Player {game.WinningPlayer}.");
            }
            Console.WriteLine("The game is draw.");
        }
    }
}
