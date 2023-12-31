using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Timetable.Db;
using Timetable.Db.DbModels;
using Timetable.Models;
using Timetable.Shared.Filters;
using Activity = Timetable.Models.Activity;

namespace Timetable.Repositories
{
    class ActivitiesRepository : IRepository<Activity, ActivityFilter>
    {
        public void Delete(int id)
        {
            using var db = new TimetableDbContext();
            var toRemove = db.Activities.FirstOrDefault(p => p.Id == id);

            if (toRemove == null)
                return;

            db.Activities.Remove(toRemove);
        }

        public async Task<Activity> GetItem(int id)
        {
            using var db = new TimetableDbContext();

            var activityDb = await db.Activities
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (activityDb == null)
            {
                // TODO: return error message
                throw new InvalidOperationException();
            }

            return new Activity()
            {
                Id = id,
                Title = activityDb.Title,
                Description = activityDb.Description,
            };
        }

        public async Task<List<Activity>> GetItems(ActivityFilter? filterCriteria = null)
        {
            using var db = new TimetableDbContext();

            var query = db.Activities
                .AsNoTracking();

            if (filterCriteria != null)
            {
                var filterTitle = filterCriteria.Title;
                query = query.Where(dbAct => 
                    dbAct.Title.ToLower().Contains(filterTitle.ToLower()));
            }

            return await query
                .Select(dbActivity =>
                new Activity()
                {
                    Id = dbActivity.Id,
                    Title = dbActivity.Title,
                }).ToListAsync();
        }

        public void Save(Activity item)
        {
            using var db = new TimetableDbContext();

            ActivityDb activityDb = new();

            if (item.Id > 0)
                activityDb = db.Activities.First(x => x.Id == item.Id);
            else
                db.Add(activityDb);

            activityDb.Title = item.Title;
            activityDb.Description = item.Description;

            db.SaveChanges();
        }
    }
}
