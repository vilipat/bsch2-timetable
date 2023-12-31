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

        }
    }
}
