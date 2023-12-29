using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable.Db;
using Timetable.Db.DbModels;
using Timetable.Models;

namespace Timetable.Repositories
{
    internal class PersonsRepository
    {
        public Person GetPerson(int personId)
        {
            using var db = new TimetableDbContext();

            var person = db.Persons
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == personId);

            return new Person()
            {
                Id = personId,
                FirstName = person.FirstName,
                LastName = person.LastName,
            };
        }

        public List<Person> GetPersons()
        {
            //Task.Delay(1000).Wait();

            using var db = new TimetableDbContext();

            return db.Persons
                .AsNoTracking()
                .Select(dbPers =>
            new Person()
            {
                Id = dbPers.Id,
                FirstName = dbPers.FirstName,
                LastName = dbPers.LastName,
            }).ToList();
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

        public void Delete(List<Person> persons)
        {

        }

    }
}
