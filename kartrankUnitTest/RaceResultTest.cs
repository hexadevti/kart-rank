using System;
using kartrank.BusinessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace kartrankUnitTest
{
    [TestClass]
    public class RaceProcessTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RaceProcess_Blank_File_Name()
        {
            RaceProcess rp = new RaceProcess(null);
        }

        [TestMethod]
        public void RaceProcess_Internal_file()
        {
            RaceProcess rp = new RaceProcess();
            var raceResults = rp.RaceResult();

            Assert.IsTrue(raceResults.Any());
        }
    }
}
