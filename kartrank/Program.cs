using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kartrank.BusinessLayer;

namespace kartrank
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                RaceProcess rp = null;
                if (args.Length == 0)
                    rp = new RaceProcess();
                else if (args[0] == "?" || args[0].StartsWith("help", StringComparison.InvariantCultureIgnoreCase) || args[0].StartsWith("-h", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine("usage: kartrank [drive:][path][filename]");
                    Console.WriteLine();
                    Console.WriteLine("[drive:][path][filename] Specifies drive, directory, and/or files to process.");
                }
                else
                    rp = new RaceProcess(args[0]);

                if (rp != null)
                {
                    var raceResults = rp.RaceResult();
                    Console.WriteLine("Race Results");
                    Console.WriteLine("Position PilotId PilotName               CompletedLaps TotalRaceTime");
                    foreach (var item in raceResults)
                    {
                        Console.WriteLine("{0,-8} {1,-7} {2,-23} {3,-13} {4,-13}", item.Position, item.PilotId, item.PilotName, item.CompletedLaps, item.TotalRaceTime.ToString(@"hh\:mm\:ss\.fff"));
                    }

                    Console.WriteLine();

                    var raceStats = rp.RaceStats();
                    Console.WriteLine("Race Optional Statistics");
                    Console.WriteLine("Pilot                   BestLap       AverageSpeed FirstPlaceTimeDiff");
                    foreach (var item in raceStats)
                    {
                        Console.WriteLine("{0,-23} {1,-13} {2,-12} {3,-19}", item.Pilot, item.BestLap.ToString(@"hh\:mm\:ss\.fff"), item.AverageSpeed.ToString("0.000"), item.FirstPlaceTimeDiff.HasValue ? item.FirstPlaceTimeDiff.Value.ToString(@"hh\:mm\:ss\.fff") : "-");
                    }

                    Console.WriteLine();

                    var overallBestLap = rp.OverallBestLap();

                    Console.WriteLine("Overall Best Lap");
                    Console.WriteLine(overallBestLap.ToString(@"hh\:mm\:ss\.fff"));
                }

                Console.ReadLine();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
