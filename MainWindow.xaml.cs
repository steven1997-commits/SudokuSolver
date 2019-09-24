using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SimpleSudokuSolver.Debug;

namespace SimpleSudokuSolver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;
                int[][] sudoku = Parser.FileStringToSudoku(fileName);
                board.LoadBoard(sudoku);
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            board.KeyPressed(e);
        }

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.CurrentDirectory;
            if (saveFileDialog.ShowDialog() == true)
            {
                Parser.SudokuBoardToFile(board.GetBoard(),saveFileDialog.FileName);
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            board.ResetToInitialBoard();
        }

        private void SolveButton_Click(object sender, RoutedEventArgs e)
        {
            Solver solver = Solver.Instance;
            //don't load board, reset to initial 
            board.ResetToInitialBoard();
            solver.LoadBoard(board.GetBoard());
            string solved = solver.Solve(0);
            Console.WriteLine("Solved board: {0}",solved);
            if (solved.Equals(string.Empty))
            {
                MessageBox.Show("Could not be solved, please load another puzzle.");
            }
            board.LoadBoard(solved);
            MessageBox.Show("Puzzle solved! Please load a new puzzle.");
        }

        private void VerifyButton_Click(object sender, RoutedEventArgs e)
        {
            int[][] boardInt = board.GetBoard();
            Solver solver = Solver.Instance;
            solver.LoadBoard(boardInt);
            bool isVerified = solver.Verify();
            if (isVerified)
            {
                MessageBox.Show("Correct! Please reset or load a new puzzle.");
            } else
            {
                MessageBox.Show("Incorrect. Please try again.");
            }
        }

        private void CloseMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
