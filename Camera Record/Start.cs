using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Camera_Record
{
    public partial class Start : Form
    {
        Form1 form1;
        Form2 form2;
        public Start()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            form1 = new Form1();
            form1.FormClosed += Form_FormClosed;
            form1.Show();
            HideForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            form2 = new Form2();
            form2.FormClosed += Form_FormClosed;
            form2.Show();
            HideForm();
        }

        private void HideForm()
        {
            this.Hide();
        }
      
        void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


    }
}
