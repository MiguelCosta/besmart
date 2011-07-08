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
    public partial class EditSWList : Form
    {

        public EditSWList()
        {
            InitializeComponent();

            refreshTableSoftwares();
        }



        private void refreshTableSoftwares()
        {
            dataGridViewTabelaSoftware.DataSource = Business.ManagementDataBase.tableSoftwares(true) ;
        }


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.ShowDialog();
        }

        private void viewSoftwareWebsiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ConsultWebpage consultwp = new ConsultWebpage();
            //consultwp.Show();
        }

        private void editSoftwareListToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void buttonAddCharacteristics_Click(object sender, EventArgs e)
        {
            /*
            string m = "Whta type of characteristic do want to insert?\n1 - Boolean\n2 - Numeric\n3 - Qualitative";
            string r = Microsoft.VisualBasic.Interaction.InputBox(m, "Insert Caracteristics");

            if (r.Equals("1"))
            {
                string x = Microsoft.VisualBasic.Interaction.InputBox("What is the name?", "Insert Caracteristics");
                _tabela_softwares.Columns.Add(x);

                DataView view = new DataView(_tabela_softwares);
                dataGridViewTabelaSoftware.DataSource = view;
            }

            if (r.Equals("2"))
            {
                string x = Microsoft.VisualBasic.Interaction.InputBox("What is the name?", "Insert Caracteristics");
                _tabela_softwares.Columns.Add(x);

                DataView view = new DataView(_tabela_softwares);
                dataGridViewTabelaSoftware.DataSource = view;
            }

            if (r.Equals("3"))
            {
                string x = Microsoft.VisualBasic.Interaction.InputBox("What is the name?\nvalue-valueorder;value-valueorder\nExample:bad-0;good-1\n", "Insert Caracteristics");
                _tabela_softwares.Columns.Add(x);

                DataView view = new DataView(_tabela_softwares);
                dataGridViewTabelaSoftware.DataSource = view;
            }
             * */

        }
    }
}
