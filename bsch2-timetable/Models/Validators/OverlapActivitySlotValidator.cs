using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable.Db.DbModels;
using Timetable.Shared;

namespace Timetable.Models.Validators
{
    public static class OverlapActivitySlotValidator
    {
        public static bool AreSlotsValid(ActivitySlot slotA, ActivitySlot slotB)
        {
            if (slotA.ActivityId != slotB.ActivityId)
                return true;

            if (slotA.DayOfWeek != slotB.DayOfWeek)
                return true;

            /* if they do not share same periodicity -> valid */
            if ((slotA.Regularity != WeekPeriod.ALL && slotB.Regularity != WeekPeriod.ALL &&
                slotA.Regularity != slotB.Regularity))
                return true;

            if (slotA.StartTime > slotB.EndTime || slotA.EndTime < slotB.StartTime)
                return true;

            return false;
        }

        public static bool AreSlotsValid(ActivitySlotDb slotA, ActivitySlotDb slotB)
        {
            if (slotA.ActivityId != slotB.ActivityId) 
                return true;

            if (slotA.DayOfWeek != slotB.DayOfWeek)
                return true;

            /* if they do not share same periodicity -> valid */
            if ((slotA.Period != WeekPeriod.ALL && slotB.Period != WeekPeriod.ALL &&
                slotA.Period != slotB.Period))
                return true;

            if (slotA.From > slotB.To || slotA.To < slotB.From)
                return true;

            return false;
        }
    }
}
