using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace ConOnesProject
{
    class MatrixFromFile
    {
        public string filePath;
        public int[,] matrix;
        public int width;
        public int height;


        public MatrixFromFile(string aFilePath)
        {
            filePath = aFilePath;
        }


        public void ReadFile()
        {
            string[] lines = File.ReadAllLines(filePath);
            height = lines.Length;
            width = lines[0].Split(' ').Length;
            matrix = new int[height, width];

            for (int i = 0; i < height; i++)
            {
                string[] line = lines[i].Split(' ');
                for (int j = 0; j < width; j++)
                {
                    matrix[i, j] = Convert.ToInt32(line[j]);
                }
            }
        }
    }
}
