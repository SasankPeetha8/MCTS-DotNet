using System;


namespace GameEnvironment
{
    public class TicTacToe
    {
        // Defining fields
        byte BoardSize;
        //char[,] BoardPositions;
        /*private char[] boardPositions;*/
        char[,] boardPositions;
        // Defining players
        char Player1;
        char Player2;
        // Empty board character
        private char boardCharacter = '-';

        // Defining constructor
        public TicTacToe(byte boardSize, char playerOne='X', char playerTwo='O')
        {
            
            // Defining the game board size
            BoardSize = boardSize;
            // Creating the board and filling initial values
            boardPositions = new char[boardSize, boardSize];

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    boardPositions[i,j] = '-';
                }
            }

            /*Array.Fill<char>(array: boardPositions, value: boardCharacter);*/
            // Defining players
            Player1 = playerOne;
            Player2 = playerTwo;
        }

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

        /*public string DisplayBoard()
        {
            // Building the string
            string boardString = string.Empty;
            for (byte i = 0; i < BoardPositions.Length; i++)
            {
                boardString = boardString + boardPositions[i] + (((i+1) % BoardSize == 0) ? "\n" : " ");
                //boardString = boardString + BoardPositions[i];

            }
            return boardString;
        }*/

        public string DisplayBoard()
        {
            // Building the string
            string boardString = "";
            for (int i = 0; i < BoardPositions.GetLength(0); i++)
            {
                for(int j = 0;j < BoardPositions.GetLength(1); j++)
                {
                    boardString = boardString + BoardPositions[i,j] + " ";
                }
                boardString = boardString + "\n";
            }
            return boardString;
        }

        // Defining method to check which player to make their move
        public char CurrentPlayer
        {
            get
            {
                byte playerOnePositions = 0;
                byte playerTwoPositions = 0;
                for (int i = 0; i < BoardPositions.GetLength(0); i++)
                {
                    for (int j = 0; j < BoardPositions.GetLength(1); j++)
                    {
                        if (BoardPositions[i,j] == Player1)
                        {
                            playerOnePositions++;
                        }
                        else if (BoardPositions[i,j] == Player2)
                        {
                            playerTwoPositions++;
                        }
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

            /*List<byte> emptyPositions = new List<byte>();
            foreach (char element in BoardPositions)
                for (byte i = 0; i < BoardPositions.Length; i++)
                {
                    if (BoardPositions[i] == boardCharacter)
                    {
                        emptyPositions.Add(i);
                    }
                }
            return emptyPositions.ToArray();*/
        }

        // Defining method to check if the game is over of not
        public bool IsGameOver
        {
            get
            {
                string availablePositions = BoardPositions.ToString();
                return availablePositions.Contains(boardCharacter);
            }
        }

        // Defining the game win state
        public bool GameWin()
        {

            return true;
        }
    }
}