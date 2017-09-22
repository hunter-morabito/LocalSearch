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
            root = grid.cells[0, 0];

        }
    }
}
