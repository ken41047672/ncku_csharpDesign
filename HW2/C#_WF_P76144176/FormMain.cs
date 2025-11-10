using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;



namespace C__WF_P76144176
{
    public partial class FormMain : Form
    {
        private readonly Random random = new Random();
        private SqlConnection cn;
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" + "AttachDbFilename=|DataDirectory|myDB.mdf;" + "Integrated Security=True";
        private Dictionary<string, int> remainProductTodoForMachine;
        private Dictionary<string, int> targetQuanForWorkOrder;
        
        public FormMain()
        {
            remainProductTodoForMachine = new Dictionary<string, int>();
            targetQuanForWorkOrder = new Dictionary<string, int>();
            InitializeComponent();
            dgvMachine.CellClick += dataGridView1_CellClick;
            this.FormClosing += FormMain_FormClosing;
            this.timer1.Interval = 1000;
            this.timer1.Tick += timer1_Tick;
            cn = new SqlConnection(connectionString);
            
            try
            {
                cn.Open();
                toolStripStatusLabel1.Text = "資料庫已連接";

                timer1.Start();
            }
            catch (Exception ex){
                MessageBox.Show(ex.ToString());
                Application.Exit();
            }
        }

      

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {

            CleanUpTable(false);
            if (cn.State == ConnectionState.Open)
                cn.Close();
        }

        
        private void 儀表板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDashboard f = new FormDashboard(cn);
            f.ShowDialog();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            dgvMachine.SelectionMode = DataGridViewSelectionMode.FullRowSelect; 
            SetRichTextBoxFont();
            CleanUpTable();
            insertInitValue();
            ResetSystemState();
            showMachineToolsTable();
            showWaitToStartOrder();
        }
       
