using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Local_Search
{
    /// <summary>
    /// This Class holds the row, col coordinates for a space on a grid
    /// </summary>
    class Coordinate
    {
        public int row;
        public int col;

        public Coordinate(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

        
        override public string ToString()
        {
            return "(" + row + "," + col + ")";
        }

        public bool Equals(Coordinate coordinate)
        {
            if (row == coordinate.row && col == coordinate.col)
                return true;
            return false;
        }
    }
}
