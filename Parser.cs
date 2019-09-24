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
        private static int[] ParseLine(string line)
        {
            string[] lineArray = line.Trim().Split(' ');
            int[] lineAsNumbers = new int[9];
            if (lineArray.Length != 9)
            {
                throw new InvalidSudokuException("Line too short, invalid sudoku puzzle");
            }
            for (int i=0;i<lineArray.Length;i++)
            {
                if (lineArray[i].Equals("-"))
                {
                    lineAsNumbers[i] = -1;
                } else
                {
                    lineAsNumbers[i] = int.Parse(lineArray[i]);
                }
            }
            return lineAsNumbers;
        }

        public static int[][] FileStringToSudoku(string fileName)
        {
            int[][] sudokuNums = new int[9][];

            try
            {
                int linePointer = 0;
                using (StreamReader reader = new StreamReader(fileName))
                {
                    while (reader.Peek() != -1)
                    {
                        string line = reader.ReadLine();
                        int[] sudokuLine = ParseLine(line);
                        sudokuNums[linePointer] = sudokuLine;
                        linePointer++;
                    }
                }
                if (linePointer != 9)
                {
                    throw new InvalidSudokuException("Not enough lines in file, invalid sudoku puzzle.");
                }
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return sudokuNums;
        }

        public static void SudokuBoardToFile(int[][] board, string fileName)
        {
            string fileText = string.Empty;
            for (int i=0;i<9;i++)
            {
                for (int j=0;j<9;j++)
                {
                    string cell;
                    int cellNum = board[i][j];
                    if (cellNum == -1)
                    {
                        cell = "-";
                    } else
                    {
                        cell = cellNum.ToString();
                    }
                    fileText += (cell + " ");
                }
                fileText += "\n";
            }
            File.WriteAllText(fileName,fileText);
        }
    }
}
