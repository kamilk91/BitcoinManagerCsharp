using BitcoinBasedNode.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinBasedNode.Helpers
{
    public static class TimeCalculator
    {
        enum periodOfTime { seconds, minutes, hours, days, weeks, months, years }

        private static int second = 1;
        private static int minute = 60;
        private static int hour = 60 * 60;
        private static int day = 60 * 60 * 24;
        private static int week = 60 * 60 * 24 * 7;
        private static int month = 60 * 60 * 24 * 30;
        private static int year = 60 * 60 * 24 * 365;


        public static int CalculateTime(Time time)
        {
            return CalculateEachSecond(time.seconds, periodOfTime.seconds) +
                CalculateEachSecond(time.minutes, periodOfTime.minutes) +
                CalculateEachSecond(time.hours, periodOfTime.hours) +
                CalculateEachSecond(time.days, periodOfTime.days) +
                CalculateEachSecond(time.weeks, periodOfTime.weeks) +
                CalculateEachSecond(time.months, periodOfTime.months) +
                CalculateEachSecond(time.years, periodOfTime.years);


        }

        private static int CalculateEachSecond(int time, periodOfTime of)
        {
            int unlockFor = 0;
            switch (of)
            {
                case periodOfTime.seconds:
                    unlockFor = time * second;
                    break;
                case periodOfTime.minutes:
                    unlockFor = time * minute;
                    break;
                case periodOfTime.hours:
                    unlockFor = time * hour;
                    break;
                case periodOfTime.days:
                    unlockFor = time * day;
                    break;
                case periodOfTime.weeks:
                    unlockFor = time * week;
                    break;
                case periodOfTime.months:
                    unlockFor = time * month;
                    break;
                case periodOfTime.years:
                    unlockFor = time * year;
                    break;
                default:
                    unlockFor = time * second;
                    break;
            }

            return unlockFor;
        }
    }
}
