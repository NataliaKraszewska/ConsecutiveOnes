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
        public int[,] greedyHeuristicsMatrix;


        public TabuSearch(int[,] inputMatrix)
        {
            matrix = inputMatrix;
            TabuSearchAlgorythm();
        }


        public int[,] OutputMatrix()
        {
            return matrix;
        }


        public void ShowMatrix(int[,] matrix)
        {
            int matrixHeight = matrix.GetLength(0);
            int matrixWidth = matrix.GetLength(1);
            for (int i = 0; i < matrixHeight; i++)
            {
                for (int j = 0; j < matrixWidth; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }


        public void TabuSearchAlgorythm()
        {
            GreedyHeuristic initialMatrix = new GreedyHeuristic(matrix);
            greedyHeuristicsMatrix = initialMatrix.GreedyHeuristicAlgorythm();
            ShowMatrix(greedyHeuristicsMatrix);
            Console.WriteLine();
            CmaxEstimation cmaxEstimator = new CmaxEstimation(greedyHeuristicsMatrix);
            int cmaxValue = cmaxEstimator.GetCmaxValue();
        }
    }
}
