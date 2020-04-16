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
            textBox5.Clear();
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
            textBox5.Text = (isOptimalMatrix) ? this.inputMatrix.numerOfColumnsToDeleteForOptimalMatrix.ToString() : this.inputMatrix.numerOfColumnsToDeleteForMixedColumnsMatrix.ToString();
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
    }
}
