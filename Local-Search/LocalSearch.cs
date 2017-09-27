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
        public static Random rand;

        public static void Main()
        {
            rand = new Random();

            LocalSearch ls = new LocalSearch();

            //double average = 0;
            //for (int i = 0; i < 50; i++)
            //{
            //    GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm();
            //    geneticAlgorithm.RunGeneticAlgorithm(5, 10, 20);
            //    average += geneticAlgorithm.winner.value;
            //}

            //average = average / 50;
            //Console.WriteLine(average);

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
                        if ((ls.grid = Task1()) != null)
                        {
                            ls.grid.PrintGrid();
                            break;
                        }

                        Console.WriteLine("returning to task selection... ");
                        break;
                    case 2:
                        Console.WriteLine("Puzzle Evaluation");
                        ls.grid = Task2();
                        Console.WriteLine("value of grid is: " + ls.grid.Evaluate());
                        ls.grid.PrintDepth();
                        break;
                    case 3:
                        Console.WriteLine("Basic Hill Climb");
                        ls.grid = Task1();
                        Console.WriteLine("input value for iterations (>=1): ");
                        int i = int.Parse(Console.ReadLine());
                        var watch0 = System.Diagnostics.Stopwatch.StartNew();
                        ls.grid.HillClimb(i);
                        watch0.Stop();
                        Console.WriteLine("time: " + watch0.ElapsedMilliseconds + " milliseconds");
                        ls.grid.PrintGrid();


                        break;
                    case 4:
                        Console.WriteLine("Basic Hill Climb with Random Restart");
                        ls.grid = Task1();
                        Console.WriteLine("=== Before Hill Function ===");
                        ls.grid.PrintGrid();
                        Console.WriteLine("input value for # of restarts");

                        int numRestart = int.Parse(Console.ReadLine());
                        Console.WriteLine("input value for # of iterations per restart: ");
                        int numIterationsPer = int.Parse(Console.ReadLine());
                        var watch1 = System.Diagnostics.Stopwatch.StartNew();
                        ls.grid.RandomRestarts(numRestart, numIterationsPer);
                        watch1.Stop();
                        Console.WriteLine("=== After Hill Function ===");
                        Console.WriteLine("time: " + watch1.ElapsedMilliseconds + " milliseconds");
                        ls.grid.PrintGrid();

                        break;
                    case 5:
                        Console.WriteLine("Basic Hill Climb with Random walk");
                        ls.grid = Task1();
                        Console.WriteLine("=== Before Random Walk ===");
                        ls.grid.PrintGrid();
                        Console.WriteLine("input value for r, the probability (0 <= r <= 1): ");
                        double r = double.Parse(Console.ReadLine());
                        Console.WriteLine("input number of times to run Hill Climb function: ");
                        int numOfHill = int.Parse(Console.ReadLine());
                        if ((r >= 0 && r <= 1) && (numOfHill >= 1))
                        {
                            var watch2 = System.Diagnostics.Stopwatch.StartNew();
                            ls.grid.RandomWalk(numOfHill, r);
                            watch2.Stop();
                            Console.WriteLine("=== After Random Walk ===");
                            Console.WriteLine("time: " + watch2.ElapsedMilliseconds + " milliseconds");
                            ls.grid.PrintGrid();
                            break;
                        }
                        else
                        {
                            break;
                        }
                    case 6:
                        ls.grid = Task1();
                        Console.WriteLine("=== Before Simulated Annealing ===");
                        ls.grid.PrintGrid();

                        Console.WriteLine("input number for Hill Clim function iterations: ");
                        int numOfHill2 = int.Parse(Console.ReadLine());
                        Console.WriteLine("input initial temperature: ");
                        double initTemp = double.Parse(Console.ReadLine());
                        Console.WriteLine("input temperature decay rate, r: ");
                        double r1 = double.Parse(Console.ReadLine());

                        var watch3 = System.Diagnostics.Stopwatch.StartNew();
                        ls.grid.SimulatedAnnealing(numOfHill2, initTemp, r1);
                        watch3.Stop();
                        Console.WriteLine("=== After Simulated Annealing ===");
                        Console.WriteLine("time: " + watch3.ElapsedMilliseconds + " milliseconds");
                        ls.grid.PrintGrid();
                        break;
                    case 7:
                        Console.WriteLine("GENETIC ALGORITHM");
                        Console.WriteLine("Enter # for nxn matrix: ");
                        int n = int.Parse(Console.ReadLine());
                        Check(n);
                        Console.WriteLine("Enter Start Sample Size: ");
                        int sampleSize = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Number of Iterations through Genetic Algorithm:");
                        int iterations = int.Parse(Console.ReadLine());
                        GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm();
                        geneticAlgorithm.RunGeneticAlgorithm(n, sampleSize, iterations);
                        break;
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

            return new Grid(n, rand);
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