        public void SetRichTextBoxFont()
        {
            if (IsFontInstalled("Consolas"))
            {
                float fontSize = 10f;

                Font newFont = new Font("Consolas", fontSize, FontStyle.Regular);

                this.rTBMachineDetail.Font = newFont;
                this.rTBAlarm.Font         = newFont;
            }
        }
        private bool IsFontInstalled(string fontName)
        {
            using (InstalledFontCollection fonts = new InstalledFontCollection())
            {
                return fonts.Families.Any(font => font.Name.Equals(fontName, StringComparison.OrdinalIgnoreCase));
            }
        }
        public void CleanUpTable(bool ShowMessage = false)
        {

            string[] tableList =
             {
                "ProductionLog",  
                "AlarmLog"       
            };

            foreach (string table in tableList)
            {
                string deleteQuery = $"TRUNCATE TABLE {table};";
                using (SqlCommand cmd = new SqlCommand(deleteQuery, cn))
                {
                  
                    try
                    {
                        cmd.ExecuteNonQuery();

                    }
                    catch (Exception ex2)
                    {
                        MessageBox.Show($"清空表格 {table} 失敗 (DELETE 錯誤): {ex2.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            string[] tableList2 =
             {
              
                "MachineTools", 
                "WorkOrders"      
            };

            foreach (string table in tableList2)
            {
                string deleteQuery = $"DELETE FROM {table};";
                using (SqlCommand cmd = new SqlCommand(deleteQuery, cn))
                {
                        
                    try
                    {
                        cmd.ExecuteNonQuery();
                           
                    }
                    catch (Exception ex2)
                    {
                        MessageBox.Show($"清空表格 {table} 失敗 (DELETE 錯誤): {ex2.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
           
        }
        private void showWaitToStartOrder()
        {
            List<string> WorkOrderID = new List<string>();
            string selectQuery = $"SELECT WorkOrderID FROM WorkOrders WHERE Status = N'未開始';";    
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, cn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                 
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string id = dt.Rows[i]["WorkOrderID"].ToString();
                    WorkOrderID.Add(id);
                }
                cBWorkOrder.DataSource = WorkOrderID;
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"載入表格 MachineTools 失敗: {ex.Message}", "資料庫錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void showMachineToolsTable()
        {

            int lastRow = 0;
            for(int i = 0; i < dgvMachine.Rows.Count; i++)
            {
                if (dgvMachine.Rows[i].Selected)
                {
                    lastRow = i;
                }
            }
            string selectQuery = "SELECT * FROM MachineTools;";
            try
            {
                SqlCommand cmd = new SqlCommand(selectQuery, cn);
             

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                adapter.Fill(dt);

                dgvMachine.DataSource = dt;

                dgvMachine.Columns["MachineID"].HeaderText = "機台編號";
                dgvMachine.Columns["Status"].HeaderText = "目前狀態";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // 直接從綁定的 DataTable 中讀取數據
                    string statusValue = dt.Rows[i]["Status"].ToString();
                    DataGridViewRow row = dgvMachine.Rows[i];

                    switch (statusValue)
                    {
                        case "運轉中":
                            row.DefaultCellStyle.BackColor = Color.LightGreen;
                            break;
                        case "警報":
                            row.DefaultCellStyle.BackColor = Color.LightCoral;
                            break;
                        case "閒置":
                            row.DefaultCellStyle.BackColor = Color.LightGray;
                            break;
                        default:
                            row.DefaultCellStyle.BackColor = dgvMachine.DefaultCellStyle.BackColor;
                            break;
                    }
                }
                dgvMachine.ClearSelection();
                dgvMachine.Rows[lastRow].Selected = true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"載入表格 MachineTools 失敗: {ex.Message}", "資料庫錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
       
        public void insertInitValue()
        {
            
             DataTable dt = new DataTable("MachineToolsData");
             string MTCsvFilePath = "../../MachineTools.csv";
             // 定義 DataTable 的欄位，必須與 MachineTools 表格的欄位匹配
             dt.Columns.Add("MachineID", typeof(string));
             dt.Columns.Add("Model", typeof(string));
             dt.Columns.Add("PurchaseDate", typeof(DateTime)); // 確保類型正確
             dt.Columns.Add("Status", typeof(string));
             dt.Columns.Add("CurrentWorkOrderID", typeof(string));

             try
             {
                 var lines = File.ReadAllLines(MTCsvFilePath).Skip(1); // 假設跳過第一行標題

                 foreach (var line in lines)
                 {
                     // 這裡假設 CSV 使用逗號 (,) 分隔，您可能需要根據實際情況修改分隔符
                     var fields = line.Split(',');

                     if (fields.Length >= 5)
                     {
                         DataRow dr = dt.NewRow();
                         dr["MachineID"] = fields[0].Trim();
                         dr["Model"] = fields[1].Trim();

                         // 處理日期格式轉換
                         if (DateTime.TryParse(fields[2].Trim(), out DateTime purchaseDate))
                         {
                             dr["PurchaseDate"] = purchaseDate;
                         }
                         else
                         {
                             // 處理日期轉換錯誤 (可選：跳過或給予預設值)
                             continue;
                         }

                         dr["Status"] = fields[3].Trim();

                         // 處理允許空值的 CurrentWorkOrderID
                         string workOrderId = fields[4].Trim();
                         dr["CurrentWorkOrderID"] = string.IsNullOrEmpty(workOrderId) ? DBNull.Value : (object)workOrderId;

                         dt.Rows.Add(dr);
                     }
                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show("讀取 CSV 檔案失敗: " + ex.Message);
             }

             DataTable dataToInsert = dt;

             if (dataToInsert.Rows.Count == 0)
             {
                 MessageBox.Show("CSV 中沒有有效的數據可供插入。", "提示");
                 return;
             }

            string insertQuery = @"
                INSERT INTO MachineTools (
                    MachineID, 
                    Model, 
                    PurchaseDate, 
                    Status, 
                    CurrentWorkOrderID
                ) 
                VALUES (
                    @MachineID, 
                    @Model, 
                    @PurchaseDate, 
                    @Status, 
                    @CurrentWorkOrderID
                );";

                // 啟用事務，確保所有 INSERT 要麼全成功，要麼全失敗
            SqlTransaction transaction = cn.BeginTransaction();

            try
            {
                using (SqlCommand cmd = new SqlCommand(insertQuery, cn, transaction))
                {
                    // 預先定義參數，在迴圈中重複使用
                    cmd.Parameters.Add("@MachineID", SqlDbType.NVarChar, 50);
                    cmd.Parameters.Add("@Model", SqlDbType.NVarChar, 50);
                    cmd.Parameters.Add("@PurchaseDate", SqlDbType.Date);
                    cmd.Parameters.Add("@Status", SqlDbType.NVarChar, 20);
                    cmd.Parameters.Add("@CurrentWorkOrderID", SqlDbType.NVarChar, 50);

                    // 遍歷 DataTable 中的每一行
                    foreach (DataRow row in dataToInsert.Rows)
                    {
                        // 1. 從 DataRow 獲取值
                        // 必須處理 DBNull.Value 和可能的類型轉換錯誤

                        // 2. 賦值給參數
                        cmd.Parameters["@MachineID"].Value = row["MachineID"];
                        cmd.Parameters["@Model"].Value = row["Model"];

                        // 處理可為 NULL 的欄位 (PurchaseDate, CurrentWorkOrderID)
                        cmd.Parameters["@PurchaseDate"].Value = row.IsNull("PurchaseDate")
                                                                ? DBNull.Value
                                                                : row["PurchaseDate"];

                        cmd.Parameters["@Status"].Value = row["Status"];

                        cmd.Parameters["@CurrentWorkOrderID"].Value = row.IsNull("CurrentWorkOrderID")
                                                                        ? DBNull.Value
                                                                        : row["CurrentWorkOrderID"];

                        // 3. 執行 INSERT
                        cmd.ExecuteNonQuery();
                    }
                }

                // 所有行都成功插入，提交事務
                transaction.Commit();
                //MessageBox.Show($"成功逐筆插入 {dataToInsert.Rows.Count} 筆資料到 MachineTools 表格。", "匯入成功");
            }
            catch (SqlException ex)
            {
                // 發生 SQL 錯誤，回滾所有操作
                transaction.Rollback();
                MessageBox.Show("逐筆插入失敗 (已回滾): " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // 處理其他錯誤，例如資料類型轉換錯誤
                transaction.Rollback();
                MessageBox.Show("發生未知錯誤 (已回滾): " + ex.Message, "錯誤");
            }
        


            string CsvFilePath = "../../WorkOrders.csv";

            dt = new DataTable("WorkOrdersData");

            // 定義 DataTable 的欄位，必須與 WorkOrders 表格的欄位匹配
            dt.Columns.Add("WorkOrderID", typeof(string));
            dt.Columns.Add("ProductName", typeof(string));
            dt.Columns.Add("TargetQuantity", typeof(int)); // INT 類型
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("CostTime", typeof(int));       // INT 類型

            try
            {
                // 讀取 CSV 檔案，跳過第一行標題 (如果存在)
                var lines = File.ReadAllLines(CsvFilePath).Skip(1);

                foreach (var line in lines)
                {
                    // 假設 CSV 使用逗號 (,) 分隔
                    var fields = line.Split(',');

                    if (fields.Length >= 5)
                    {
                        DataRow dr = dt.NewRow();

                        // 1. WorkOrderID (string)
                        dr["WorkOrderID"] = fields[0].Trim();

                        // 2. ProductName (string)
                        dr["ProductName"] = fields[1].Trim();

                        // 3. TargetQuantity (int)
                        if (int.TryParse(fields[2].Trim(), out int targetQuantity))
                        {
                            dr["TargetQuantity"] = targetQuantity;
                        }
                        else { continue; } // 數據無效則跳過

                        // 4. Status (string)
                        dr["Status"] = fields[3].Trim();

                        // 5. CostTime (int)
                        if (int.TryParse(fields[4].Trim(), out int costTime))
                        {
                            dr["CostTime"] = costTime;
                        }
                        else { continue; } // 數據無效則跳過

                        dt.Rows.Add(dr);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("讀取 WorkOrder CSV 檔案失敗: " + ex.Message);
            }

            dataToInsert = dt;

            if (dataToInsert.Rows.Count == 0)
            {
                MessageBox.Show("WorkOrder CSV 中沒有有效的數據可供插入。", "提示");
                return;
            }

            insertQuery = @"
            INSERT INTO WorkOrders (
                WorkOrderID, 
                ProductName, 
                TargetQuantity, 
                Status, 
                CostTime
            ) 
            VALUES (
                @WorkOrderID, 
                @ProductName, 
                @TargetQuantity, 
                @Status, 
                @CostTime
            );";

     
            transaction = cn.BeginTransaction(); // 啟用事務

            try
            {
                using (SqlCommand cmd = new SqlCommand(insertQuery, cn, transaction))
                {
                    // 預先定義參數 (根據 WorkOrders 資料表欄位型別)
                    cmd.Parameters.Add("@WorkOrderID", SqlDbType.NVarChar, 50);
                    cmd.Parameters.Add("@ProductName", SqlDbType.NVarChar, 100);
                    cmd.Parameters.Add("@TargetQuantity", SqlDbType.Int); // 假設為 INT
                    cmd.Parameters.Add("@Status", SqlDbType.NVarChar, 20);
                    cmd.Parameters.Add("@CostTime", SqlDbType.Int);       // 假設為 INT

                    // 遍歷 DataTable 中的每一行
                    foreach (DataRow row in dataToInsert.Rows)
                    {
                        // 1. 處理字串欄位
                        cmd.Parameters["@WorkOrderID"].Value = row["WorkOrderID"];
                        cmd.Parameters["@ProductName"].Value = row["ProductName"];
                        cmd.Parameters["@Status"].Value = row["Status"];

                        // 2. 處理數值欄位 (TargetQuantity, CostTime)
                        // 必須處理 DBNull.Value，因為數值欄位不能直接為 C# null
                        cmd.Parameters["@TargetQuantity"].Value = row.IsNull("TargetQuantity")
                                                                    ? DBNull.Value
                                                                    : row["TargetQuantity"];

                        cmd.Parameters["@CostTime"].Value = row.IsNull("CostTime")
                                                            ? DBNull.Value
                                                            : row["CostTime"];

                        // 3. 執行 INSERT
                        cmd.ExecuteNonQuery();
                    }
                }

                // 所有行成功插入，提交事務
                transaction.Commit();
                //MessageBox.Show($"成功逐筆插入 {dataToInsert.Rows.Count} 筆資料到 WorkOrders 表格。", "匯入成功");
            }
            catch (SqlException ex)
            {
                // 發生 SQL 錯誤，回滾所有操作
                transaction.Rollback();
                MessageBox.Show("WorkOrder 逐筆插入失敗 (已回滾): " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // 處理其他錯誤
                transaction.Rollback();
                MessageBox.Show("發生未知錯誤 (已回滾): " + ex.Message, "錯誤");
            }
               
        }

        public bool ResetSystemState()
        {
           
            SqlTransaction transaction = cn.BeginTransaction(); 

            try
            {
                string updateWorkOrdersQuery = @"
                UPDATE WorkOrders
                SET Status = N'未開始'
                WHERE WorkOrderID IN (
                    SELECT CurrentWorkOrderID 
                    FROM MachineTools 
                    WHERE Status <> N'閒置' AND CurrentWorkOrderID IS NOT NULL
                );";

                using (SqlCommand cmd1 = new SqlCommand(updateWorkOrdersQuery, cn, transaction))
                {
                    cmd1.ExecuteNonQuery();
                }

                string updateAlarmLogQuery = @"
                UPDATE AlarmLog
                SET Status = N'Cleared', AlarmEndTime = GETDATE()
                WHERE Status = N'Active';";

                using (SqlCommand cmd2 = new SqlCommand(updateAlarmLogQuery, cn, transaction))
                {
                    cmd2.ExecuteNonQuery();
                }
                string updateMachineToolsQuery = @"
                UPDATE MachineTools
                SET Status = N'閒置', CurrentWorkOrderID = NULL
                WHERE Status <> N'閒置';";

                using (SqlCommand cmd3 = new SqlCommand(updateMachineToolsQuery, cn, transaction))
                {
                    cmd3.ExecuteNonQuery();
                }

                transaction.Commit();
                //MessageBox.Show("系統狀態重置成功：機台、工單和警報狀態已初始化。", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return true;
            }
            catch (SqlException ex)
            {
                // 發生錯誤，回滾交易
                transaction.Rollback();
                MessageBox.Show($"系統重置失敗 (已回滾): {ex.Message}", "資料庫錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show($"系統重置失敗: {ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvMachine.Rows.Count)
                return;
            string machineID = dgvMachine.Rows[e.RowIndex].Cells["MachineID"].Value.ToString();

            MachineTool selectedMachine = GetMachineTools(machineID);
            if (selectedMachine != null)
                showMachineToolsDetail(selectedMachine);
            
        }

        private void showMachineToolsDetail(MachineTool m)
        {
            btnDisableAlarm.Visible = false;


            rTBMachineDetail.Text =
                $"MachineID            : {m.MachineID}\n" +
                $"Model                : {m.Model}\n" +
                $"Purchase Date        : {m.PurchaseDate.ToString()}\n" +
                $"Status               : {m.Status}\n" +
                $"Current WorkOrder ID : {m.CurrentWorkOrderID}";


            if(m.Status == "警報")
            {
                btnDisableAlarm.Visible = true;
                AlarmLog a = GetLatestActiveAlarm(m.MachineID);
                if(a != null)
                {
                    rTBAlarm.Text =
                        $"AlarmLogID             : {a.AlarmLogID}\n" +
                        $"MachineID              : {a.MachineID}\n" +
                        $"WorkOrderID            : {a.WorkOrderID}\n" +
                        $"ProductionCountAtAlarm : {a.ProductionCountAtAlarm}\n" +
                        $"AlarmStartTime         : {a.AlarmStartTime}\n" +
                        $"AlarmEndTime           : {a.AlarmEndTime}\n" +
                        $"Status                 : {a.Status}\n" 
                    ;
                }
            }
        }
        public AlarmLog GetLatestActiveAlarm(string machineID)
        {
            AlarmLog alarm = null;

            string selectQuery = @"
            SELECT TOP 1 
                AlarmLogID, MachineID, WorkOrderID, ProductionCountAtAlarm, 
                AlarmStartTime, AlarmEndTime, Status 
            FROM AlarmLog 
            WHERE MachineID = @ID AND Status = N'Active' 
            ORDER BY AlarmStartTime DESC;";

            using (SqlCommand cmd = new SqlCommand(selectQuery, cn))
            {
                cmd.Parameters.AddWithValue("@ID", machineID);

                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // 讀取第一筆紀錄
                        {
                            alarm = new AlarmLog();
                            int ordinal;

                            alarm.AlarmLogID = reader.GetInt32(reader.GetOrdinal("AlarmLogID"));
                            alarm.MachineID = reader.GetString(reader.GetOrdinal("MachineID"));
                            alarm.WorkOrderID = reader.GetString(reader.GetOrdinal("WorkOrderID"));
                            ordinal = reader.GetOrdinal("ProductionCountAtAlarm");
                            alarm.ProductionCountAtAlarm = reader.IsDBNull(ordinal)
                                ? (int?)null
                                : reader.GetInt32(ordinal);

                            alarm.AlarmStartTime = reader.GetDateTime(reader.GetOrdinal("AlarmStartTime"));

                            ordinal = reader.GetOrdinal("AlarmEndTime");
                            alarm.AlarmEndTime = reader.IsDBNull(ordinal)
                                ? (DateTime?)null
                                : reader.GetDateTime(ordinal);

                            alarm.Status = reader.GetString(reader.GetOrdinal("Status"));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"查詢最新警報失敗: {ex.Message}", "資料庫錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            return alarm;
        }
        public MachineTool GetMachineTools(string machineID)
        {
            string selectQuery = $"SELECT * FROM MachineTools WHERE MachineID = '{machineID}'";
            MachineTool machine = new MachineTool();
            using (SqlCommand cmd = new SqlCommand(selectQuery, cn))
            {
                try
                {

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            machine = new MachineTool
                            {
                                MachineID = reader.GetString(reader.GetOrdinal("MachineID")),
                                Model = reader.GetString(reader.GetOrdinal("Model")),
                                PurchaseDate = reader.IsDBNull(reader.GetOrdinal("PurchaseDate"))
                                    ? (DateTime?)null
                                    : reader.GetDateTime(reader.GetOrdinal("PurchaseDate")),
                                Status = reader.GetString(reader.GetOrdinal("Status")),
                                CurrentWorkOrderID = reader.IsDBNull(reader.GetOrdinal("CurrentWorkOrderID"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("CurrentWorkOrderID"))
                            };

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"錯誤 + {ex.ToString()}");
                    return null;
                }
                return machine;
            }
        }
        public List<MachineTool> GetMachineToolsList()
        {
            string selectQuery = $"SELECT * FROM MachineTools";
            MachineTool machine = new MachineTool();
            using (SqlCommand cmd = new SqlCommand(selectQuery, cn))
            {
                List<MachineTool> machineTools = new List<MachineTool>();
                try
                {

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // 逐行讀取查詢結果
                        while (reader.Read())
                        {
                            machine = new MachineTool
                            {
                                MachineID = reader.GetString(reader.GetOrdinal("MachineID")),
                                Model = reader.GetString(reader.GetOrdinal("Model")),
                                PurchaseDate = reader.IsDBNull(reader.GetOrdinal("PurchaseDate"))
                                    ? (DateTime?)null
                                    : reader.GetDateTime(reader.GetOrdinal("PurchaseDate")),
                                Status = reader.GetString(reader.GetOrdinal("Status")),
                                CurrentWorkOrderID = reader.IsDBNull(reader.GetOrdinal("CurrentWorkOrderID"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("CurrentWorkOrderID"))
                            };
                            machineTools.Add(machine);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"錯誤 + {ex.ToString()}");
                    return null;
                }
                return machineTools;
            }
        }
        private void btnAssignWorkID_Click(object sender, EventArgs e)
        {

            DataGridViewRow currentRow = null;
            for (int i = 0; i < dgvMachine.Rows.Count; i++)
            {
                if (dgvMachine.Rows[i].Selected)
                {
                    currentRow = dgvMachine.Rows[i];
                }
            }
            if (currentRow != null)
            {
                string machineID = currentRow.Cells["MachineID"].Value.ToString();              
                string machineStatus = currentRow.Cells["Status"].Value.ToString();

                if(machineStatus != "閒置")
                {
                    MessageBox.Show("無法進行指派","提示",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if(machineStatus == "閒置")
                {
                    if (cBWorkOrder.SelectedValue != null)
                    {
                        string workorderid = cBWorkOrder.SelectedValue.ToString();


                        string getWorkOrderQuan = @"
                        SELECT TargetQuantity 
                        FROM WorkOrders 
                        WHERE WorkOrderID = @WorkOrderID;";

                        try
                        {
                            using (SqlCommand cmd = new SqlCommand(getWorkOrderQuan, cn))
                            {
                                cmd.Parameters.AddWithValue("@WorkOrderID", workorderid);
                                using (var reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        int temp = Convert.ToInt32(reader["TargetQuantity"].ToString());
                                        //MessageBox.Show($"指派 {workorderid}, 生產 {temp} ");

                                        remainProductTodoForMachine[machineID] = temp;
                                        targetQuanForWorkOrder[workorderid] = temp;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("發生錯誤: " + ex.Message, "錯誤");
                            return;
                        }



                        string updateMachineWorkId = "UPDATE MachineTools " +
                            $"SET CurrentWorkOrderID = N'{workorderid}', " +
                            $"Status = N'運轉中' WHERE MachineID = N'{machineID}'";

                        try
                        {
                            using (SqlCommand cmd = new SqlCommand(updateMachineWorkId, cn))
                            {
                                int affectRow = cmd.ExecuteNonQuery();
                              
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("發生錯誤: " + ex.Message, "錯誤");
                            return;
                        }
                        

                        string updateWorkOrders = "UPDATE WorkOrders " +
                                       "SET Status = N'運作中'" +
                                        $"WHERE WorkOrderID = '{workorderid}'; ";
                        
                        try
                        {
                            using (SqlCommand cmd = new SqlCommand(updateWorkOrders, cn))
                            {
                                int affectRow = cmd.ExecuteNonQuery();
                              
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("發生更新錯誤: " + ex.Message, "錯誤");
                            return;
                        }


                        string insertLogQuery = @"
                        INSERT INTO ProductionLog (MachineID, WorkOrderID, LogType, TimeStamp, GoodCount, BadCount) 
                        VALUES (@MachineID, @WorkOrderID, N'開始生產', GETDATE(), 0, 0);";

                        SqlTransaction transaction = cn.BeginTransaction();

                        try
                        {

                            using (SqlCommand cmdInsertLog = new SqlCommand(insertLogQuery, cn, transaction))
                            {
                                cmdInsertLog.Parameters.AddWithValue("@MachineID", machineID);
                                cmdInsertLog.Parameters.AddWithValue("@WorkOrderID", workorderid);
                                cmdInsertLog.ExecuteNonQuery();
                            }

                            transaction.Commit();

                        }
                        catch (Exception ex)
                        {
                            // 發生錯誤，回滾事務
                            transaction.Rollback();
                            MessageBox.Show($"解除警報失敗 (已回滾): {ex.Message}", "錯誤",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }



                        showMachineToolsTable();
                        showWaitToStartOrder();
                    }
                }
            }
            else
            {
                MessageBox.Show("請先點選 DataGridView 中的一筆資料。", "提示",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDisableAlarm_Click(object sender, EventArgs e)
        {
            if (this.dgvMachine.SelectedRows.Count > 0)
            {
               
                DataGridViewRow selectedRow = this.dgvMachine.SelectedRows[0];

                int rowIndex         = selectedRow.Index;
                string machineID     = selectedRow.Cells["MachineID"].Value.ToString();
                string machineStatus = selectedRow.Cells["Status"].Value.ToString();
                string workOrderID   = selectedRow.Cells["CurrentWorkOrderID"].Value.ToString();
                if(machineStatus == "警報")
                {
                    string updateLatestAlarmQuery = @"
                    WITH LatestAlarm AS (
                        SELECT TOP 1 
                            AlarmLogID 
                        FROM AlarmLog 
                        WHERE MachineID = @MachineID 
                          AND Status = N'Active' 
                        ORDER BY AlarmStartTime DESC
                    )
                    -- 2. 針對該筆紀錄執行 UPDATE
                    UPDATE AlarmLog 
                    SET AlarmEndTime = GETDATE(), 
                        Status = N'Cleared' 
                    WHERE AlarmLogID IN (SELECT AlarmLogID FROM LatestAlarm);";


                   
                    using (SqlCommand cmd = new SqlCommand(updateLatestAlarmQuery, cn))
                    {
                        // 使用參數化查詢
                        cmd.Parameters.AddWithValue("@MachineID", machineID);

                        try
                        {
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                               
                            }
                            else
                            {
                                MessageBox.Show($"機台 {machineID} 沒有需要解除的 Active 警報。", "提示");
                                return;
                            }
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show($"更新 AlarmLog 失敗: {ex.Message}", "資料庫錯誤");
                            return;
                        }
                    }
                    

                    // iii. 更新 MachineTools 的 SQL：Status = "運轉中"
                    string updateMachineQuery = @"
                    UPDATE MachineTools 
                    SET Status = N'運轉中' 
                    WHERE MachineID = @MachineID;";

                    // iv. 新增 ProductionLog 的 SQL：LogType="警報解除"
                    string insertLogQuery = @"
                    INSERT INTO ProductionLog (MachineID, WorkOrderID, LogType, TimeStamp, GoodCount, BadCount) 
                    VALUES (@MachineID, @WorkOrderID, N'警報解除', GETDATE(), 0, 0);";

                  
                    SqlTransaction transaction = cn.BeginTransaction();

                    try
                    {
              
                        // ------------------- iii. 步驟：更新 MachineTools -------------------
                        using (SqlCommand cmdUpdateMachine = new SqlCommand(updateMachineQuery, cn, transaction))
                        {
                            cmdUpdateMachine.Parameters.AddWithValue("@MachineID", machineID);
                            cmdUpdateMachine.ExecuteNonQuery();
                        }

                        // ------------------- iv. 步驟：新增 ProductionLog -------------------
                        using (SqlCommand cmdInsertLog = new SqlCommand(insertLogQuery, cn, transaction))
                        {
                            cmdInsertLog.Parameters.AddWithValue("@MachineID", machineID);
                            cmdInsertLog.Parameters.AddWithValue("@WorkOrderID", workOrderID);
                            cmdInsertLog.ExecuteNonQuery();
                        }

                        // 所有操作成功，提交事務
                        transaction.Commit();
                           
                    }
                    catch (Exception ex)
                    {
                        // 發生錯誤，回滾事務
                        transaction.Rollback();
                        MessageBox.Show($"解除警報失敗 (已回滾): {ex.Message}", "錯誤",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }
            }
            showMachineToolsTable();
            this.btnDisableAlarm.Visible = false;
            rTBAlarm.Text = "";
        }
        public void ClearAlarmAndResumeProduction(int alarmLogID)
        {
            string getIDsQuery = @"
            SELECT MachineID, WorkOrderID 
            FROM AlarmLog 
            WHERE AlarmLogID = @AlarmLogID;";

            string updateAlarmQuery = @"
            UPDATE AlarmLog 
            SET AlarmEndTime = GETDATE(), 
                Status = N'Cleared' 
            WHERE AlarmLogID = @AlarmLogID AND Status = N'Active';";

            string updateMachineQuery = @"
            UPDATE MachineTools 
            SET Status = N'運轉中' 
            WHERE MachineID = @MachineID;";

            string insertLogQuery = @"
            INSERT INTO ProductionLog (MachineID, WorkOrderID, LogType, LogTime) 
            VALUES (@MachineID, @WorkOrderID, N'警報解除', GETDATE());";

            string machineID = null;
            string workOrderID = null;

           
            SqlTransaction transaction = cn.BeginTransaction();

            try
            {
                using (SqlCommand cmdGetIDs = new SqlCommand(getIDsQuery, cn, transaction))
                {
                    cmdGetIDs.Parameters.AddWithValue("@AlarmLogID", alarmLogID);
                    using (var reader = cmdGetIDs.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            machineID = reader["MachineID"].ToString();
                            workOrderID = reader["WorkOrderID"].ToString();
                        }
                    }
                    if (machineID == null || workOrderID == null)
                    {
                        throw new Exception("找不到對應的警報記錄或缺少 MachineID/WorkOrderID。");
                    }
                }

                using (SqlCommand cmdUpdateAlarm = new SqlCommand(updateAlarmQuery, cn, transaction))
                {
                    cmdUpdateAlarm.Parameters.AddWithValue("@AlarmLogID", alarmLogID);
                    if (cmdUpdateAlarm.ExecuteNonQuery() == 0)
                    {
                        throw new Exception("警報狀態更新失敗，可能該警報已是 Cleared 狀態。");
                    }
                }

                using (SqlCommand cmdUpdateMachine = new SqlCommand(updateMachineQuery, cn, transaction))
                {
                    cmdUpdateMachine.Parameters.AddWithValue("@MachineID", machineID);
                    cmdUpdateMachine.ExecuteNonQuery();
                }

                using (SqlCommand cmdInsertLog = new SqlCommand(insertLogQuery, cn, transaction))
                {
                    cmdInsertLog.Parameters.AddWithValue("@MachineID", machineID);
                    cmdInsertLog.Parameters.AddWithValue("@WorkOrderID", workOrderID);
                    cmdInsertLog.ExecuteNonQuery();
                }

                transaction.Commit();
                MessageBox.Show($"警報 ID: {alarmLogID} 已解除，機台 {machineID} 恢復生產。", "警報解除成功",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                // 發生錯誤，回滾事務
                transaction.Rollback();
                MessageBox.Show($"解除警報失敗 (已回滾): {ex.Message}", "錯誤",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        private void dgvMachine_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvMachine.Rows.Count)
                return;
            string machineID = dgvMachine.Rows[e.RowIndex].Cells["MachineID"].Value.ToString();

            MachineTool selectedMachine = GetMachineTools(machineID);
            if (selectedMachine != null)
                showMachineToolsDetail(selectedMachine);
        }

        private void btnSimulate_Click(object sender, EventArgs e)
        {
            
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetSystemState();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            List<MachineTool> machineTools = GetMachineToolsList();
            foreach (MachineTool machineTool in machineTools)
            {
                if (machineTool.Status == "運轉中")
                {
                    if (remainProductTodoForMachine.ContainsKey(machineTool.MachineID) &&
                        remainProductTodoForMachine[machineTool.MachineID] > 0)
                    {
                        int isalarm = random.Next(1, 101);
                        if(isalarm <= 3)
                        {
                            alarmHappen(machineTool.MachineID, machineTool.CurrentWorkOrderID, targetQuanForWorkOrder[machineTool.CurrentWorkOrderID] - remainProductTodoForMachine[machineTool.MachineID]);
                            continue;
                        }

                        int goodCount = 0, badCount = 0;    
                        int quality = random.Next(1, 101);
                        if(quality <= 95)
                        {
                            goodCount++;
                        }
                        else
                        {
                            badCount++;   
                        }
                        // add new product log

                        string insertProductionLogQuery = @"
                        INSERT INTO ProductionLog (MachineID, WorkOrderID, Timestamp, LogType, GoodCount, BadCount)
                        VALUES (@MachineID, @WorkOrderID, GETDATE(), N'生產', @GoodCount, @BadCount);";

                        SqlTransaction transaction = cn.BeginTransaction();

                        try
                        {

                            using (SqlCommand cmd2 = new SqlCommand(insertProductionLogQuery, cn, transaction))
                            {
                                cmd2.Parameters.AddWithValue("@MachineID", machineTool.MachineID);
                                cmd2.Parameters.AddWithValue("@WorkOrderID", machineTool.CurrentWorkOrderID);
                                cmd2.Parameters.AddWithValue("@GoodCount", goodCount);
                                cmd2.Parameters.AddWithValue("@BadCount", badCount);
                                cmd2.ExecuteNonQuery();
                            }
                            transaction.Commit();
                        }
                        catch (SqlException ex)
                        {
                            // 4. 任何步驟失敗，回滾事務
                            if (transaction != null)
                            {
                                try
                                {
                                    transaction.Rollback();
                                    MessageBox.Show($"警報記錄失敗，所有變更已還原: {ex.Message}", "資料庫錯誤 - 事務已回滾");
                                }
                                catch (Exception rollbackEx)
                                {
                                    // 處理回滾失敗的罕見情況
                                    MessageBox.Show($"警報記錄失敗，且回滾時發生錯誤: {rollbackEx.Message}", "嚴重錯誤");
                                }
                            }
                            else
                            {
                                MessageBox.Show($"警報記錄失敗: {ex.Message}", "資料庫錯誤");
                            }
                        }


                        remainProductTodoForMachine[machineTool.MachineID] -= 1;
                        if (remainProductTodoForMachine[machineTool.MachineID] == 0)
                        {
                            remainProductTodoForMachine.Remove(machineTool.MachineID);
                            workOrderFinish(machineTool.MachineID, machineTool.CurrentWorkOrderID);

                        }
                    }
                }
            }

            showMachineToolsTable();
        }

        private void workOrderFinish(string machineID, string workOrderID)
        {
            string updateMachineStatusQuery = @"
            UPDATE MachineTools 
            SET Status = N'閒置', CurrentWorkOrderID = NULL
            WHERE MachineID = @MachineID;";

            string updateWorkOrder = @"
            UPDATE WorkOrders 
            SET Status = N'已完成' 
            WHERE WorkOrderID = @WorkOrderID;";


            string insertProductionLogQuery = @"
            INSERT INTO ProductionLog (MachineID, WorkOrderID, Timestamp, LogType, GoodCount, BadCount)
            VALUES (@MachineID, @WorkOrderID, GETDATE(), N'完成生產', 0, 0);";

           

            SqlTransaction transaction = null;

            try
            {
                // 2. 開始事務 (Transaction)
                transaction = cn.BeginTransaction();

                using (SqlCommand cmd1 = new SqlCommand(updateMachineStatusQuery, cn, transaction))
                {
                    cmd1.Parameters.AddWithValue("@MachineID", machineID);
                    cmd1.ExecuteNonQuery();
                }
                using (SqlCommand cmd2 = new SqlCommand(insertProductionLogQuery, cn, transaction))

                {
                    cmd2.Parameters.AddWithValue("@MachineID", machineID);
                    cmd2.Parameters.AddWithValue("@WorkOrderID", workOrderID);
                    cmd2.ExecuteNonQuery();
                }

                using (SqlCommand cmd3 = new SqlCommand(updateWorkOrder, cn, transaction))
                {
                    cmd3.Parameters.AddWithValue("@WorkOrderID", workOrderID);
                    cmd3.ExecuteNonQuery();
                }

                transaction.Commit();
                //MessageBox.Show($"機台 {machineID} 成功記錄警報並更新狀態。", "操作成功");
            }
            catch (SqlException ex)
            {
                // 4. 任何步驟失敗，回滾事務
                if (transaction != null)
                {
                    try
                    {
                        transaction.Rollback();
                        MessageBox.Show($"警報記錄失敗，所有變更已還原: {ex.Message}", "資料庫錯誤 - 事務已回滾");
                    }
                    catch (Exception rollbackEx)
                    {
                        // 處理回滾失敗的罕見情況
                        MessageBox.Show($"警報記錄失敗，且回滾時發生錯誤: {rollbackEx.Message}", "嚴重錯誤");
                    }
                }
                else
                {
                    MessageBox.Show($"警報記錄失敗: {ex.Message}", "資料庫錯誤");
                }
            }

        }


        private void alarmHappen(string machineID, string workOrderID, int ProductionCount)
        {
            string updateMachineStatusQuery = @"
            UPDATE MachineTools SET Status = N'警報' 
            WHERE MachineID = @MachineID;";

            // SQL 命令 2: 插入 ProductionLog (LogType="發生警報")
            string insertProductionLogQuery = @"
            INSERT INTO ProductionLog (MachineID, WorkOrderID, Timestamp, LogType, GoodCount, BadCount)
            VALUES (@MachineID, @WorkOrderID, GETDATE(), N'發生警報', 0, 0);";

            // SQL 命令 3: 插入 AlarmLog (Status="Active")
            string insertAlarmLogQuery = @"
            INSERT INTO AlarmLog (MachineID, WorkOrderID, AlarmStartTime, Status, ProductionCountAtAlarm)
            VALUES (@MachineID, @WorkOrderID, GETDATE(), N'Active', @ProductionCountAtAlarm);";

            // 1. 建立連線物件
         
            SqlTransaction transaction = null;

            try
            {
                // 2. 開始事務 (Transaction)
                transaction = cn.BeginTransaction();

                // --- 執行命令 1: 更新 MachineTools 狀態 ---
                using (SqlCommand cmd1 = new SqlCommand(updateMachineStatusQuery, cn, transaction))
                {
                    cmd1.Parameters.AddWithValue("@MachineID", machineID);
                    cmd1.ExecuteNonQuery();
                }

                // --- 執行命令 2: 插入 ProductionLog ---
                using (SqlCommand cmd2 = new SqlCommand(insertProductionLogQuery, cn, transaction))
                {
                    cmd2.Parameters.AddWithValue("@MachineID", machineID);
                    cmd2.Parameters.AddWithValue("@WorkOrderID", workOrderID);
                    cmd2.ExecuteNonQuery();
                }

                // --- 執行命令 3: 插入 AlarmLog ---
                using (SqlCommand cmd3 = new SqlCommand(insertAlarmLogQuery, cn, transaction))
                {
                    cmd3.Parameters.AddWithValue("@MachineID", machineID);
                    cmd3.Parameters.AddWithValue("@WorkOrderID", workOrderID);
                    cmd3.Parameters.AddWithValue("@ProductionCountAtAlarm", ProductionCount);
                    cmd3.ExecuteNonQuery();
                }

                // 3. 所有步驟都成功，提交事務
                transaction.Commit();
               // MessageBox.Show($"機台 {machineID} 成功記錄警報並更新狀態。", "操作成功");
            }
            catch (SqlException ex)
            {
                // 4. 任何步驟失敗，回滾事務
                if (transaction != null)
                {
                    try
                    {
                        transaction.Rollback();
                        MessageBox.Show($"警報記錄失敗，所有變更已還原: {ex.Message}", "資料庫錯誤 - 事務已回滾");
                    }
                    catch (Exception rollbackEx)
                    {
                        // 處理回滾失敗的罕見情況
                        MessageBox.Show($"警報記錄失敗，且回滾時發生錯誤: {rollbackEx.Message}", "嚴重錯誤");
                    }
                }
                else
                {
                    MessageBox.Show($"警報記錄失敗: {ex.Message}", "資料庫錯誤");
                }
            }
            
        }

        private void btnCheckWorkOrder_Click(object sender, EventArgs e)
        {
            string selectQuery = "SELECT * FROM WorkOrders;";
            try
            {
                SqlCommand cmd = new SqlCommand(selectQuery, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvDebug.DataSource = dt;   
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"載入表格 WorkOrders 失敗: {ex.Message}", "資料庫錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCheckAlarm_Click(object sender, EventArgs e)
        {
            string selectQuery = "SELECT * FROM AlarmLog;";
            try
            {
                SqlCommand cmd = new SqlCommand(selectQuery, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvDebug.DataSource = dt;
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"載入表格 AlarmLog 失敗: {ex.Message}", "資料庫錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCheckProductionLog_Click(object sender, EventArgs e)
        {
            string selectQuery = "SELECT * FROM ProductionLog;";
            try
            {
                SqlCommand cmd = new SqlCommand(selectQuery, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvDebug.DataSource = dt;
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"載入表格 ProductionLog 失敗: {ex.Message}", "資料庫錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    
    }
}
