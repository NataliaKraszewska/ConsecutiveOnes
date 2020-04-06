using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test2
{
    public partial class Form1 : Form, Interface1
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(this);
            form3.Show();
        }

        void Interface1.changeWindow(string text)
        {
            label1.Text = text;
            label1.Refresh();
            Console.WriteLine("text value in forms1:");
            Console.WriteLine(text);
            Console.WriteLine("label1.text value");
            Console.WriteLine(label1.Text);
        }

    }
}
