using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable.Db;
using Timetable.Db.DbModels;
using Timetable.Models;
using Timetable.Models.Validators;
using Timetable.Shared.Filters;

namespace Timetable.Repositories
{
    class ActivitySlotsRepository : IRepository<ActivitySlot, ActivitySlotFilter>
    {
        public async Task<ActivitySlot> GetItem(int id)
        {
            using var db = new TimetableDbContext();

            var slotDb = await db.ActivitySlots
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (slotDb == null)
            {
                // return error message
                throw new InvalidOperationException();
            }

            return new ActivitySlot()
            {
                Id = id,
                ActivityId = slotDb.ActivityId,
                DayOfWeek = slotDb.DayOfWeek,
                StartTime = slotDb.From.ToTimeSpan(),
                EndTime = slotDb.To.ToTimeSpan(),
                Regularity = slotDb.Period,
            };
        }

        public async Task<List<ActivitySlot>> GetItems(ActivitySlotFilter? filterCriteria = null)
        {
            using var db = new TimetableDbContext();

            var query = db.ActivitySlots
                .AsNoTracking();

            if (filterCriteria != null)
            {
                var filterActivityId = filterCriteria.ActivityId;
                query = query.Where(dbSlot => filterActivityId == null || dbSlot.ActivityId == filterActivityId);
            }

            return await query
                .Select(dbSlot =>
                new ActivitySlot()
                {
                    Id = dbSlot.Id,
                    ActivityTitle = dbSlot.Activity!.Title,
                    DayOfWeek = dbSlot.DayOfWeek,
                    StartTime = dbSlot.From.ToTimeSpan(),
                    EndTime = dbSlot.To.ToTimeSpan(),
                    Regularity = dbSlot.Period,

                }).ToListAsync();
        }

        public void Save(ActivitySlot item)
        {
            using var db = new TimetableDbContext();

            ActivitySlotDb slotDb = new();

            if (slotDb.Id > 0)
                slotDb = db.ActivitySlots.First(p => p.Id == slotDb.Id);
            else
                db.Add(slotDb);

            slotDb.Id = slotDb.Id;
            slotDb.ActivityId = item.Activity!.Id;
            slotDb.From = TimeOnly.FromTimeSpan(item.StartTime);
            slotDb.To = TimeOnly.FromTimeSpan(item.EndTime);
            slotDb.Period = item.Regularity;

            // validate
            var slotsDb = db.ActivitySlots.ToList();
            var edited = db.ActivitySlots.FirstOrDefault(x => x.Id == item.Id);

            if (edited != null)
                slotsDb.Remove(edited);


            foreach (var slot in slotsDb)
            {
                if (!OverlapActivitySlotValidator.AreSlotsValid(slotDb, slot))
                    throw new ValidationException("Timeslots overlap");
            }

            db.SaveChanges();
        }
    }
}
