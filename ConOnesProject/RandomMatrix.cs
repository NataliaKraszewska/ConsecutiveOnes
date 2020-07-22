using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConOnesProject
{
    class RandomMatrix
    {
        public int[,] matrix;
        int width;
        int height;
        int numberOfMistakes;
        int percentageLengthStringOfOnes;
        Random rPosition = new Random();


        public RandomMatrix(int aWidth, int aHeight, int aNumberOfMistakes, int aPercentageLengthStringOfOnes)
        {
            width = aWidth;
            height = aHeight;
            numberOfMistakes = aNumberOfMistakes;
            percentageLengthStringOfOnes = aPercentageLengthStringOfOnes;
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


        void AddStringOfOnesToMatrix(int[,] matrix)
        {
            Random r = new Random();
            int startStringOfOnes = 0;
            int endStringOfOnes = 0;
            bool allColumnsCovered = false;

            for (int i = 0; i < height; i++)
            {
                int margin = r.Next(Convert.ToInt32(width * 0.05));
                int lengthStringOfOnes = ((percentageLengthStringOfOnes * width) / 100) + margin;
                startStringOfOnes = allColumnsCovered ? r.Next(width - lengthStringOfOnes) : endStringOfOnes;
                endStringOfOnes = startStringOfOnes + lengthStringOfOnes;

                if (endStringOfOnes > width)
                {
                    startStringOfOnes = startStringOfOnes - (endStringOfOnes - width);
                    endStringOfOnes = width;
                }
                if (endStringOfOnes == width)
                    allColumnsCovered = true;
                for (int j = startStringOfOnes; j < endStringOfOnes; j++)
                    matrix[i, j] = 1;
            }

        }


        int[,] MixRowsInMatrix(int[,] matrix)
        {
            int[] rowPosition = Enumerable.Range(0, height).ToArray();
            Randomize(rowPosition);
            int[,] tmpMatrix = new int[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    tmpMatrix[i, j] = matrix[rowPosition[i], j];
                }
            }

            return tmpMatrix;
        }

        int GetHeight(List<int> visitedRow)
        {
            int row = rPosition.Next(height);
            while (visitedRow.Contains(row))
                row = rPosition.Next(height);

            return row;
        }

        void AddMistakesToMatrix(int[,] matrix)
        {

            int mistakesCount = 0;
            if (width < 3)
            {
                return;
            }

            List<int> visitedRow = new List<int>();
            while (mistakesCount != numberOfMistakes)
            {
                int randomRow = GetHeight(visitedRow);
                visitedRow.Add(randomRow);

                int start = rPosition.Next(width - 3);
                for (int i = start; i < width - 2; i++)
                {
                    if (matrix[randomRow, i] == 1 && matrix[randomRow, i + 1] == 1 && matrix[randomRow, i + 2] == 1)
                    {
                        matrix[randomRow, i + 1] = 0;
                        mistakesCount++;
                        break;
                    }
                    if (matrix[randomRow, i] == 1 && matrix[randomRow, i + 1] == 0 && matrix[randomRow, i + 2] == 0)
                    {
                        matrix[randomRow, i + 2] = 1;
                        mistakesCount++;
                        break;
                    }
                    if (matrix[randomRow, i] == 0 && matrix[randomRow, i + 1] == 0 && matrix[randomRow, i + 2] == 1)
                    {
                        matrix[randomRow, i] = 1;
                        mistakesCount++;
                        break;
                    }
                    if (matrix[randomRow, i] == 0 && matrix[randomRow, i + 1] == 0 && matrix[randomRow, i + 2] == 0)
                    {
                        matrix[randomRow, i + 1] = 1;
                        mistakesCount++;
                        break;
                    }

                }
                if (visitedRow.Count == height)
                    mistakesCount = numberOfMistakes;

            }
        }


        public void GenerateInputMatrix()
        {
            matrix = new int[height, width];
            Array.Clear(matrix, 0, matrix.Length);
            AddStringOfOnesToMatrix(matrix);
            matrix = MixRowsInMatrix(matrix);
            AddMistakesToMatrix(matrix);
        }
    }
}
