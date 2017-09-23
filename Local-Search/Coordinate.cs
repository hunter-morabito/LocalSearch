using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Local_Search
{
    class Coordinate
    {
        public int row;
        public int col;

        public Coordinate(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

        public bool Equals(Coordinate coordinate)
        {
            if(row == coordinate.row && col == coordinate.col) 
                return true;
            return false;
        }
    }
}
