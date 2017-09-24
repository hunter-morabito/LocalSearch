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
            Grid grid = new Grid(11/*int.Parse(Console.ReadLine())*/);
            Console.WriteLine();
            //sets grid to example in task 2
            //grid.makeExample1();
            grid.Evaluate();
            grid.HillClimb(4000);
            //grid.PrintGrid();
            //Task2
            //grid.Evaluate();
            //grid.PrintDepth();
            Console.WriteLine();
            //Task3
            //grid.HillClimb(5);

            /*grid.makeExample2();
            grid.PrintGrid();
            gridTree = new GridTree(ref grid);
            grid.PrintDepth();*/
        }
       
        
    }
}
