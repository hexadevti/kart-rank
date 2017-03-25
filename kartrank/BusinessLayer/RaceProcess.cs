using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kartrank.EntityLayer;

namespace kartrank.BusinessLayer
{
    public class RaceProcess
    {
        private string DEFAULT_LOG_ENTRY_FILE_PATH = Path.Combine(Directory.GetCurrentDirectory(), "kart-rank.log");
        private string _logFile = null;

        public RaceProcess()
        {
            _logFile = DEFAULT_LOG_ENTRY_FILE_PATH;
        }

        public RaceProcess(string logEntryFilePath)
        {
            if (String.IsNullOrEmpty(logEntryFilePath))
                throw new ArgumentNullException("LogEntryFilePath must have a valid value.");

            FileInfo fi = new FileInfo(logEntryFilePath);

            if (!fi.Exists)
                throw new ArgumentException(logEntryFilePath + " does not exists.");

            

            _logFile = logEntryFilePath;
        }

        public List<RaceResultItem> RaceResult()
        {

            List<LogEntry> LogEntries = LogImportHelper.ImportLogFile(_logFile); // return original information
                                //.OrderBy(x => x.Time).ToList(); // Assure the correct order os logs

            ushort position = 1;
            List<RaceResultItem> result = LogEntries
                                            .GroupBy(x => x.Pilot.Split(' ')[0]) // using only pilot id to avoid wrong name in log
                                            .Select(x => new RaceResultItem
                                            {
                                                PilotId = UInt16.Parse(x.First().Pilot.Split(' ')[0]),
                                                PilotName = x.First().Pilot.Split(' ')[2],
                                                CompletedLaps = Convert.ToUInt16(x.Count()),
                                                TotalRaceTime = TimeSpan.FromTicks(x.Sum(y => y.LapTime.Ticks))
                                            })
                                            .OrderByDescending(x => x.CompletedLaps)
                                            .ThenBy(x => x.TotalRaceTime)
                                            .Select(x => { x.Position = position++; return x; })
                                            .ToList();

            return result;
        }

        public List<PilotStats> RaceStats()
        {

            List<LogEntry> LogEntries = LogImportHelper.ImportLogFile(_logFile); // return original information
                                //.OrderBy(x => x.Time).ToList(); // Assure the correct order of logs entries

            TimeSpan RaceFinishTime = LogEntries
                                            .GroupBy(x => x.Pilot.Split(' ')[0]) // using only pilot id to avoid wrong name in log
                                            .Select(x => new RaceResultItem
                                            {
                                                TotalRaceTime = TimeSpan.FromTicks(x.Sum(y => y.LapTime.Ticks))
                                            })
                                            .OrderByDescending(x => x.CompletedLaps)
                                            .ThenBy(x => x.TotalRaceTime)
                                            .First().TotalRaceTime; // return the time of first pilot on fourth lap


            List<PilotStats> stats = LogEntries
                                        .GroupBy(x => x.Pilot.Split(' ')[0])
                                        .Select(x => new PilotStats
                                        {
                                            Pilot = x.First().Pilot,
                                            BestLap = x.Min(y => y.LapTime),
                                            AverageSpeed = x.Average(y => y.AvarageLapSpeed),
                                            FirstPlaceTimeDiff = Convert.ToUInt16(x.Count()) == 4 ? (TimeSpan?)TimeSpan.FromTicks(x.Sum(y => y.LapTime.Ticks)) - RaceFinishTime : null,
                                        })
                                        .OrderBy(x => !x.FirstPlaceTimeDiff.HasValue)
                                        .ThenBy(x => x.FirstPlaceTimeDiff)
                                        .ToList();


            return stats;
        }

        public TimeSpan OverallBestLap()
        {
            List<LogEntry> LogEntries = LogImportHelper.ImportLogFile(_logFile); // return original information

            TimeSpan bestLap = LogEntries.Min(x => x.LapTime);
            return bestLap;
        }
            

    }
}
