using System;
using System.Collections.Generic;
using System.Text;
using FareCalculator.Interface;

namespace FareCalculator.Class
{
    public class TravelzoneInfo  
    { 
        public int dailycap { get; private set; }
        public int travelfromzone { get; private set; }
        public int traveltozone { get; private set; }
        public int peakhourcharges { get; private set; }
        public int offpeakhourcharges { get; private set; }
        public TravelzoneInfo(int fromzone, int tozone)
        {
            travelfromzone = fromzone;
            traveltozone = tozone;
            if (fromzone == tozone)
            {
                switch (fromzone)
                {
                    case 1:
                        dailycap = 100;
                        peakhourcharges = 30;
                        offpeakhourcharges = 25;
                        break;
                    case 2:
                        dailycap = 80;
                        peakhourcharges = 25;
                        offpeakhourcharges = 20;
                        break;
                }
            }
            else
            {
                dailycap = 120;
                peakhourcharges = 35;
                offpeakhourcharges = 30;
            }
        }
        
    }
}
