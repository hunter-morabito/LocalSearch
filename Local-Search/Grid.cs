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
        public Grid() { }

        //Grid construct
        public Grid(int n)
        {
            rand = new Random();
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
                        //Console.WriteLine("number is " + moveNum);
                        //Console.WriteLine("row is: " + count + " and col is: " + c);
                        //Console.Write(cells[count, c].moveNum + " ");
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

        #region Task 3 Functions
        public void HillClimb(int iterations)
        {
            //loop
            Grid testGrid;

            for (int i = 0; i < iterations; i++)
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

                //evaluate testGrid to get its value
                testGrid.Evaluate();

                //check to see if grid is solvable
                if (value >= 0)
                {
                    //check to see if new grid is solvable
                    if (testGrid.value >= 0)
                    {
                        //check to see if grid has improved
                        if (testGrid.value <= value)
                        {
                            //copy cells from test grid to this grid
                            CopyGrid(ref testGrid);
                        }
                    }
                }
                //grid is not solvable
                else
                {
                    //check to see if new grid has improved
                    if (testGrid.value >= value)
                    {
                        //copy cells from test grid to this grid
                        CopyGrid(ref testGrid);
                    }
                }


            }
            //USED FOR TESTING
            ToString();
            Console.WriteLine("Value of the grid after " + iterations + " iterations is: " + value);
            //USED FOR TESTING
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
            Console.WriteLine();
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
            Console.WriteLine("\nValue of the grid is: " + value);

        }

        //used as grid from example in assignment
        public void makeExample1()
        {

            this.cells[0, 0] = new CellNode(2, 0, 0);
            this.cells[0, 1] = new CellNode(2, 0, 1);
            this.cells[0, 2] = new CellNode(2, 0, 2);
            this.cells[0, 3] = new CellNode(4, 0, 3);
            this.cells[0, 4] = new CellNode(3, 0, 4);
            this.cells[1, 0] = new CellNode(2, 1, 0);
            this.cells[1, 1] = new CellNode(2, 1, 1);
            this.cells[1, 2] = new CellNode(3, 1, 2);
            this.cells[1, 3] = new CellNode(3, 1, 3);
            this.cells[1, 4] = new CellNode(3, 1, 4);
            this.cells[2, 0] = new CellNode(3, 2, 0);
            this.cells[2, 1] = new CellNode(3, 2, 1);
            this.cells[2, 2] = new CellNode(2, 2, 2);
            this.cells[2, 3] = new CellNode(3, 2, 3);
            this.cells[2, 4] = new CellNode(3, 2, 4);
            this.cells[3, 0] = new CellNode(4, 3, 0);
            this.cells[3, 1] = new CellNode(3, 3, 1);
            this.cells[3, 2] = new CellNode(2, 3, 2);
            this.cells[3, 3] = new CellNode(2, 3, 3);
            this.cells[3, 4] = new CellNode(2, 3, 4);
            this.cells[4, 0] = new CellNode(1, 4, 0);
            this.cells[4, 1] = new CellNode(2, 4, 1);
            this.cells[4, 2] = new CellNode(1, 4, 2);
            this.cells[4, 3] = new CellNode(4, 4, 3);
            this.cells[4, 4] = new CellNode(0, 4, 4);

        }
        //used as grid from example in assignment
        public void makeExample2()
        {

            this.cells[0, 0] = new CellNode(3, 0, 0);
            this.cells[0, 1] = new CellNode(3, 0, 1);
            this.cells[0, 2] = new CellNode(2, 0, 2);
            this.cells[0, 3] = new CellNode(4, 0, 3);
            this.cells[0, 4] = new CellNode(3, 0, 4);
            this.cells[1, 0] = new CellNode(2, 1, 0);
            this.cells[1, 1] = new CellNode(2, 1, 1);
            this.cells[1, 2] = new CellNode(2, 1, 2);
            this.cells[1, 3] = new CellNode(1, 1, 3);
            this.cells[1, 4] = new CellNode(1, 1, 4);
            this.cells[2, 0] = new CellNode(4, 2, 0);
            this.cells[2, 1] = new CellNode(3, 2, 1);
            this.cells[2, 2] = new CellNode(1, 2, 2);
            this.cells[2, 3] = new CellNode(3, 2, 3);
            this.cells[2, 4] = new CellNode(4, 2, 4);
            this.cells[3, 0] = new CellNode(2, 3, 0);
            this.cells[3, 1] = new CellNode(3, 3, 1);
            this.cells[3, 2] = new CellNode(1, 3, 2);
            this.cells[3, 3] = new CellNode(1, 3, 3);
            this.cells[3, 4] = new CellNode(3, 3, 4);
            this.cells[4, 0] = new CellNode(1, 4, 0);
            this.cells[4, 1] = new CellNode(1, 4, 1);
            this.cells[4, 2] = new CellNode(3, 4, 2);
            this.cells[4, 3] = new CellNode(2, 4, 3);
            this.cells[4, 4] = new CellNode(0, 4, 4);

        }
        #endregion
    }
}
