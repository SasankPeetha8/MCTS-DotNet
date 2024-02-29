using System;


namespace GameEnvironment
{
    public class TicTacToe
    {
        // Defining fields
        byte BoardSize;
        // Defining boardPositions
        char[,] boardPositions;
        // Defining players
        public char Player1;
        public char Player2;
        // Empty board character
        private char boardCharacter = '-';

        // Defining constructor
        public TicTacToe(byte boardSize, char playerOne='X', char playerTwo='O')
        {
            
            // Defining the game board size
            BoardSize = boardSize;
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

        // Defining Property for the BoardPositions
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

        public string DisplayBoard()
        {
            // Building the string
            string boardString = "";
            byte counter = 0;
            foreach (char element in boardPositions)
            {
                boardString = boardString + element + " ";
                counter++;
                if (counter == BoardSize)
                {
                    boardString += "\n";
                    counter = 0;
                }

            }
            return boardString;
        }

        // Defining method to check who is the previous player
        public char WinningPlayer
        {
            get
            {
                return CurrentPlayer == Player1 ? Player2 : Player1;
            }
        }

        // Defining method to check which player to make their move
        public char CurrentPlayer
        {
            get
            {
                byte playerOnePositions = 0;
                byte playerTwoPositions = 0;
                foreach(char element in boardPositions)
                {
                    if (element == Player1)
                    {
                        playerOnePositions++;
                    }
                    else if (element == Player2)
                    {
                        playerTwoPositions++;
                    }
                }
                if (playerOnePositions == playerTwoPositions)
                {
                    return Player1;
                }
                else if (playerOnePositions == playerTwoPositions + 1)
                {
                    return Player2;
                }
                else
                {
                    throw new Exception(message: "Invalid Game Board Positions Detected");
                }
            }
        }

        // Defining method to get a random position
        public (byte, byte) GetRandomPosition()
        {
            int randomPosition = 0;
            List<(byte, byte)> positionsAvailable = AvailablePositions();
            Random randomGen = new Random();
            randomPosition = randomGen.Next(randomPosition, positionsAvailable.Count);
            return positionsAvailable[randomPosition];
        }

        public void MakeRandomMove()
        {
            // Getting current player
            char currentGamePlayer = CurrentPlayer;
            (byte i, byte j) = GetRandomPosition();
            BoardPositions[i, j] = currentGamePlayer;
        }

        // Defining method to fetch the empty board positions
        public List<(byte, byte)> AvailablePositions()
        {
            List<(byte, byte)> positionsAvailable = new List<(byte, byte)> ();
            for (byte i = 0; i < BoardPositions.GetLength(0); i++)
            {
                for ( byte j = 0; j < BoardPositions.GetLength(1); j++)
                {
                    if (BoardPositions[i,j] == boardCharacter)
                    {
                        positionsAvailable.Add((i,j));
                    }
                }
            }
            return positionsAvailable;
        }

        // Defining method to check if the game is draw or not
        public bool GameDraw
        {
            get
            {
                if (GameWin)
                {
                    return false;
                }
                else
                {
                    string availablePositions = DisplayBoard();
                    return !availablePositions.Contains(boardCharacter);
                }

            }
        }

        public bool ContinueGamePlay
        {
            get
            {
                return !GameWin && !GameDraw;
            }
        }

        // Defining method to check if the string is winning or not
        public bool ValidateString(string positions)
        {
            string checkPlayerOne = positions.Replace(Player1, ' ').Trim();
            string checkPlayerTwo = positions.Replace(Player2, ' ').Trim();
            if (checkPlayerOne.Length == 0)
            {
                return true;
            }
            else if (checkPlayerTwo.Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool checkHorizontalPosition()
        {
            int[] values = new int[BoardSize];
            string displayValues = "";
            for (byte i = 0; i < BoardPositions.GetLength(0); i++)
            {
                for (byte j = 0; j < BoardPositions.GetLength(1); j++)
                {
                    displayValues = displayValues + $"{BoardPositions[i,j]}";
                }
                //Console.WriteLine($"Row {i}: {displayValues} - {ValidateString(displayValues)}");
                if (ValidateString(displayValues))
                {
                    return true;
                }
                displayValues = "";
            }
            return false;
        }

        public bool checkVerticalPosition()
        {
            string displayValues = "";
            for (byte j = 0; j < BoardPositions.GetLength(1); j++)
            {
                for (byte i = 0; i < BoardPositions.GetLength(0); i++)
                {
                    displayValues = displayValues + $"{BoardPositions[i,j]}";
                }
                //Console.WriteLine($"Column {j}: {displayValues} - {ValidateString(displayValues)}");
                if (ValidateString(displayValues) )
                {
                    return true;
                }
                displayValues = "";
            }
            return false;
        }

        public bool checkbackslashDiagonalPosition()
        {
            byte i;
            byte j;
            string displayValues = "";
            for ( i = j = 0; i < BoardPositions.GetLength(0); i++, j++)
            {
                displayValues = displayValues + $"{BoardPositions[i, j]}";
            }
            //Console.WriteLine($"Backslash Diagonal: {displayValues} - {ValidateString(displayValues)}");
            if (ValidateString(displayValues))
            {
                return true;
            }
            else { return false; }
        }

        public bool checkforwardslashDiagonalPosition()
        {
            byte i = 0;
            byte j = BoardSize;
            string displayValues = "";
            for (j--; i < BoardPositions.GetLength(0); i++, j--)
            {
                displayValues = displayValues + $"{BoardPositions[i, j]}";
            }
            //Console.WriteLine($"Forwardslash Diagonal: {displayValues} - {ValidateString(displayValues)}");
            if (ValidateString(displayValues))
            {
                return true;
            }
            else { return false; }
        }

        // Defining the game win state
        public bool GameWin
        {
            get
            {
                return (checkHorizontalPosition() || checkVerticalPosition() || checkforwardslashDiagonalPosition() || checkbackslashDiagonalPosition());
            }
        }
    }
}