using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Local_Search
{
    class GridTree
    {
        public CellNode root;

        public GridTree(Grid grid)
        {
            BuildTree(grid);
        }

        public void BuildTree(Grid grid)
        {
            //create queue
            Queue<CellNode> queue = new Queue<CellNode>();
            //create binary array
            int[,] binaryArray = new int[grid.cells.GetLength(0), grid.cells.GetLength(1)];

            //set root to top left cell
            root = grid.cells[0, 0];
            //set Current Node to root
            CellNode currentNode = root;
            //add root to queue
            queue.Enqueue(root);
            //root is visited
            binaryArray[root.coordinate.row, root.coordinate.col] = 1;

            //loop through queue
            while (queue.Count > 0)
            {
                //remove currentNode from queue
                currentNode = queue.Dequeue();

                //legal check for going up 
                if (grid.IsLegalUp(currentNode))
                {
                    //initialize contender
                    CellNode contender = grid.cells[(currentNode.coordinate.row - currentNode.moveNum), currentNode.coordinate.col];
                    //check if contender visited
                    if (!InTree(binaryArray, contender.coordinate.row, contender.coordinate.col))
                        //Add Node
                        AddNodeToTree(ref queue, ref currentNode, ref contender, ref binaryArray);
                }

                //legal check for going down
                if (grid.IsLegalDown(currentNode))
                {
                    //initialize contender
                    CellNode contender = grid.cells[(currentNode.coordinate.row + currentNode.moveNum), currentNode.coordinate.col];
                    //check if contender visited
                    if (!InTree(binaryArray, contender.coordinate.row, contender.coordinate.col))
                        //Add Node
                        AddNodeToTree(ref queue, ref currentNode, ref contender, ref binaryArray);
                }

                //legal check for going left
                if (grid.IsLegalLeft(currentNode))
                {
                    //initialize contender
                    CellNode contender = grid.cells[currentNode.coordinate.row, (currentNode.coordinate.col - currentNode.moveNum)];
                    //check if contender visited
                    if (!InTree(binaryArray, contender.coordinate.row, contender.coordinate.col))
                        //Add Node
                        AddNodeToTree(ref queue, ref currentNode, ref contender, ref binaryArray);
                }

                //legal check for going right
                if (grid.IsLegalRight(currentNode))
                {
                    //initialize contender
                    CellNode contender = grid.cells[currentNode.coordinate.row, (currentNode.coordinate.col + currentNode.moveNum)];
                    //check if contender visited
                    if (!InTree(binaryArray, contender.coordinate.row, contender.coordinate.col))
                        //Add Node
                        AddNodeToTree(ref queue, ref currentNode, ref contender, ref binaryArray);
                }
            }//end loop through queue

            //PrintBinaryArray(binaryArray);

            //recursive treverse and assign depth
            AssignDepth(root, 0);
            
        }

        //Assigns each node in the grid its depth on the tree
        private void AssignDepth(CellNode current, int depth)
        {
            //base case
            if (current == null)
                return;

            //assign depth
            current.depth = depth;

            //recursive reverse
            foreach (CellNode node in current.children)
                AssignDepth(node, depth + 1);
            
        }

        private void AddNodeToTree(ref Queue<CellNode> queue, ref CellNode currentNode, ref CellNode contender, ref int[,] binaryArray)
        {
            //add contender to currentNode children
            currentNode.AddChild(contender);
            //add contender to queue
            queue.Enqueue(contender);
            //check as added to tree
            binaryArray[contender.coordinate.row, contender.coordinate.col] = 1;
        }

        //returns bool depending if node is in tree or not
        private bool InTree(int[,] binaryArray, int row, int col) 
        {
            return binaryArray[row, col] == 1 ? true : false;
        }

        private void PrintBinaryArray(int[,] ba)
        {
            Console.WriteLine("Binary Array:");
            for (int row = 0; row < ba.GetLength(0); row++)
            {
                for (int col = 0; col < ba.GetLength(1); col++)
                {
                    Console.Write(ba[row, col] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        
    }
}
