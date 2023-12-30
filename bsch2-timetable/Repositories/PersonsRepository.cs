using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using Timetable.Db;
using Timetable.Db.DbModels;
using Timetable.Models;

namespace Timetable.Repositories
{
    internal class PersonsRepository : IRepository<Person>
    {
        public PersonsRepository(Expression<Func<PersonDb, bool>>? filter = null)
        {
            if (filter != null)
                this.filter = filter;
        }

        private Expression<Func<PersonDb, bool>> filter = _ => true;

        public async Task<Person> GetItem(int id)
        {
            using var db = new TimetableDbContext();

            var person = await db.Persons
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return new Person()
            {
                Id = id,
                FirstName = person.FirstName,
                LastName = person.LastName,
            };
        }

        public async Task<List<Person>> GetItems()
        {
            using var db = new TimetableDbContext();

            var query = db.Persons
                .AsNoTracking()
                .Where(filter);

            return await query
                .Select(dbPers =>
                new Person()
                {
                    Id = dbPers.Id,
                    FirstName = dbPers.FirstName,
                    LastName = dbPers.LastName,
                }).ToListAsync();
        }


        public void Save(Person person)
        {
            using var db = new TimetableDbContext();

            PersonDb personDb = new();

            if (person.Id > 0)
                personDb = db.Persons.First(p => p.Id == person.Id);
            else
                db.Add(personDb);

            personDb.FirstName = person.FirstName;
            personDb.LastName = person.LastName;

            db.SaveChanges();
        }

        public void Delete(IList<int> personsIds)
        {
            using var db = new TimetableDbContext();

            db.Persons.RemoveRange(
                db.Persons.Where(dbPerson => personsIds.Contains(dbPerson.Id)));
        }
    }
}
