using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConOnesProject
{
    public class ResultMatrix
    {
        public int[,] matrix;
        public int cmax;
        public List<int> columnsOrder;
        public List<int> columnsToDelete;
        public int diverse;
        public int neighborhood;
        public int tabuListSize;
        public int maxCountTheSameResult;
        public string seconds;

        public ResultMatrix(int [,] in_matrix, int in_cmax, List<int> in_columnsOrder, List<int> in_columnsToDelete, int in_deverse, int in_neighborhood, int in_tabuListSize, int in_maxCountTheSameResult, string in_seconds)
        {
            matrix = in_matrix;
            cmax = in_cmax;
            columnsOrder = in_columnsOrder;
            columnsToDelete = in_columnsToDelete;
            diverse = in_deverse;
            neighborhood = in_neighborhood;
            tabuListSize = in_tabuListSize;
            maxCountTheSameResult = in_maxCountTheSameResult;
            seconds = in_seconds;
        }
        
    }
}
