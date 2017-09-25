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
                    ls.grid = Task1();
                    ls.grid.PrintGrid();
                    break; 
                case 2:
                    ls.grid = Task2();
                    ls.grid.PrintGrid();
                    break;
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


        }


        public static void Check(int n)
        {
            if (n != 5 && n != 7 && n != 9 && n != 11)
            {
                Console.Error.WriteLine("n must be 5, 7, 9, or 11.");
                System.Environment.Exit(1);
            }

        }

        public static Grid Task1()
        {
            Console.WriteLine("Enter # for nxn matrix: ");
            int n = int.Parse(Console.ReadLine());
            Check(n);

            return new Grid(n); 
        }

        public static Grid Task2()
        {
			Console.WriteLine("Enter the name of the text file: ");
			string name = Console.ReadLine();
			System.IO.StreamReader file =
					  new System.IO.StreamReader("../../../../../Downloads/" + name);


            Grid grid = new Grid(file);
            return grid;
		}
    }
}
