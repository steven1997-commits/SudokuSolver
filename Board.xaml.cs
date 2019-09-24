using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SimpleSudokuSolver
{
    /// <summary>
    /// Interaction logic for Board.xaml
    /// </summary>
    public partial class Board : UserControl
    {

        private CellCollection cellCollection;
        private TextBlock selectedTextBlock = null;
        private string initialBoard = string.Empty;

        public Board()
        {
            InitializeComponent();
            this.cellCollection = new CellCollection(9);
            this.BoardControl.DataContext = this.cellCollection;
            for (int i = 0; i < 81; i++)
            {
                this.initialBoard += "0";
            }
        }

        public void ResetToInitialBoard()
        {
            this.LoadBoard(this.initialBoard);
        }

        public void LoadBoard(int[][] sudokuBoard)
        {
            List<List<Cell>> cells = this.cellCollection.RowCollection;
            this.initialBoard = string.Empty;
            for (int i=0;i<9;i++)
            {
                for (int j=0;j<9;j++)
                {
                    Cell cell = cells[i][j];
                    int num = sudokuBoard[i][j];
                    if (num > 0 && num < 10)
                    {
                        cell.Value = num.ToString();
                        this.initialBoard += num.ToString();
                    } else
                    {
                        cell.Value = string.Empty;
                        this.initialBoard += "0";
                    }
                }
            }
            //Debug.Debug.PrintSudokuBoard(((CellCollection)this.BoardControl.DataContext).RowCollection);
        }

        public void LoadBoard(string board)
        {
            List<List<Cell>> cells = this.cellCollection.RowCollection;
            this.initialBoard = board;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Cell cell = cells[i][j];
                    int num = (int)Char.GetNumericValue(this.initialBoard[(i * 9) + j]);
                    if (num > 0 && num < 10)
                    {
                        cell.Value = num.ToString();
                    }
                    else
                    {
                        cell.Value = string.Empty;
                    }
                }
            }
        }

        public int[][] GetBoard()
        {
            List<List<Cell>> cells = this.cellCollection.RowCollection;
            int[][] board = new int[9][];
            for (int i=0;i<9;i++)
            {
                int[] row = new int[9];
                for (int j=0;j<9;j++)
                {
                    if (cells[i][j].Value.Equals(string.Empty))
                    {
                        row[j] = -1;
                    }
                    else
                    {
                        row[j] = int.Parse(cells[i][j].Value);
                    }
                }
                board[i] = row;
            }
            return board;
        }

        public class Cell : INotifyPropertyChanged
        {
            private string value = string.Empty;
            
            public int CellNumber { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;
            public Cell(int cellNumber)
            {
                this.CellNumber = cellNumber;
            }

            private void NotifyPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            public string Value {
                get { return this.value; }
                set {
                    if (value != this.value)
                    {
                        this.value = value;
                        NotifyPropertyChanged("Value");
                    }
                }
            }
        }

        private class CellCollection
        {
            public List<List<Cell>> RowCollection { get; set; }

            public CellCollection(int size)
            {
                this.RowCollection = new List<List<Cell>>();
                for (int i = 0; i < size; i++)
                {
                    List<Cell> columns = new List<Cell>();
                    for (int j = 0; j < size; j++)
                    {
                        Cell c = new Cell(i * size + j);
                        //Cell c = new Cell((i * size + j).ToString(), i * size + j);
                        columns.Add(c);
                    }
                    this.RowCollection.Add(columns);
                }
            }
        }

        private void TextBlock_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            textBlock.Background = Brushes.LightGray;
            //Console.WriteLine("Entered: {0}",textBlock.Tag);
        }

        private void TextBlock_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            if (textBlock != this.selectedTextBlock)
            {
                textBlock.Background = Brushes.White;
            }
        }

        private void TextBlock_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ClearSelectedBlock();
            this.selectedTextBlock = (TextBlock)sender;
            this.selectedTextBlock.Background = Brushes.LightGray;
            //Console.WriteLine("Clicked {0}",this.selectedTextBlock.Tag);
        }

        public void KeyPressed(KeyEventArgs e)
        {
            if (this.selectedTextBlock != null)
            {
                string key = e.Key.ToString();
                if (key.Equals("Escape"))
                {
                    ClearSelectedBlock();
                    return;
                } else if (key.Equals("Back")) {
                    this.selectedTextBlock.Text = string.Empty;
                } else if (key.Length == 2)
                {
                    try
                    {
                        int keyNum = int.Parse(key.Substring(1, 1));
                        if (1 <= keyNum && 9 >= keyNum)
                        {
                            this.selectedTextBlock.Text = keyNum.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            //Debug.Debug.PrintSudokuBoard(((CellCollection)this.BoardControl.DataContext).RowCollection);
        }

        private void ClearSelectedBlock()
        {
            if (this.selectedTextBlock != null)
            {
                this.selectedTextBlock.Background = Brushes.White;
                this.selectedTextBlock = null;
            }
        }
    }
}
