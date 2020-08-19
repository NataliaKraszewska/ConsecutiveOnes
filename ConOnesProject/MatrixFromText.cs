using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ConOnesProject
{
    class MatrixFromText
    { 
        public string textFromUser;
        public int[,] matrix;
        public int width;
        public int height;


        public MatrixFromText(string Text)
        {
            textFromUser = Text;
            matrix = GenerateMatrixFromText(textFromUser);
        }


        public int[,] GenerateMatrixFromText(string textFromUser)
        {
            foreach (string lines in textFromUser.Split('\n'))
            {
                if (lines.Length != 0)
                    height++;
            }
            width = (textFromUser.Count(x => x == '0') + textFromUser.Count(x => x == '1')) / height;
            matrix = new int[height, width];
            int i = 0;

            try
            {
                foreach (string line in textFromUser.Split('\n'))
                {
                    string strippedLine = Regex.Replace(line, @"\s+", "");
                    for (int j = 0; j < strippedLine.Length; j++)
                    {
                        matrix[i, j] = (int)(strippedLine[j] - '0');
                    }
                    if (line.Length != 0)
                        i++;
                }

                return matrix;
            }
            catch(IndexOutOfRangeException e)
            {
                return null;
            }
        }
    }
}
