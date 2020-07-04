using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConOnesProject
{
    class CurrentMatrix
    {
        public int[,] newMatrix;
        public List<int> columnsOrder;

        public CurrentMatrix(int[,] in_matrix, List<int> in_columnsOrder)
        {
            newMatrix = in_matrix;
            columnsOrder = in_columnsOrder;
        }
    }
}
