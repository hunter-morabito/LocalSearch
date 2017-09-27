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
       

        public GeneticAlgorithm() {
            //initialize list that will hold current generation
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
                //select and purge the bottom 50% of grids
                Select();
                //mutate remaining grids
                List<Grid> mutations = Mutate();
                //crossbreed remaining grids
                List<Grid> crossovers = Crossover();
                //make merge the three lists of mutations and crossovers
                parentGrids = parentGrids.Concat(mutations).Concat(crossovers).ToList();
            }
            //sort end list
            Select();
            //set the highest valued cell
            winner = parentGrids[parentGrids.Count - 1];
            //print highest valued cell
            PrintWinner();
            
        }

        private void Select()
        {
            //select the top 50% of the Grids
            parentGrids = parentGrids.OrderBy(o => o.value).ToList();
            //Console.WriteLine((parentGrids.Count / 2) - 1);
            parentGrids.RemoveRange(0, (parentGrids.Count / 2));
        }

        //takes current generation and alters a random cell
        private List<Grid> Mutate()
        {
            //create a list to keep mutations
            List<Grid> mutations = new List<Grid>();
            foreach(Grid grid in parentGrids)
            {
                //mutate cell
                grid.HillClimb(1);
                //add mutation to list
                mutations.Add(grid);
            }
            //return mutations
            return mutations;
        }

        private List<Grid> Crossover()
        {
            List<Grid> crossovers = new List<Grid>();
            Grid crossedGrid = new Grid(parentGrids[0].NumOfCol, LocalSearch.rand);

            for (int i = 0; i < parentGrids.Count; i++)
            {
                //set blank grid
                crossedGrid = new Grid(parentGrids[0].NumOfCol, LocalSearch.rand);
                Grid grid1;
                Grid grid2;

                //if i is at the end of the list, create a new grid with the last and first element 
                if ((i + 1) == parentGrids.Count)
                {
                    grid1 = parentGrids[0];
                    grid2 = parentGrids[i];
                }
                //iterate through list pairing a grid with the one next to it
                else
                {
                    grid1 = parentGrids[i];
                    grid2 = parentGrids[i + 1];
                }
                
                //crossover grids using random cells
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
                //add crossed grid to list
                crossovers.Add(crossedGrid);
            }

            return crossovers;
        }

        //prints highest valued grid
        public void PrintWinner()
        {
            parentGrids[parentGrids.Count - 1].PrintGrid();
        }

        //prints the current list of generation
        private void PrintParents()
        {
            foreach(Grid grid in parentGrids)
            {
                grid.PrintGrid();
            }
        }
    }
}
