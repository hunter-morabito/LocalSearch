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
            Grid grid = new Grid(5/*int.Parse(Console.ReadLine())*/);

            grid.makeExample1();
            grid.PrintGrid();
            GridTree gridTree = new GridTree(ref grid);
            grid.PrintDepth();
            Console.WriteLine();

            grid.makeExample2();
            grid.PrintGrid();
            gridTree = new GridTree(ref grid);
            grid.PrintDepth();
        }
       
        
    }
}
