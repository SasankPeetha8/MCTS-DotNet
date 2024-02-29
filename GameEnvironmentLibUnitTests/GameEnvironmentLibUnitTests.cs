namespace GameEnvironmentLibUnitTests
{
    public class GameEnvironmentLibUnitTests
    {
        // Creating a universal items
        static byte size = 4;
        static char emptyBoardCharacter = '-';
        private TicTacToe game = new TicTacToe(boardSize: size);

        [Fact]
        public void TestInitialPlayers()
        {
            // Arrange
            string expectedPlayers = "XO";
            // Assert
            Assert.Equal(expectedPlayers, $"{game.Player1}{game.Player2}");
        }

        [Fact]
        public void TestInitialGamePositions()
        {
            // Arrange
            char[,] expectedBoardPositions = new char[size,size];
            // Act
            // Filling the board positions
            for (byte i = 0; i < expectedBoardPositions.GetLength(0); i++)
            {
                for (byte j = 0; j < expectedBoardPositions.GetLength(1); j++)
                {
                    expectedBoardPositions[i,j] = emptyBoardCharacter;
                }
            }
            // Assert
            Assert.Equal(expectedBoardPositions, game.BoardPositions);
        }

        [Fact]
        public void TestBoardSize()
        {
            // Assert
            Assert.Equal((game.BoardPositions.GetLength(0), game.BoardPositions.GetLength(1)), (size, size));
        }

        [Fact]
        public void TestHorizontalX()
        {
            // Arrange
            char win = 'X';
            // Act
            game.BoardPositions = new char[,]
            {
                {'O', 'X', 'O', 'O'},
                {'X', 'X', 'X', 'X'},
                {'-', 'O', 'O', 'O'},
                {'X', 'O', 'X', 'X'}
            };
            // Assert
            Assert.Equal(win, game.WinningPlayer);
        }

        [Fact]
        public void TestVerticalX()
        {
            // Arrange
            char win = 'O';
            // Act
            game.BoardPositions = new char[,]
            {
                { 'O', 'O', 'O', 'O' },
                { 'O', 'X', 'X', 'O' },
                { 'X', 'X', 'X', 'O' },
                { 'X', 'X', 'X', 'O' },
            };
            // Assert
            Assert.Equal(win, game.WinningPlayer);
        }

        [Fact]
        public void TestVerticalO()
        {
            // Arrange
            char win = 'O';
            // Act
            game.BoardPositions = new char[,]
            {
                { 'O', 'O', 'O', 'O' },
                { 'O', 'X', 'X', 'O' },
                { 'X', 'X', 'X', 'O' },
                { 'X', 'X', 'X', 'O' },
            };
            // Assert
            Assert.Equal(win, game.WinningPlayer);
        }

        [Fact]
        public void TestHorizontalO()
        {
            // Arrange
            char win = 'O';
            // Act
            game.BoardPositions = new char[,]
            {
                { 'O', 'O', 'O', 'O' },
                { 'O', 'X', 'X', 'O' },
                { 'X', 'X', 'X', 'O' },
                { 'X', 'X', 'X', 'O' },
            };
            // Assert
            Assert.Equal(win, game.WinningPlayer);
        }

        [Fact]
        public void TestGameDraw()
        {
            // Arrange
            bool expectedDraw = false;
            // Act
            game.BoardPositions = new char[,]
            {
                { 'O', 'O', 'O', '-' },
                { 'O', 'X', 'X', 'O' },
                { 'X', 'X', 'X', 'O' },
                { 'X', 'X', 'X', 'O' },
            };
            // Assert
            Assert.Equal(expectedDraw, game.GameDraw);
        }

        [Fact]
        public void TestGameDrawWhenGameWon()
        {
            // Arrange
            bool expectedDraw = false;
            // Act
            game.BoardPositions = new char[,]
            {
                { 'O', 'O', 'O', 'O' },
                { 'O', 'X', 'X', 'O' },
                { 'X', 'X', 'X', 'O' },
                { 'X', 'X', 'X', 'O' },
            };
            // Assert
            Assert.Equal(expectedDraw, game.GameDraw);
        }

        [Fact]
        public void TestForawardSlashDiagonal()
        {
            // Arrange
            char win = 'O';
            // Act
            game.BoardPositions = new char[,]
            {
                { 'X', '-', '-', 'O' },
                { 'O', 'X', 'O', 'X' },
                { 'O', 'O', 'X', 'X' },
                { 'O', 'X', '-', '-' },
            };
            // Assert
            Assert.Equal(win, game.WinningPlayer);
        }

        [Fact]
        public void TestBackwardSlashDiagonal()
        {
            // Arrange
            char win = 'X';
            // Act
            game.BoardPositions = new char[,]
            {
                { 'X', 'O', 'X', 'X' },
                { 'X', 'X', 'X', 'O' },
                { 'O', 'O', 'X', 'O' },
                { '-', 'O', 'O', 'X' },
            };
            // Assert
            Assert.Equal(win, game.WinningPlayer);
        }

        [Fact]
        public void TestContinueGamePlay()
        {
            // Arrange
            bool expectedContinue = true;
            // Act
            game.BoardPositions = new char[,]
            {
                { 'O', 'O', 'O', '-' },
                { 'O', 'X', 'X', 'O' },
                { 'X', 'X', 'X', 'O' },
                { 'X', 'X', 'X', 'O' },
            };
            // Assert
            Assert.Equal(expectedContinue, game.ContinueGamePlay);
        }
    }
}