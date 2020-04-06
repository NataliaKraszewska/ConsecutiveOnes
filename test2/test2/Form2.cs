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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        

        private Form1 mainForm = null;

        public Form2(Form callingForm)
        {
            mainForm = callingForm as Form1;
            InitializeComponent();
        }

        Interface1 obj = new Form1();

        private void button1_Click(object sender, EventArgs e)
        {
            obj.changeWindow(textBox1.Text);
            Console.WriteLine("text value in forms2:");
            Console.WriteLine(textBox1.Text);
            this.Close();
        }
    }
}
