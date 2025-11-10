using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C__WF_P76144176
{
    public class LongestDowntimeEntry
    {
        public DateTime AlarmStartTime { get; set; }
        public string AlarmEndTimeDisplay { get; set; } // 可能是日期時間或 "進行中"
        public double DurationMinutes { get; set; }

    }
}
