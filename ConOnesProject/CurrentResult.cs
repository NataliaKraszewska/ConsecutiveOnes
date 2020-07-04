using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConOnesProject
{
    class CurrentResult
    {
        public int cmax;
        public List<int> columnsOrder;
        public int[,] matrix;
        public List<int> columnToDelete;

        public CurrentResult(int in_cmax, List<int> in_columnsOrder, int[,] in_matrix, List<int> in_columnToDelete)
        {
            cmax = in_cmax;
            columnsOrder = in_columnsOrder;
            matrix = in_matrix;
            columnToDelete = in_columnToDelete;
        }
    }
}
