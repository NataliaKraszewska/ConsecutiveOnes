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
    public partial class SaveMatrixInFileForm : Form
    {
        int[,] matrix;
        public SaveMatrixInFileForm( int [,] in_matrix)
        {
            matrix = in_matrix;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fileName = textBox1.Text + ".txt";
            using (TextWriter tw = new StreamWriter(fileName))
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    for (int i = 0; i < matrix.GetLength(1); i++)
                    {
                        tw.Write(matrix[j, i].ToString() + " ");
                    }
                    tw.WriteLine();
                }
            }
            this.Close();
        }
    }
}