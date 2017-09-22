using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;


namespace Local_Search
{
    class LocalSearch
    {
        public static void Main()
        {
            Console.Write("Enter n for the nxn matrix: ");
            Grid grid = new Grid(int.Parse(Console.ReadLine()));
            grid.makeExample();
            grid.PrintGrid();

            GridTree gridTree = new GridTree(grid);

        }
       
        
    }
}
