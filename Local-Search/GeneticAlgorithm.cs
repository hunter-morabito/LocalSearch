using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Local_Search
{
    class GeneticAlgorithm
    {
        List<Grid> parentGrids;
        public Grid winner;
        //double averageVal;

        public GeneticAlgorithm() {
            parentGrids = new List<Grid>();
        }

        public void RunGeneticAlgorithm(int n, int startSampleSize, int iterations)
        {
           
            //create list of random Grids
            for(int i = 0; i < startSampleSize; i++)
            {
                Grid newGrid = new Grid(n, LocalSearch.rand);
                parentGrids.Add(newGrid);
            }
            //PrintParents();
           
            //iterate through sample set
            for(int i = 0; i < iterations; i++)
            {
                //select
                Select();
                List<Grid> mutations = Mutate();
                List<Grid> crossovers = Crossover();
                parentGrids = parentGrids.Concat(mutations).Concat(crossovers).ToList();
            }
            Select();
            //PrintParents();
            winner = parentGrids[parentGrids.Count - 1];
            //PrintWinner();
            
        }

        private void Select()
        {
            //select the top 50% of the Grids
            parentGrids = parentGrids.OrderBy(o => o.value).ToList();
           // Console.WriteLine((parentGrids.Count / 2) - 1);
            parentGrids.RemoveRange(0, (parentGrids.Count / 2));
        }

        private List<Grid> Mutate()
        {
            List<Grid> mutations = new List<Grid>();
            foreach(Grid grid in parentGrids)
            {
                grid.HillClimb(1);
                mutations.Add(grid);
            }
            return mutations;
        }

        private List<Grid> Crossover()
        {
            List<Grid> crossovers = new List<Grid>();
            Grid crossedGrid = new Grid(parentGrids[0].NumOfCol, LocalSearch.rand);

            for (int i = 0; i < parentGrids.Count; i++)
            {
                crossedGrid = new Grid(parentGrids[0].NumOfCol, LocalSearch.rand);
                Grid grid1;
                Grid grid2;
                if ((i + 1) == parentGrids.Count)
                {
                    grid1 = parentGrids[0];
                    grid2 = parentGrids[i];
                    if (grid1 == null || grid2 == null)
                        Console.WriteLine("Probelm");
                }
                else
                {
                    grid1 = parentGrids[i];
                    grid2 = parentGrids[i + 1];
                    if (grid1 == null || grid2 == null)
                        Console.WriteLine("Probelm");
                }
                
                //get first grid
                //get second grid
                for (int row = 0; row < grid1.NumOfRows; row++)
                {
                    for (int col = 0; col < grid1.NumOfCol; col++)
                    {
                        //loop through and randomly add cells
                        if (LocalSearch.rand.NextDouble() >= .5)
                        {
                            crossedGrid.cells[row, col] = grid1.cells[row, col];
                        }
                        else
                        {
                            crossedGrid.cells[row, col] = grid2.cells[row, col];
                        }
                    }
                }
                crossovers.Add(crossedGrid);
            }

            return crossovers;
        }

        public void PrintWinner()
        {
            parentGrids[parentGrids.Count - 1].PrintGrid();
        }

        private void PrintParents()
        {
            foreach(Grid grid in parentGrids)
            {
                grid.PrintGrid();
            }
        }
    }
}
