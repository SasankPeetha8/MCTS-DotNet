using System;

namespace GameEnvironment
{
    /// <summary>
    /// This class is used to define the TicTacToe game and it's logics.
    /// </summary>
    public class TicTacToe
    {
        // Defining board size
        private byte boardSize;
        // Defining boardPositions
        private char[,] boardPositions;
        // Defining players
        public char Player1;
        public char Player2;
        // Empty board character
        private char emptyBoardCharacter = '-';

        /// <summary>
        /// This constructor allows you to create a instance of type TicTacToe.
        /// </summary>
        /// <param name="sizeOfBoard">A byte parameter is required to define the size of the board.</param>
        /// <param name="playerOne">An optional char parameter is required to define the First player. If no value is provided, then by default the player one will be 'X'.</param>
        /// <param name="playerTwo">An optional char parameter is required to define the Second player. If no value is provided, then by default the player two will be 'O'.</param>
        public TicTacToe(byte sizeOfBoard, char playerOne='X', char playerTwo='O')
        {
            
            // Defining the game board size
            boardSize = sizeOfBoard;
            // Creating the board and filling initial values
            boardPositions = new char[boardSize, boardSize];
            // Iterating through the 2D array.
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    boardPositions[i,j] = '-';
                }
            }
            // Defining players
            Player1 = playerOne;
            Player2 = playerTwo;
        }

        /// <summary>
        /// This property is used to fetch the current positions of the board.
        /// </summary>
        public char[,] BoardPositions
        {
            get { return boardPositions; }
            set 
            { 
                if ((boardPositions.GetLength(0) == value.GetLength(0)) && (boardPositions.GetLength(1) == value.GetLength(1)))
                {
                    boardPositions = value;
                }
                else
                {
                    throw new Exception(message: "Invalid Board Position provided.");
                }
            }
        }

        /// <summary>
        /// This method is used to fetch the current positions of the board in the form of a string.
        /// </summary>
        /// <returns>It returns a string which spans over multiple lines.</returns>
        public string DisplayBoard()
        {
            // Building the string
            string boardString = "";
            byte counter = 0;
            foreach (char element in boardPositions)
            {
                boardString = boardString + element + " ";
                counter++;
                if (counter == boardSize)
                {
                    boardString += "\n";
                    counter = 0;
                }

            }
            return boardString;
        }

        /// <summary>
        /// This property is used to display the player who has won the game.
        /// </summary>
        public char WinningPlayer
        {
            get { return CurrentPlayer == Player1 ? Player2 : Player1; }
        }

        /// <summary>
        /// This property is used to display the current player who has to make their move.
        /// </summary>
        public char CurrentPlayer
        {
            get
            {
                byte playerOnePositions = 0;
                byte playerTwoPositions = 0;
                foreach(char element in boardPositions)
                {
                    if (element == Player1) { playerOnePositions++; }
                    else if (element == Player2) { playerTwoPositions++; }
                }
                if (playerOnePositions == playerTwoPositions) { return Player1; }
                else if (playerOnePositions == playerTwoPositions + 1) { return Player2; }
                else
                {
                    throw new Exception(message: "Invalid Game Board Positions Detected");
                }
            }
        }

        /// <summary>
        /// This method is used to fetch a position available on the board randomly.
        /// </summary>
        /// <returns>It returns a tuple of type 'byte' which represents the chosen position of a 2D array.</returns>
        public (byte, byte) GetRandomPosition()
        {
            int randomPosition = 0;
            List<(byte, byte)> positionsAvailable = AvailablePositions();
            Random randomGen = new Random();
            randomPosition = randomGen.Next(randomPosition, positionsAvailable.Count);
            return positionsAvailable[randomPosition];
        }

        /// <summary>
        /// This method is used to make a random move for the current player.
        /// </summary>
        public void MakeRandomMove()
        {
            // Getting current player
            char currentGamePlayer = CurrentPlayer;
            (byte i, byte j) = GetRandomPosition();
            BoardPositions[i, j] = currentGamePlayer;
        }

        /// <summary>
        /// This method is used to fetch the list of available positions in the current game.
        /// </summary>
        /// <returns>It returns a List of tuples of type 'byte' where each tuple in the list represents the available position in a 2D array.</returns>
        public List<(byte, byte)> AvailablePositions()
        {
            List<(byte, byte)> positionsAvailable = new List<(byte, byte)> ();
            for (byte i = 0; i < BoardPositions.GetLength(0); i++)
            {
                for ( byte j = 0; j < BoardPositions.GetLength(1); j++)
                {
                    if (BoardPositions[i,j] == emptyBoardCharacter) 
                    { 
                        positionsAvailable.Add((i,j)); 
                    }
                }
            }
            return positionsAvailable;
        }

        /// <summary>
        /// This property is uses a bool to indicate if the game is draw or not.
        /// </summary>
        public bool GameDraw
        {
            get
            {
                if (GameWin) { return false; }
                else
                {
                    string availablePositions = DisplayBoard();
                    return !availablePositions.Contains(emptyBoardCharacter);
                }

            }
        }

        /// <summary>
        /// This property uses a bool to indicate if the game can continue or not.
        /// </summary>
        public bool ContinueGamePlay
        {
            get { return !GameWin && !GameDraw; }
        }

        /// <summary>
        /// This method is used to check if the string has winning game pattern or not.
        /// </summary>
        /// <param name="positions">This parameter requires the positions of game board in the form of a string.</param>
        /// <returns>It returns a boolean value which indicates whether the string has winning game pattern  of not.</returns>
        public bool ValidateString(string positions)
        {
            string checkPlayerOne = positions.Replace(Player1, ' ').Trim();
            string checkPlayerTwo = positions.Replace(Player2, ' ').Trim();
            if (checkPlayerOne.Length == 0) { return true; }
            else if (checkPlayerTwo.Length == 0) { return true; }
            else { return false; }
        }

        /// <summary>
        /// This method is used to extract the board positions horizontally.
        /// </summary>
        /// <returns>It returns a boolean value which indicates whether any horizontal board positions led to game win or not.</returns>
        public bool checkHorizontalPosition()
        {
            int[] values = new int[boardSize];
            string displayValues = "";
            for (byte i = 0; i < BoardPositions.GetLength(0); i++)
            {
                for (byte j = 0; j < BoardPositions.GetLength(1); j++)
                {
                    displayValues = displayValues + $"{BoardPositions[i,j]}";
                }
                if (ValidateString(displayValues)) { return true; }
                displayValues = "";
            }
            return false;
        }

        /// <summary>
        /// This method is used to extract the board positions vertically.
        /// </summary>
        /// <returns>It returns a boolean value which indicates whether any vertical board positions led to game win or not.</returns>
        public bool checkVerticalPosition()
        {
            string displayValues = "";
            for (byte j = 0; j < BoardPositions.GetLength(1); j++)
            {
                for (byte i = 0; i < BoardPositions.GetLength(0); i++)
                {
                    displayValues = displayValues + $"{BoardPositions[i,j]}";
                }
                if (ValidateString(displayValues)) { return true; }
                displayValues = "";
            }
            return false;
        }

        /// <summary>
        /// This method is used to extract the board positions diagonally starting from top left to bottom right.
        /// </summary>
        /// <returns>It returns a boolean value which indicates whether any diagonal board positions led to game win or not.</returns>
        public bool checkbackslashDiagonalPosition()
        {
            byte i;
            byte j;
            string displayValues = "";
            for ( i = j = 0; i < BoardPositions.GetLength(0); i++, j++)
            {
                displayValues = displayValues + $"{BoardPositions[i, j]}";
            }
            if (ValidateString(displayValues)) { return true; }
            else { return false; }
        }

        /// <summary>
        /// This method is used to extract the board positions diagonally starting from top right to bottom left.
        /// </summary>
        /// <returns>It returns a boolean value which indicates whether any diagonal board positions led to game win or not.</returns>
        public bool checkforwardslashDiagonalPosition()
        {
            byte i = 0;
            byte j = boardSize;
            string displayValues = "";
            for (j--; i < BoardPositions.GetLength(0); i++, j--)
            {
                displayValues = displayValues + $"{BoardPositions[i, j]}";
            }
            if (ValidateString(displayValues)) { return true; }
            else { return false; }
        }

        /// <summary>
        /// This property uses a boolean value to indicate if the game was won or not.
        /// </summary>
        public bool GameWin
        {
            get
            {
                return (checkHorizontalPosition() || checkVerticalPosition() || checkforwardslashDiagonalPosition() || checkbackslashDiagonalPosition());
            }
        }
    }
}