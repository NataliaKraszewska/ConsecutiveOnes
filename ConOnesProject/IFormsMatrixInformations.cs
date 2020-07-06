using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConOnesProject
{
    public interface IFormsMatrixInformations
    {
        void addRandomMatrix(int width, int height, int  numberOfMistakes, string diffChoice, bool show);
        void addMatrixFromText(string text);
        void matrixFilePath(string text, bool show);
        void resultMatrix(int[,] matrix, int cmax, List<int> columnsOrder, List<int> columnsToDelete,int diverse, int neighbornhood, int tabuListSize, int maxCountTheSameResult, string seconds, bool show);
    }
}
