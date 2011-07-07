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
    public partial class Tutorials : Form
    {
        public Tutorials(string url)
        {
            InitializeComponent();
            webBrowser.Navigate(url);
        }

        private void Tutorials_Load(object sender, EventArgs e)
        {

        }
    }
}
