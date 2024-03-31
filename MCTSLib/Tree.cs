using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTSLib
{
    /// <summary>
    /// This class is used to create a Tree Data Structure which is used for building Decision Trees for MCTS.
    /// </summary>
    public class Tree
    {
        // Defining Parent Node.
        internal Tree parentNode;
        // Defining the current game board positions at this node.
        internal char[,] boardPositions;
        // Defining children nodes for the current node
        internal List<Tree> childNodes;
        // Defining the visits made to this node
        public int Visits = 0;
        // Defining the node score
        public double Score = 0;
        // Defining bool to indicate if the node is a leaf node or not
        public bool IsLeafNode = true;
        // Creating a new guid which is used to for displaying Mermaid tree
        internal Guid guid = new Guid();

        /// <summary>
        /// This constructor is used to create an Instance of MCTS.
        /// </summary>
        /// <param name="positionsOfBoard">This parameter is used to define the game board positions.</param>
        /// <param name="parentNodeInfo">This parameter is used to define the information of the parent node.</param>
        public Tree(char[,] positionsOfBoard, Tree parentNodeInfo = null)
        {
            // Defining the board positions
            boardPositions = positionsOfBoard;
            // Defining the parent node information
            parentNode = parentNodeInfo;
            // Initialising an empty list of type Tree
            childNodes = new List<Tree>();
        }

        /// <summary>
        /// This field is used to retrive and set the information of the Parent Node. The type of this field is 'Tree'.
        /// </summary>
        public Tree ParentNode
        {
            get { return parentNode; }
            set { parentNode = value; }
        }

        /// <summary>
        /// This field is used to retrive the clone of game board position. The type of this field is a 2D array of type 'char'.
        /// </summary>
        public char[,] BoardPositions
        {
            // Returning a new clone of boardPositions not it's reference
            get { return (char[,])boardPositions.Clone(); } 
        }

        /// <summary>
        /// This field is used to retrive and set the information of the child nodes. The type of this field is a list of type 'Tree'.
        /// </summary>
        public List<Tree> ChildNodes
        {
            get { return childNodes; }
            set { childNodes = value; }
        }

        /// <summary>
        /// This field is used to retrive the GUID information. This field is used for generate structure for mermaid diagram. The type of this field is 'Guid'.
        /// </summary>
        public Guid GUID { get { return guid; } }
    }
}
