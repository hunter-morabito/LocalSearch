using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Local_Search
{
    class Grid
    {
        public CellNode[,] cells;
        public int value;

        public int NumOfRows { get; }
        public int NumOfCol { get; }

        public Coordinate goalCoordinate;

        private Random rand;

        #region Constructors
        public Grid()
        {
            cells = new CellNode[NumOfRows, NumOfCol];
        }

        //Grid construct
        public Grid(int n, Random rand)
        {
            this.rand = rand;
            NumOfRows = n;
            NumOfCol = n;

            cells = new CellNode[NumOfRows, NumOfCol];

            //gives every space in array a value
            for (int row = 0; row < n; row++)
            {
                for (int col = 0; col < n; col++)
                {
                    //returns random legal value
                    int moveNum = getRandMoveNum(row, col);
                    //sets space value
                    cells[row, col] = new CellNode(moveNum, row, col);
                }
            }

            //sets goal space
            cells[NumOfRows - 1, NumOfCol - 1] = new CellNode(0, NumOfRows - 1, NumOfCol - 1);
            goalCoordinate = new Coordinate(NumOfRows - 1, NumOfCol - 1);

            //Evaluate Grid
            Evaluate();
        }

        //constructor for input text files
        public Grid(System.IO.StreamReader file)
        {
            //set random value

            rand = new Random();

            string line; // variable to read the file line by line
            int n, count = 0; // n is the output for the first line (size of matrix). 
            // if it parses a number, it will create the cells[n, n];
            if (int.TryParse(file.ReadLine(), out n))
            {
                cells = new CellNode[n, n];
                NumOfCol = NumOfRows = n;
            }
            else
            {
                Console.Error.WriteLine("error: not a valid number");
                System.Environment.Exit(1);
            }

            while ((line = file.ReadLine()) != null)
            { // continue reading from the file, line by line, until we reach the end
                int col = 0; // variable for which column spot we are on; it resets to zero when on a new row (line) 
                for (int j = 0; j < line.Length; j++)
                {
                    if (char.IsNumber(line[j]))
                    { // if the current line's spot is a number, then we add that number into the corresponding cell. 
                        int moveNum = int.Parse(line[j].ToString());
                        cells[count, col] = new CellNode(moveNum, count, col);
                        // when the new CellNode instance is created, it prints properly, but in LocalSearch.cs, it doesn't print at all..
                        col++;
                    }
                }

                count++;
            }

            file.Close();

            goalCoordinate = new Coordinate(n - 1, n - 1);

            Evaluate();
        }

        //contructor to duplicate grid
        public Grid(Grid oldGrid)
        {
            //set random var
            rand = oldGrid.rand;
            //set row number
            NumOfRows = oldGrid.NumOfRows;
            //number of columns
            NumOfCol = oldGrid.NumOfCol;
            //initializes space array
            cells = new CellNode[NumOfRows, NumOfCol];

            value = oldGrid.value;

            for (int row = 0; row < NumOfRows; row++)
            {
                for (int col = 0; col < NumOfCol; col++)
                {
                    cells[row, col] = new CellNode(oldGrid.cells[row, col].moveNum, row, col);
                }
            }
            goalCoordinate = new Coordinate(NumOfRows - 1, NumOfCol - 1);

        }
        #endregion

        #region Task 2 Functions
        public int Evaluate()
        {
            GridTree gridTree = new GridTree(this);
            //assign grid its value
            AssignValue();
            return value;
        }

        internal void AssignValue()
        {
            //gets depth 
            value = cells[NumOfRows - 1, NumOfCol - 1].depth;
            //if the value is -1, count cells that are not on GridTree
            if (value == -1)
            {
                value = 0;

                for (int i = 0; i < NumOfRows; i++)
                    for (int j = 0; j < NumOfCol; j++)
                        if (cells[i, j].depth == -1)
                            value -= 1;
            }
        }
        #endregion

        #region Task 3 & 4 & 5 Functions
        //TASK 3 
        /// <summary>
        /// Hill climb will alter a non-goal cell's moveNum to a different moveNum and then evaluate that grid
        /// If that grid is of a higher value,  then the change will be accepted and the grid will update to 
        /// changed grid
        /// </summary>
        /// <param name="iterations">Enter number of Iterations per HillClimb</param>
        public void HillClimb(int iterations) => HillClimb(iterations, 0);
        private void HillClimb(int iterations, double randomWalkProbability)
        {
            //loop
            Grid testGrid;

            //loops until given iterations hit
            for (int i = 0; i < iterations; i++)
            {

                //make new grid copy
                testGrid = new Grid(this);

                //get a rand coordinate thats not the goal
                Coordinate randomCoordinate = getRandCoordinate();

                //loops until the cell in the new coordinate has a different moveNum
                do
                {
                    //get new moveNum
                    testGrid.cells[randomCoordinate.row, randomCoordinate.col].moveNum = getRandMoveNum(randomCoordinate.row, randomCoordinate.col);
                    //check to make sure movenum is not currently held in given coordinate
                } while (testGrid.cells[randomCoordinate.row, randomCoordinate.col].moveNum == cells[randomCoordinate.row, randomCoordinate.col].moveNum);
                //set value to new test grid
                testGrid.Evaluate();
                //update grid given both values
                UpdateGrid(ref testGrid, randomWalkProbability);
            }
        }


        //TASK 4
        /// <summary>
        /// Random restarts is a loop around hill climb with random grids trying to get the best value
        /// </summary>
        /// <param name="numberOfRestarts">Enter the number of times you would like Hill Climb to restart</param>
        /// <param name="iterationsPerRestart">Enter Number of iterations per Hill Climb</param>
        public void RandomRestarts(int numberOfRestarts, int iterationsPerRestart)
        {
            //copy the current grid as the new test grid
            Grid testGrid = new Grid(this);


            //loop for as many restarts as user input
            for (int restartCounter = 0; restartCounter < numberOfRestarts; restartCounter++)
            {
                /* Run the Hill Climb Method on the New Random state grid
                 * If that random state test grid has a better value, then the 
                 * current grid will copy the cells in the random state grid
                 * into this grid object, and it will become the new best 
                 * valued grid.
                 */
                testGrid.HillClimb(iterationsPerRestart);
                //Update to the better valued grid
                UpdateGrid(ref testGrid);
                //PrintGrid();
                //generate new random state test grid
                testGrid = new Grid(NumOfRows, LocalSearch.rand);
            }
        }

        //TASK 5
        /// <summary>
        /// Random walk is the same steps as HillClimb(), just with an added probability the grid does not update
        /// </summary>
        /// <param name="hillClimbIterations">Enter number of iterations per Hill Climb</param>
        /// <param name="randomWalkProbability">Enter the probability (0 - 1) that the Hill Climb change will not be accepted</param>
        public void RandomWalk(int hillClimbIterations, double randomWalkProbability) => HillClimb(hillClimbIterations, randomWalkProbability);

        //if calling just UpdateGrid, forward to other function with a random walk probability of '0'
        private void UpdateGrid(ref Grid testGrid) => UpdateGrid(ref testGrid, 0);
        private void UpdateGrid(ref Grid testGrid, double randomWalkProbabilty)
        {
            /* If the randomWalkProbability is greater than the random number, then walk;
             * The random number will never be below 0
             */
            if (randomWalkProbabilty >= rand.NextDouble())
            {
                CopyGrid(ref testGrid);
            }
            //no random walk occurred
            else
            {
                //check to see if grid is solvable
                if (value >= 0)
                {
                    //check to see if new grid is solvable
                    if (testGrid.value >= 0)
                    {
                        //check to see if grid has improved
                        if (testGrid.value >= value)
                        {
                            //copy cells from test grid to this grid
                            CopyGrid(ref testGrid);

                        }
                    }
                }
                //grid is not solvable
                else
                {
                    //check to see if new grid has improved or gone out of 0
                    if (testGrid.value >= value)
                    {
                        //copy cells from test grid to this grid
                        CopyGrid(ref testGrid);
                    }
                }
            }
        }
        #endregion

        #region Task 6 Functions
        /// <summary>
        /// Simulated annealing process. This will run Hill Climb and then make a more informed decision as to whether
        /// or not to accept a downhill change or not.
        /// It uses the formula exp(-(Value(testgrid) - Value(currentgrid)) / Temperature)
        /// Through each iteration the temperature will begin to cool through a given decay rate
        /// As the iterations increase, the chance of a random walk will fall
        /// </summary>
        /// <param name="hillClimbIterations">Enter number of Iterations per Hill climb</param>
        /// <param name="initialTemp">Enter the initial temperature to begin at</param>
        /// <param name="tempertureDecayRate">Enter the rate of decay the temp decreases at per iteration</param>
        public void SimulatedAnnealing(int hillClimbIterations, double initialTemp, double tempertureDecayRate)
        {
            //loop
            Grid testGrid;

            double currentTemp = initialTemp;

            //loops until given iterations hit
            for (int i = 0; i < hillClimbIterations; i++)
            {
                //make new grid copy
                testGrid = new Grid(this);

                //get a rand coordinate thats not the goal
                Coordinate randomCoordinate = getRandCoordinate();

                //loops until the cell in the new coordinate has a different moveNum
                do
                {
                    testGrid.cells[randomCoordinate.row, randomCoordinate.col].moveNum = getRandMoveNum(randomCoordinate.row, randomCoordinate.col);
                } while (testGrid.cells[randomCoordinate.row, randomCoordinate.col].moveNum == cells[randomCoordinate.row, randomCoordinate.col].moveNum);
                testGrid.Evaluate();

                //upgrade the grid depending on the results of the simulated anneal
                UpdateGridSimulatedAnnealing(ref testGrid, currentTemp);

                //decay the temperature
                currentTemp = currentTemp * tempertureDecayRate;
            }
        }

        private void UpdateGridSimulatedAnnealing(ref Grid testGrid, double currentTemp)
        {

            if (value >= 0)
            {
                //check to see if new grid is solvable
                if (testGrid.value >= 0)
                {
                    //check to see if grid has improved
                    if (testGrid.value >= value)
                    {
                        //copy cells from test grid to this grid
                        CopyGrid(ref testGrid);

                    }
                    else if (PassedSimulatedAnneal(currentTemp, testGrid))
                    {
                        //copy cells from test grid to this grid
                        CopyGrid(ref testGrid);
                    }
                }
            }
            //grid is not solvable
            else
            {
                //check to see if new grid has improved out of 0
                if (testGrid.value >= value)
                {
                    //copy cells from test grid to this grid
                    CopyGrid(ref testGrid);
                }
                else if (PassedSimulatedAnneal(currentTemp, testGrid))
                {
                    //copy cells from test grid to this grid
                    CopyGrid(ref testGrid);
                }
            }
        }

        /// <summary>
        /// Returns true if simulated anneal passed the probability
        /// Function is exp( -(V(testGrid) - V(grid)) / currentTemp )
        /// If the Random Number is less than the probability, then go downhill;
        /// If the temperature is high, then the probability value will be set high.
        /// If the probability is set high, then there is a greater chance of returning true and 
        /// Running Downhill.
        /// 
        /// lim (fn) = 0, meaning it is more likely to go downhill the higher the temperature
        /// T-> Infinity
        /// 
        /// lim (fn) = infinity, meaning it is less likely to go downhill the lower the temp
        /// T-> 0
        /// </summary>
        /// <param name="currentTemp"></param>
        /// <param name="testGrid"></param>
        /// <returns></returns>
        private bool PassedSimulatedAnneal(double currentTemp, Grid testGrid)
        {
            //gets the probability of a downhill move given the formula
            double downhillProbability = Math.Exp(-(testGrid.value - value) / currentTemp);
            double randomNumber = rand.NextDouble();
            //if the probability is higher than the random number, then move downhill
            return (downhillProbability >= randomNumber) ? true : false;
        }
        #endregion

        #region Cell Functions

        private void CopyGrid(ref Grid testGrid)
        {
            for (int row = 0; row < NumOfRows; row++)
                for (int col = 0; col < NumOfCol; col++)
                    cells[row, col] = testGrid.cells[row, col];
            value = testGrid.value;
        }

        #region Legal Checks
        internal bool IsLegalCell(CellNode cellNode)
        {
            if (!IsLegalUp(cellNode))
                if (!IsLegalDown(cellNode))
                    if (!IsLegalLeft(cellNode))
                        if (!IsLegalRight(cellNode))
                            return false;
            return true;
        }
        internal bool IsLegalUp(CellNode cellNode)
        {
            return (cellNode.coordinate.row - cellNode.moveNum) >= 0 ? true : false;
        }
        internal bool IsLegalDown(CellNode cellNode)
        {
            return ((cellNode.coordinate.row + cellNode.moveNum) < cells.GetLength(0)) ? true : false;
        }
        internal bool IsLegalLeft(CellNode cellNode)
        {
            return (cellNode.coordinate.col - cellNode.moveNum) >= 0 ? true : false;
        }
        internal bool IsLegalRight(CellNode cellNode)
        {
            return (cellNode.coordinate.col + cellNode.moveNum) < cells.GetLength(1) ? true : false;
        }
        #endregion

        private int getRandMoveNum(int row, int col)
        {
            int minValue = 1;
            int maxValue;
            //find Max of Left and Right
            maxValue = Math.Max(NumOfRows - row, row);
            //compare new Max to Up
            maxValue = Math.Max(maxValue, NumOfCol - col);
            //compare new Max to Down
            maxValue = Math.Max(maxValue, col);

            //return random int between 1 and maxvalue
            return rand.Next(minValue, maxValue);
        }

        //gets random grid coordinate
        private Coordinate getRandCoordinate()
        {
            //init
            Coordinate randCoordinate;
            //loop through at least once
            do
            {
                //init random coordinate
                randCoordinate = new Coordinate(rand.Next(0, NumOfRows), rand.Next(0, NumOfCol));
                //randCoordinate.ToString();
            } while (randCoordinate.Equals(goalCoordinate));
            //check to see if its the same at the goal coordinate

            return randCoordinate;
        }
        #endregion

        #region Print Functions

        public void PrintGrid()
        {
            Console.WriteLine("Grid:");
            for (int row = 0; row < NumOfRows; row++)
            {
                for (int col = 0; col < NumOfCol; col++)
                {
                    Console.Write(cells[row, col].moveNum + " ");
                }
                Console.WriteLine();
            }
            PrintValue();
        }

        public void PrintDepth()
        {
            Console.WriteLine("Depth:");
            for (int row = 0; row < NumOfRows; row++)
            {
                for (int col = 0; col < NumOfCol; col++)
                {
                    if (cells[row, col].depth == -1)
                    {
                        Console.Write("X ");
                    }
                    else
                    {
                        Console.Write(cells[row, col].depth + " ");
                    }
                }
                Console.WriteLine();
            }
            //PrintValue();
        }

        public void PrintValue()
        {
            Console.WriteLine("\nValue of the grid is: " + value);
        }
        #endregion
    }
}
