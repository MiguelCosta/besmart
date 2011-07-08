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
    public partial class ConsultWebpage : Form
    {
        public ConsultWebpage()
        {
            InitializeComponent();

            refreshTable();
        }

        public void refreshTable()
        {
            dataGridViewSimpleSoftware.DataSource = Business.ManagementDataBase.tableSoftwaresWebPage();
        }

        private void ConsultWebpage_FormClosing(Object sender, FormClosingEventArgs e) 
        {
            Close();
        }

        private void ConsultWebpage_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'lI4DataSet.software' table. You can move, or remove it, as needed.
            this.softwareTableAdapter.Fill(this.lI4DataSet.software);

        }

        private void editSoftwareListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditSWList editList = new EditSWList();
            editList.Show();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.ShowDialog();
        }

        private void dataGridViewSimpleSoftware_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.AppStarting;

            marqueeProgressBar.Style = ProgressBarStyle.Marquee;
            
            int linha = dataGridViewSimpleSoftware.CurrentRow.Index;
            if (linha >= 0)
            {
                string cellValue = dataGridViewSimpleSoftware["Link", linha].Value.ToString();
                webBrowser.Navigate(cellValue);
            }

        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.Default;
            marqueeProgressBar.Style = ProgressBarStyle.Blocks;
        }

    }
}
