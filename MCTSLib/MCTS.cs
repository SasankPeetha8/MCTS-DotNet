using GameEnvironment;

namespace MCTSLib
{
    public class MCTS
    {
        // Defining an game object
        public TicTacToe gameObject;
        // Defining the tree
        public Tree nodeData;
        // Defining the iteration limit
        int iterationLimit;
        // Defining the time limit
        TimeSpan timeLimit;
        // Exploration constant
        double explorationConstant = Math.Sqrt(2);

        // Defining the constructor
        public MCTS(int limitsForIteration, double limitsForTime, byte boardSize)
        {
            // Iteration limit
            iterationLimit = limitsForIteration;
            // Time Limit
            timeLimit = TimeSpan.FromSeconds(iterationLimit);
            // Defining the board size
            gameObject = new TicTacToe(boardSize);
            // Creating a new node
        }

        public double CalculateScore(double childNodeScore, int childNodeVisits, int nodeVisits)
        {
            // Calculating the exploitation value
            double exploitationValue = childNodeScore / childNodeVisits;
            // Calculating the exploration value
            double N2NVisitsRatio = nodeVisits / childNodeVisits;
            // Calculating the log10 value
            double logValue = (Math.Log10(nodeVisits))/childNodeVisits;
            //double logValue = Math.Log(N2NVisitsRatio);
            double log_value = Math.Log10(logValue);
            double ratio_value = log_value / childNodeVisits;
            double sqrt_ratio = Math.Sqrt(ratio_value);
            //decimal explorationValue = Convert.ToDecimal(explorationConstant * (Math.Sqrt(logValue)));
            double explorationValue = explorationConstant * Math.Sqrt(logValue);
            // Displaying the score values
            Console.WriteLine($" ##########Child Score: {childNodeScore}, Child Visits: {childNodeVisits}, Parent Visits: {nodeVisits} ##########");
            Console.WriteLine($"Exp Const: {explorationConstant}, Log Value: {log_value}, Ratio Value: {ratio_value}, Sqrt Ratio: {sqrt_ratio}");
            Console.WriteLine($" ##########Exploitation Value: {exploitationValue}, Exploration Value: {explorationValue} ##########");
            // returing the total score
            return  Math.Round((exploitationValue + explorationValue),2);
        }

        public string DisplayNode(char[,] positionsOnBoard)
        {
            // Updating the positions on the board
            gameObject.BoardPositions = positionsOnBoard;
            // Displaying the board positions
            return gameObject.DisplayBoard();
        }

        public Tree SingleSelect(Tree node)
        {
            Console.WriteLine("---------- Inside Single Select Node ----------");
            // Initialising the score
            double bestScore = double.MinValue;
            // Defining the counter
            byte counter = 1;
            // Defining the best moves
            List<Tree> bestMoves = new List<Tree>();
            // Calculating the score of each child node
            foreach (Tree childNode in node.ChildNodes)
            {
                // Calculating the score
                //decimal score = CalculateScore(childNodeScore: childNode.Score, childNodeVisits: childNode.Visits, nodeVisits: node.Visits);
                double score = CalculateScore(childNodeScore: childNode.Score, childNodeVisits: childNode.Visits, nodeVisits: node.Visits);

                // Displaying the board position and score of the node.
                Console.WriteLine($"Child Node - {counter} : [ {score} ]\n{DisplayNode(positionsOnBoard: childNode.BoardPositions)}");
                // Displaying the Leaf node status
                // Console.WriteLine($"Is Leaf Node: {childNode.IsLeafNode}");
                // Checking if the child node has the best score or not
                if ( score > bestScore )
                {
                    bestMoves = new List<Tree> { childNode };
                    bestScore = score;
                }
                // Updating the counter
                else if (score == bestScore )
                {
                    bestMoves.Add(childNode);
                }
                // Updating the counter value
                counter++;
            }
            Console.WriteLine("---------- Outside Single Select Node ----------");
            // Selecting the best move
            if ( bestMoves.Count > 1 )
            {
                // Creating an instance of random object
                Random randomGen = new Random();
                // Picking up the random move
                return bestMoves[randomGen.Next(0, bestMoves.Count)];
            }
            else
            {
                return bestMoves[0];
            }
        }

