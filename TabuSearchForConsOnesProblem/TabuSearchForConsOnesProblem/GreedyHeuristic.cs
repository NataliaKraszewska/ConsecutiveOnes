using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabuSearchForConsOnesProblem
{
    class GreedyHeuristic
    {
        int[,] matrix;
        int[,] greedyHeuristicsMatrix;
        public List<int> columnsOrderInOryginalMatrix;

        public GreedyHeuristic(int [,] inputMatrix)
        {
            matrix = inputMatrix;
            GreedyHeuristicAlgorythm();
        }


        bool IsMatrixConsecutiveOnes(int [,] matrix)
        {
            int matrixWidth = matrix.GetLength(1);
            int matrixHeight = matrix.GetLength(0);
            for(int i = 0; i < matrixHeight; i++)
            {
                bool foundOne = false;
                bool foundZeroAfterOne = false;

                for(int j = 0; j < matrixWidth; j++)
                {
                    if(!foundOne && matrix[i, j] == 1)
                    {
                        foundOne = true;
                    }
                    if(foundOne && matrix[i, j] == 0)
                    {
                        foundZeroAfterOne = true;
                    }
                    if(foundZeroAfterOne && matrix[i,j] == 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        bool IsColumnMatchesTheMatrix(int[,] matrix, int column, List<int> columnsOrder, int checkPointOnMatrix)
        {
            if (columnsOrder.Count - checkPointOnMatrix < 2)
            {
                return true;
            }
            else
            {
                int[,] matrixToCheck;
                int matrixHeight = matrix.GetLength(0);
                matrixToCheck = new int[matrixHeight, columnsOrder.Count + 1];

                for (int i = 0; i < matrixHeight; i++)
                {
                    for (int j = checkPointOnMatrix; j < columnsOrder.Count; j++)
                    {
                        matrixToCheck[i, j] = matrix[i, columnsOrder[j]];
                    }
                }
                for (int i = 0; i < matrixHeight; i++)
                {
                    matrixToCheck[i, matrixToCheck.GetLength(1) - 1] = matrix[i, column];
                }

                return IsMatrixConsecutiveOnes(matrixToCheck);
            }
        }


        List<int> SortColumnsToAddByNumberOfOnes()
        {
            int matrixHeight = matrix.GetLength(0);
            int matrixWidth = matrix.GetLength(1);
            List<int> sortedColumns = new List<int>();
            List<OnesCount> onesCount = new List<OnesCount>();
            
            for (int i = 0; i < matrixWidth; i++)
            {
                int count = 0;
                for(int j = 0; j <matrixHeight; j++)
                {
                    if (matrix[j, i] == 1)
                    {
                        count += 1;
                    }
                }
                OnesCount onesCountInColumn = new OnesCount(count, i);
                onesCount.Add(onesCountInColumn);
            }

            List<OnesCount> sortedOnes = onesCount.OrderBy(o => o.onesCount).ToList();
            sortedOnes.Reverse();
            for(int i = 0; i<sortedOnes.Count; i++)
            {
                sortedColumns.Add(sortedOnes[i].numberOfColumn);
            }


            return sortedColumns;
        }

        public int[,] GreedyHeuristicAlgorythm()
        {
            int matrixHeight = matrix.GetLength(0);
            int matrixWidth = matrix.GetLength(1);
            var columnsOrder = new List<int>();
            var columnsToAdd = new List<int>();
            Random r = new Random();


            List<int> columnsOrderedByNumberOfOnes = SortColumnsToAddByNumberOfOnes();
            int firstColumn = columnsOrderedByNumberOfOnes[0];
            columnsToAdd.Add(firstColumn);
            columnsOrder = columnsOrderedByNumberOfOnes;
            
            int numberOfColumnsToAdd = columnsToAdd.Count;
            int checkPointOnMatrix = 0;
            //dodajemy kolejno kolumny do rozwiazania
            for(int i = 0; i < numberOfColumnsToAdd; i++)
            {
                bool foundColumnWhichMatchesTheMatrix = false;
                for (int columnIndex = 0; columnIndex < columnsToAdd.Count; columnIndex++)
                {
                    if (!columnsOrder.Contains(columnsToAdd[columnIndex]) && IsColumnMatchesTheMatrix(matrix, columnsToAdd[columnIndex], columnsOrder, checkPointOnMatrix))
                    {//jeli macierz spelnia warunek cons1 po dodaniu kolumny
                        columnsOrder.Add(columnsToAdd[columnIndex]);
                        foundColumnWhichMatchesTheMatrix = true;
                        break;
                    }
                }

                for (int columnIndex = 0; columnIndex < columnsOrder.Count; columnIndex++) //czyscimy liste kandydatow na macierze z listy
                    columnsToAdd.Remove(columnsOrder[columnIndex]);

                if (!foundColumnWhichMatchesTheMatrix)//jesli nie spelnia to bierzemy losowa i szukamy od nowa
                {
                    columnsOrderedByNumberOfOnes = SortColumnsToAddByNumberOfOnes();
                    firstColumn = columnsOrderedByNumberOfOnes[0];
                    columnsToAdd.Add(firstColumn);

                    checkPointOnMatrix = columnsOrder.Count;
                }
            }

            greedyHeuristicsMatrix = new int[matrixHeight, matrixWidth];
            for (int i = 0; i < matrixHeight; i++)
            {
                for (int j = 0; j < matrixWidth; j++)
                {
                    greedyHeuristicsMatrix[i,j] = matrix[i, columnsOrder[j]];
                }
            }

            columnsOrderInOryginalMatrix = columnsOrder;
            return greedyHeuristicsMatrix;
        }
    }
}
