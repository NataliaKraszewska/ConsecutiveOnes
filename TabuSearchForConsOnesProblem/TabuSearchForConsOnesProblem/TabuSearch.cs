using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabuSearchForConsOnesProblem
{
    class TabuSearch
    {
        public int[,] matrix;
        

        public TabuSearch(int [,] inputMatrix)
        {
            matrix = inputMatrix;
            TabuSearchAlgorythm();
        }

        
        public int [,] OutputMatrix()
        {
            return matrix;
        }

        public void TabuSearchAlgorythm()
        {
            GreedyHeuristic initialMatrix = new GreedyHeuristic(matrix);

        }
        

    }

}
