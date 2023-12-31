using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Timetable.Db;
using Timetable.Db.DbModels;
using Timetable.Models;
using Timetable.Shared.Filters;

namespace Timetable.Repositories
{
    public class CalendarRepository
    {
        public async Task<List<CalendarItem>> GetItemsForPerson(int personId)
        {
            using var db = new TimetableDbContext();

            var person = await db.Persons.Include(p => p.ActivitySlots).ThenInclude(s => s.Activity)
                .FirstAsync(p => p.Id == personId);

            var results = person.ActivitySlots
                .Select(ToCalendarItem);

            return results.ToList();
        }
        public async Task<List<CalendarItem>> GetItemsForActivityType(int activityTypeId)
        {
            using var db = new TimetableDbContext();

            var activityType = await db.Activities
                .Include(a => a.ActivitySlots)
                .FirstAsync(a => a.Id == activityTypeId);

            var results = activityType.ActivitySlots
                .Select(ToCalendarItem);

            return results.ToList();
        }

        private CalendarItem ToCalendarItem(ActivitySlotDb slot)
        {
            return new CalendarItem()
            {
                Id = slot.Id,
                Title = slot.Activity!.Title,
                StartHour = slot.From.Hour,
                DurationHours = slot.To.Hour - slot.From.Hour,
                DayOfWeek = (int)slot.DayOfWeek
            };
        }
    }
}
