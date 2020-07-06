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
            newMatrix.matrix = newMatrix.ReadFile();
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
            EnabledButtonsAfterAddingInputMatrix();
            int percentageLen = Convert.ToInt32(lenChoice.Substring(0, lenChoice.Length - 1));
            int[,] matrix = GenerateRandomMatrix(width, height, numberOfMistakes, percentageLen);
            inputMatrix = new InputMatrix(matrix, numberOfMistakes, percentageLen, "random");
            SetValuesInTextBoxes(true);
            if (showMatrix)
                AddMatrixToRichTextBox(matrix);
        }


        void IFormsMatrixInformations.addMatrixFromText(string text)
        {
            EnabledButtonsAfterAddingInputMatrix();
            richTextBox1.Text = text;
            int[,] matrix = ReadMatrixFromUserText(text);
            inputMatrix = new InputMatrix(matrix, "text");
            SetValuesInTextBoxes(false);
            AddMatrixToRichTextBox(matrix);
            button3.Click -= button3_Click;
        }


        void IFormsMatrixInformations.matrixFilePath(string filePath, bool showMatrix)
        {
            EnabledButtonsAfterAddingInputMatrix();
            int[,] matrix = ReadMatrixFromInputFile(filePath);

            inputMatrix = new InputMatrix(matrix, "file");
            SetValuesInTextBoxes(false);
            if (showMatrix)
                AddMatrixToRichTextBox(matrix);
            button3.Click -= button3_Click;
        }

        void IFormsMatrixInformations.resultMatrix(int[,] matrix, int cmax, List<int> columnsOrder, List<int> columnsToDelete, int diverse, int neighborhood, int tabuListSize, int maxCountTheSameResult, string seconds, bool showMatrix)
        {
            button6.Enabled = true;
            button8.Enabled = true;
            button4.Enabled = false;

            result = new ResultMatrix(matrix, cmax, columnsOrder, columnsToDelete, diverse, neighborhood, tabuListSize, maxCountTheSameResult, seconds); ;
            textBox6.Text = cmax.ToString();
            textBox7.Text = seconds;
            textBox8.Text = diverse.ToString();
            textBox9.Text = neighborhood.ToString();
            textBox10.Text = tabuListSize.ToString();
            textBox11.Text = maxCountTheSameResult.ToString();
            if(showMatrix)
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


        private void button3_Click(object sender, EventArgs e)
        {
            SetValuesInTextBoxes(true);
            AddMatrixToRichTextBox(inputMatrix.rndResultlMatrix);
        }


        private void button4_Click_1(object sender, EventArgs e)
        {
            ModifyMatrixForms modify = new ModifyMatrixForms(this as IFormsMatrixInformations, inputMatrix.inputMatrix, inputMatrix.rndResultlMatrix);
            modify.Show();

        }

        private void button5_Click_1(object sender, EventArgs e)
        {

            TabuSearchAlgorithmForm tabu = new TabuSearchAlgorithmForm(this as IFormsMatrixInformations, inputMatrix.inputMatrix);
            tabu.Show();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            ColumnToDeleteForm columnsToDelete = new ColumnToDeleteForm(resultMatrix, columnsToDeleteInMatrix);
            columnsToDelete.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SaveMatrixInFileForm outputFileForm = new SaveMatrixInFileForm(inputMatrix.inputMatrix);
            outputFileForm.Show();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            ReportForm report = new ReportForm(inputMatrix, result);
            report.Show();
        }

    }
}
