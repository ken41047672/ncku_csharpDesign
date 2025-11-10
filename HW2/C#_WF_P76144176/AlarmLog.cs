using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C__WF_P76144176
{
    public class AlarmLog
    {
        public int AlarmLogID { get; set; }

        // Foreign Keys
        public string MachineID { get; set; }
        public string WorkOrderID { get; set; }

        // 允許 NULL
        public int? ProductionCountAtAlarm { get; set; }

        public DateTime AlarmStartTime { get; set; }

        // 允許 NULL
        public DateTime? AlarmEndTime { get; set; }

        public string Status { get; set; }
    }
}
