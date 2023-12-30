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
using Activity = Timetable.Models.Activity;

namespace Timetable.Repositories
{
    class ActivitiesRepository : IRepository<Activity>
    {
        public ActivitiesRepository(Expression<Func<ActivityDb, bool>>? filter = null)
        {
            if (filter != null)
                this.filter = filter;
        }

        private Expression<Func<ActivityDb, bool>> filter = _ => true;

        public async Task<Activity> GetItem(int id)
        {
            using var db = new TimetableDbContext();

            var activity = await db.Activities
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return new Activity()
            {
                Id = id,
                Title = activity.Title,
                Description = activity.Description,
            };
        }

        public async Task<List<Activity>> GetItems()
        {
            using var db = new TimetableDbContext();

            var query = db.Activities
                .AsNoTracking()
                .Where(filter);

            return await query
                .Select(dbActivity =>
                new Activity()
                {
                    Id = dbActivity.Id,
                    Title = dbActivity.Title,
                    Description = dbActivity.Description,
                }).ToListAsync();
        }

        public void Save(Activity item)
        {
            throw new NotImplementedException();
        }
    }
}
