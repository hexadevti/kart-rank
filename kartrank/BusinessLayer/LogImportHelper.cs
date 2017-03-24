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
        private const string LOG_ENTRY_FILE_PATH = Path.Combine(Directory.GetCurrentDirectory(), "kart-rank.log");
        private const CultureInfo PT_BR_CULTURE_INFO = CultureInfo.GetCultureInfo("pt-BR");


        public static List<LogEntry> Import()
        {
            return new List<LogEntry>();
        }

        public static LogEntry DeserializeLine(string logEntryLine)
        {
            try
            {
                LogEntry le = new LogEntry();
                
                var parts = logEntryLine.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                le.Time = DateTime.MinValue + TimeSpan.ParseExact(parts[0], @"hh\:mm\:ss\.fff", PT_BR_CULTURE_INFO);
                le.Pilot = String.Concat(parts[1], " ", parts[2], " ", parts[3]);
                le.Lap = Int32.Parse(parts[4]);
                le.LapTime = TimeSpan.ParseExact(parts[5], @"m\:ss\.fff", PT_BR_CULTURE_INFO);
                le.AvarageLapSpeed = Decimal.Parse(parts[6], PT_BR_CULTURE_INFO);

                return le;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deserializing line: '" + logEntryLine + "'");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw ex;
            }

        }
             
    }
}
