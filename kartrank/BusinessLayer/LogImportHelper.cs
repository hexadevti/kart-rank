using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kartrank.EntityLayer;

namespace kartrank.BusinessLayer
{
    public static class LogImportHelper
    {
        
        private static CultureInfo PT_BR_CULTURE_INFO = CultureInfo.GetCultureInfo("pt-BR");

        /// <summary>
        /// Imports the log entry file
        /// </summary>
        /// <param name="logEntryFilePath"></param>
        /// <returns></returns>
        public static List<LogEntry> ImportLogFile(string logEntryFilePath)
        {
            List<LogEntry> logResult = new List<LogEntry>();
            using (StreamReader sr = new StreamReader(logEntryFilePath))
            {
                sr.ReadLine(); // Discart header
                while (!sr.EndOfStream)
                {
                    logResult.Add(DeserializeLine(sr.ReadLine()));
                }
            }

            return logResult;
        }
                
        /// <summary>
        /// Internal Method to Deserialize Log File Entry
        /// </summary>
        /// <param name="logEntryLine"></param>
        /// <returns></returns>
        internal static LogEntry DeserializeLine(string logEntryLine)
        {
            if (String.IsNullOrEmpty(logEntryLine))
                throw new ArgumentException("LogEntryLine can't be null or empty string.");

            LogEntry le = new LogEntry();
            var parts = logEntryLine.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 7)
                throw new ArgumentException("Wrong number of arguments on logEntry line");

            TimeSpan TimeParsed;
            if (!TimeSpan.TryParseExact(parts[0], @"hh\:mm\:ss\.fff", PT_BR_CULTURE_INFO, out TimeParsed))
                throw new ArgumentException("Cannot parse Time!");
            le.Time = DateTime.MinValue + TimeParsed;

            le.Pilot = String.Concat(parts[1], " ", parts[2], " ", parts[3]);

            Int32 LapParsed;
            if (!Int32.TryParse(parts[4], out LapParsed))
                throw new ArgumentException("Cannot parse Lap!");
            le.Lap = LapParsed;

            TimeSpan LapTimeParsed;
            if (!TimeSpan.TryParseExact(parts[5], @"m\:ss\.fff", PT_BR_CULTURE_INFO, out LapTimeParsed))
                throw new ArgumentException("Cannot parse LapTime!");
            le.LapTime = LapTimeParsed;

            Decimal AvarageLapSpeedParsed;
            if (!Decimal.TryParse(parts[6], NumberStyles.Float, PT_BR_CULTURE_INFO, out AvarageLapSpeedParsed))
                throw new ArgumentException("Cannot parse Lap!");
            le.AvarageLapSpeed = AvarageLapSpeedParsed;

            return le;

        }
             
    }
}
