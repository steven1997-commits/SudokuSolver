using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSudokuSolver
{
    class Solver
    {
        string stringBoard;

        private Solver()
        {
        }

        private static Solver instance = null;

        public void LoadBoard(string board)
        {
            this.stringBoard = board;
        }

        public static Solver Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Solver();
                }
                return instance;
            }
        }

        public string Solve(int ind)
        {
            if (ind == 81)
            {
                return this.stringBoard;
            }

            int cell = (int)Char.GetNumericValue(this.stringBoard[ind]);
            if (cell > 0 && 10 > cell)
            {
                return Solve(ind + 1);
            } else
            {
                int row = ind / 9;
                int col = ind % 9;
                for (int i=1;i<=9;i++)
                {
                    StringBuilder sb = new StringBuilder(this.stringBoard);
                    sb[ind] = (char)(i + 48);
                    this.stringBoard = sb.ToString();
                    if (isRowAcceptable(row) && isColAcceptable(col) && isSubGridAcceptable(GetSubGridNum(row,col)))
                    {
                        string res = Solve(ind + 1);
                        if (!res.Equals(string.Empty))
                        {
                            return res;
                        }
                        sb[ind] = (char)48;
                        this.stringBoard = sb.ToString();
                    }
                }
            }

            return string.Empty;
        }

        private bool isSubGridAcceptable(int gridNum)
        {
            int[][] subGrid = this.GetSubGrid(gridNum);
            int count = 0;
            HashSet<int> cellNumbers = new HashSet<int>();

            foreach (int[] row in subGrid)
            {
                foreach (int cell in row)
                {
                    if (1 <= cell && cell <= 9)
                    {
                        count += 1;
                        cellNumbers.Add(cell);
                    }
                }
            }

            if (cellNumbers.Count == count)
            {
                return true;
            }
            return false;
        }

        private bool isColAcceptable(int colNum)
        {
            HashSet<int> cellNumbers = new HashSet<int>();
            int count = 0;
            for (int i = 0; i < 9; i++)
            {
                int index = (i * 9) + colNum;
                int cell = (int)Char.GetNumericValue(this.stringBoard[index]);
                if (1 <= cell && cell <= 9)
                {
                    count += 1;
                    cellNumbers.Add(cell);
                }
            }

            if (cellNumbers.Count != count)
            {
                return false;
            }

            return true;
        }

        private bool isRowAcceptable(int rowNum)
        {
            HashSet<int> cellNumbers = new HashSet<int>();
            int count = 0;
            int startIndex = rowNum * 9;
            int endIndex = startIndex + 9;
            for (int i = startIndex; i < endIndex; i++)
            {
                int cell = (int)Char.GetNumericValue(this.stringBoard[i]);
                if (1 <= cell && cell <= 9)
                {
                    count += 1;
                    cellNumbers.Add(cell);
                }
            }

            if (cellNumbers.Count != count)
            {
                return false;
            }
            return true;
        }

        public bool Verify()
        {
            bool subGridsVerified = VerifyAllSubGrids();
            bool rowsVerified = VerifyAllRows();
            bool colsVerified = VerifyAllColumns();

            if (subGridsVerified && rowsVerified && colsVerified)
            {
                return true;
            }

            return false;
        }

        public bool VerifyAllColumns()
        {
            for (int i=0;i<9;i++)
            {
                bool isColAcceptable = VerifyColumn(i);
                if (!isColAcceptable)
                {
                    //Console.WriteLine(i);
                    return false;
                }
            }
            return true;
        }

        public bool VerifyColumn(int colNum)
        {
            HashSet<int> cellNumbers = new HashSet<int>();
            for (int i=0;i<9;i++)
            {
                int index = (i * 9) + colNum;
                int cell = (int)Char.GetNumericValue(this.stringBoard[index]);
                if (1 <= cell && cell <= 9)
                {
                    cellNumbers.Add(cell);
                }
            }

            if (cellNumbers.Count != 9)
            {
                return false;
            }

            return true;
        }

        public bool VerifyAllRows()
        {
            for (int i=0;i<9;i++)
            {
                bool isRowAcceptable = VerifyRow(i);
                //Console.WriteLine("{0}",isRowAcceptable);
                if (!isRowAcceptable)
                {
                    //Console.WriteLine(i);
                    return false;
                }
            }

            return true;
        }

        public bool VerifyRow(int rowNum)
        {
            HashSet<int> cellNumbers = new HashSet<int>();
            int startIndex = rowNum * 9;
            int endIndex = startIndex + 9;
            for (int i=startIndex;i<endIndex;i++)
            {
                int cell = (int)Char.GetNumericValue(this.stringBoard[i]);
                if (1 <= cell && cell <= 9)
                {
                    cellNumbers.Add(cell);
                }
            }

            if (cellNumbers.Count != 9)
            {
                return false;
            }
            return true;
        }

        private bool VerifyAllSubGrids()
        {
            for (int i=0;i<9;i++)
            {
                bool isSubGridAcceptable = VerifySubGrid(i);
                if (!isSubGridAcceptable)
                {
                    //Console.WriteLine(i);
                    return false;
                }
            }

            return true;
        }

        private bool VerifySubGrid(int gridNum)
        {
            int[][] subGrid = this.GetSubGrid(gridNum);
            HashSet<int> cellNumbers = new HashSet<int>();

            foreach (int[] row in subGrid)
            {
                foreach (int cell in row)
                {
                    if (1 <= cell && cell <= 9)
                    {
                        cellNumbers.Add(cell);
                    }
                }
            }

            if (cellNumbers.Count == 9)
            {
                return true;
            }
            return false;
        }

        private int GetSubGridNum(int row, int col)
        {
            int gridNum = 0;

            gridNum += (row / 3) * 3;
            gridNum += (col / 3);

            return gridNum;
        }

        private int[][] GetSubGrid(int gridNum)
        {
            int[][] subGrid = new int[3][];

            int rowUpTo = ((gridNum % 3) + 1) * 3;
            int rowStart = (gridNum % 3) * 3;

            int colUpTo = ((gridNum / 3) + 1) * 3;
            int colStart = (gridNum / 3) * 3;

            int y = 0;
            for (int i=colStart;i<colUpTo;i++)
            {
                int[] row = new int[3];
                int x = 0;
                for (int j=rowStart;j<rowUpTo;j++)
                {
                    int index = (i * 9) + j;
                    row[x] = (int)Char.GetNumericValue(this.stringBoard[index]);
                    x++;
                }
                subGrid[y] = row;
                y++;
            }

            return subGrid;
        }
    }
}
