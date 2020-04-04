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
    public partial class ConsecutiveOnesProgram : Form
    {
        public ConsecutiveOnesProgram()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddMatrixMenu menu = new AddMatrixMenu(this);
            menu.Show();
        }  

        public string widthText
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public string heightText
        {
            get { return textBox2.Text; }
            set { textBox2.Text = value; }
        }

        public string NumberOfMistakesText
        {
            get { return textBox3.Text; }
            set { textBox3.Text = value; }
        }

        public string diffChoiceText
        {
            get { return textBox4.Text; }
            set { textBox4.Text = value; }
        }
    }
}
