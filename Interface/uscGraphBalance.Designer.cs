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
            ((System.ComponentModel.ISupportInitialize)(this.dgvBalance)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvBalance
            // 
            this.dgvBalance.AllowUserToAddRows = false;
            this.dgvBalance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBalance.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvBalance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
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
            this.graphRankingFinal.Size = new System.Drawing.Size(376, 431);
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
            // 
            // uscGraphBalance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lstCriteria);
            this.Controls.Add(this.graphRankingFinal);
            this.Controls.Add(this.dgvBalance);
            this.Name = "uscGraphBalance";
            this.Size = new System.Drawing.Size(836, 495);
            this.Load += new System.EventHandler(this.uscGraphBalance_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBalance)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBalance;
        private ZedGraph.ZedGraphControl graphRankingFinal;
        private System.Windows.Forms.ListBox lstCriteria;
    }
}
