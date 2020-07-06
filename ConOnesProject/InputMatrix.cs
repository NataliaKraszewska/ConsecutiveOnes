using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConOnesProject
{
    public class InputMatrix
    {
        public int[,] rndResultlMatrix;
        public int[,] inputMatrix;
        public int[] columnPosition;
        public int width;
        public int height;
        public int numerOfColumnsToDeleteForMixedColumnsMatrix;
        public int numerOfColumnsToDeleteForOptimalMatrix;
        public int numberOfMistakes;
        public int percentageLengthStringOfOnes;
        public string howMatrixIsGenerated;

        public InputMatrix(int [,] a_matrix, string in_howMatrixIsGenerated)
        {
            inputMatrix = a_matrix;
            height = a_matrix.GetLength(0);
            width = a_matrix.GetLength(1);
            numerOfColumnsToDeleteForOptimalMatrix = GetNumberOfColumnsToDelete(a_matrix);
            numerOfColumnsToDeleteForMixedColumnsMatrix = GetNumberOfColumnsToDelete(inputMatrix);
            numberOfMistakes = 0;
            percentageLengthStringOfOnes = 0;
            howMatrixIsGenerated = in_howMatrixIsGenerated;
        }


        public InputMatrix(int[,] a_matrix, int a_numberOfMistakes, int a_percentageLengthStringOfOnes, string in_howMatrixIsGenerated)
        {
            rndResultlMatrix = a_matrix;
            height = a_matrix.GetLength(0);
            width = a_matrix.GetLength(1);
            columnPosition = Enumerable.Range(0, width).ToArray();
            inputMatrix = RandomizeColumnsInMatrix(a_matrix, columnPosition);
            numerOfColumnsToDeleteForOptimalMatrix = GetNumberOfColumnsToDelete(a_matrix);
            numerOfColumnsToDeleteForMixedColumnsMatrix = GetNumberOfColumnsToDelete(inputMatrix);
            numberOfMistakes = a_numberOfMistakes;
            percentageLengthStringOfOnes = a_percentageLengthStringOfOnes;
            howMatrixIsGenerated = in_howMatrixIsGenerated;
        }


        public void UpdateInputMatrix(int[,] a_matrix)
        {
            rndResultlMatrix = a_matrix;
            height = a_matrix.GetLength(0);
            width = a_matrix.GetLength(1);
            inputMatrix = RandomizeColumnsInMatrix(a_matrix, columnPosition);
            numerOfColumnsToDeleteForOptimalMatrix = GetNumberOfColumnsToDelete(a_matrix);
            numerOfColumnsToDeleteForMixedColumnsMatrix = GetNumberOfColumnsToDelete(inputMatrix);
        }

  
        //todo
        public int GetNumberOfColumnsToDelete(int[,] matrix)
        {
            int matrixHeight = matrix.GetLength(0);
            int matrixWidth = matrix.GetLength(1);
            int[] columns = new int[matrixWidth];
            Array.Clear(columns, 0, columns.Length);

            for (int i = 0; i < matrixHeight; i++)
            {
                for (int j = 0; j < matrixWidth - 2; j++)
                {
                    if (matrix[i, j] == 1 && matrix[i, j + 1] == 0 && matrix[i, j + 2] == 1)
                        columns[j + 1] = 1;
                }
            }

            return columns.Count(x => x == 1);
        }


        //http://csharphelper.com/blog/2014/07/randomize-arrays-in-c/
        public static void Randomize<T>(T[] items)
        {
            Random rand = new Random();
            for (int i = 0; i < items.Length - 1; i++)
            {
                int j = rand.Next(i, items.Length);
                T temp = items[i];
                items[i] = items[j];
                items[j] = temp;
            }
        }


        int[,] RandomizeColumnsInMatrix(int[,] tmpMatrix, int[] columnPosition)
        {
            Randomize(columnPosition);
            int[,] matrix = new int[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    matrix[i, j] = tmpMatrix[i, columnPosition[j]];
                }
            }

            return matrix;
        }
        
    }
}
