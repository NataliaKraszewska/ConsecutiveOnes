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
    public partial class AddRandomMatrixForm : Form
    {
        public AddRandomMatrixForm()
        {
            InitializeComponent();
        }


        private IFormsMatrixInformations menuForm;
        public AddRandomMatrixForm(IFormsMatrixInformations callingForm)
        {
            menuForm = callingForm;
            InitializeComponent();
        }
        
        
        private void button1_Click(object sender, EventArgs e)
        {
            AddMatrixMenuForm menu = new AddMatrixMenuForm(menuForm);
            this.Close();
            menu.Show();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            int widthText;
            int heightText;
            int numberOfMistakesText;

            int i;
            if (!int.TryParse(textBox1.Text, out i) || textBox1.Text == "0")
            {
                MessageBox.Show("Add value too width field!!!");
                return;
            }
            else
            {
                widthText = Convert.ToInt32(textBox1.Text);
            }

            if (Convert.ToInt32(textBox1.Text) <= 1 && (Convert.ToInt32(textBox2.Text) <= 1))
            {
                MessageBox.Show("Matrix is too small!");
                return;
            }

            if (!int.TryParse(textBox2.Text, out i) || textBox2.Text == "0")
            {
                MessageBox.Show("Add value to height field!!!");
                return;
            }
            else
            {
                heightText = Convert.ToInt32(textBox2.Text);
            }
           
            string lenChoiceText;
            if(comboBox1.Text == "")
            {
                MessageBox.Show("Please select value of string of ones length!!!");
                return;
            }
            else
            {
                lenChoiceText = comboBox1.Text;
            }

            int percentageLen = Convert.ToInt32(lenChoiceText.Substring(0, lenChoiceText.Length - 1));
            int maxNumberOfMistakes = (widthText * heightText)/5;

            if (!int.TryParse(textBox3.Text, out i))
            {
                MessageBox.Show("Add value to number of mistakes field!!!");
                return;
            }
            else if(Convert.ToInt32(textBox3.Text) > maxNumberOfMistakes)
            {
                MessageBox.Show("Number of mistakes can't be bigger than 20% of width * height");
                return;

            }
            else
            {
                numberOfMistakesText = Convert.ToInt32(textBox3.Text);
            }

            if(widthText < 3 && numberOfMistakesText != 0)
            {
                MessageBox.Show("Width size is to small. Program can't make mistakes! Change width size or set number of mistakes to 0");
                return;

            }

            bool showMatrix = true;
            if (widthText > 50 || heightText > 50)
            {
                DialogResult dialogResult = MessageBox.Show("Your matrix is big. Do you want to show matrix in program?", "Do you want to show matrix?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    showMatrix = false;
                }
            }

            menuForm.addRandomMatrix(widthText, heightText, numberOfMistakesText, lenChoiceText, showMatrix);

            this.Close();
        }
    }
}
