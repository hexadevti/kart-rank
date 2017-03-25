using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kartrank.BusinessLayer
{
    public class RaceResultItem
    {
        public UInt16 Position { get; set; }
        public UInt16 PilotId { get; set; }
        public String PilotName { get; set; }
        public UInt16 CompletedLaps { get; set; }
        public TimeSpan TotalRaceTime { get; set; }
    }
}
