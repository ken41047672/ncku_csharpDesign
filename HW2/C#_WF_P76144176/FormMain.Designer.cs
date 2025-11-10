namespace C__WF_P76144176
{
    partial class FormMain
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dgvMachine = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.儀表板ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.cBWorkOrder = new System.Windows.Forms.ComboBox();
            this.fixlblMachineID = new System.Windows.Forms.Label();
            this.fixlblModel = new System.Windows.Forms.Label();
            this.fixlblPurchaseDate = new System.Windows.Forms.Label();
            this.fixlblStatus = new System.Windows.Forms.Label();
            this.fixlblCurrentWorkOrderID = new System.Windows.Forms.Label();
            this.lblMachineID = new System.Windows.Forms.Label();
            this.lblModel = new System.Windows.Forms.Label();
            this.lblPurchaseDate = new System.Windows.Forms.Label();
            this.lblCurrentWorkOrderID = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnDisableAlarm = new System.Windows.Forms.Button();
            this.fixlblNotStartWorkOrder = new System.Windows.Forms.Label();
            this.btnAssignWorkID = new System.Windows.Forms.Button();
            this.rTBAlarm = new System.Windows.Forms.RichTextBox();
            this.rTBMachineDetail = new System.Windows.Forms.RichTextBox();
            this.fixlbl_dgvMachine = new System.Windows.Forms.Label();
            this.fixlbl_MachineDetail = new System.Windows.Forms.Label();
            this.fixlbl_alarmLogDetail = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnCheckWorkOrder = new System.Windows.Forms.Button();
            this.btnCheckAlarm = new System.Windows.Forms.Button();
            this.btnCheckProductionLog = new System.Windows.Forms.Button();
            this.dgvDebug = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMachine)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDebug)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvMachine
            // 
            this.dgvMachine.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMachine.Location = new System.Drawing.Point(12, 50);
            this.dgvMachine.Name = "dgvMachine";
            this.dgvMachine.RowTemplate.Height = 24;
            this.dgvMachine.Size = new System.Drawing.Size(522, 352);
            this.dgvMachine.TabIndex = 0;
            this.dgvMachine.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMachine_CellContentClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.儀表板ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1199, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 儀表板ToolStripMenuItem
            // 
            this.儀表板ToolStripMenuItem.Name = "儀表板ToolStripMenuItem";
            this.儀表板ToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.儀表板ToolStripMenuItem.Text = "儀表板";
            this.儀表板ToolStripMenuItem.Click += new System.EventHandler(this.儀表板ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 869);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1199, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(128, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // cBWorkOrder
            // 
            this.cBWorkOrder.FormattingEnabled = true;
            this.cBWorkOrder.Location = new System.Drawing.Point(986, 50);
            this.cBWorkOrder.Name = "cBWorkOrder";
            this.cBWorkOrder.Size = new System.Drawing.Size(121, 20);
            this.cBWorkOrder.TabIndex = 3;
            // 
            // fixlblMachineID
            // 
            this.fixlblMachineID.AutoSize = true;
            this.fixlblMachineID.Location = new System.Drawing.Point(554, 50);
            this.fixlblMachineID.Name = "fixlblMachineID";
            this.fixlblMachineID.Size = new System.Drawing.Size(66, 12);
            this.fixlblMachineID.TabIndex = 4;
            this.fixlblMachineID.Text = "MachineID : ";
            // 
            // fixlblModel
            // 
            this.fixlblModel.AutoSize = true;
            this.fixlblModel.Location = new System.Drawing.Point(554, 74);
            this.fixlblModel.Name = "fixlblModel";
            this.fixlblModel.Size = new System.Drawing.Size(44, 12);
            this.fixlblModel.TabIndex = 5;
            this.fixlblModel.Text = "Model : ";
            // 
            // fixlblPurchaseDate
            // 
            this.fixlblPurchaseDate.AutoSize = true;
            this.fixlblPurchaseDate.Location = new System.Drawing.Point(554, 100);
            this.fixlblPurchaseDate.Name = "fixlblPurchaseDate";
            this.fixlblPurchaseDate.Size = new System.Drawing.Size(76, 12);
            this.fixlblPurchaseDate.TabIndex = 6;
            this.fixlblPurchaseDate.Text = "PurchaseDate : ";
            // 
            // fixlblStatus
            // 
            this.fixlblStatus.AutoSize = true;
            this.fixlblStatus.Location = new System.Drawing.Point(554, 124);
            this.fixlblStatus.Name = "fixlblStatus";
            this.fixlblStatus.Size = new System.Drawing.Size(41, 12);
            this.fixlblStatus.TabIndex = 7;
            this.fixlblStatus.Text = "Status : ";
            // 
            // fixlblCurrentWorkOrderID
            // 
            this.fixlblCurrentWorkOrderID.AutoSize = true;
            this.fixlblCurrentWorkOrderID.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fixlblCurrentWorkOrderID.Location = new System.Drawing.Point(554, 149);
            this.fixlblCurrentWorkOrderID.Name = "fixlblCurrentWorkOrderID";
            this.fixlblCurrentWorkOrderID.Size = new System.Drawing.Size(116, 12);
            this.fixlblCurrentWorkOrderID.TabIndex = 8;
            this.fixlblCurrentWorkOrderID.Text = "CurrentWorkOrderID : ";
            // 
            // lblMachineID
            // 
            this.lblMachineID.AutoSize = true;
            this.lblMachineID.Location = new System.Drawing.Point(674, 50);
            this.lblMachineID.Name = "lblMachineID";
            this.lblMachineID.Size = new System.Drawing.Size(30, 12);
            this.lblMachineID.TabIndex = 9;
            this.lblMachineID.Text = "None";
            // 
            // lblModel
            // 
            this.lblModel.AutoSize = true;
            this.lblModel.Location = new System.Drawing.Point(674, 74);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(30, 12);
            this.lblModel.TabIndex = 10;
            this.lblModel.Text = "None";
            // 
            // lblPurchaseDate
            // 
            this.lblPurchaseDate.AutoSize = true;
            this.lblPurchaseDate.Location = new System.Drawing.Point(674, 100);
            this.lblPurchaseDate.Name = "lblPurchaseDate";
            this.lblPurchaseDate.Size = new System.Drawing.Size(30, 12);
            this.lblPurchaseDate.TabIndex = 11;
            this.lblPurchaseDate.Text = "None";
            // 
            // lblCurrentWorkOrderID
            // 
            this.lblCurrentWorkOrderID.AutoSize = true;
            this.lblCurrentWorkOrderID.Location = new System.Drawing.Point(674, 149);
            this.lblCurrentWorkOrderID.Name = "lblCurrentWorkOrderID";
            this.lblCurrentWorkOrderID.Size = new System.Drawing.Size(30, 12);
            this.lblCurrentWorkOrderID.TabIndex = 12;
            this.lblCurrentWorkOrderID.Text = "None";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(674, 124);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(30, 12);
            this.lblStatus.TabIndex = 13;
            this.lblStatus.Text = "None";
            // 
            // btnDisableAlarm
            // 
            this.btnDisableAlarm.Location = new System.Drawing.Point(692, 379);
            this.btnDisableAlarm.Name = "btnDisableAlarm";
            this.btnDisableAlarm.Size = new System.Drawing.Size(75, 23);
            this.btnDisableAlarm.TabIndex = 14;
            this.btnDisableAlarm.Text = "解除警報";
            this.btnDisableAlarm.UseVisualStyleBackColor = true;
            this.btnDisableAlarm.Click += new System.EventHandler(this.btnDisableAlarm_Click);
            // 
            // fixlblNotStartWorkOrder
            // 
            this.fixlblNotStartWorkOrder.AutoSize = true;
            this.fixlblNotStartWorkOrder.Location = new System.Drawing.Point(984, 35);
            this.fixlblNotStartWorkOrder.Name = "fixlblNotStartWorkOrder";
            this.fixlblNotStartWorkOrder.Size = new System.Drawing.Size(77, 12);
            this.fixlblNotStartWorkOrder.TabIndex = 15;
            this.fixlblNotStartWorkOrder.Text = "未開始的工單";
            // 
            // btnAssignWorkID
            // 
            this.btnAssignWorkID.Location = new System.Drawing.Point(1113, 50);
            this.btnAssignWorkID.Name = "btnAssignWorkID";
            this.btnAssignWorkID.Size = new System.Drawing.Size(75, 23);
            this.btnAssignWorkID.TabIndex = 16;
            this.btnAssignWorkID.Text = "指派工單";
            this.btnAssignWorkID.UseVisualStyleBackColor = true;
            this.btnAssignWorkID.Click += new System.EventHandler(this.btnAssignWorkID_Click);
            // 
            // rTBAlarm
            // 
            this.rTBAlarm.Location = new System.Drawing.Point(556, 226);
            this.rTBAlarm.Name = "rTBAlarm";
            this.rTBAlarm.Size = new System.Drawing.Size(402, 147);
            this.rTBAlarm.TabIndex = 17;
            this.rTBAlarm.Text = "";
            // 
            // rTBMachineDetail
            // 
            this.rTBMachineDetail.Location = new System.Drawing.Point(556, 50);
            this.rTBMachineDetail.Name = "rTBMachineDetail";
            this.rTBMachineDetail.Size = new System.Drawing.Size(402, 148);
            this.rTBMachineDetail.TabIndex = 18;
            this.rTBMachineDetail.Text = "";
            // 
            // fixlbl_dgvMachine
            // 
            this.fixlbl_dgvMachine.AutoSize = true;
            this.fixlbl_dgvMachine.Location = new System.Drawing.Point(10, 35);
            this.fixlbl_dgvMachine.Name = "fixlbl_dgvMachine";
            this.fixlbl_dgvMachine.Size = new System.Drawing.Size(72, 12);
            this.fixlbl_dgvMachine.TabIndex = 19;
            this.fixlbl_dgvMachine.Text = "Machine 列表";
            // 
            // fixlbl_MachineDetail
            // 
            this.fixlbl_MachineDetail.AutoSize = true;
            this.fixlbl_MachineDetail.Location = new System.Drawing.Point(554, 35);
            this.fixlbl_MachineDetail.Name = "fixlbl_MachineDetail";
            this.fixlbl_MachineDetail.Size = new System.Drawing.Size(96, 12);
            this.fixlbl_MachineDetail.TabIndex = 20;
            this.fixlbl_MachineDetail.Text = "Machine 詳細資訊";
            // 
            // fixlbl_alarmLogDetail
            // 
            this.fixlbl_alarmLogDetail.AutoSize = true;
            this.fixlbl_alarmLogDetail.Location = new System.Drawing.Point(554, 211);
            this.fixlbl_alarmLogDetail.Name = "fixlbl_alarmLogDetail";
            this.fixlbl_alarmLogDetail.Size = new System.Drawing.Size(107, 12);
            this.fixlbl_alarmLogDetail.TabIndex = 21;
            this.fixlbl_alarmLogDetail.Text = "Alarm Log 詳細資訊";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(1067, 379);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 23;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnCheckWorkOrder
            // 
            this.btnCheckWorkOrder.Location = new System.Drawing.Point(12, 409);
            this.btnCheckWorkOrder.Name = "btnCheckWorkOrder";
            this.btnCheckWorkOrder.Size = new System.Drawing.Size(160, 23);
            this.btnCheckWorkOrder.TabIndex = 24;
            this.btnCheckWorkOrder.Text = "CheckWorkOrder";
            this.btnCheckWorkOrder.UseVisualStyleBackColor = true;
            this.btnCheckWorkOrder.Click += new System.EventHandler(this.btnCheckWorkOrder_Click);
            // 
            // btnCheckAlarm
            // 
            this.btnCheckAlarm.Location = new System.Drawing.Point(178, 409);
            this.btnCheckAlarm.Name = "btnCheckAlarm";
            this.btnCheckAlarm.Size = new System.Drawing.Size(158, 23);
            this.btnCheckAlarm.TabIndex = 25;
            this.btnCheckAlarm.Text = "CheckAlarm";
            this.btnCheckAlarm.UseVisualStyleBackColor = true;
            this.btnCheckAlarm.Click += new System.EventHandler(this.btnCheckAlarm_Click);
            // 
            // btnCheckProductionLog
            // 
            this.btnCheckProductionLog.Location = new System.Drawing.Point(342, 409);
            this.btnCheckProductionLog.Name = "btnCheckProductionLog";
            this.btnCheckProductionLog.Size = new System.Drawing.Size(192, 23);
            this.btnCheckProductionLog.TabIndex = 26;
            this.btnCheckProductionLog.Text = "CheckProductionLog";
            this.btnCheckProductionLog.UseVisualStyleBackColor = true;
            this.btnCheckProductionLog.Click += new System.EventHandler(this.btnCheckProductionLog_Click);
            // 
            // dgvDebug
            // 
            this.dgvDebug.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDebug.Location = new System.Drawing.Point(12, 438);
            this.dgvDebug.Name = "dgvDebug";
            this.dgvDebug.RowTemplate.Height = 24;
            this.dgvDebug.Size = new System.Drawing.Size(1176, 428);
            this.dgvDebug.TabIndex = 27;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1199, 891);
            this.Controls.Add(this.dgvDebug);
            this.Controls.Add(this.btnCheckProductionLog);
            this.Controls.Add(this.btnCheckAlarm);
            this.Controls.Add(this.btnCheckWorkOrder);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.fixlbl_alarmLogDetail);
            this.Controls.Add(this.fixlbl_MachineDetail);
            this.Controls.Add(this.fixlbl_dgvMachine);
            this.Controls.Add(this.rTBMachineDetail);
            this.Controls.Add(this.rTBAlarm);
            this.Controls.Add(this.btnAssignWorkID);
            this.Controls.Add(this.fixlblNotStartWorkOrder);
            this.Controls.Add(this.btnDisableAlarm);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblCurrentWorkOrderID);
            this.Controls.Add(this.lblPurchaseDate);
            this.Controls.Add(this.lblModel);
            this.Controls.Add(this.lblMachineID);
            this.Controls.Add(this.fixlblCurrentWorkOrderID);
            this.Controls.Add(this.fixlblStatus);
            this.Controls.Add(this.fixlblPurchaseDate);
            this.Controls.Add(this.fixlblModel);
            this.Controls.Add(this.fixlblMachineID);
            this.Controls.Add(this.cBWorkOrder);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dgvMachine);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMachine)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDebug)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

     



        #endregion

        private System.Windows.Forms.DataGridView dgvMachine;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 儀表板ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ComboBox cBWorkOrder;
        private System.Windows.Forms.Label fixlblMachineID;
        private System.Windows.Forms.Label fixlblModel;
        private System.Windows.Forms.Label fixlblPurchaseDate;
        private System.Windows.Forms.Label fixlblStatus;
        private System.Windows.Forms.Label fixlblCurrentWorkOrderID;
        private System.Windows.Forms.Label lblMachineID;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.Label lblPurchaseDate;
        private System.Windows.Forms.Label lblCurrentWorkOrderID;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnDisableAlarm;
        private System.Windows.Forms.Label fixlblNotStartWorkOrder;
        private System.Windows.Forms.Button btnAssignWorkID;
        private System.Windows.Forms.RichTextBox rTBAlarm;
        private System.Windows.Forms.RichTextBox rTBMachineDetail;
        private System.Windows.Forms.Label fixlbl_dgvMachine;
        private System.Windows.Forms.Label fixlbl_MachineDetail;
        private System.Windows.Forms.Label fixlbl_alarmLogDetail;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnCheckWorkOrder;
        private System.Windows.Forms.Button btnCheckAlarm;
        private System.Windows.Forms.Button btnCheckProductionLog;
        private System.Windows.Forms.DataGridView dgvDebug;
    }
}

