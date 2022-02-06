using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using FareCalculator.Interface;
namespace FareCalculator.Class
{
    public class TravelDateInfo
    {
        public TravelDateInfo(DateTime traveldatetime)
        {
            traveldate = traveldatetime;
            isweekday = CheckIsWeekday(traveldate);
            ispeakhour = CheckIsPeakhour(traveldate);
            weeknumber = GetWeekno(traveldate);
        }
        public DateTime traveldate
        {
            get;
            private set;
        }
        public int weeknumber { get; private set; }
        public bool isweekday { get; private set; }
        public bool ispeakhour { get; private set; }

        private bool CheckIsPeakhour(DateTime timeoftravel)
        {
            TimeSpan t = timeoftravel.TimeOfDay;
            if (isweekday)
            {
                if ((t.Hours >= 7 && (t.Hours <= 10 && t.Minutes<=30)) || t.Hours >= 17 && t.Hours <= 20)
                {
                    return true;
                }
            }
            else
            {
                if ((t.Hours >= 9 && t.Hours <= 11) || (t.Hours >= 18 && t.Hours <= 22))
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckIsWeekday(DateTime traveldate)
        {
            if (traveldate.DayOfWeek == DayOfWeek.Saturday || traveldate.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }
            return true;
        }

        private int GetWeekno(DateTime traveldate)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weekNum;
        }
    }
}
