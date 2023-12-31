using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using Timetable.Db;
using Timetable.Db.DbModels;
using Timetable.Models;
using Timetable.Shared.Filters;

namespace Timetable.Repositories
{
    public interface IFilter
    {

    }

    internal class PersonsRepository : IRepository<Person, PersonFilter>
    {
        public async Task<Person> GetItem(int id)
        {
            using var db = new TimetableDbContext();

            var personDb = await db.Persons
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (personDb == null)
            {
                // TODO: return error message
                throw new InvalidOperationException();
            }

            return new Person()
            {
                Id = id,
                FirstName = personDb.FirstName,
                LastName = personDb.LastName,
            };
        }

        public async Task<List<Person>> GetItems(PersonFilter filterCriteria)
        {
            using var db = new TimetableDbContext();

            var filterFirstName = filterCriteria.FirstName;
            var filterLastName = filterCriteria.LastName;

            Expression<Func<PersonDb, bool>> filter = dbPerson =>
                    (string.IsNullOrEmpty(filterFirstName) || dbPerson.FirstName.ToLower().Contains(filterFirstName.ToLower())) &&
                    (string.IsNullOrEmpty(filterLastName) || dbPerson.LastName.ToLower().Contains(filterFirstName.ToLower()));

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
