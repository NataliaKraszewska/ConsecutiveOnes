using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace ConOnesProject
{
    public partial class ReportForm : Form 
    {
        InputMatrix inputMatrix;
        ResultMatrix resultMatrix;

        public ReportForm(InputMatrix in_inputMatrix, ResultMatrix in_resultMatrix)
        {
            inputMatrix = in_inputMatrix;
            resultMatrix = in_resultMatrix;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fileName = textBox1.Text + ".txt";


            using (TextWriter tw = new StreamWriter(fileName))
            {
                tw.Write("Input matrix: ");
                tw.WriteLine();
                for (int j = 0; j < inputMatrix.inputMatrix.GetLength(0); j++)
                {
                    for (int i = 0; i < inputMatrix.inputMatrix.GetLength(1); i++)
                    {
                        tw.Write(inputMatrix.inputMatrix[j, i].ToString() + " ");
                    }
                    tw.WriteLine();
                }
                tw.WriteLine();
                tw.Write("Input matrix parameters: ");
                tw.WriteLine();
                tw.Write("Width: " + inputMatrix.width);
                tw.WriteLine();
                tw.Write("Height: " + inputMatrix.height);
                tw.WriteLine();
                tw.Write("Columns to delete to get consecutive ones matrix (cmax): " + inputMatrix.getCmax());
                tw.WriteLine();
                string howMatrixWasGenerated = inputMatrix.howMatrixIsGenerated;
                if(howMatrixWasGenerated == "random")
                {
                    tw.WriteLine();
                    tw.Write("How matrix was generated: random generator " );
                    tw.WriteLine();
                    tw.Write(" \t Random generator parameters: ");
                    tw.WriteLine();
                    tw.Write(" \t Number of mistakes in matrix: " + inputMatrix.numberOfMistakes);
                    tw.WriteLine();
                    tw.Write(" \t Percentage length string of ones: " + inputMatrix.percentageLengthStringOfOnes + "%");
                    tw.WriteLine();
                    tw.WriteLine();
                    tw.Write("Matrix before mixing columns and before adding users changes: ");
                    tw.WriteLine();
                    for (int j = 0; j < inputMatrix.rndResultlMatrix.GetLength(0); j++)
                    {
                        for (int i = 0; i < inputMatrix.rndResultlMatrix.GetLength(1); i++)
                        {
                            tw.Write(inputMatrix.rndResultlMatrix[j, i].ToString() + " ");
                        }
                        tw.WriteLine();
                    }
                }

                else if(howMatrixWasGenerated == "file")
                {
                    tw.WriteLine();
                    tw.Write("How matrix was generated: matrix from input file");
                }

                else if (howMatrixWasGenerated == "text")
                {
                    tw.WriteLine();
                    tw.Write("How matrix was generated: matrix from user");
                }
                tw.WriteLine();
                tw.WriteLine();
                tw.Write("TabuSearch algorithm result: ");
                tw.WriteLine();
                for (int j = 0; j < resultMatrix.matrix.GetLength(0); j++)
                {
                    for (int i = 0; i < resultMatrix.matrix.GetLength(1); i++)
                    {
                        tw.Write(resultMatrix.matrix[j, i].ToString() + " ");
                    }
                    tw.WriteLine();
                }
                tw.WriteLine();
                tw.Write("Number of columns to delete to get the consecutive ones matrix (cmax): " + resultMatrix.cmax);
                tw.WriteLine();
                tw.Write("Columns to delete in matrix to get the consecutive ones matrix: ");
                for(int i = 0; i <resultMatrix.columnsToDelete.Count; i++)
                {
                    tw.Write(resultMatrix.columnsToDelete[i] + " ");
                }
                tw.WriteLine();
                tw.WriteLine();
                tw.Write("Consecutive ones matrix:");
                tw.WriteLine();
                for (int i = 0; i < resultMatrix.matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < resultMatrix.matrix.GetLength(1); j++)
                    {
                        if (!resultMatrix.columnsToDelete.Contains(j))
                        {
                            tw.Write(resultMatrix.matrix[i, j].ToString() + " ");

                        }
                    }
                    tw.WriteLine();
                }
                tw.WriteLine();
                tw.Write("TabuSearch algorithm parameters: ");
                tw.WriteLine();
                tw.Write(" \t How many percentages of maximum movements are added to the matrixto diversify it. " + resultMatrix.percentageOfDivSteps);
                tw.WriteLine();
                tw.Write(" \t  Maximum the same results before running the tabu search algorithm again: " + resultMatrix.maxCountTheSameResult);
                tw.WriteLine();
                tw.Write(" \t How many times the algorithm runs again: " + resultMatrix.newResultCount);
                tw.WriteLine();
                tw.Write(" \t TabuList max size: " + resultMatrix.tabuListSize);
                tw.WriteLine();
                tw.WriteLine();
                tw.Write("TabuSearch working time: " + resultMatrix.seconds);
                
            }
            this.Close();

        }
    }
}
