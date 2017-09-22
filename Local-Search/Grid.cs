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
        int rowNum;
        int colNum;

        public Grid() { }

        //Grid construct
        public Grid(int n)
        {
            int i = 0;
            int j = 0;
            Random rand = new Random();
            //number of rows
            rowNum = n;
            //number of columns
            colNum = n;
            //initializes space array
            cells = new CellNode[rowNum,colNum];

            //gives every space in array a value
            for (i  = 0; i < n; i++)
            {
                for(j = 0; j < n; j++)
                {
                    //returns random legal value
                    int value = getRand(i, j, rand);
                    //Console.Write(value);
                    //sets space value
                    cells[i, j] = new CellNode(value);
                }
            }

            //sets goal space
            cells[rowNum - 1, colNum - 1] = new CellNode(0);
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
            int ro = rand.Next(minValue,maxValue);
           
            return ro;
        }

        #region util
        public  void  PrintGrid()
        {
            for (int row = 0; row < rowNum; row++)
            {
                for (int col = 0; col < colNum; col++)
                {
                    Console.Write(cells[row,col] + " ");
                }
                Console.WriteLine();
            }
        }

        //used as grid from example in assignment
        public void makeExample()
        {
            
            this.cells[0, 0] = new CellNode(2);
            this.cells[0, 1] = new CellNode(2);
            this.cells[0, 2] = new CellNode(2);
            this.cells[0, 3] = new CellNode(4);
            this.cells[0, 4] = new CellNode(3);
            this.cells[1, 0] = new CellNode(2);
            this.cells[1, 1] = new CellNode(2);
            this.cells[1, 2] = new CellNode(3);
            this.cells[1, 3] = new CellNode(3);
            this.cells[1, 4] = new CellNode(3);
            this.cells[2, 0] = new CellNode(3);
            this.cells[2, 1] = new CellNode(3);
            this.cells[2, 2] = new CellNode(2);
            this.cells[2, 3] = new CellNode(3);
            this.cells[2, 4] = new CellNode(3);
            this.cells[3, 0] = new CellNode(4);
            this.cells[3, 1] = new CellNode(3);
            this.cells[3, 2] = new CellNode(2);
            this.cells[3, 3] = new CellNode(2);
            this.cells[3, 4] = new CellNode(2);
            this.cells[4, 0] = new CellNode(1);
            this.cells[4, 1] = new CellNode(2);
            this.cells[4, 2] = new CellNode(1);
            this.cells[4, 3] = new CellNode(4);
            this.cells[4, 4] = new CellNode(0);
            
        }
        #endregion
    }
}
