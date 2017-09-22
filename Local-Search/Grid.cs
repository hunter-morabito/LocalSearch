using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Local_Search
{
    public class Grid
    {
        Space[,] spaces;
        int rowNum;//test
        int colNum;

        public Grid() { }

        //Grid construct
        public Grid(int n, int m)
        {
            int i = 0;
            int j = 0;
            Random rand = new Random();
            //number of rows
            rowNum = n;
            //number of columns
            colNum = m;
            //initializes space array
            spaces = new Space[rowNum,colNum];

            //gives every space in array a value
            for (i  = 0; i < n; i++)
            {
                for(j = 0; j < m; j++)
                {
                    //returns random legal value
                    int value = getRand(i, j, rand);
                    //Console.Write(value);
                    //sets space value
                    spaces[i,j] = new Space(value);
                }
            }

            //sets goal space
            spaces[rowNum-1, colNum-1] = new Space(0);
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

        public void toString()
        {
            for (int row = 0; row < rowNum; row++)
            {
                for (int col = 0; col < colNum; col++)
                {
                    Console.Write(spaces[row,col].num + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
