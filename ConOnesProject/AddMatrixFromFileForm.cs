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
            menuForm.matrixFilePath(inputFilePath);
            this.Close();
        }

    }
}
