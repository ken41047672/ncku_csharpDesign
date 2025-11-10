using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C__WF_P76144176
{
    public class CompletedWOAnalysis
    {
        public string WorkOrderID { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public double ActualTimeMinutes { get; set; }
        public int TargetTimeMinutes { get; set; } // 來自 WorkOrders.CostTime
        public double TimeDifference { get; set; } // Actual - Target
        public double YieldRate { get; set; }
    }

}
