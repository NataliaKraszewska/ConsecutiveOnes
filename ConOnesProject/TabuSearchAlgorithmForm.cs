using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ConOnesProject
{
    public partial class TabuSearchAlgorithmForm : Form
    {
        string text;
        bool stopTabuAlgorithm = false;

        private IFormsMatrixInformations menuForm;
        public TabuSearchAlgorithmForm(IFormsMatrixInformations callingForm, string in_text)
        {
            menuForm = callingForm;
            text = in_text;
            InitializeComponent();
        }


        void Append_text()
        {
            for (int i = 0; i < 10000; i++)
            {
                richTextBox1.Text += text + " ";
                Thread.Sleep(10);
                if (stopTabuAlgorithm)
                    Thread.CurrentThread.Abort();

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            stopTabuAlgorithm = false;
            Thread t = new Thread(Append_text);
            t.Name = "TabuSearchAlgorithm";
            t.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stopTabuAlgorithm = true;

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}