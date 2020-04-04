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
    public partial class AddRandomMatrix : Form
    {
        public AddRandomMatrix()
        {
            InitializeComponent();
        }

        private AddMatrixMenu menuForm = null;
        public AddRandomMatrix(Form callingForm)
        {
            menuForm = callingForm as AddMatrixMenu;
            InitializeComponent();
            menuForm.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddMatrixMenu menu = new AddMatrixMenu(this);
            menu.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConsecutiveOnesProgram program = new ConsecutiveOnesProgram();
            
            program.widthText = textBox1.Text;
            program.heightText = textBox2.Text;
            program.NumberOfMistakesText = textBox3.Text;
            program.diffChoiceText = comboBox1.Text;
            this.Close();
            program.Show();
        }
    }
}
