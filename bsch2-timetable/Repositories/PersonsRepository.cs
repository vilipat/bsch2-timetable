using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
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
using Timetable.Shared;
using Timetable.Shared.Filters;

namespace Timetable.Repositories
{
    internal class PersonsRepository : IRepository<Person, PersonFilter>
    {
        public async Task<Person> GetItem(int id)
        {
            using var db = new TimetableDbContext();

            var personDb = await db.Persons
                .Include(p => p.ActivitySlots).ThenInclude(p => p.Activity)
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
                ActivitySlots = new ObservableCollection<ActivitySlot>(
                    personDb.ActivitySlots

                        .Select(ActivitySlotsRepository.ToModel))
            };
        }

        public async Task<List<Person>> GetItems(PersonFilter? filterCriteria = null)
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


            // Extract the IDs into a list
            var slotIds = person.ActivitySlots.Select(slot => slot.Id).ToList();

            // Use the IDs directly in the query
            var newSlots = db.ActivitySlots
                             .Where(slotDb => slotIds.Contains(slotDb.Id))
                             .ToList();

            foreach (var slot in newSlots)
                personDb.ActivitySlots.Add(slot);



            for (int i = 0; i < personDb.ActivitySlots.Count; i++)
            {
                for (int j = i + 1; j < personDb.ActivitySlots.Count; j++) // Start from i+1 to not compare the slot with itself
                {
                    if (!IsPersonSlotsValid(personDb.ActivitySlots[i], personDb.ActivitySlots[j]))
                    {
                        throw new ValidationException("Lang.Error.PersonSlots");
                    }
                }
            }

            db.SaveChanges();
        }

        private bool IsPersonSlotsValid(ActivitySlotDb slotA, ActivitySlotDb slotB)
        {
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

        public void Delete(int id)
        {
            using var db = new TimetableDbContext();
            var toRemove = db.Persons.FirstOrDefault(p => p.Id == id);

            if (toRemove == null)
                return;

            db.Persons.Remove(toRemove);
            db.SaveChanges();
        }
    }
}
