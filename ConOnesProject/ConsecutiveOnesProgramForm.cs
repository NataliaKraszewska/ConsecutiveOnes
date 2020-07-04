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
        InputMatrix inputMatrix;
        TabuSearchAlgorithmForm tabu;

        int[,] resultMatrix;
        List<int> columnsToDeleteInMatrix;

        public ConsecutiveOnesProgramForm()
        {
            InitializeComponent();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
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
            newMatrix.ReadFile();
       
            return newMatrix.matrix;
        }


        public int[,] ReadMatrixFromUserText(string text)
        {
            MatrixFromText newMatrix = new MatrixFromText(text);

            return newMatrix.matrix;
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
            textBox3.Text = this.inputMatrix.numberOfMistakes.ToString();
            textBox4.Text = this.inputMatrix.percentageLengthStringOfOnes.ToString();
            //textBox5.Text = (isOptimalMatrix) ? this.inputMatrix.numerOfColumnsToDeleteForOptimalMatrix.ToString() : this.inputMatrix.numerOfColumnsToDeleteForMixedColumnsMatrix.ToString();
            int[,] matrix = isOptimalMatrix ? this.inputMatrix.rndResultlMatrix : this.inputMatrix.inputMatrix;
            AddMatrixToRichTextBox(matrix);
        }


        void IFormsMatrixInformations.addRandomMatrix(int width, int height, int numberOfMistakes, string lenChoice)
        {
            int percentageLen = Convert.ToInt32(lenChoice.Substring(0, lenChoice.Length - 1));
            int[,] matrix = GenerateRandomMatrix(width, height, numberOfMistakes, percentageLen);
            inputMatrix = new InputMatrix(matrix, numberOfMistakes, percentageLen);
            SetValuesInTextBoxes(true);
        }


        void IFormsMatrixInformations.addMatrixFromText(string text)
        {
            richTextBox1.Text = text;
            int[,] matrix = ReadMatrixFromUserText(text);
            inputMatrix = new InputMatrix(matrix);
            SetValuesInTextBoxes(false);
            button3.Click -= button3_Click;
        }


        void IFormsMatrixInformations.matrixFilePath(string filePath)
        {
            int[,] matrix = ReadMatrixFromInputFile(filePath);
            inputMatrix = new InputMatrix(matrix);
            SetValuesInTextBoxes(false);
            button3.Click -= button3_Click;
        }

        void IFormsMatrixInformations.resultMatrix(int[,] matrix, int cmax, List<int> columnsOrder, List<int> columnsToDelete, int diverse, int neighborhood, int tabuListSize, int maxCountTheSameResult, string seconds)
        {
            textBox6.Text = cmax.ToString();
            textBox7.Text = seconds;
            textBox8.Text = diverse.ToString();
            textBox9.Text = neighborhood.ToString();
            textBox10.Text = tabuListSize.ToString();
            textBox11.Text = maxCountTheSameResult.ToString();
            AddMatrixToRichTextBox2(matrix);
            resultMatrix = matrix;
            columnsToDeleteInMatrix = columnsToDelete;

        }
        


        private void button2_Click(object sender, EventArgs e)
        {
            SetValuesInTextBoxes(false);
        }


        private void button3_Click(object sender, EventArgs e)
        {
            SetValuesInTextBoxes(true);
        }


        private void button4_Click(object sender, EventArgs e)
        {
            ModifyMatrixForms modify = new ModifyMatrixForms(this as IFormsMatrixInformations, inputMatrix.inputMatrix, inputMatrix.rndResultlMatrix);
            modify.Show();

        }

        private void button5_Click_1(object sender, EventArgs e)
        {

            //TabuSearchAlgorithmForm tabu = new TabuSearchAlgorithmForm(this as IFormsMatrixInformations, inputMatrix.inputMatrix);
            var matrix = new int[,]
          {
                { 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1 },
                { 0, 0, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 0, 0, 1, 1, 1 },
                { 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1 },
                { 0, 0, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 0, 0, 1, 1, 1 },
            };
            TabuSearchAlgorithmForm tabu = new TabuSearchAlgorithmForm(this as IFormsMatrixInformations, matrix);
            tabu.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ColumnToDeleteForm columnsToDelete = new ColumnToDeleteForm(resultMatrix, columnsToDeleteInMatrix);
            columnsToDelete.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
        }
    }
}
