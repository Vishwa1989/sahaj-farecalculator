using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FareCalculator.Class;
using System.Collections;
using System.Collections.Generic;

namespace FareCalculatorTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Fare_for_Multiple_Travel_In_Same_Day_Same_Zone()
        {
            List<Journey> lstjourney = new List<Journey>();
            lstjourney.Add(MockJourneydata(new DateTime(2022, 2, 4, 09, 15, 00), 1, 1)); // 4th Feb 09.00 A.M Peak time
            lstjourney.Add(MockJourneydata(new DateTime(2022, 2, 4, 10, 00, 00), 1, 1)); // 4th Feb 10.00 A.M Peak time
            lstjourney.Add(MockJourneydata(new DateTime(2022, 2, 4, 03, 15, 00), 1, 1)); // 4th Feb 03.15 P.M not Peak time
            lstjourney.Add(MockJourneydata(new DateTime(2022, 2, 4, 19, 15, 00), 1, 1)); // 4th Feb 07.15 P.M not Peak time
            Fare f = new Fare(lstjourney);
            var far = f.CalculateTravelFare();
            Assert.AreEqual(100, far);
        }
        [TestMethod]
        public void Fare_for_Multiple_Travel_In_Same_Day_Different_Zone()
        {
            List<Journey> lstjourney = new List<Journey>();
            lstjourney.Add(MockJourneydata(new DateTime(2022, 2, 4, 09, 15, 00), 1, 1)); // 4th Feb 09.15 A.M Peak time
            lstjourney.Add(MockJourneydata(new DateTime(2022, 2, 4, 10, 00, 00), 1, 2)); // 4th Feb 10.00 A.M peak time -> Zome 1 to 2
            lstjourney.Add(MockJourneydata(new DateTime(2022, 2, 4, 03, 15, 00), 1, 1)); // 4th Feb 03.15 P.M not Peak time
            lstjourney.Add(MockJourneydata(new DateTime(2022, 2, 4, 19, 15, 00), 1, 1)); // 4th Feb 07.15 P.M not Peak time
            Fare f = new Fare(lstjourney);
            var far = f.CalculateTravelFare();
            Assert.AreEqual(120, far);
        }
        [TestMethod]
        public void Fare_for_Multiple_Travel_In_Week_Same_Zone()
        {
            List<Journey> lstjourney = new List<Journey>();
            lstjourney.Add(MockJourneydata(new DateTime(2022, 1, 31, 09, 15, 00), 1, 1)); // 31st jan 9.15 peak time
            lstjourney.Add(MockJourneydata(new DateTime(2022, 2, 1, 10, 00, 00), 1, 1)); // 1st feb 10.00 A.M peak time
            lstjourney.Add(MockJourneydata(new DateTime(2022, 2, 2, 15, 15, 00), 1, 1)); // 2nd Feb 03.15 p.M not peak time
            lstjourney.Add(MockJourneydata(new DateTime(2022, 2, 2, 17, 15, 00), 1, 1)); // 2nd Feb 5.15 p.M peak time
            lstjourney.Add(MockJourneydata(new DateTime(2022, 2, 3, 19, 15, 00), 1, 1)); // 
            lstjourney.Add(MockJourneydata(new DateTime(2022, 2, 4, 19, 15, 00), 1, 1)); // 
            lstjourney.Add(MockJourneydata(new DateTime(2022, 2, 5, 19, 15, 00), 1, 1)); // 
            lstjourney.Add(MockJourneydata(new DateTime(2022, 2, 6, 19, 15, 00), 1, 1)); // 
            Fare f = new Fare(lstjourney);
            var far = f.CalculateTravelFare();
            Assert.AreEqual(235, far);
        }

        [TestMethod]
        public void Fare_for_Multiple_Travel_In_Week_Different_Zone()
        {
            List<Journey> lstjourney = new List<Journey>();
            lstjourney.Add(MockJourneydata(new DateTime(2022, 1, 31, 09, 15, 00), 1, 1)); // 
            lstjourney.Add(MockJourneydata(new DateTime(2022, 2, 1, 10, 00, 00), 1, 1)); // 4th Feb 10.00 A.M
            lstjourney.Add(MockJourneydata(new DateTime(2022, 2, 2, 15, 15, 00), 1, 2)); // 
            lstjourney.Add(MockJourneydata(new DateTime(2022, 2, 2, 17, 15, 00), 2, 1)); // 
            lstjourney.Add(MockJourneydata(new DateTime(2022, 2, 3, 19, 15, 00), 1, 1)); // 
            lstjourney.Add(MockJourneydata(new DateTime(2022, 2, 4, 19, 15, 00), 1, 1)); // 
            lstjourney.Add(MockJourneydata(new DateTime(2022, 2, 5, 19, 15, 00), 1, 1)); // 
            lstjourney.Add(MockJourneydata(new DateTime(2022, 2, 6, 19, 15, 00), 1, 1)); // 
            Fare f = new Fare(lstjourney);
            var far = f.CalculateTravelFare();
            Assert.AreEqual(245, far);
        }

        [TestMethod]
        public void Fare_for_Multiple_Travel_In_Different_Week_Different_Zone()
        {
            List<Journey> lstjourney = new List<Journey>();

            //Limit not exceeded 
            //30
            //30
            lstjourney.Add(MockJourneydata(new DateTime(2022, 1, 31, 09, 15, 00), 1, 1)); // 31st jan 9.15 peak time
            lstjourney.Add(MockJourneydata(new DateTime(2022, 2, 1, 10, 00, 00), 1, 1)); // 1st Feb 10.00 A.M peak time

            //Limit exceeded
            //120
            lstjourney.Add(MockJourneydata(new DateTime(2022, 2, 2, 11, 00, 00), 1, 2)); //  2 Feb 11.00 A.M peak time
            lstjourney.Add(MockJourneydata(new DateTime(2022, 2, 2, 15, 15, 00), 1, 2)); //  2 Feb 3.15 P.M not a peak time
            lstjourney.Add(MockJourneydata(new DateTime(2022, 2, 2, 16, 15, 00), 1, 2)); //  2 Feb 04.15 P.M peak time
            lstjourney.Add(MockJourneydata(new DateTime(2022, 2, 2, 17, 15, 00), 2, 2)); //  2 Feb 05.15 P.M peak time
            lstjourney.Add(MockJourneydata(new DateTime(2022, 2, 2, 17, 30, 00), 2, 1)); //  2 Feb 05.30 P.M peak time

            //Limit not exceeded
            //30
            //30
            //30
            //30
            lstjourney.Add(MockJourneydata(new DateTime(2022, 2, 3, 19, 15, 00), 1, 1)); //  3 Feb 7.15 P.M peak time
            lstjourney.Add(MockJourneydata(new DateTime(2022, 2, 4, 19, 15, 00), 1, 1)); //  4 Feb 7.15 P.M peak time
            lstjourney.Add(MockJourneydata(new DateTime(2022, 2, 5, 19, 15, 00), 1, 1)); //  5 Feb 7.15 P.M peak time
            lstjourney.Add(MockJourneydata(new DateTime(2022, 2, 6, 19, 15, 00), 1, 1)); //  6 Feb 7.15 P.M peak time
            Fare f = new Fare(lstjourney);
            var far = f.CalculateTravelFare();
            Assert.AreEqual(300, far);  // Total 60 + 120 + 120 = 300
        }

        [TestMethod]
        public void Fare_for_Single_Travel_In_Same_Day()
        {
            List<Journey> lstjourney = new List<Journey>();
            lstjourney.Add(MockJourneydata(new DateTime(2022, 2, 4, 09, 15, 00), 1, 1));  // 4th Feb 9.15 A.M peak time
            Fare f = new Fare(lstjourney);
            var far = f.CalculateTravelFare();
            Assert.AreEqual(30, far);
        }
        /// <summary>
        /// Takes DateTime and From - To Zone to Create Mock object
        /// </summary>
        /// <param name="tdatetime"></param>
        /// <param name="fromz"></param>
        /// <param name="toz"></param>
        /// <returns></returns>
        private Journey MockJourneydata(DateTime tdatetime, int fromz, int toz)
        {
            TravelDateInfo travelday = new TravelDateInfo(tdatetime);
            TravelzoneInfo travelzone = new TravelzoneInfo(fromz, toz);
            Journey journey = new Journey();
            journey.journeydateinformation = travelday;
            journey.journeyzoneinformation = travelzone;
            return journey;
        }
    }
}
