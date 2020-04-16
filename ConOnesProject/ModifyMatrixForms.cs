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
    public partial class ModifyMatrixForms : Form
    {

        int[,] mixedColumnsMatrix;
        int[,] rndResultMatrix;

        private IFormsMatrixInformations menuForm;
        public ModifyMatrixForms(IFormsMatrixInformations callingForm, int [,] aMixedColumnsMatrix, int [,] aRndResultMatrix)
        {
            menuForm = callingForm;
            mixedColumnsMatrix = aMixedColumnsMatrix;
            rndResultMatrix = aRndResultMatrix;
            InitializeComponent();          
        }


        public void showMatrixInRichTextBox(int [,] matrix)
        {
            if( matrix != null)
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
            else
                richTextBox1.Text = "There is no matrix to modify";            
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string text = richTextBox1.Text;
            menuForm.addMatrixFromText(text);
            this.Close();
        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radioButton2.Checked = false;
            richTextBox1.Clear();
            showMatrixInRichTextBox(mixedColumnsMatrix);
        }


        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            richTextBox1.Clear();
            showMatrixInRichTextBox(rndResultMatrix);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
