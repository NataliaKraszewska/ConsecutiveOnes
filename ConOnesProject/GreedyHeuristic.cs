using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConOnesProject
{
    class GreedyHeuristic
    {
        int[,] matrix;
        int[,] greedyHeuristicsMatrix;
        public List<int> columnsOrderInOryginalMatrix;

        public GreedyHeuristic(int[,] inputMatrix)
        {
            matrix = inputMatrix;
        }
        
      
        List<int> SortColumnsToAddByNumberOfOnes(int [,] matrix)
        {
            int matrixHeight = matrix.GetLength(0);
            int matrixWidth = matrix.GetLength(1);
            List<int> sortedColumns = new List<int>();
            List<OnesCount> onesCount = new List<OnesCount>();

            for (int i = 0; i < matrixWidth; i++)
            {
                int count = 0;
                for (int j = 0; j < matrixHeight; j++)
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
            for (int i = 0; i < sortedOnes.Count; i++)
            {
                sortedColumns.Add(sortedOnes[i].numberOfColumn);
            }

            return sortedColumns;
        }


        int GetNumberOfOpositeOnes(List <int> columnsToAdd, List<int> column, int index)
        {
            List<int> columnToAdd = new List<int>();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                columnToAdd.Add(matrix[i, columnsToAdd[index]]);
            }

            int onesCount = 0;
            for(int i = 0; i< columnToAdd.Count(); i++)
            {
                if (columnToAdd[i] == 1 && column[i] == 1)
                    onesCount++;
                
            }

            return onesCount;
        }


        int FindColumnToAdd(List <int> columnsToAdd, List <int> columnsOrder)
        {
            int columnWithMostOnesInTheSamePosition = 0;
            int numberOfOpositeOnes = 0;
            int columnIndex = columnsOrder[columnsOrder.Count() - 1];

            List<int> column = new List<int>();
            for(int i = 0; i< matrix.GetLength(0); i++)
            {
                column.Add(matrix[i, columnIndex]);
            }

            if(columnsToAdd.Count() == 1)
            {
                return columnsToAdd[0];
            }

            bool foundColumn = false;

            for(int i = 0; i < columnsToAdd.Count; i++)
            {
                int tmp = GetNumberOfOpositeOnes(columnsToAdd, column, i);
                if (tmp > numberOfOpositeOnes && !columnsOrder.Contains(columnsToAdd[i]))
                {
                    numberOfOpositeOnes = tmp;
                    columnWithMostOnesInTheSamePosition = columnsToAdd[i];
                    foundColumn = true;
                }
            }

            if(!foundColumn)
                return  columnsToAdd[0];

            return columnWithMostOnesInTheSamePosition;
        }


        public int[,] GreedyHeuristicAlgorythm()
        {
            int matrixHeight = matrix.GetLength(0);
            int matrixWidth = matrix.GetLength(1);
            var columnsOrder = new List<int>();
            var columnsToAdd = new List<int>();
            Random r = new Random();
            List<int> columnsOrderedByNumberOfOnes = SortColumnsToAddByNumberOfOnes(matrix);
            int firstColumn = columnsOrderedByNumberOfOnes[0];

            columnsOrderedByNumberOfOnes.Remove(columnsOrderedByNumberOfOnes[0]);
            columnsToAdd = columnsOrderedByNumberOfOnes;
            columnsOrder.Add(firstColumn);
            int columnsToAddCount = columnsOrderedByNumberOfOnes.Count();

            for(int i = 0; i< columnsToAddCount; i++)
            {
                int columnIndex = FindColumnToAdd(columnsToAdd, columnsOrder);
                columnsOrder.Add(columnIndex);
                columnsToAdd.Remove(columnIndex);
            }
            greedyHeuristicsMatrix = new int[matrixHeight, matrixWidth];

            for (int i = 0; i < matrixHeight; i++)
            {
                for (int j = 0; j < matrixWidth; j++)
                {
                    greedyHeuristicsMatrix[i, j] = matrix[i, columnsOrder[j]];
                }
            }
            columnsOrderInOryginalMatrix = columnsOrder;

            return greedyHeuristicsMatrix;
        }
    }
}