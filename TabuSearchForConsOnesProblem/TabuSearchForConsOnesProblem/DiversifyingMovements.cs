using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabuSearchForConsOnesProblem
{
    class DiversifyingMovements
    {
        public int column;
        public int newPosition;

        public DiversifyingMovements(int in_column, int in_newPosition)
        {
            column = in_column;
            newPosition = in_newPosition;
        }
    }
}
