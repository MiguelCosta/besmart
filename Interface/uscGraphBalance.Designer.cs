namespace Interface {
    partial class uscGraphBalance {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.dgvBalance = new System.Windows.Forms.DataGridView();
            this.graphRankingFinal = new ZedGraph.ZedGraphControl();
            this.lstCriteria = new System.Windows.Forms.ListBox();
            this.dgvBalance_Software = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvBalance_Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nudCriteria = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCriteria)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvBalance
            // 
            this.dgvBalance.AllowUserToAddRows = false;
            this.dgvBalance.AllowUserToDeleteRows = false;
            this.dgvBalance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBalance.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvBalance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBalance.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvBalance_Software,
            this.dgvBalance_Value});
            this.dgvBalance.Location = new System.Drawing.Point(612, 3);
            this.dgvBalance.Name = "dgvBalance";
            this.dgvBalance.ReadOnly = true;
            this.dgvBalance.Size = new System.Drawing.Size(221, 489);
            this.dgvBalance.TabIndex = 0;
            // 
            // graphRankingFinal
            // 
            this.graphRankingFinal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphRankingFinal.BackgroundImage = global::Interface.Properties.Resources.Logo_Marca_de_água;
            this.graphRankingFinal.IsEnableHZoom = false;
            this.graphRankingFinal.IsEnableVZoom = false;
            this.graphRankingFinal.IsSynchronizeXAxes = true;
            this.graphRankingFinal.IsSynchronizeYAxes = true;
            this.graphRankingFinal.Location = new System.Drawing.Point(230, 8);
            this.graphRankingFinal.Name = "graphRankingFinal";
            this.graphRankingFinal.ScrollGrace = 0D;
            this.graphRankingFinal.ScrollMaxX = 0D;
            this.graphRankingFinal.ScrollMaxY = 0D;
            this.graphRankingFinal.ScrollMaxY2 = 0D;
            this.graphRankingFinal.ScrollMinX = 0D;
            this.graphRankingFinal.ScrollMinY = 0D;
            this.graphRankingFinal.ScrollMinY2 = 0D;
            this.graphRankingFinal.Size = new System.Drawing.Size(376, 457);
            this.graphRankingFinal.TabIndex = 3;
            // 
            // lstCriteria
            // 
            this.lstCriteria.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstCriteria.FormattingEnabled = true;
            this.lstCriteria.Location = new System.Drawing.Point(3, 8);
            this.lstCriteria.Name = "lstCriteria";
            this.lstCriteria.Size = new System.Drawing.Size(221, 485);
            this.lstCriteria.TabIndex = 4;
            this.lstCriteria.SelectedIndexChanged += new System.EventHandler(this.lstCriteria_SelectedIndexChanged);
            // 
            // dgvBalance_Software
            // 
            this.dgvBalance_Software.DataPropertyName = "Key";
            this.dgvBalance_Software.HeaderText = "Software";
            this.dgvBalance_Software.Name = "dgvBalance_Software";
            this.dgvBalance_Software.ReadOnly = true;
            // 
            // dgvBalance_Value
            // 
            this.dgvBalance_Value.DataPropertyName = "Value";
            this.dgvBalance_Value.HeaderText = "Value";
            this.dgvBalance_Value.Name = "dgvBalance_Value";
            this.dgvBalance_Value.ReadOnly = true;
            // 
            // nudCriteria
            // 
            this.nudCriteria.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudCriteria.DecimalPlaces = 3;
            this.nudCriteria.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudCriteria.Location = new System.Drawing.Point(230, 471);
            this.nudCriteria.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCriteria.Name = "nudCriteria";
            this.nudCriteria.Size = new System.Drawing.Size(120, 20);
            this.nudCriteria.TabIndex = 5;
            this.nudCriteria.ValueChanged += new System.EventHandler(this.nudCriteria_ValueChanged);
            // 
            // uscGraphBalance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nudCriteria);
            this.Controls.Add(this.lstCriteria);
            this.Controls.Add(this.graphRankingFinal);
            this.Controls.Add(this.dgvBalance);
            this.Name = "uscGraphBalance";
            this.Size = new System.Drawing.Size(836, 495);
            this.Load += new System.EventHandler(this.uscGraphBalance_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCriteria)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBalance;
        private ZedGraph.ZedGraphControl graphRankingFinal;
        private System.Windows.Forms.ListBox lstCriteria;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvBalance_Software;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvBalance_Value;
        private System.Windows.Forms.NumericUpDown nudCriteria;
    }
}
