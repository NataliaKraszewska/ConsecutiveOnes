using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

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


        public int[,] ReadFile()
        {
            try
            {
                File.ReadAllLines(filePath);
            }
            catch (Exception ex)
            {
                return null;
            }

            string[] lines = File.ReadAllLines(filePath);

                height = lines.Length;
                width = Regex.Replace(lines[0], @"\s+", "").Length;
                matrix = new int[height, width];

            try
            {
                for (int i = 0; i < height; i++)
                {
                    List<char> line = new List<char>();
                    string l = Regex.Replace(lines[i], @"\s+", "");
                    foreach (char x in l)
                    {
                        if (x != '0' && x != '1')
                        {
                            return null;
                        }
                        line.Add(x);
                    }
                    for (int j = 0; j < width; j++)
                    {
                        matrix[i, j] = line[j] - '0';
                    }
                }
            }
            catch(ArgumentOutOfRangeException e)
            {
                return null;
            }
            

            return matrix;
        }
    }
}
