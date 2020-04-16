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
    public partial class AddMatrixMenuForm : Form
    {
        public AddMatrixMenuForm()
        {
            InitializeComponent();
        }


        private IFormsMatrixInformations mainForm;
        public AddMatrixMenuForm(IFormsMatrixInformations callingForm)
        {
            mainForm = callingForm;
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            AddRandomMatrixForm rndMatrixForm = new AddRandomMatrixForm(mainForm);
            rndMatrixForm.Show();
            this.Close();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            AddYourOwnMatrixForm yourMatrixForm = new AddYourOwnMatrixForm(mainForm);
            yourMatrixForm.Show();
            this.Close();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            AddMatrixFromFileForm matrixFromFileForm = new AddMatrixFromFileForm(mainForm);
            matrixFromFileForm.Show();
            this.Close();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
