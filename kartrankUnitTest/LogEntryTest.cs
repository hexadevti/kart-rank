using System;
using kartrank.BusinessLayer;
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
        [ExpectedException(typeof(ArgumentException))]
        public void LogEntry_Null_Or_Empty_Test()
        {
            LogEntry le = LogImportHelper.DeserializeLine(LogEntry_Empty);
            Assert.Fail("Test must throw ArgumentException.");
        }

        [TestMethod]
        public void LogEntry_Valid_Line_Test()
        {
            // Arrange
            LogEntry le = new LogEntry();
            le.Time = DateTime.MinValue.AddHours(23).AddMinutes(49).AddSeconds(8).AddMilliseconds(277);
            le.Pilot = "038 – F.MASSA";
            le.Lap = 1;
            le.LapTime = new TimeSpan(0, 0, 1, 2, 852);
            le.AvarageLapSpeed = new Decimal(44.275);

            LogEntry result = null;

            //Act
            try
            {
                result = LogImportHelper.DeserializeLine(LogEntry_ValidLine);
            }
            catch (Exception)
            {
                Assert.Fail("Test must not throw Exception.");
            }

            //Assert
            Assert.AreNotEqual(result, null, "Result must not be null.");
            Assert.AreEqual(le.Time, result.Time, "Time property not match.");
            Assert.AreEqual(le.Pilot, result.Pilot, "Pilot property not match.");
            Assert.AreEqual(le.Lap, result.Lap, "Lap property not match.");
            Assert.AreEqual(le.LapTime, result.LapTime, "LapTime property not match.");
            Assert.AreEqual(le.AvarageLapSpeed, result.AvarageLapSpeed, "AvarageLapSpeed property not match.");
            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LogEntry_Invalid_Date_Test()
        {
            LogEntry le = LogImportHelper.DeserializeLine(LogEntry_Invalid_Date);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LogEntry_Invalid_Name_Test()
        {
            LogEntry le = LogImportHelper.DeserializeLine(LogEntry_Invalid_Name);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LogEntry_Invalid_Lap_Test()
        {
            LogEntry le = LogImportHelper.DeserializeLine(LogEntry_Invalid_Lap);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LogEntry_Invalid_Lap_Time_Test()
        {
            LogEntry le = LogImportHelper.DeserializeLine(LogEntry_Invalid_Lap_Time);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LogEntry_Invalid_AverageLap_Time_Test()
        {
            LogEntry le = LogImportHelper.DeserializeLine(LogEntry_Invalid_AverageLap_Time);
        }

    }
}
