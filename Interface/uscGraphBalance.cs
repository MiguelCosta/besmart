using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Interface {
    public partial class uscGraphBalance : UserControl {
        public uscGraphBalance() {
            InitializeComponent();
        }

        private void uscGraphBalance_Load(object sender, EventArgs e) {
            try {

                lstCriteria.DataSource = Business.ManagementDataBase.caracteristicas_escolhidas.Values.ToList();

            } catch(Exception) {
                
            }
        }
    }
}
