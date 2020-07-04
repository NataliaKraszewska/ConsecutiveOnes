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
    public partial class ColumnToDeleteForm : Form
    {
        int[,] matrix;
        List<int> columns;

        public ColumnToDeleteForm(int[,] in_matrix, List<int> in_columns)
        {
            matrix = in_matrix;
            columns = in_columns;
            InitializeComponent();
            AddTextToTextBoxes();
        }

        void AddTextToTextBoxes()
        {
            for(int i = 0; i< columns.Count; i++)
            {
                richTextBox1.Text += columns[i] + " ";
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (!columns.Contains(j))
                    {
                        richTextBox2.Text += matrix[i, j].ToString() + " ";

                    }
                }
                richTextBox2.Text += "\n";
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
