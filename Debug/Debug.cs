using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSudokuSolver.Debug
{
    class Debug
    {
        public static void PrintSudokuBoard(int[][] board)
        {
            for (int i=0;i<9;i++)
            {
                string line = string.Empty;
                for (int j=0;j<9;j++)
                {
                    line += board[i][j].ToString() + " ";
                }
                Console.WriteLine("{0}",line);
            }
        }

        public static void PrintSudokuBoard(List<List<Board.Cell>> cells)
        {
            for (int i = 0; i < 9; i++)
            {
                string line = string.Empty;
                for (int j = 0; j < 9; j++)
                {
                    line += cells[i][j].Value + " ";
                }
                Console.WriteLine("{0}", line);
            }
        }
    }
}
