using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Interface
{

    public partial class Init : Form
    {

        public Init()
        {
            InitializeComponent();

        }



        private void buttonLogin_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Welcome!");
            this.Dispose();

        }


    }


}

