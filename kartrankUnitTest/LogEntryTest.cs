using System;
using kartrank.EntityLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace kartrankUnitTest
{
    [TestClass]
    public class LogEntryTest
    {
        string LogEntry_Empty = "";
        string LogEntry_ValidLine = "23:49:08.277 	  038 – F.MASSA		  	  1		1:02.852 			44,275";
        string LogEntry_Invalid_Date = ":49:08.277 	  038 – F.MASSA		  	  1		1:02.852 			44,275";
        string LogEntry_Invalid_Name = "23:49:08.277 	  		  	  1		1:02.852 			44,275";
        string LogEntry_Invalid_Lap = "23:49:08.277 	  038 – F.MASSA		  	  X		1:02.852 			44,275";
        string LogEntry_Invalid_Lap_Time = "23:49:08.277 	  038 – F.MASSA		  	  1		X:02.852 			44,275";
        string LogEntry_Invalid_AverageLap_Time = "23:49:08.277 	  038 – F.MASSA		  	  1		X:02.852 			XX,275";

        [TestMethod]
        public void LogEntry_Null_Or_Empty()
        {
            LogEntry le = new LogEntry();

        }
    }
}
