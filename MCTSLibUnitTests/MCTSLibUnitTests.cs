namespace MCTSLibUnitTests
{
    public class MCTSLibUnitTests
    {
        // Defining board positions
        char[,] boardPositions = new char[,]
        {
            { 'X', 'O', '-' },
            { 'O', 'O', 'X' },
            { 'X', 'X', '-' }
        };
        // Defining an empty node as root node
        Tree node;
        // Creating 3 child nodes
        Tree childNode1;
        Tree childNode2;
        Tree childNode3;
        // Creating Node for the expansion phase
        Tree expansionPhaseResult;
        // Creating Node for the backpropagation phase
        Tree backpropagationPhaseResult;

        // 
        // Defining the board size
        // byte boardOfSize = 3;
        // Creating an MCTS Object
        public MCTS aiObject = new MCTS(limitsForIteration: 100, limitsForTime: 0, boardSize: 3);

        public void BuildTree()
        {
            // Creating root node
            node = new Tree(
                positionsOfBoard: new char[,]
                {
                    { '-', '-', '-' },
                    { '-', '-', '-' },
                    { '-', '-', '-' },
                },
                parentNodeInfo: null
            );
            // Creating a child nodes
            // 3.09732400987854
            childNode1 = new Tree(
                positionsOfBoard: new char[,]
                {
                    { '-', '-', '-' },
                    { '-', 'X', '-' },
                    { '-', '-', '-' },
                },
                parentNodeInfo: node
            );
            // 0.6666666666666666666666666667
            childNode2 = new Tree(
                positionsOfBoard: new char[,]
                {
                    { '-', '-', '-' },
                    { '-', '-', '-' },
                    { '-', '-', 'X' },
                },
                parentNodeInfo: node
            );
            // 1.09732400987854
            childNode3 = new Tree(
                positionsOfBoard: new char[,]
                {
                    { '-', '-', 'X' },
                    { '-', '-', '-' },
                    { '-', '-', '-' },
                },
                parentNodeInfo: node
            );

            // Updating the score values
            node.Visits = 4;
            // Child Node 2
            childNode2.Score = 2;
            childNode2.Visits = 3;

            // Child Node 1
            childNode1.Visits = 1;
            childNode1.Score = 2;

            // Child Node 3
            childNode3.Visits = 1;
            childNode3.Score = 0;


            // Adding child nodes to the root node
            node.ChildNodes.Add(childNode1);
            node.ChildNodes.Add(childNode2);
            node.ChildNodes.Add(childNode3);
        }

        [Fact]
        public void TestCalculateScore()
        {
            // Arrange: Creating the Tree
            BuildTree();
            // Act: Updating the node score and node visit counts of a child node and parent node
            // Assert \\
            decimal requiredValue = 0.6666666666666666666666666667M;
            Assert.Equal(requiredValue, aiObject.CalculateScore(childNode2.Score, childNode2.Visits, node.Visits));
        }

        /*[Fact]
        public void TestIsLeafNode()
        {
            // Arrange
            BuildTree();
            Tree node = new Tree(positionsOfBoard: new char[,]
            {
                { 'O', 'O', 'X' },
                { 'X', 'O', 'X' },
                { 'O', 'X', 'X' },
            },
            parentNodeInfo: null);
            Tree resultNode = aiObject.ExpandNode(node);
            // Assert
            Assert.Equal(false, resultNode.ParentNode.IsLeafNode);
        }*/

        [Fact]
        public void TestSingleSelectPhase()
        {
            // Arrange:
            // Build Tree
            BuildTree();
            Tree resultNode = aiObject.SingleSelect(node);
            // Act
            // Assert
            Assert.Equal(resultNode.BoardPositions, childNode1.BoardPositions);
        }

        [Fact]
        public void TestExpansionPhase()
        {
            // Arrange:
            // Creating Node
            // Creating root node
            node = new Tree(
                positionsOfBoard: new char[,]
                {
                    { 'X', 'O', 'O' },
                    { 'O', '-', 'X' },
                    { 'X', 'X', 'O' },
                },
                parentNodeInfo: null
            );
            // Creating root node
            Tree expectedNode = new Tree(
                positionsOfBoard: new char[,]
                {
                    { 'X', 'O', 'O' },
                    { 'O', 'X', 'X' },
                    { 'X', 'X', 'O' },
                },
                parentNodeInfo: null
            );
            // Act
            Tree resultNode = aiObject.ExpandNode(node);
            // Assert
            Assert.Equal(resultNode.BoardPositions, expectedNode.BoardPositions);
        }

        [Fact]
        public void TestExpansionPhaseNoPossibility()
        {
            // Arrange:
            // Creating Node
            // Creating root node
            node = expansionPhaseResult = new Tree(
                positionsOfBoard: new char[,]
                {
                    { 'X', 'O', 'O' },
                    { 'O', 'X', 'X' },
                    { 'X', 'X', 'O' },
                },
                parentNodeInfo: null
            );
            // Modifying the IsLeafNodeStatus
            node.IsLeafNode = true;
            // Act
            Tree resultNode = aiObject.ExpandNode(node);
            // Assert
            Assert.Equal(expansionPhaseResult.BoardPositions, resultNode.BoardPositions);
        }

        [Fact]
        public void TestSimulationPhase()
        {
            // Arrange:
            // Creating Node
            // Creating root node
            node = new Tree(
                positionsOfBoard: new char[,]
                {
                    { 'X', 'X', 'O' },
                    { 'O', 'O', 'X' },
                    { 'O', 'X', '-' },
                },
                parentNodeInfo: null
            );
            // Expected output is 1 as O wins the game and it's the current player
            decimal expectedScore = 1M;
            // Act
            decimal result = aiObject.SimulateNode(boardSize: 3, boardPositions: node.BoardPositions);
            // Assert
            Assert.Equal(expected: expectedScore, actual: result);
        }

        // Defining method to build Expansion Phase
        public void BuildExpansionTest()
        {
            // Creating Parent Node
            node = new Tree(
                positionsOfBoard: new char[,]
                {
                    { 'X', 'O', 'O' },
                    { 'O', 'O', 'X' },
                    { 'X', 'X', '-' },
                },
                parentNodeInfo: null
                );
            // Creating child node
            childNode1 = new Tree(
                positionsOfBoard: new char[,]
                {
                    { 'X', 'O', 'O' },
                    { 'O', 'O', 'X' },
                    { 'X', 'X', 'X' },
                },
                parentNodeInfo: node
                );
            // Adding child node to the parent node
            node.ChildNodes.Add( childNode1 );
            // Adding the visits
            node.Visits = 3;
            node.Score = 2;
            // Adding the score
            childNode1.Visits = 2;
            childNode1.Score = 1;
            // Calling the Expansion Phase, adding score of 1M
            backpropagationPhaseResult = aiObject.Backpropagate(1M, childNode1);
        }

        [Fact]
        public void TestBackpropagatePhaseChildNodeVisits()
        {
            // Act: Calling the Build BackPropagatePhase
            BuildExpansionTest();
            // Required Child Node visit count
            int expectedChildNodeVisits = 3;
            /*// Adding the visits
            node.Visits = 3;
            node.Score = 2;
            // Adding the score
            childNode1.Visits = 2;
            childNode1.Score = 1;*/
            // Assert
            Assert.Equal(expectedChildNodeVisits, childNode1.Visits);
        }

        [Fact]
        public void TestBackpropagatePhaseChildNodeScore()
        {
            // Act: Calling the Build BackPropagatePhase
            BuildExpansionTest();
            // Required Child Node visit count
            int expectedChildNodeScore = 2;
            /*// Adding the visits
            node.Visits = 3;
            node.Score = 2;
            // Adding the score
            childNode1.Visits = 2;
            childNode1.Score = 1;*/
            // Assert
            Assert.Equal(expectedChildNodeScore, childNode1.Score);
        }

        [Fact]
        public void TestBackpropagatePhaseParentNodeScore()
        {
            // Act: Calling the Build BackPropagatePhase
            BuildExpansionTest();
            // Required Child Node visit count
            int expectedParentNodeScore = 2;
            /*// Adding the visits
            node.Visits = 3;
            node.Score = 2;
            // Adding the score
            childNode1.Visits = 2;
            childNode1.Score = 1;*/
            // Assert
            Assert.Equal(expectedParentNodeScore, node.Score);
        }

        [Fact]
        public void TestBackpropagatePhaseParentNodeVisits()
        {
            // Act: Calling the Build BackPropagatePhase
            BuildExpansionTest();
            // Required Child Node visit count
            int expectedParentNodeVisits = 4;
            /*// Adding the visits
            node.Visits = 3;
            node.Score = 2;
            // Adding the score
            childNode1.Visits = 2;
            childNode1.Score = 1;*/
            // Assert
            Assert.Equal(expectedParentNodeVisits, node.Visits);
        }
    }
}