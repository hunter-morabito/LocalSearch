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

        Grid grid;

        public static void Main()
        {
            LocalSearch ls = new LocalSearch();
            Console.WriteLine("Enter task number: ");
            int task = int.Parse(Console.ReadLine());
            switch (task)
            {
                case 1:
                    Task1().PrintGrid();
                    break; 
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                default:
                    Console.Error.WriteLine("task # must be between 1-7.");
                    System.Environment.Exit(1);
                    break;
            }

<<<<<<< HEAD


            //grid.makeExample1();
            //grid.PrintGrid();
            /*GridTree gridTree = new GridTree(ref grid);
=======
            //sets grid to example in task 2
            grid.makeExample1();
            grid.PrintGrid();
            //Task2
            grid.Evaluate();
>>>>>>> 3d6867dfc4a636fb4b07188b417740fa7d5c2eb7
            grid.PrintDepth();
            Console.WriteLine();
            //Task3
            //grid.HillClimb(5);

            /*grid.makeExample2();
            grid.PrintGrid();
            gridTree = new GridTree(ref grid);
<<<<<<< HEAD
            grid.PrintDepth();

            Console.WriteLine("Enter the number of iterations for hill climbing process: ");
            int x = Console.Read();
            Console.WriteLine(x);*/
        }


        public static void Check(int n)
        {
            if (n != 5 && n != 7 && n != 9 && n != 11)
            {
                Console.Error.WriteLine("n must be 5, 7, 9, or 11.");
                System.Environment.Exit(1);
            }
=======
            grid.PrintDepth();*/
>>>>>>> 3d6867dfc4a636fb4b07188b417740fa7d5c2eb7
        }

        public static Grid Task1()
        {
            Console.WriteLine("Enter # for nxn matrix: ");
            int n = int.Parse(Console.ReadLine());
            Check(n);

            return new Grid(n); 
        }

        public static void Task2()
        {
			Console.WriteLine("Enter the name of the text file: ");
			string name = Console.ReadLine();
			System.IO.StreamReader file =
					  new System.IO.StreamReader("../../../../../Downloads/" + name);


			int n = int.Parse(file.ReadLine());
			Console.WriteLine();

		}
    }
}
