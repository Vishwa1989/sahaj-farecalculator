using System;
using System.Collections.Generic;
using System.Text; 

namespace FareCalculator.Class
{
    public class Zone
    {
        public static Dictionary<int,int> getZoneswithCap()
        {
            Dictionary<int,int>zones= new Dictionary<int, int>() { };
            zones.Add(1, 100);
            zones.Add(2, 80);
            return zones;
        }
    }
}
