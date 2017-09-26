using System;
using System.Collections.Generic;
using System.IO;
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

            Console.Write("Enter n for the nxn matrix for task 4: ");
            Grid grid = new Grid(int.Parse(Console.ReadLine()));
            //grid.RandomRestarts(3, 10);
            // grid.HillClimb(5000);
            //grid.RandomWalk(50, .20);
            grid.SimulatedAnnealing(50, 100, .9);
            grid.PrintGrid();
            
            LocalSearch ls = new LocalSearch();
            bool on = true;
            while (on)
            {
				Console.WriteLine("enter task number: ");
				int task = int.Parse(Console.ReadLine());
                switch (task)
                {
                    case 0:
                        Console.WriteLine("closing application...");
                        on = false;
                        break;
                    case 1:
                        Task1().PrintGrid();
                        break;
                    case 2:
                        ls.grid = Task2();
                        Console.WriteLine("value of grid is: " + ls.grid.Evaluate());
                        ls.grid.PrintDepth();
                        break;
                    case 3:
                        Console.WriteLine("enter 0 to implement on a randomly generated grid");
                        Console.WriteLine("enter 1 to implement on a file generated grid");
                        int opt;
                        if (int.TryParse(Console.ReadLine(), out opt))
                        {
                            if (opt == 0) {
                                ls.grid = Task1();
                            } else if (opt == 1) {
                                ls.grid = Task2();
                            } else {
                                Console.WriteLine("invalid number; returning to task selection...");
                                break;
                            }
                        }
                        int i;
                        Console.Write("enter # of iterations (>=50) for hill climb: ");
                        if ((i = int.Parse(Console.ReadLine())) >= 50)
                        {
                            ls.grid.HillClimb(i);
                            ls.grid.PrintGrid();
                        } else {
                            Console.WriteLine("invalid number; returning to task selection...");
                            break;
                        }

                        break;
                    case 4:
                        
                    case 5:
                    case 6:
                    case 7:
                    default:
                        Console.Error.WriteLine("task # must be between 0-7; 0 is to terminate");
                        break;
                }
            }
           
        }


        public static void Check(int n)
        {
            if (n != 5 && n != 7 && n != 9 && n != 11)
            {
                Console.Error.WriteLine("n must be 5, 7, 9, or 11");
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

            //Josh's Path for Files
            /*System.IO.StreamReader file =
					  new System.IO.StreamReader("../../../../../Downloads/" + name);*/

            //Hunter's Path for files
            StreamReader file = new StreamReader(Directory.GetCurrentDirectory() + "\\" + name);
            
            return new Grid(file);
		}
    }
}