        public Tree SelectNode(Tree node)
        {
            while (true)
            {
                Console.WriteLine("---------- Inside Select Node ----------");
                // Finding the total number of possible positions
                int totalPossiblePositions = gameObject.PossibleRandomMoves(positionsOfBoard: node.BoardPositions);
                // Checking if the node has the same number of the child nodes or not
                if (( node.ChildNodes.Count < totalPossiblePositions ) || (totalPossiblePositions == 0))
                {
                    // Then the current node is the leaf node
                    Console.WriteLine($"The Current node is a leaf node:");
                    if (node.ParentNode != null)
                    {
                        Console.WriteLine($"Score: {CalculateScore(node.Score, node.Visits, node.ParentNode.Visits)}");
                    }
                    Console.WriteLine(gameObject.DisplayBoard(positionsOnBoard: node.BoardPositions));
                    Console.WriteLine("---------- Outside Select Node ----------");
                    // Returning the leaf node
                    return node;
                }
                // If not, then current node is not a leaf node
                else if ( node.ChildNodes.Count == totalPossiblePositions )
                {
                    // Then the node is fully expanded and it is not a leaf node
                    Console.WriteLine($"The Current node is not a leaf node, it's fully expanded node:");
                    if (node.ParentNode != null)
                    {
                        Console.WriteLine($"Score: {CalculateScore(node.Score, node.Visits, node.ParentNode.Visits)}");
                    }
                    Console.WriteLine(gameObject.DisplayBoard(positionsOnBoard: node.BoardPositions));
                    Console.WriteLine(":::::: Finding the Best Child Nodes ::::::");
                    node = SingleSelect(node);
                }
                // If nothing is selected then something is wrong
                else
                {
                    Console.WriteLine("The current node is something wrong");
                    Console.WriteLine($"Node Score: {node.Score}, Node Visits: {node.Visits}");
                    Console.WriteLine(gameObject.DisplayBoard(node.BoardPositions));
                    throw new Exception(message: "Please check the detected invalid node");
                }
            }
        }

/*        public Tree SelectNode(Tree node)
        {
            Console.WriteLine("------ Inside Select Node ------");
            // Iterating until leaf node is reached
            while (true)
            {
                if (node.IsLeafNode)
                {
                    // Adding the board positions of the node to the game object.
                    gameObject.BoardPositions = node.BoardPositions;
                    // Displaying the message.
                    Console.WriteLine($"The following node is selected in Select Node Phase:\n{gameObject.DisplayBoard(node.BoardPositions)}");
                    // Returning the leaf node
                    return node;
                }
                else
                {
                    node = SingleSelect(node);
                }
            }
        }*/

