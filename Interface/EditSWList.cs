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
            refreshTableCharacteristics();
        }



        private void refreshTableSoftwares()
        {
            dataGridViewTabelaSoftware.DataSource = Business.ManagementDataBase.tableSoftwaresSimple();
        }

        private void refreshTableCharacteristics()
        {
            dataGridViewCharacteristicsList.DataSource = Business.ManagementDataBase.tableCharacteristics();

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
            Add_Characteristics a = new Add_Characteristics();
            a.ShowDialog();
            refreshTableSoftwares();
        }

        private void buttonAddnew_Click(object sender, EventArgs e)
        {
            Add_Software a = new Add_Software();
            a.ShowDialog();
            refreshTableSoftwares();
        }

        private void dataGridViewTabelaSoftware_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow line in dataGridViewTabelaSoftware.Rows)
            {
                if (line.Cells[0].Value != null && line.Cells[0].Value.ToString().Equals("True"))
                {
                    line.Selected = true;
                }
                if (line.Cells[0].Value != null && line.Cells[0].Value.ToString().Equals("False"))
                {
                    line.Selected = false;
                }
            }
        }

        private void dataGridViewTabelaSoftware_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow line in dataGridViewTabelaSoftware.Rows)
            {
                if (line.Cells[0].Value != null && line.Cells[0].Value.ToString().Equals("True"))
                {
                    line.Selected = true;
                }
                if (line.Cells[0].Value != null && line.Cells[0].Value.ToString().Equals("False"))
                {
                    line.Selected = false;
                }
            }

        }

        private void dataGridViewCharacteristicsList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow line in dataGridViewCharacteristicsList.Rows)
            {
                if (line.Cells[0].Value != null && line.Cells[0].Value.ToString().Equals("True"))
                {
                    line.Selected = true;
                }
                if (line.Cells[0].Value != null && line.Cells[0].Value.ToString().Equals("False"))
                {
                    line.Selected = false;
                }
            }
        }

        private void dataGridViewCharacteristicsList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow line in dataGridViewCharacteristicsList.Rows)
            {
                if (line.Cells[0].Value != null && line.Cells[0].Value.ToString().Equals("True"))
                {
                    line.Selected = true;
                }
                if (line.Cells[0].Value != null && line.Cells[0].Value.ToString().Equals("False"))
                {
                    line.Selected = false;
                }
            }
        }


    }
}
