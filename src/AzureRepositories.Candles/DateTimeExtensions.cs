﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lykke.Domain.Prices;

namespace AzureRepositories.Candles
{
    internal static class DateTimeExtensions
    {
        /// <summary>
        /// Returns T (Tick) value for the specified datetime.
        /// </summary>
        public static int GetIntervalTick(this DateTime dateTime, TimeInterval interval)
        {
            switch (interval)
            {
                case TimeInterval.Month: return dateTime.Month;
                case TimeInterval.Week: return (int)(dateTime - DateTimeUtils.GetFirstWeekOfYear(dateTime)).TotalDays / 7;
                case TimeInterval.Day: return dateTime.Day;
                case TimeInterval.Hour12: return dateTime.Hour / 12;
                case TimeInterval.Hour6: return dateTime.Hour / 6;
                case TimeInterval.Hour4: return dateTime.Hour / 4;
                case TimeInterval.Hour: return dateTime.Hour;
                case TimeInterval.Min30: return dateTime.Minute / 30;
                case TimeInterval.Min15: return dateTime.Minute / 15;
                case TimeInterval.Min5: return dateTime.Minute / 5;
                case TimeInterval.Minute: return dateTime.Minute;
                case TimeInterval.Sec: return dateTime.Second;
                default:
                    throw new ArgumentOutOfRangeException(nameof(interval), interval, "Unexpected TimeInterval value.");
            }
        }

        public static int GetIntervalCell(this DateTime dateTime, TimeInterval interval)
        {
            switch (interval)
            {
                case TimeInterval.Month: return 0;
                case TimeInterval.Week: return 0;
                case TimeInterval.Day: return dateTime.Month - 1;
                case TimeInterval.Hour12:
                case TimeInterval.Hour6:
                case TimeInterval.Hour4:
                case TimeInterval.Hour: return dateTime.Day - 1;
                case TimeInterval.Min30: return dateTime.Hour;
                case TimeInterval.Min15: return dateTime.Hour;
                case TimeInterval.Min5: return dateTime.Hour;
                case TimeInterval.Minute: return dateTime.Hour;
                case TimeInterval.Sec: return dateTime.Minute;
                default:
                    throw new ArgumentOutOfRangeException(nameof(interval), interval, "Unexpected TimeInterval value.");
            }
        }

        public static DateTime AddIntervalTicks(this DateTime baseTime, int cell, int ticks, TimeInterval interval)
        {
            switch (interval)
            {
                case TimeInterval.Month: return baseTime.AddMonths(ticks - 1);  // Month ticks are in range [1..12]
                case TimeInterval.Week: return baseTime.AddDays(ticks * 7);
                case TimeInterval.Day: return baseTime.AddMonths(cell).AddDays(ticks - 1);      // Days ticks are in range [1..31]
                case TimeInterval.Hour12: return baseTime.AddDays(cell).AddHours(ticks * 12);
                case TimeInterval.Hour6: return baseTime.AddDays(cell).AddHours(ticks * 6);
                case TimeInterval.Hour4: return baseTime.AddDays(cell).AddHours(ticks * 4);
                case TimeInterval.Hour: return baseTime.AddDays(cell).AddHours(ticks);
                case TimeInterval.Min30: return baseTime.AddHours(cell).AddMinutes(ticks * 30);
                case TimeInterval.Min15: return baseTime.AddHours(cell).AddMinutes(ticks * 15);
                case TimeInterval.Min5: return baseTime.AddHours(cell).AddMinutes(ticks * 5);
                case TimeInterval.Minute: return baseTime.AddHours(cell).AddMinutes(ticks);
                case TimeInterval.Sec: return baseTime.AddMinutes(cell).AddSeconds(ticks);
                default:
                    throw new ArgumentOutOfRangeException(nameof(interval), interval, "Unexpected TimeInterval value.");
            }
        }
    }
}
