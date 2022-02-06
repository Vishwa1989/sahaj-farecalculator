using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FareCalculator.Interface;

namespace FareCalculator.Class
{
    public class Fare : IFare
    {
        private List<Journey> _journeys;

        Dictionary<DateTime, int> datewithfarekeyvaluepair = new Dictionary<DateTime, int>();
        public Fare(List<Journey> journeys)
        {
            _journeys = journeys;
        }
        
        //Calculates the Total fare for the given List of Journeys
        public int CalculateTravelFare()
        {
            int totalcharge = 0;
            int farecapforthisday, maxcapforday = 0;
            bool isweektravel = IsaWeekTravel();

            foreach (var journey in _journeys)
            {
                farecapforthisday = GetMaximumCapforJourney(journey.journeydateinformation.weeknumber);

                DateTime currentdate = journey.journeydateinformation.traveldate.Date;

                if (isweektravel)
                {
                    maxcapforday = farecapforthisday * 5;
                }
                else
                {
                    maxcapforday = farecapforthisday;
                }

                if (totalcharge < maxcapforday)
                {                     
                    if (journey.journeydateinformation.ispeakhour)
                    { 
                        totalcharge += AddChargetotheDateandReturnitsFare(currentdate, journey.journeyzoneinformation.peakhourcharges, farecapforthisday); 
                    }
                    else
                    { 
                        totalcharge += AddChargetotheDateandReturnitsFare(currentdate, journey.journeyzoneinformation.offpeakhourcharges, farecapforthisday); 
                    }
                }
            }
            return totalcharge;
        }

        //This Function adds the date to Dictionary and 
        //Sums all the Charge for this journey by date
        //if reaches the maxcap then it return 0 or the differnce amount
        private int AddChargetotheDateandReturnitsFare(DateTime currentdate, int chargeforthisjourney, int maxcapforthisday)
        {
            int thistripcharge = 0;
            if (!datewithfarekeyvaluepair.ContainsKey(currentdate))
            {
                thistripcharge = chargeforthisjourney;
                datewithfarekeyvaluepair.Add(currentdate, chargeforthisjourney);
            }
            else
            {
                int tempdatefare = 0;
                datewithfarekeyvaluepair.TryGetValue(currentdate, out tempdatefare);
                //E.g 120 should be greater than 100+30
                // if exceeds add the breached limit only to dictionary
                if (maxcapforthisday >= (chargeforthisjourney + tempdatefare))
                {
                    thistripcharge = chargeforthisjourney;
                    datewithfarekeyvaluepair[currentdate] = tempdatefare + chargeforthisjourney;
                }
                else
                {
                    //if below true then the trip is free
                    if (tempdatefare == maxcapforthisday)
                    {
                        thistripcharge = 0;
                    }
                    else
                    {
                        //Remaining difference to reach max cap for a day
                        int difference = Math.Abs(maxcapforthisday -  tempdatefare);
                        thistripcharge = difference;
                    }

                    //We already know that it breached the limit so directly adding the Maximum cap for that day
                    datewithfarekeyvaluepair[currentdate] = maxcapforthisday;
                }

            }
            return thistripcharge;
        }         
         
        //Gets the Daily cap amount based on Week number
        //Ideally it is the journey date daily cap
        private int GetMaximumCapforJourney(int week)
        {

            var results = _journeys.Where(f => f.journeydateinformation.weeknumber == week).Max(f => f.journeyzoneinformation.dailycap);
            return results;
        }
        //this function is to determine whether the Journeys has a differnt travel dates and says it is for a week 
        private bool IsaWeekTravel()
        {
            return (_journeys.GroupBy(f => f.journeydateinformation.weeknumber).Count() > 1) ||
                   (_journeys.GroupBy(f => f.journeydateinformation.traveldate.Date).Count() > 1);
        }
    }
}
