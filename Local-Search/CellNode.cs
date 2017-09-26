using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Local_Search
{
    class CellNode
    {
        public Coordinate coordinate;

        public int moveNum;
        public int depth;
        public List<CellNode> children;

        public CellNode() { }
        public CellNode(int moveNum, int row, int col)
        {
            coordinate = new Coordinate(row, col);

            this.moveNum = moveNum;
            depth = -1;
            children = new List<CellNode>();
        }
        

        public void AddChild(CellNode child)
        {
            children.Add(child);
        }
    }
}
