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
        int rowNum;
        int colNum;

        public Grid() { }

        //Grid construct
        public Grid(int n)
        {
            //number of rows
            rowNum = n;
            //number of columns
            colNum = n;
            //initializes space array
            spaces = new Space[n,n];

            //gives every space in array a value
            for (int row  = 0; row < n; row++)
            {
                for(int col = 0; col < n; col++)
                {
                    //returns random legal value
                    int value = getRand(row, col);
                    //sets space value
                    spaces[row,col] = new Space(value);
                }
            }

            //sets goal space
            spaces[rowNum-1, colNum-1] = new Space(0);
        }

        public int getRand(int row, int col)
        {
            int minValue = 1;
            int maxValue;
            //find Max of Left and Right
            maxValue = Math.Max((row - rowNum), (rowNum - row));
            //compare new Max to Up
            maxValue = Math.Max(maxValue, (col - colNum));
            //compare new Max to Down
            maxValue = Math.Max(maxValue, (colNum - col));
            Random rand = new Random();
            return rand.Next(minValue, maxValue);
        }

        public void toString()
        {
            for (int row = 0; row < rowNum; row++)
            {
                for (int col = 0; col < colNum; col++)
                {
                    Console.Write(spaces[row, col].num + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
