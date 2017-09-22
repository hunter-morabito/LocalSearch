using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Local_Search
{
    class CellNode
    {
        public int moveNum;
        public int depth;
        public List<CellNode> children;

        public CellNode() { }
        public CellNode(int moveNum)
        {
            this.moveNum = moveNum;
            depth = 0;
            children = new List<CellNode>();
        }

        public void AddChild(CellNode child)
        {
            children.Add(child);
        }
    }
}
