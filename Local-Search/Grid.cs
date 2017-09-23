using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Local_Search
{
    class Grid
    {
        public CellNode[,] cells;
        public int value;

        int rowNum;
        int colNum;

        public Grid() { }

        //Grid construct
        public Grid(int n)
        {
            int row = 0;
            int col = 0;
            Random rand = new Random();
            //number of rows
            rowNum = n;
            //number of columns
            colNum = n;
            //initializes space array
            cells = new CellNode[rowNum, colNum];

            //gives every space in array a value
            for (row = 0; row < n; row++)
            {
                for (col = 0; col < n; col++)
                {
                    //returns random legal value
                    int value = getRand(row, col, rand);
                    //Console.Write(value);
                    //sets space value
                    cells[row, col] = new CellNode(value, row, col);
                }
            }

            //sets goal space
            cells[rowNum - 1, colNum - 1] = new CellNode(0, rowNum - 1, colNum - 1);
        }

        public int getRand(int row, int col, Random rand)
        {
            int minValue = 1;
            int maxValue;
            //find Max of Left and Right
            maxValue = Math.Max((rowNum - row), (row - 1));
            //compare new Max to Up
            maxValue = Math.Max(maxValue, (colNum - col));
            //compare new Max to Down
            maxValue = Math.Max(maxValue, (col - 1));

            //return random int between 1 and maxvalue
            return rand.Next(minValue, maxValue);
        }

        #region util
        public void PrintGrid()
        {
            Console.WriteLine("Grid:");
            for (int row = 0; row < rowNum; row++)
            {
                for (int col = 0; col < colNum; col++)
                {
                    Console.Write(cells[row, col].moveNum + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void PrintDepth()
        {
            Console.WriteLine("Depth:");
            for (int row = 0; row < rowNum; row++)
            {
                for (int col = 0; col < colNum; col++)
                {
                    if (cells[row, col].depth == -1)
                    {
                        Console.Write("X ");
                    }
                    else
                    {
                        Console.Write(cells[row, col].depth + " ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("\nValue of Function is: " + value);
            
        }

        //used as grid from example in assignment
        public void makeExample1()
        {

            this.cells[0, 0] = new CellNode(2, 0, 0);
            this.cells[0, 1] = new CellNode(2, 0, 1);
            this.cells[0, 2] = new CellNode(2, 0, 2);
            this.cells[0, 3] = new CellNode(4, 0, 3);
            this.cells[0, 4] = new CellNode(3, 0, 4);
            this.cells[1, 0] = new CellNode(2, 1, 0);
            this.cells[1, 1] = new CellNode(2, 1, 1);
            this.cells[1, 2] = new CellNode(3, 1, 2);
            this.cells[1, 3] = new CellNode(3, 1, 3);
            this.cells[1, 4] = new CellNode(3, 1, 4);
            this.cells[2, 0] = new CellNode(3, 2, 0);
            this.cells[2, 1] = new CellNode(3, 2, 1);
            this.cells[2, 2] = new CellNode(2, 2, 2);
            this.cells[2, 3] = new CellNode(3, 2, 3);
            this.cells[2, 4] = new CellNode(3, 2, 4);
            this.cells[3, 0] = new CellNode(4, 3, 0);
            this.cells[3, 1] = new CellNode(3, 3, 1);
            this.cells[3, 2] = new CellNode(2, 3, 2);
            this.cells[3, 3] = new CellNode(2, 3, 3);
            this.cells[3, 4] = new CellNode(2, 3, 4);
            this.cells[4, 0] = new CellNode(1, 4, 0);
            this.cells[4, 1] = new CellNode(2, 4, 1);
            this.cells[4, 2] = new CellNode(1, 4, 2);
            this.cells[4, 3] = new CellNode(4, 4, 3);
            this.cells[4, 4] = new CellNode(0, 4, 4);

        }
        //used as grid from example in assignment
        public void makeExample2()
        {

            this.cells[0, 0] = new CellNode(3, 0, 0);
            this.cells[0, 1] = new CellNode(3, 0, 1);
            this.cells[0, 2] = new CellNode(2, 0, 2);
            this.cells[0, 3] = new CellNode(4, 0, 3);
            this.cells[0, 4] = new CellNode(3, 0, 4);
            this.cells[1, 0] = new CellNode(2, 1, 0);
            this.cells[1, 1] = new CellNode(2, 1, 1);
            this.cells[1, 2] = new CellNode(2, 1, 2);
            this.cells[1, 3] = new CellNode(1, 1, 3);
            this.cells[1, 4] = new CellNode(1, 1, 4);
            this.cells[2, 0] = new CellNode(4, 2, 0);
            this.cells[2, 1] = new CellNode(3, 2, 1);
            this.cells[2, 2] = new CellNode(1, 2, 2);
            this.cells[2, 3] = new CellNode(3, 2, 3);
            this.cells[2, 4] = new CellNode(4, 2, 4);
            this.cells[3, 0] = new CellNode(2, 3, 0);
            this.cells[3, 1] = new CellNode(3, 3, 1);
            this.cells[3, 2] = new CellNode(1, 3, 2);
            this.cells[3, 3] = new CellNode(1, 3, 3);
            this.cells[3, 4] = new CellNode(3, 3, 4);
            this.cells[4, 0] = new CellNode(1, 4, 0);
            this.cells[4, 1] = new CellNode(1, 4, 1);
            this.cells[4, 2] = new CellNode(3, 4, 2);
            this.cells[4, 3] = new CellNode(2, 4, 3);
            this.cells[4, 4] = new CellNode(0, 4, 4);

        }

        internal void AssignValue()
        {
            value = cells[rowNum - 1, colNum - 1].depth;
            if(value == -1)
            {
                value = 0;

                for(int i = 0; i < rowNum; i++)
                    for(int j =0; j< colNum; j++)
                        if (cells[i, j].depth == -1)
                            value -= 1;
                    
            }
        }
        #endregion
    }
}
