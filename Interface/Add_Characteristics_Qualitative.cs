using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Interface
{
    public partial class Add_Characteristics_Qualitative : UserControl
    {

        public Add_Characteristics_Qualitative()
        {
            InitializeComponent();

        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            int id = Business.ManagementDataBase.next_ID();
            textBoxID.Text = "" + id;
        }
    }
}
