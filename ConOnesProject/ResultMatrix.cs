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
        public int tabuListSize;
        public int maxCountTheSameResult;
        public string seconds;
        public int divStepsVal;
        public int newResultCount;
        public int percentageOfDivSteps;


        public ResultMatrix(int [,] in_matrix, int in_cmax, List<int> in_columnsOrder, List<int> in_columnsToDelete, int in_divStepsVal, int in_newResultCount, int in_tabuListSize, int in_maxCountTheSameResult, string in_seconds, int in_percentageOfDivSteps)
        {
            matrix = in_matrix;
            cmax = in_cmax;
            columnsOrder = in_columnsOrder;
            columnsToDelete = in_columnsToDelete;
            divStepsVal = in_divStepsVal;
            newResultCount = in_newResultCount;
            tabuListSize = in_tabuListSize;
            maxCountTheSameResult = in_maxCountTheSameResult;
            seconds = in_seconds;
            percentageOfDivSteps = in_percentageOfDivSteps;
        }
        
    }
}
