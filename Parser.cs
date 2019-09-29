using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SimpleSudokuSolver.Exceptions;

namespace SimpleSudokuSolver
{
    class Parser
    {
        public static string FileStringToSudoku(string fileName)
        {
            string sudokuBoard = string.Empty;

            using (StreamReader reader = new StreamReader(fileName))
            {
                if (reader.Peek() != -1)
                {
                    sudokuBoard = reader.ReadLine();
                    if (sudokuBoard.Length != 81)
                    {
                        throw new InvalidSudokuException("Line should be 81 characters long.");
                    }
                }
                else
                {
                    throw new InvalidSudokuException("Not enough lines in file, invalid sudoku puzzle.");
                }
            }
            return sudokuBoard;
        }

        public static void SudokuBoardToFile(string board, string fileName)
        {
            File.WriteAllText(fileName,board);
        }
    }
}
