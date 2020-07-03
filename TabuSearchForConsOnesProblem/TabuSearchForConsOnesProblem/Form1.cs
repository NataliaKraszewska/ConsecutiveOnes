using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TabuSearchForConsOnesProblem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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


        private void button1_Click(object sender, EventArgs e)
        {

            /* var matrix = new int[,]
             {
                 {1, 0, 1, 1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 1, 0, 1, 0, 0},
                 { 1, 1, 1, 1, 1, 0, 0,0, 1, 1, 1, 0, 1, 0, 0, 1, 1, 1, 1, 0, 1, 1, 0},
                 { 1, 1, 1, 0, 1, 0, 0,1, 1, 0, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 1, 0, 0},
                 { 1, 1, 1, 0, 1, 0, 0,0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1},
                 { 1, 1, 1, 0, 1, 0, 0,1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                 { 1, 1, 1, 0, 1, 0, 1,0, 1, 1, 1, 1, 0, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1},
                 { 1, 1, 1, 0, 1, 0, 0,0, 1, 0, 1, 0, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1}

             };
             
            */

            var matrix = new int[,]
            {
              /*  { 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1 },
                { 0, 0, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 0, 0, 1, 1, 1 },
                { 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1 },
                { 0, 0, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 0, 0, 1, 1, 1 },
            };*/
            
                { 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1 },
                { 0, 0, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 0, 1, 1, 1 },
                { 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1 },
                { 0, 0, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 0, 0, 1, 1, 1 },
                { 0, 1, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1 },
                { 0, 0, 1, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 0, 0, 1, 1, 1 },
                { 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1 },
                { 0, 0, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 0, 0, 1, 1, 1 },
                { 0, 1, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1 },
                { 0, 0, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 0, 0, 1, 1, 1 },
                { 0, 1, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1 },
                { 0, 0, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 0, 0, 1, 1, 1 }

             };

            /*var matrix = new[,] // dziala
            {
                {0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1},
                {1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1},
                {0, 1, 1, 1, 1, 0, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1}
            };*/


            /*
             * 0 1 0 0 0 1 1 0 0 1 1 1 0 1 1 1 1 1 1 
            1 1 0 0 0 0 0 0 1 1 0 0 1 0 0 1 1 1 1 
            1 0 0 1 1 0 0 1 1 1 0 1 1 0 0 0 1 1 0 
            1 0 0 0 0 1 0 1 1 0 0 1 0 0 1 1 1 1 0 
            1 1 0 1 1 1 0 1 1 0 0 0 0 0 0 1 1 1 0 
            0 1 0 1 0 0 0 1 0 0 1 0 1 0 1 0 0 0 1 
            1 1 0 1 0 0 1 0 1 1 1 0 0 0 0 1 0 1 0 
            0 0 0 1 1 0 0 1 1 1 1 1 0 1 0 1 1 0 0 
            1 0 1 1 1 0 1 0 0 1 0 1 1 1 0 0 0 1 1 
            0 0 0 0 1 0 0 1 1 1 0 0 1 1 0 1 1 1 0 
        */
        TabuSearch tabu= new TabuSearch(matrix);
        int [,] outputMatrix = tabu.OutputMatrix();
        AddMatrixToRichTextBox(outputMatrix);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
