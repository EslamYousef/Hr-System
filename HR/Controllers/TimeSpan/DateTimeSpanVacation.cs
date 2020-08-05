using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayers
{
  public class DateTimeSpans
    {
        public int Years { get; }
        public int Months { get; }
        public int Days { get; }
        public int Hours { get; }
        public int Minutes { get; }
        public int Seconds { get; }
        public int Milliseconds { get; }

        public DateTimeSpans(int years, int months, int days, int hours, int minutes, int seconds, int milliseconds)
        {
            Years = years;
            Months = months;
            Days = days;
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
            Milliseconds = milliseconds;
        }

        enum Phase { Years, Months, Days, Done }

        public static DateTimeSpans CompareDates(DateTime date1, DateTime date2)
        {
            if (date2 < date1)
            {
                var sub = date1;
                date1 = date2;
                date2 = sub;
            }

            DateTime current = date1;
            int years = 0;
            int months = 0;
            int days = 0;

            Phase phase = Phase.Days;
            DateTimeSpans span = new DateTimeSpans(0,0,0,0,0,0,0);
            int officialDay = current.Day;

            while (phase != Phase.Done)
            {
                switch (phase)
                {
                    case Phase.Days:
                        if (current.AddDays(days + 1) > date2)
                        {
                            //phase = Phase.Months;
                            current = current.AddDays(days);
                            var timespan = date2 - current;
                            span = new DateTimeSpans(years, months, days, timespan.Hours, timespan.Minutes, timespan.Seconds, timespan.Milliseconds);
                            phase = Phase.Done;
                        }
                        else
                        {
                            days++;
                        }
                        break;
               
                    //case Phase.Days:
                    //    if (current.AddDays(days + 1) > date2)
                    //    {
                    //        current = current.AddDays(days);
                    //        var timespan = date2 - current;
                    //        span = new DateTimeSpans(years, months, days, timespan.Hours, timespan.Minutes, timespan.Seconds, timespan.Milliseconds);
                    //        phase = Phase.Done;
                    //    }
                    //    else
                    //    {
                    //        days++;
                    //    }
                        //break;
                }
            }

            return span;
        }
    }
}