        public bool ArraysAreEqual(char[,] arrayToCompare, char[,] arrayToAgainst)
        {
            if ((arrayToCompare.GetLength(0) == arrayToAgainst.GetLength(0)) && (arrayToCompare.GetLength(1) == arrayToAgainst.GetLength(1)))
            {
                for (int i = 0; i < arrayToCompare.GetLength(0); i++)
                {
                    for (int j = 0; j < arrayToCompare.GetLength(1); j++)
                    {
                        if (arrayToCompare[i, j] != arrayToAgainst[i, j])
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public string ConvertToString(char[,] positionsOfBoard)
        {
            // Defining a new string
            string boardString = "";
            for(int i = 0;i < positionsOfBoard.GetLength(0);i++)
            {
                for (int j = 0; j < positionsOfBoard.GetLength(1); j++)
                {
                    boardString = boardString + positionsOfBoard[i,j];
                }
            }
            // Returing the string
            return boardString;
        }

        public char[,] AddNewNode(char[,] positionsOfBoard, List<char[,]> positionsAvailable)
        {
            // Creating a list to store string values
            List<string> matchFound = new List<string>();
            // Iterating through all the positions available
            foreach (char[,] position in positionsAvailable)
            {
                // Converting each position to string
                string positionString = ConvertToString(positionsOfBoard: position);
                // Add the string to list if it doesn't exist
                if (!matchFound.Contains(positionString))
                {
                    // Adding the string to the list if it doesn't exist
                    matchFound.Add(positionString);
                }
            }
            // Defining a boolean value
            bool continueIteration = true;
            // Iterating until all new game board positions are found.
            while (continueIteration)
            {
                // Updating the board positions in the game object by cloning it's values
                gameObject.BoardPositions = (char[,])positionsOfBoard.Clone();
                // Making a random move
                gameObject.MakeRandomMove();
                // Checking if the board positions on game object exists
                string requiredString = ConvertToString(gameObject.BoardPositions);
                // Assuming that new board position doesn't exist
                continueIteration = false;
                // Iterating through all the string values
                foreach (string strValues in matchFound)
                {
                    // If any of the iteration value is true,
                    // then updating the boolean value to true
                    // Until unique board position is found
                    if ( strValues == requiredString)
                    {
                        continueIteration = true;
                    }
                }
            }
            // Returning the new game board position
            return gameObject.BoardPositions;
        }

        public Tree ExpandNode(Tree node)
        {
            Console.WriteLine("---------- Inside Expand Node ----------");
            Console.WriteLine($":::::: Displaying node details:::::: ");
            // Checking if the selected node is terminal node or not
            int totalMovesAvailable = gameObject.PossibleRandomMoves(node.BoardPositions);
            // Displaying the node information:
            Console.WriteLine($"Intial Node for Expansion -- Total Child Nodes: {node.ChildNodes.Count}, Possible Child Nodes: {totalMovesAvailable}");
            Console.WriteLine(gameObject.DisplayBoard(node.BoardPositions));
            // Checking if the game is won or draw
            gameObject.BoardPositions = node.BoardPositions;
            // Checking if terminal node is found
            bool terminalNodeFound = false;
            // Checking if the node is terminal Node:
            if (!gameObject.ContinueGamePlay)
            {
                terminalNodeFound = true;
            }
            if ( totalMovesAvailable <= 0 )
            {
                terminalNodeFound = true;
            }
            if (terminalNodeFound)
            {
                // Stop the game play
                Console.WriteLine($"Terminal Node has been reached.");
                Console.WriteLine(gameObject.DisplayBoard(node.BoardPositions));
                return node;
            }
            else
            {
                // Continue game play
                // Creating an empty list
                List<char[,]> availablePositions = new List<char[,]>();
                // Checking if child nodes are empty
                foreach (Tree childNode in node.ChildNodes)
                {
                    // If the node position is not available in the list, add it
                    availablePositions.Add(childNode.BoardPositions);
                }

                // Fetching the new board position
                char[,] newBoardPosition = AddNewNode(positionsOfBoard: node.BoardPositions, positionsAvailable: availablePositions);
                // Creating a new tree node
                Tree newNode = new Tree(positionsOfBoard: newBoardPosition, parentNodeInfo: node);
                // Adding the newly created node to the list of child nodes
                node.ChildNodes.Add(newNode);

                // Displaying the newly create node
                Console.WriteLine($"The following node is selected in the Expansion Phase: \n{DisplayNode(positionsOnBoard: newNode.BoardPositions)}");
                // Updating if the new node is a leaf node or not
                newNode.IsLeafNode = (gameObject.PossibleRandomMoves(positionsOfBoard: newNode.BoardPositions) == 0) ? false : true;
                // Updating the leaf node status for the node
                node.IsLeafNode = (gameObject.PossibleRandomMoves(positionsOfBoard: node.BoardPositions) == node.ChildNodes.Count) ? false : true;
                Console.WriteLine("---------- Outside Expand Node ----------");
                // Returning the newly created node
                return newNode;
            }
        }

        public double SimulateNode(int boardSize, char[,] boardPositions)
        {
            Console.WriteLine("---------- Inside Simulate Node ----------");
            // Temporary game objects
            TicTacToe tempGameObject = new TicTacToe(sizeOfBoard: (byte)boardSize);
            // Updating the board positions of temp game object
            tempGameObject.BoardPositions = (char[,])boardPositions.Clone();
            // Displaying the Initial Board positions
            Console.WriteLine($"Initial Positions: \n{tempGameObject.DisplayBoard(tempGameObject.BoardPositions)}");
            // Fetching the initial player at the current board positions
            char initialPlayer = tempGameObject.CurrentPlayer;
            // Initial Player
            Console.WriteLine($"The Initial Player is {initialPlayer}");
            // Checking if the game is won or not

            // Iterating until the game ends
            while (tempGameObject.ContinueGamePlay)
            {
                // Displaying which player to make the move
                Console.WriteLine($"Player {tempGameObject.CurrentPlayer} turn :");
                // Making a random move
                tempGameObject.MakeRandomMove();
                // Displaying the random move made
                Console.WriteLine(tempGameObject.DisplayBoard(tempGameObject.BoardPositions));
            }
            // Defining the score
            double score;
            // Checking the game player when the game ends
            if (tempGameObject.GameWin )
            {
                score = (tempGameObject.WinningPlayer.ToString() != initialPlayer.ToString()) ? 1 : -1 ;
                Console.WriteLine($"The game is won by player {tempGameObject.WinningPlayer} - Score [{score}]");
            }
            else if (tempGameObject.GameDraw )
            {
                score = 0.5;
                Console.WriteLine($"The game is draw - Score [{score}]");
            }
            else 
            {
                throw new Exception(message: "Invalid end game detected.");
            }
            Console.WriteLine("---------- Outside Simulate Node ----------");
            // returning score value
            return score;
        }

        public Tree Backpropagate(double currentPlayerScore, Tree node)
        {
            Console.WriteLine("---------- Inside Backpropagate Phase ----------");
            double opponentPlayerScore;
            if ( currentPlayerScore == 1)
            {
                opponentPlayerScore = -1;
            }
            else if ( currentPlayerScore == 0.5)
            {
                opponentPlayerScore = 0.5;
            }
            else if (currentPlayerScore == -1)
            {
                opponentPlayerScore = 1;
            }
            else
            {
                throw new Exception(message: "The current score is wrong, please check.");
            }
            // Updating the score values in the current node
            while (true)
            {
                // Updating the node score and node visits
                node.Score = node.Score + currentPlayerScore;
                node.Visits = node.Visits + 1;
                // Displaying the node score and node visits count
                Console.WriteLine(format: "--- [ Node Score: {0} ] --- [ Node Visits: {1} ] ---\n{2}", node.Score, node.Visits, gameObject.DisplayBoard(node.BoardPositions));
                // Swaping scores
                (currentPlayerScore, opponentPlayerScore) = (opponentPlayerScore, currentPlayerScore);
                if (node.ParentNode == null)
                {
                    break;
                }
                // Changing node to it's parent node
                node = node.ParentNode;
            }
            Console.WriteLine($"----------- Outside Backpropagate Phase ----------\n");
            return node;
        }

        public Tree PerformIteration(int iterationCount)
        {
            // Fetching the root player
            gameObject.BoardPositions = nodeData.BoardPositions;
            char currentPlayer = gameObject.CurrentPlayer;
            string message = @"////// Iteration: " + $"{iterationCount}" + @" \\\\\\";
            Console.WriteLine(message);

            // Calling the Phase 1 of MCTS
            nodeData = SelectNode(node: nodeData);
            // Calling the Phase 2 of MCTS
            nodeData = ExpandNode(node: nodeData);
            // Calling Phase 3 of MCTS
            double score = SimulateNode(boardSize: nodeData.BoardPositions.GetLength(0), boardPositions: nodeData.BoardPositions);
            // Calling Phase 4 of MCTS
            nodeData = Backpropagate(currentPlayerScore: score, node: nodeData);
            // Returing the node after building it
            return nodeData;
        }

        public void BuildTree(char[,] boardPositions, Tree existingTree = null)
        {
            // Creating a new node if there isn't any node created previously.
            if (existingTree == null)
            {
                // Creating a new node
                nodeData = new Tree(positionsOfBoard: boardPositions, parentNodeInfo: existingTree);
                // Updating the root node count visit count as 1
                //nodeData.Visits = nodeData.Visits + 1;
            }
            else { nodeData = existingTree; }

            // Checking the iteration limit
            if (iterationLimit != 0)
            {
                // Looping through all the iterations
                for (int i = 0; i < iterationLimit; i++)
                {
                    // Displaying message
                    Console.WriteLine("------ Performing Iteration Limit ------");
                    // Performing Iteration
                    PerformIteration(iterationCount: i);
                }
            }
            else
            {
                // Initial time
                DateTime initialTime = DateTime.Now;
                // Counter time
                int counter = 1;
                // Looping until time limit is reached
                for (TimeSpan timeDifference  = DateTime.Now - initialTime; timeDifference.TotalSeconds < iterationLimit ; )
                {
                    // Displaying message
                    Console.WriteLine("------ Performing Time Limit ------");
                    // Performing Iteration
                    PerformIteration(iterationCount: counter);
                }
            }
        }

        public Tree FindState(Tree nodeInfo, char[,] positionsOfBoard)
        {
            // Displaying the message
            Console.WriteLine($"----------- Inside Find State ----------");
            // Converting the positions of the board to string
            string strBoardPositions = ConvertToString(positionsOfBoard);
            // Iterating through the child nodes of the tree
            for (int i = 0; i < nodeInfo.ChildNodes.Count; i++)
            {
                // Converting the positions of the child node to string
                string strChildBoardPositions = ConvertToString(nodeInfo.ChildNodes[i].BoardPositions);
                // Displaying all the child nodes
                Console.WriteLine($"Child Node: {i} - {strChildBoardPositions}");
                // Checking if the two strings are equal or not
                if (strBoardPositions == strChildBoardPositions)
                {
                    // Removing the parent node
                    nodeInfo.ChildNodes[i].ParentNode = null;
                    Console.WriteLine($"----------- Outside Find State ----------\n");
                    return nodeInfo.ChildNodes[i];
                }
            }
            // 
            throw new Exception(message: $"{gameObject.DisplayBoard(positionsOfBoard)}\nInvalid Board Position not found in the tree.");
        }

        public void DisplayTree(Tree parentNode = null, Tree childNode = null)
        {
            if (childNode == null)
            {
                childNode = nodeData;
            }
            foreach (Tree childTree in childNode.ChildNodes)
            {
                // Defining the parent string
                string parentString = $"{childTree.ParentNode.GUID}(S:{childTree.ParentNode.Score}, V:{childTree.ParentNode.Visits}\n{gameObject.DisplayBoard(childTree.ParentNode.BoardPositions)})";
                // Child String
                string childString = $"{childTree.GUID}(S:{childTree.Score}, V:{childTree.Visits}\n{gameObject.DisplayBoard(childTree.BoardPositions)})";
                // Displaying the mermiad string
                Console.WriteLine($"    {parentString}--->{childString}");

                if (childTree.ChildNodes.Count > 0)
                {
                    DisplayTree(parentNode: null, childNode: childTree);
                }
            }
        }

        public char[,] BestMove()
        {
            Console.WriteLine("------ Inside Best Move Phase ------");
            char[,] bestFoundPosition =  SingleSelect(node: nodeData).BoardPositions;
            Console.WriteLine($"The following node is selected as best move:\n{DisplayNode(bestFoundPosition)}");
            return bestFoundPosition;
        }
    }
}
