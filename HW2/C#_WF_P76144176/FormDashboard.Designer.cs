namespace C__WF_P76144176
{
    partial class FormDashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbMachines = new System.Windows.Forms.ComboBox();
            this.fixlabelcmbMachines = new System.Windows.Forms.Label();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.lblTotalGood = new System.Windows.Forms.Label();
            this.lblTotalBad = new System.Windows.Forms.Label();
            this.lblYieldRate = new System.Windows.Forms.Label();
            this.lblAlarmCount = new System.Windows.Forms.Label();
            this.lblTotalDowntime = new System.Windows.Forms.Label();
            this.dgvLongestDowntime = new System.Windows.Forms.DataGridView();
            this.dgvCompletedWOs = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLongestDowntime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompletedWOs)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbMachines
            // 
            this.cmbMachines.FormattingEnabled = true;
            this.cmbMachines.Location = new System.Drawing.Point(95, 28);
            this.cmbMachines.Name = "cmbMachines";
            this.cmbMachines.Size = new System.Drawing.Size(121, 20);
            this.cmbMachines.TabIndex = 0;
            // 
            // fixlabelcmbMachines
            // 
            this.fixlabelcmbMachines.AutoSize = true;
            this.fixlabelcmbMachines.Location = new System.Drawing.Point(27, 31);
            this.fixlabelcmbMachines.Name = "fixlabelcmbMachines";
            this.fixlabelcmbMachines.Size = new System.Drawing.Size(62, 12);
            this.fixlabelcmbMachines.TabIndex = 1;
            this.fixlabelcmbMachines.Text = "選擇機台 : ";
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Location = new System.Drawing.Point(222, 28);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(75, 20);
            this.btnAnalyze.TabIndex = 2;
            this.btnAnalyze.Text = "開始分析";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // lblTotalGood
            // 
            this.lblTotalGood.AutoSize = true;
            this.lblTotalGood.Location = new System.Drawing.Point(30, 70);
            this.lblTotalGood.Name = "lblTotalGood";
            this.lblTotalGood.Size = new System.Drawing.Size(33, 12);
            this.lblTotalGood.TabIndex = 3;
            this.lblTotalGood.Text = "label1";
            // 
            // lblTotalBad
            // 
            this.lblTotalBad.AutoSize = true;
            this.lblTotalBad.Location = new System.Drawing.Point(30, 90);
            this.lblTotalBad.Name = "lblTotalBad";
            this.lblTotalBad.Size = new System.Drawing.Size(33, 12);
            this.lblTotalBad.TabIndex = 4;
            this.lblTotalBad.Text = "label2";
            // 
            // lblYieldRate
            // 
            this.lblYieldRate.AutoSize = true;
            this.lblYieldRate.Location = new System.Drawing.Point(30, 110);
            this.lblYieldRate.Name = "lblYieldRate";
            this.lblYieldRate.Size = new System.Drawing.Size(33, 12);
            this.lblYieldRate.TabIndex = 5;
            this.lblYieldRate.Text = "label3";
            // 
            // lblAlarmCount
            // 
            this.lblAlarmCount.AutoSize = true;
            this.lblAlarmCount.Location = new System.Drawing.Point(30, 130);
            this.lblAlarmCount.Name = "lblAlarmCount";
            this.lblAlarmCount.Size = new System.Drawing.Size(33, 12);
            this.lblAlarmCount.TabIndex = 6;
            this.lblAlarmCount.Text = "label4";
            // 
            // lblTotalDowntime
            // 
            this.lblTotalDowntime.AutoSize = true;
            this.lblTotalDowntime.Location = new System.Drawing.Point(30, 150);
            this.lblTotalDowntime.Name = "lblTotalDowntime";
            this.lblTotalDowntime.Size = new System.Drawing.Size(33, 12);
            this.lblTotalDowntime.TabIndex = 7;
            this.lblTotalDowntime.Text = "label5";
            // 
            // dgvLongestDowntime
            // 
            this.dgvLongestDowntime.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLongestDowntime.Location = new System.Drawing.Point(303, 28);
            this.dgvLongestDowntime.Name = "dgvLongestDowntime";
            this.dgvLongestDowntime.RowTemplate.Height = 24;
            this.dgvLongestDowntime.Size = new System.Drawing.Size(400, 480);
            this.dgvLongestDowntime.TabIndex = 8;
            // 
            // dgvCompletedWOs
            // 
            this.dgvCompletedWOs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCompletedWOs.Location = new System.Drawing.Point(709, 28);
            this.dgvCompletedWOs.Name = "dgvCompletedWOs";
            this.dgvCompletedWOs.RowTemplate.Height = 24;
            this.dgvCompletedWOs.Size = new System.Drawing.Size(400, 480);
            this.dgvCompletedWOs.TabIndex = 9;
// 
            // FormDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1122, 521);
            this.Controls.Add(this.dgvCompletedWOs);
            this.Controls.Add(this.dgvLongestDowntime);
            this.Controls.Add(this.lblTotalDowntime);
            this.Controls.Add(this.lblAlarmCount);
            this.Controls.Add(this.lblYieldRate);
            this.Controls.Add(this.lblTotalBad);
            this.Controls.Add(this.lblTotalGood);
            this.Controls.Add(this.btnAnalyze);
            this.Controls.Add(this.fixlabelcmbMachines);
            this.Controls.Add(this.cmbMachines);
            this.Name = "FormDashboard";
            this.Text = "FormDashboard";
            this.Load += new System.EventHandler(this.FormDashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLongestDowntime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompletedWOs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbMachines;
        private System.Windows.Forms.Label fixlabelcmbMachines;
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.Label lblTotalGood;
        private System.Windows.Forms.Label lblTotalBad;
        private System.Windows.Forms.Label lblYieldRate;
        private System.Windows.Forms.Label lblAlarmCount;
        private System.Windows.Forms.Label lblTotalDowntime;
        private System.Windows.Forms.DataGridView dgvLongestDowntime;
        private System.Windows.Forms.DataGridView dgvCompletedWOs;
    }
}