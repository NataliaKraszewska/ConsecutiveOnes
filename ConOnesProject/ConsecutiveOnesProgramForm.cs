using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConOnesProject
{
    public partial class ConsecutiveOnesProgramForm : Form, IFormsMatrixInformations
    {
        public InputMatrix inputMatrix;
        public ResultMatrix result;
        int[,] resultMatrix;
        List<int> columnsToDeleteInMatrix;

        public ConsecutiveOnesProgramForm()
        {
            InitializeComponent();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            textBox6.Text = "";
            //textBox5.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            richTextBox2.Clear();
            AddMatrixMenuForm menu = new AddMatrixMenuForm(this as IFormsMatrixInformations);
            menu.Show();
        }


        public int[,] GenerateRandomMatrix(int width, int height, int numberOfMistakes, int percentageLen)
        {
            RandomMatrix newMatrix = new RandomMatrix(width, height, numberOfMistakes, percentageLen);
            newMatrix.GenerateInputMatrix();

            return newMatrix.matrix;
        }


        public void AddMatrixToRichTextBox(int[,] matrix)
        {
            if (matrix == null)
                richTextBox1.Text = "";
            else
            {
                int matrixHeight = matrix.GetLength(0);
                int matrixWidth = matrix.GetLength(1);
                for (int i = 0; i < matrixHeight; i++)
                {
                    for (int j = 0; j < matrixWidth; j++)
                    {
                        richTextBox1.Text += matrix[i, j].ToString() + " ";
                    }
                    richTextBox1.Text += "\n";
                }
            }
        }


        public void AddMatrixToRichTextBox2(int[,] matrix)
        {
            richTextBox2.Clear();
            int matrixHeight = matrix.GetLength(0);
            int matrixWidth = matrix.GetLength(1);
            for (int i = 0; i < matrixHeight; i++)
            {
                for (int j = 0; j < matrixWidth; j++)
                {
                    richTextBox2.Text += matrix[i, j].ToString() + " ";
                }
                richTextBox2.Text += "\n";
            }
        }

        public int[,] ReadMatrixFromInputFile(string filePath)
        {
            MatrixFromFile newMatrix = new MatrixFromFile(filePath);
            newMatrix.matrix = newMatrix.ReadFile();
            return newMatrix.matrix;
        }


        public int[,] ReadMatrixFromUserText(string text)
        {
            MatrixFromText newMatrix = new MatrixFromText(text);
          
            return newMatrix.matrix;
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


        int[,] MixRowsInMatrix(int[,] matrix)
        {
            int width = matrix.GetLength(1);
            int height = matrix.GetLength(0);

            int[] rowPosition = Enumerable.Range(0, width).ToArray();
            Randomize(rowPosition);

            int[,] tmpMatrix = new int[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    tmpMatrix[i, j] = matrix[i, rowPosition[j]];
                }
            }
            return tmpMatrix;
        }


        void CleanTextBoxes()
        {
            //textBox5.Clear();
            richTextBox1.Clear();
            textBox1.Clear();
            textBox2.Clear();
        }


        void SetValuesInTextBoxes(bool isOptimalMatrix)
        {
            CleanTextBoxes();
            textBox1.Text = this.inputMatrix.width.ToString();
            textBox2.Text = this.inputMatrix.height.ToString();
            if (!isOptimalMatrix)
            {
                textBox3.Text = "";
                textBox4.Text = "";

            }
            else
            {
                textBox3.Text = this.inputMatrix.numberOfMistakes.ToString();
                textBox4.Text = this.inputMatrix.percentageLengthStringOfOnes.ToString();
            }
                //textBox5.Text = (isOptimalMatrix) ? this.inputMatrix.numerOfColumnsToDeleteForOptimalMatrix.ToString() : this.inputMatrix.numerOfColumnsToDeleteForMixedColumnsMatrix.ToString();
        }


        void EnabledButtonsAfterAddingInputMatrix()
        {
            button2.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button7.Enabled = true;
            button6.Enabled = false;
            button8.Enabled = false;
        }


        void IFormsMatrixInformations.addRandomMatrix(int width, int height, int numberOfMistakes, string lenChoice, bool showMatrix)
        {
            button3.Enabled = true;
            button3.Click += button3_Click;
            EnabledButtonsAfterAddingInputMatrix();
            int percentageLen = Convert.ToInt32(lenChoice.Substring(0, lenChoice.Length - 1));
            int[,] matrix = GenerateRandomMatrix(width, height, numberOfMistakes, percentageLen);
            inputMatrix = new InputMatrix(matrix, numberOfMistakes, percentageLen, "random");
            SetValuesInTextBoxes(true);

            CmaxEstimation cmax = new CmaxEstimation(matrix);
            textBox5.Text = cmax.GetCmaxValue().ToString();

            if (showMatrix)
                AddMatrixToRichTextBox(inputMatrix.inputMatrix);
        }


        void IFormsMatrixInformations.addMatrixFromText(string text, string type)
        {
            EnabledButtonsAfterAddingInputMatrix();
            richTextBox1.Text = text;
            int[,] matrix = ReadMatrixFromUserText(text);

            if (type == "rndResultMatrix")
            {

               int[,] mixed_matrix = MixRowsInMatrix(matrix);
                inputMatrix = new InputMatrix(mixed_matrix, "text");
                SetValuesInTextBoxes(false);
                AddMatrixToRichTextBox(mixed_matrix);
                CmaxEstimation cmax = new CmaxEstimation(mixed_matrix);
                textBox5.Text = cmax.GetCmaxValue().ToString();
                button3.Enabled = false;
            }
            else
            {
                inputMatrix = new InputMatrix(matrix, "text");
                SetValuesInTextBoxes(false);
                AddMatrixToRichTextBox(matrix);
                CmaxEstimation cmax = new CmaxEstimation(matrix);
                textBox5.Text = cmax.GetCmaxValue().ToString();
                button3.Enabled = false;
            }
        }


        void IFormsMatrixInformations.matrixFilePath(string filePath, bool showMatrix)
        {
            EnabledButtonsAfterAddingInputMatrix();
            int[,] matrix = ReadMatrixFromInputFile(filePath);

            inputMatrix = new InputMatrix(matrix, "file");
            SetValuesInTextBoxes(false);
            if (showMatrix)
                AddMatrixToRichTextBox(matrix);

            CmaxEstimation cmax = new CmaxEstimation(matrix);
            textBox5.Text = cmax.GetCmaxValue().ToString();

            button3.Enabled = false;
        }


        void IFormsMatrixInformations.resultMatrix(int[,] matrix, int cmax, List<int> columnsOrder, List<int> columnsToDelete, int divStepsVal, int newResultCount, int tabuListSize, int maxCountTheSameResult, string seconds, bool showMatrix, int percentageOfDivSteps)
        {
            button6.Enabled = true;
            button8.Enabled = true;
            button4.Enabled = false;

            result = new ResultMatrix(matrix, cmax, columnsOrder, columnsToDelete, divStepsVal, newResultCount, tabuListSize, maxCountTheSameResult, seconds, percentageOfDivSteps);
            
            textBox6.Text = cmax.ToString();
            textBox7.Text = seconds;
            textBox8.Text = percentageOfDivSteps.ToString();
            textBox9.Text = newResultCount.ToString();
            textBox10.Text = tabuListSize.ToString();
            textBox11.Text = maxCountTheSameResult.ToString();

            if (showMatrix)
                AddMatrixToRichTextBox2(matrix);
            resultMatrix = matrix;
            columnsToDeleteInMatrix = columnsToDelete;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if(inputMatrix.howMatrixIsGenerated == "random")
            {
                SetValuesInTextBoxes(true);
            }
            else
            {
                SetValuesInTextBoxes(false);
            }
            AddMatrixToRichTextBox(inputMatrix.inputMatrix);
        }


        public void ShowMatrix(int[,] matrix)
        {
            int matrixHeight = matrix.GetLength(0);
            int matrixWidth = matrix.GetLength(1);
            for (int i = 0; i < matrixHeight; i++)
            {
                for (int j = 0; j < matrixWidth; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            SetValuesInTextBoxes(true);
            AddMatrixToRichTextBox(inputMatrix.rndResultlMatrix);
        }


        private void button4_Click_1(object sender, EventArgs e)
        {

            ModifyMatrixForms modify = new ModifyMatrixForms(this as IFormsMatrixInformations, inputMatrix.inputMatrix, inputMatrix.rndResultlMatrix);
            modify.Show();
            button3.Enabled = false;

        }

        private void button5_Click_1(object sender, EventArgs e)
        {

            TabuSearchAlgorithmForm tabu = new TabuSearchAlgorithmForm(this as IFormsMatrixInformations, inputMatrix.inputMatrix);
            tabu.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ColumnToDeleteForm columnsToDelete = new ColumnToDeleteForm(resultMatrix, columnsToDeleteInMatrix);
            columnsToDelete.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SaveMatrixInFileForm outputFileForm = new SaveMatrixInFileForm(inputMatrix.inputMatrix);
            outputFileForm.Show();

        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            ReportForm report = new ReportForm(inputMatrix, result);
            report.Show();
        }

        
    }
}
