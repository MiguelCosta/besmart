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

        private Dictionary<string, float> _SoftwarePeso = new Dictionary<string, float>();
        private int _charSelect = -1;
        private float _charSelectValue = -1;

        private void uscGraphBalance_Load(object sender, EventArgs e) {
            try {
                lstCriteria.DataSource = Business.ManagementDataBase.caracteristicas_escolhidas.Values.ToList();
                FillBalance();
            } catch (Exception) {
            }
        }

        private void FillBalance() {
            int index = -1;

            Business.ManagementDataBase.resultFinal = new Dictionary<int, Dictionary<string, float>>();
            if (Business.ManagementDataBase.metodo_fase_1.Equals("smart")) {
                Business.ManagementDataBase.resultFinal = Business.ManagementDataBase.decision.analiseFinal(Business.ManagementDataBase.tabelaSmartNorm, Business.ManagementDataBase.decision.TableResult);
            }

            if (Business.ManagementDataBase.metodo_fase_1.Equals("ahp")) {
                Business.ManagementDataBase.resultFinal = Business.ManagementDataBase.decision.analiseFinal(Business.ManagementDataBase.pesosFinaisClassAHP, Business.ManagementDataBase.decision.TableResult);
            }

            _SoftwarePeso = new Dictionary<string, float>();
            foreach (Dictionary<string, float> rank in Business.ManagementDataBase.resultFinal.Values) {
                _SoftwarePeso.Add(rank.First().Key, rank.First().Value);
            }

            dgvBalance.Rows.Clear();
            foreach (var item in _SoftwarePeso) {
                index = dgvBalance.Rows.Add();
                dgvBalance.Rows[index].Cells[dgvBalance_Software.Index].Value = Business.ManagementDataBase.getSoftware(int.Parse(item.Key)).Name;
                dgvBalance.Rows[index].Cells[dgvBalance_Value.Index].Value = item.Value;
            }
        }

        /// <summary>
        /// A + x
        /// B - x*(B / sum(B..Z))
        /// ...
        /// Z - x*(Z / sum(B..Z))
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudCriteria_ValueChanged(object sender, EventArgs e) {
            Dictionary<string, float> values = new Dictionary<string, float>();
            try {
                foreach (var item in Business.ManagementDataBase.tabelaSmartNorm) {
                    values.Add(item.Key, item.Value);
                }

                float diff = (float)nudCriteria.Value - _charSelectValue;
                float sum = values.Where(x => x.Key != _charSelect.ToString()).Select(x => x.Value).Sum();
                float a, b;

                foreach (var item in values) {
                    if (item.Key == _charSelect.ToString()) {
                        a = Business.ManagementDataBase.tabelaSmartNorm[item.Key];
                        Business.ManagementDataBase.tabelaSmartNorm[item.Key] = a + diff;
                    }
                    else {
                        b = Business.ManagementDataBase.tabelaSmartNorm[item.Key];
                        Business.ManagementDataBase.tabelaSmartNorm[item.Key] = b - diff * (b / (sum));
                    }
                }
                FillBalance();
            } catch (Exception) {
            }
        }

        private void lstCriteria_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                Business.Characteristic c = Business.ManagementDataBase.getCharacteristics(lstCriteria.SelectedItem.ToString());
                _charSelect = c.Id;
                _charSelectValue = Business.ManagementDataBase.tabelaSmartNorm[c.Id.ToString()];
                nudCriteria.Value = (decimal)_charSelectValue;
            } catch (Exception) {
            }
        }

    }
}
