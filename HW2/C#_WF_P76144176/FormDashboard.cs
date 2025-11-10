using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C__WF_P76144176
{
    
    public partial class FormDashboard : Form
    {
        public SqlConnection cn;
        //private string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" + "AttachDbFilename=|DataDirectory|myDB.mdf;" + "Integrated Security=True";

        public FormDashboard(SqlConnection cn)
        {
            InitializeComponent();
            this.cn = cn;
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            string machineID = null;
            if (cmbMachines.SelectedValue != null)
            {
                machineID = cmbMachines.SelectedValue.ToString();
            }
            List<ProductionLog> productionLogs = GetProductionLogListByMachineID(machineID);
            List<AlarmLog>      alarmLogs      = GetAlarmLogsListByMachineID(machineID);

            var t = productionLogs.Where(l => l.LogType == "生產");

            int totalGood = t.Sum(l => l.GoodCount);
            int totalBad = t.Sum(l => l.BadCount);

            double totalProduction = totalGood + totalBad;
            double yieldRate = (totalProduction > 0)
                ? (double)totalGood / totalProduction
                : 0.0;

            int alarmCount = alarmLogs.Count;
            double totalDowntimeMinutes = CalculateTotalDowntime(alarmLogs);

            List<LongestDowntimeEntry> longestDowntimeList = GetTop3LongestDowntime(alarmLogs);

            List<CompletedWOAnalysis> completedWOs = AnalyzeCompletedWorkOrders(machineID, productionLogs);

            lblTotalGood.Text = $"GoodCount 總和          : {totalGood.ToString("N0")}";
            lblTotalBad.Text = $"GoodCount 總和           : {totalBad.ToString("N0")}";
            lblYieldRate.Text = $"歷史總體良率             : {yieldRate.ToString("P2")}";
            lblAlarmCount.Text = $"AlarmLog 查詢結果的筆數 : {alarmCount.ToString()}";
            lblTotalDowntime.Text = $"總停機時間 (分鐘)    ： {totalDowntimeMinutes.ToString("N2")}"; 

            dgvLongestDowntime.DataSource = longestDowntimeList;
            dgvCompletedWOs.DataSource = completedWOs;
        }
        public List<LongestDowntimeEntry> GetTop3LongestDowntime(List<AlarmLog> alarmLogs)
        {
            var downtimeData = alarmLogs.Select(log =>
            {
                DateTime endTime = log.AlarmEndTime ?? DateTime.Now;
                double durationMinutes = endTime.Subtract(log.AlarmStartTime).TotalMinutes;

                return new LongestDowntimeEntry
                {
                    AlarmStartTime = log.AlarmStartTime,
                    AlarmEndTimeDisplay = log.AlarmEndTime.HasValue ? log.AlarmEndTime.Value.ToString() : "進行中",
                    DurationMinutes = durationMinutes
                };
            })
            .OrderByDescending(d => d.DurationMinutes) // 依持續時間降冪排序
            .Take(3) // 選取前 3 筆
            .ToList();

            return downtimeData;
        }

        public List<CompletedWOAnalysis> AnalyzeCompletedWorkOrders(string machineID, List<ProductionLog> allProductionLogs)
        {
            // 1. 找出該機台所有 LogType == "完成生產" 的 WorkOrderID
            var completedWoIDs = allProductionLogs
                .Where(l => l.LogType == "完成生產")
                .Select(l => l.WorkOrderID)
                .Distinct()
                .ToList();

            List<CompletedWOAnalysis> results = new List<CompletedWOAnalysis>();

            foreach (string woID in completedWoIDs)
            {
                // 2. 獲取單一工單的詳細日誌
                var woLogs = allProductionLogs.Where(l => l.WorkOrderID == woID).ToList();

                // 獲取時間戳記
                DateTime startTime = woLogs.First(l => l.LogType == "開始生產").Timestamp;
                DateTime finishTime = woLogs.First(l => l.LogType == "完成生產").Timestamp;
                double actualTimeMinutes = finishTime.Subtract(startTime).TotalMinutes;

                (string productName, int costTime) = GetWorkOrderDetailsFromDB(woID);

                // 查詢生產記錄並計算良率
                var productionOnlyLogs = woLogs.Where(l => l.LogType == "生產");
                int woGood = productionOnlyLogs.Sum(l => l.GoodCount);
                int woBad = productionOnlyLogs.Sum(l => l.BadCount);
                double woYield = (woGood + woBad > 0) ? (double)woGood / (woGood + woBad) : 0.0;

                results.Add(new CompletedWOAnalysis
                {
                    WorkOrderID = woID,
                    ProductName = productName,
                    ActualTimeMinutes = actualTimeMinutes,
                    TargetTimeMinutes = costTime,
                    TimeDifference = actualTimeMinutes - costTime,
                    YieldRate = woYield
                });
            }

            return results;
        }

        private (string ProductName, int CostTime) GetWorkOrderDetailsFromDB(string WorkOrderID)
        {
            string query = @"
            SELECT ProductName, CostTime
            FROM WorkOrders
            WHERE WorkOrderID = @WorkOrderID; ";
            string pname = "";
            int ct = 0;
            using(SqlCommand cmd = new SqlCommand(query, cn))
            {
                cmd.Parameters.AddWithValue("@WorkOrderID", WorkOrderID);

                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ct       = reader.GetInt32(reader.GetOrdinal("CostTime"));
                            pname = reader.GetString(reader.GetOrdinal("ProductName"));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"錯誤: {ex.Message}", "錯誤");
                }
            }
            return (pname, ct);
        }
        public double CalculateTotalDowntime(List<AlarmLog> alarmLogs)
        {
            return alarmLogs.Sum(log =>
            {
                DateTime endTime = log.AlarmEndTime ?? DateTime.Now;
                TimeSpan duration = endTime.Subtract(log.AlarmStartTime);
                return duration.TotalMinutes;
            });
        }
        private void FormDashboard_Load(object sender, EventArgs e)
        {
            List<string> MachineID = new List<string>();



            string query = @"
            SELECT MachineID 
            FROM MachineTools 
            ";
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, cn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    string id = dt.Rows[i]["MachineID"].ToString();
                    MachineID.Add(id);
                }
                cmbMachines.DataSource = MachineID;
                    
            }
            catch (SqlException ex)
            {
                    
                MessageBox.Show($"載入表格 MachineTools 失敗: {ex.Message}", "資料庫錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
      
        }

        private List<ProductionLog> GetProductionLogListByMachineID(string machineID)
        {
            List<ProductionLog> logs = new List<ProductionLog>();

            string query = "SELECT * FROM ProductionLog WHERE MachineID = @MachineID ORDER BY Timestamp DESC;";

           
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                // 參數化查詢
                cmd.Parameters.AddWithValue("@MachineID", machineID);

                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // 逐行讀取資料
                        while (reader.Read())
                        {
                            ProductionLog p = new ProductionLog
                            {
                                LogID = reader.GetInt32(reader.GetOrdinal("LogID")),
                                MachineID = reader.GetString(reader.GetOrdinal("MachineID")),
                                Timestamp = reader.GetDateTime(reader.GetOrdinal("Timestamp")),
                                LogType = reader.GetString(reader.GetOrdinal("LogType")),
                                GoodCount = reader.GetInt32(reader.GetOrdinal("GoodCount")),
                                BadCount = reader.GetInt32(reader.GetOrdinal("BadCount")),

                                WorkOrderID = reader.IsDBNull(reader.GetOrdinal("WorkOrderID"))
                                                ? null
                                                : reader.GetString(reader.GetOrdinal("WorkOrderID"))
                            };
                            logs.Add(p);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"撈取生產日誌失敗: {ex.Message}", "資料庫錯誤");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"資料轉換錯誤: {ex.Message}", "錯誤");
                }
            }
            return logs;
        }
        private List<AlarmLog>      GetAlarmLogsListByMachineID(string machineID)
        {
            List<AlarmLog> logs = new List<AlarmLog>();

            string query = "SELECT * FROM AlarmLog WHERE MachineID = @MachineID;";


            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                // 參數化查詢
                cmd.Parameters.AddWithValue("@MachineID", machineID);

                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AlarmLog a = new AlarmLog
                            {
                                AlarmLogID = reader.GetInt32(reader.GetOrdinal("AlarmLogID")),
                                MachineID = reader.GetString(reader.GetOrdinal("MachineID")),
                                WorkOrderID = reader.GetString(reader.GetOrdinal("WorkOrderID")),
                                ProductionCountAtAlarm = reader.GetInt32(reader.GetOrdinal("ProductionCountAtAlarm")),
                                AlarmStartTime = reader.GetDateTime(reader.GetOrdinal("AlarmStartTime")),
                                AlarmEndTime = reader.IsDBNull(reader.GetOrdinal("AlarmEndTime"))
                                    ? (DateTime?)null
                                    : reader.GetDateTime(reader.GetOrdinal("AlarmEndTime")),

                                Status = reader.GetString(reader.GetOrdinal("Status")),
                            };
                            logs.Add(a);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"撈取生產日誌失敗: {ex.Message}", "資料庫錯誤");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"資料轉換錯誤: {ex.Message}", "錯誤");
                }
            }
            return logs;
        }

        
    }
}
