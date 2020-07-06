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
    public partial class AddMatrixFromFileForm : Form
    {
        private IFormsMatrixInformations menuForm;
        public AddMatrixFromFileForm(IFormsMatrixInformations callingForm)
        {
            menuForm = callingForm;
            InitializeComponent();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            AddMatrixMenuForm menu = new AddMatrixMenuForm(menuForm);
            this.Close();
            menu.Show();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string inputFilePath = textBox1.Text;
            MatrixFromFile newMatrix = new MatrixFromFile(inputFilePath);
            newMatrix.matrix = newMatrix.ReadFile();
            if (inputFilePath== "")
            {
                MessageBox.Show("Add file path!");
                return;
            }
            if (newMatrix.matrix == null)
            {
                MessageBox.Show("All rows and columns must have the same size and matrix must contains only 0 and 1 values! Check your input file path.");
                MessageBox.Show("The correct input file must contains a matrix, where columns are separated by whitespace, rows are separated by newline.");
                return;
            }
            if(newMatrix.matrix.GetLength(0) == 1 && newMatrix.matrix.GetLength(1) == 1)
            {
                MessageBox.Show("Matrix is too small!");
                return;

            }

            bool showMatrix = true;
            int width = newMatrix.width;
            int height = newMatrix.height;
            if (width > 50 || height > 50)
            {
                DialogResult dialogResult = MessageBox.Show("Your matrix is big. Do you want to show matrix in program?", "Do you want to show matrix?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    showMatrix = false;
                }
            }

            menuForm.matrixFilePath(inputFilePath, showMatrix);
            this.Close();
        }

    }
}
