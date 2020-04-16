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
            int widthText = Convert.ToInt32(textBox1.Text);
            int heightText = Convert.ToInt32(textBox2.Text);
            int numberOfMistakesText = Convert.ToInt32(textBox3.Text);
            string lenChoiceText = comboBox1.Text;
            menuForm.addRandomMatrix(widthText, heightText, numberOfMistakesText, lenChoiceText);
            this.Close();
        }
    }
}
