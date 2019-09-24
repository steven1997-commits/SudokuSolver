using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSudokuSolver.Exceptions
{
    class InvalidSudokuException : Exception
    {
        public InvalidSudokuException()
        {

        }

        public InvalidSudokuException(string Message) : base(Message)
        {
        }

        public InvalidSudokuException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
