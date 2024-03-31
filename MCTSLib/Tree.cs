using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTSLib
{
    public class Tree
    {
        // Defining Parent Node.
        Tree parentNode;
        // Defining the current node info i.e., current game board positions at this node.
        char[,] boardPositions;
        // Defining children nodes for the current node
        List<Tree> childNodes;
        // Defining the visits made to this node
        public int Visits = 0;
        // Defining the node score
        public double Score = 0;
        // Defining bool to indicate if the node is a leaf node or not
        public bool IsLeafNode = true;
        // Defining list to store all the possible states
        public Guid guid;

        public Tree(char[,] positionsOfBoard, Tree parentNodeInfo = null)
        {
            boardPositions = positionsOfBoard;
            parentNode = parentNodeInfo;
            childNodes = new List<Tree>();
            guid = Guid.NewGuid();
        }

        public Tree ParentNode
        {
            get { return parentNode; }
            set { parentNode = value; }
        }

        public char[,] BoardPositions
        {
            // Returning a new clone of boardPositions not it's reference
            get { return (char[,])boardPositions.Clone(); } 
        }

        public List<Tree> ChildNodes
        {
            get { return childNodes; }
            set { childNodes = value; }
        }

        public Guid GUID
        {
            get { return guid; }
            set {  guid = value; }
        }

    }
}
