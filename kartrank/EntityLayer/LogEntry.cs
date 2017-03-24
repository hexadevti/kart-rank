using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kartrank.EntityLayer
{
    public class LogEntry
    {
        public DateTime Time { get; set; }
        public String Pilot { get; set; }
        public Int32 Lap { get; set; }
        public TimeSpan LapTime { get; set; }
        public Decimal AvarageLapSpeed { get; set; }
    }
}
