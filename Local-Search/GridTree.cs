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

        public GridTree(ref Grid grid)
        {
            BuildTree(ref grid);
        }

        public void BuildTree(ref Grid grid)
        {
            //create queue
            Queue<CellNode> queue = new Queue<CellNode>();
            //create binary array
            int[,] binaryArray = new int[grid.cells.GetLength(0), grid.cells.GetLength(1)];

            //set root to top left cell
            root = grid.cells[0, 0];
            //set Current Node
            CellNode currentNode = root;
            //add root to queue
            queue.Enqueue(root);
            //root is visited
            binaryArray[root.row, root.col] = 1;

            //loop through queue
            while(queue.Count > 0)
            {
                //remove currentNode from queue
                currentNode = queue.Dequeue();
                //legal check for going up 
                if ((currentNode.row - currentNode.moveNum) >= 0)
                {
                    //initialize contender
                    CellNode contender = grid.cells[(currentNode.row - currentNode.moveNum), currentNode.col];
                    //check if contender visited
                    if (!InTree(binaryArray, contender.row, contender.col))
                        //Add Node
                        AddNodeToTree(ref queue, ref currentNode, ref contender, ref binaryArray);
                }
                //legal check for going down
                if ((currentNode.row + currentNode.moveNum) < grid.cells.GetLength(0))
                {
                    //initialize contender
                    CellNode contender = grid.cells[(currentNode.row + currentNode.moveNum), currentNode.col];
                    //check if contender visited
                    if (!InTree(binaryArray, contender.row, contender.col))
                        //Add Node
                        AddNodeToTree(ref queue, ref currentNode, ref contender, ref binaryArray);
                }
                //legal check for going left
                if ((currentNode.col - currentNode.moveNum) >= 0)
                {
                    //initialize contender
                    CellNode contender = grid.cells[currentNode.row, (currentNode.col - currentNode.moveNum)];
                    //check if contender visited
                    if (!InTree(binaryArray, contender.row, contender.col))
                        //Add Node
                        AddNodeToTree(ref queue, ref currentNode, ref contender, ref binaryArray);
                }
                //legal check for going right
                if ((currentNode.col + currentNode.moveNum) < grid.cells.GetLength(1))
                {
                    //initialize contender
                    CellNode contender = grid.cells[currentNode.row, (currentNode.col + currentNode.moveNum)];
                    //check if contender visited
                    if (!InTree(binaryArray, contender.row, contender.col))
                        //Add Node
                        AddNodeToTree(ref queue, ref currentNode, ref contender, ref binaryArray);
                }

            }
                printBA(binaryArray);
   

            //recursive treverse and assign depth
        }

        private void AddNodeToTree(ref Queue<CellNode> queue, ref CellNode currentNode, ref CellNode contender, ref int[,] binaryArray)
        {
            //add contender to currentNode children
            currentNode.children.Add(contender);
            //add contender to queue
            queue.Enqueue(contender);
            //check as added to tree
            binaryArray[contender.row, contender.col] = 1;
        }

        private bool InTree(int[,] binaryArray, int row, int col)
        {
            return binaryArray[row,col] == 1 ? true : false;
        }

        private void printBA(int[,] ba)
        {
            for (int row = 0; row < ba.GetLength(0); row++)
            {
                for (int col = 0; col < ba.GetLength(1); col++)
                {
                    Console.Write(ba[row, col] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
