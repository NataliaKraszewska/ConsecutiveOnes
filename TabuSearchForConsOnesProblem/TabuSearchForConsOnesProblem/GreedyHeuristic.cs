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


        public void GreedyHeuristicAlgorythm()
        {
            int matrixWidth = matrix.GetLength(1);
            var columnsOrder = new List<int>();
            var columnsToAdd = new List<int>();
            //losujemy kolumne początkową
            Random r = new Random();
            int randomColumn = r.Next(matrixWidth);
            columnsOrder.Add(randomColumn);
            //tworzymy liste kolumn do dodania
            for (int i = 0; i < matrixWidth; i++)
            {
                if (i != randomColumn)
                {
                    columnsToAdd.Add(i);
                }
            }

            int numberOfColumnsToAdd = columnsToAdd.Count;
            int checkPointOnMatrix = 0;
            //dodajemy kolejno kolumny do rozwiazania
            for(int i = 0; i < numberOfColumnsToAdd; i++)
            {
                bool foundColumnWhichMatchesTheMatrix = false;
                for (int columnIndex = 0; columnIndex < columnsToAdd.Count; columnIndex++)
                {
                    if (!columnsOrder.Contains(columnsToAdd[columnIndex]) && IsColumnMatchesTheMatrix(matrix, columnsToAdd[columnIndex], columnsOrder, checkPointOnMatrix))
                    {//jeli macierz spelnia warunek cons1
                        columnsOrder.Add(columnsToAdd[columnIndex]);
                        foundColumnWhichMatchesTheMatrix = true;
                        break;
                    }
                }
                for (int columnIndex = 0; columnIndex < columnsOrder.Count; columnIndex++) //czyscimy liste kandydatow na macierze z listy
                    columnsToAdd.Remove(columnsOrder[columnIndex]);

                if (!foundColumnWhichMatchesTheMatrix)//jesli nie spelnia to bierzemy losowa i szukamy od nowa
                {
                    randomColumn = r.Next(columnsToAdd.Count);
                    checkPointOnMatrix = columnsOrder.Count;
                    columnsOrder.Add(columnsToAdd[randomColumn]);
                }
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int ij = 0; ij < columnsOrder.Count; ij++)
                {
                    Console.Write(matrix[i, columnsOrder[ij]] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
