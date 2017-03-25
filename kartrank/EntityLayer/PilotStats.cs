using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kartrank.EntityLayer
{
    public class PilotStats
    {
        public string Pilot { get; set; }
        public TimeSpan BestLap { get; set; }
        public Decimal AverageSpeed { get; set; }
        public TimeSpan? FirstPlaceTimeDiff { get; set; }

    }
}
