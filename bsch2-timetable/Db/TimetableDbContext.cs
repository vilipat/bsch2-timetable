using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable.Db.DbModels;

namespace Timetable.Db
{
    internal class TimetableDbContext : DbContext
    {
        public DbSet<ActivityDb> Activities { get; set; }
        public DbSet<ActivitySlotDb> ActivitySlots { get; set; }
        public DbSet<PersonDb> Persons{ get; set; }

        public string DbPath { get; }

        public TimetableDbContext()
        {
            DbPath = "data.db";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }

    }
}
